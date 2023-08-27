using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic.FileIO;
using RestSharp;
using System.Security.Cryptography.X509Certificates;



namespace App.EnglishBuddy.Application.Features.UserFeatures.UsersImages;

public sealed class UsersImagesHandler : IRequestHandler<UsersImagesRequest, UsersImagesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRandomUsersRepository _iRandomCallsRepository;
    private readonly IMeetingIdsRepository _iIMeetingIdsRepository;

    private readonly IUserRepository _iUserRepository;
    private readonly IMapper _mapper;

    public UsersImagesHandler(IUnitOfWork unitOfWork,
        IRandomUsersRepository iRandomCallsRepository,
        IUserRepository iUserRepository,
        IMeetingIdsRepository iIMeetingIdsRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _iRandomCallsRepository = iRandomCallsRepository;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
        _iIMeetingIdsRepository = iIMeetingIdsRepository;

    }

    public async Task<UsersImagesResponse> Handle(UsersImagesRequest request, CancellationToken cancellationToken)
    {
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


                var fileName = Path.GetFileName($"{request.UserId}");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\user_images", fileName);
                //using (var fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    await request.File.CopyToAsync(fileStream);
                //}
                var fileExtension = filePath + "." + "jpeg";
                File.WriteAllBytes(fileExtension, imgByteArray);
                response.IsSuccess = true;
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