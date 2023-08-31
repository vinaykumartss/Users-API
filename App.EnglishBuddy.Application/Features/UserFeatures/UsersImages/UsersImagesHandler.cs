using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic.FileIO;
using RestSharp;
using Sentry;
using System.Security.Cryptography.X509Certificates;



namespace App.EnglishBuddy.Application.Features.UserFeatures.UsersImages;

public sealed class UsersImagesHandler : IRequestHandler<UsersImagesRequest, UsersImagesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersImagesRepository _iUsersImagesRepository;
    private readonly IMapper _mapper;

    public UsersImagesHandler(IUnitOfWork unitOfWork,
       IUsersImagesRepository iUsersImagesRepository,
        IMapper mapper
        )
    {
        _iUsersImagesRepository = iUsersImagesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UsersImagesResponse> Handle(UsersImagesRequest request, CancellationToken cancellationToken)
    {
        var fileName = Path.GetFileName($"{Guid.NewGuid()}");

        UsersImagesResponse response = new UsersImagesResponse();
        try
        {
            if (request.File == null)
            {
                response.IsSuccess = false;
                return response;
            }

            if (request.File.Length > 0)
            {
                byte[] imgByteArray = Convert.FromBase64String(request.File);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\user_images", fileName);
                var imageName = $"{filePath}.{request.FileType}";
                File.WriteAllBytes(imageName, imgByteArray);
               
                response.ImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\user_images", fileName + "." + "jpeg");

                Domain.Entities.UsersImages userImage = await _iUsersImagesRepository.FindByUserId(x => x.UserId == request.UserId, cancellationToken);
                if (userImage == null)
                {
                    Domain.Entities.UsersImages usersImages = new Domain.Entities.UsersImages()
                    {
                        UserId = request.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ImagePath = $"{fileName}.{request.FileType}"
                    };
                    _iUsersImagesRepository.Create(usersImages);
                    await _unitOfWork.Save(cancellationToken);
                }
                else
                {
                    userImage.ImagePath = $"{fileName}.{request.FileType}";
                    userImage.CreatedDate = DateTime.UtcNow;
                    _iUsersImagesRepository.Update(userImage);
                    await _unitOfWork.Save(cancellationToken);
                }

                response.IsSuccess = true;
                response.UserId = request.UserId;
                response.ImagePath = $"/app-images/{fileName}.{request.FileType}";
                return response;
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        return response;
    }
}