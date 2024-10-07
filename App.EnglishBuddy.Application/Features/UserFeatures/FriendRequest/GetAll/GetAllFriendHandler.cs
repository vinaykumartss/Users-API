using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Common.Mail;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sentry;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FriendRequest.GetAllFriend;

public sealed class GetAllFriendHandler : IRequestHandler<GetAllFriendRequest, GetAllFriendResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFriendRepository _friendRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllFriendHandler> _logger;
    private readonly IMediator _mediator;
    public GetAllFriendHandler(IUnitOfWork unitOfWork, IFriendRepository friendRepository,
        IMapper mapper, ILogger<GetAllFriendHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _friendRepository = friendRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<GetAllFriendResponse> Handle(GetAllFriendRequest request, CancellationToken cancellationToken)
    {
        GetAllFriendResponse response = new GetAllFriendResponse();
        try
        {
            Domain.Entities.Friend contactUs = new()
            {
               UserId = request.UserId,
               ToUserId = request.ToUserId,
               Createdby= request.UserId,
               CreatedDate = DateTime.UtcNow,
               IsActive=false
            };
            _friendRepository.Create(contactUs);
            await _unitOfWork.Save(cancellationToken);
            response.IsSuccess = true;
        }
        catch (BadRequestException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong, please try again");

        }
        return response;
    }
}