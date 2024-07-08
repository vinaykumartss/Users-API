using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUser;

public sealed class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IUsersImagesRepository _iUsersImagesRepository;
    
    public GetUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<GetUserHandler> logger, IMediator mediator, IUsersImagesRepository iUsersImagesRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
        _iUsersImagesRepository= iUsersImagesRepository;
    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        GetUserResponse response = new GetUserResponse();
        try
        {
            Users user = await _userRepository.GetById(request.Id, cancellationToken);
            _logger.LogInformation("user" + JsonConvert.SerializeObject(user));
            response = _mapper.Map<GetUserResponse>(user);
            Domain.Entities.UsersImages userImage = await _iUsersImagesRepository.FindByUserId(x => x.UserId == user.Id, cancellationToken);
            _logger.LogInformation("userImage" + JsonConvert.SerializeObject(userImage));
            if (userImage != null)
            {
                response.ImagePath = $"{userImage.ImagePath}";
            }
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}