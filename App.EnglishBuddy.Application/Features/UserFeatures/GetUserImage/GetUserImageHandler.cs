using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserImage;

public sealed class GetUserImageHandler : IRequestHandler<GetUserImageRequest, GetUserImageResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersImagesRepository _iUsersImagesRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserImageHandler> _logger;
    public GetUserImageHandler(IUnitOfWork unitOfWork,
       IUsersImagesRepository iUsersImagesRepository,
        IMapper mapper,
         ILogger<GetUserImageHandler> logger
        )
    {
        _iUsersImagesRepository = iUsersImagesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetUserImageResponse> Handle(GetUserImageRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        GetUserImageResponse response = new GetUserImageResponse();
        try
        {
            Domain.Entities.UsersImages userImage = await _iUsersImagesRepository.FindByUserId(x => x.UserId == request.UserId, cancellationToken);
            response.ImagePath = $"https://insightxdev.com:801/{userImage.ImagePath}";
            response.UserId = request.UserId;
            _logger.LogDebug($"Ending method {nameof(Handle)}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

        return response;
    }
}