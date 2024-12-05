using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<UsersImagesHandler> _logger;
    public UsersImagesHandler(IUnitOfWork unitOfWork,
       IUsersImagesRepository iUsersImagesRepository,
        IMapper mapper,
         ILogger<UsersImagesHandler> logger
        )
    {
        _iUsersImagesRepository = iUsersImagesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UsersImagesResponse> Handle(UsersImagesRequest request, CancellationToken cancellationToken)
    {
        var fileName = Path.GetFileName($"{Guid.NewGuid()}");
        _logger.LogDebug($"Statring method {nameof(Handle)}");
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
                Domain.Entities.UsersImages userImage = await _iUsersImagesRepository.FindByUserId(x => x.UserId == request.UserId, cancellationToken);

                var myfilename = string.Format(@"{0}", Guid.NewGuid());
                string path = SaveImage(request.File, myfilename);
                if (userImage == null)
                {
                    Domain.Entities.UsersImages usersImages = new Domain.Entities.UsersImages()
                    {
                        UserId = request.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ImagePath = path
                    };
                    _iUsersImagesRepository.Create(usersImages);
                    await _unitOfWork.Save(cancellationToken);
                }
                else
                {
                    userImage.ImagePath = path;
                    userImage.CreatedDate = DateTime.UtcNow;
                    _iUsersImagesRepository.Update(userImage);
                    await _unitOfWork.Save(cancellationToken);
                }

                response.IsSuccess = true;
                response.UserId = request.UserId;
                response.ImagePath = $"https://insightxdev.com:801/{myfilename}.jpeg";
                return response;
                _logger.LogDebug($"Ending method {nameof(Handle)}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

        return response;
    }

    public string SaveImage(string base64, string myfilename)
    {
        string strm = base64;

        //this is a simple white background image
      

        string fileName =  myfilename + ".jpeg";
        
        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"{fileName}");

        //Generate unique filename
        filepath = filepath.Replace("\\", "/");

         //string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
         var bytess = Convert.FromBase64String(strm);
        using (var imageFile = new FileStream(filepath, FileMode.Create))
        {
            imageFile.Write(bytess, 0, bytess.Length);
            imageFile.Flush();
        }
        return fileName;    
    }
}