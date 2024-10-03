using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;



namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserImage;

public sealed class GetUserImageHandler : IRequestHandler<GetUserImageRequest, GetUserImageResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersImagesRepository _iUsersImagesRepository;
    private readonly IMapper _mapper;

    public GetUserImageHandler(IUnitOfWork unitOfWork,
       IUsersImagesRepository iUsersImagesRepository,
        IMapper mapper
        )
    {
        _iUsersImagesRepository = iUsersImagesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetUserImageResponse> Handle(GetUserImageRequest request, CancellationToken cancellationToken)
    {
        GetUserImageResponse response = new GetUserImageResponse();
        try
        {
            Domain.Entities.UsersImages userImage = await _iUsersImagesRepository.FindByUserId(x => x.UserId == request.UserId, cancellationToken);
            response.ImagePath = $"https://insightxdev.com:801/{userImage.ImagePath}";
            response.UserId = request.UserId;
        }
        catch (Exception ex)
        {
            throw;
        }

        return response;
    }
}