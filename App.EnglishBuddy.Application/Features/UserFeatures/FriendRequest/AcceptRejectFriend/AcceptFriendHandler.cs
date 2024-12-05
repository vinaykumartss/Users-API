using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FriendRequest.AcceptFriend;

public sealed class AcceptFriendHandler : IRequestHandler<AcceptFriendRequest, AcceptFriendResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFriendRepository _friendRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AcceptFriendHandler> _logger;
    private readonly IMediator _mediator;
    public AcceptFriendHandler(IUnitOfWork unitOfWork, IFriendRepository friendRepository,
        IMapper mapper, ILogger<AcceptFriendHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _friendRepository = friendRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<AcceptFriendResponse> Handle(AcceptFriendRequest request, CancellationToken cancellationToken)
    {
         _logger.LogDebug($"Statring method {nameof(Handle)}");
        AcceptFriendResponse response = new AcceptFriendResponse();
        try
        {
            Domain.Entities.Friend users = await _friendRepository.FindByUserId(x => x.UserId == request.UserId && x.ToUserId == request.ToUserId && x.IsActive == request.IsStatus, cancellationToken);
            users.IsActive = !request.IsStatus;
            _friendRepository.Update(users);
            await _unitOfWork.Save(cancellationToken);
            response.IsSuccess = true;
              _logger.LogDebug($"Ending method {nameof(Handle)}");
        }
        catch (BadRequestException ex)
        {
             _logger.LogError(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
             _logger.LogError(ex.Message);
            throw new Exception("Something went wrong, please try again");

        }
        return response;
    }
}