using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;

public sealed class FcmTokenHandler : IRequestHandler<FcmTokenRequest, FcmTokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FcmTokenHandler> _logger;
    private readonly IMediator _mediator;
    public FcmTokenHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<FcmTokenHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<FcmTokenResponse> Handle(FcmTokenRequest request, CancellationToken cancellationToken)
    {
        FcmTokenResponse response = new FcmTokenResponse();
        try
        {
            Domain.Entities.Users users = await _userRepository.FindByUserId(x => x.Id == request.UserId, cancellationToken);
            if (users != null)
            {
                users.FcmToken =request.FcmToken;
               
                _userRepository.Update(users);
                await _unitOfWork.Save(cancellationToken);
                
                response.IsSuccess = true;
            }
            else
            {
                throw new BadRequestException("User does not exist, please try agin");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong, please try again");

        }
        return response;
    }
}