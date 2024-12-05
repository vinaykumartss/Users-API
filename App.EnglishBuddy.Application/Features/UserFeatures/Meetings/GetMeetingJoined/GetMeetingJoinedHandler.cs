using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingJoined;

public sealed class GetMeetingJoinedHandler : IRequestHandler<GetMeetingJoinedRequest, GetMeetingJoinedResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly IMeetingsUsersRepository _iMeetingsUserRepository;
    private readonly ILogger<GetMeetingJoinedHandler> _logger;
    public GetMeetingJoinedHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
        IMeetingsUsersRepository iMeetingsUserRepository,
         ILogger<GetMeetingJoinedHandler> logger
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _iMeetingsUserRepository = iMeetingsUserRepository;
        _logger = logger;
    }

    public async Task<GetMeetingJoinedResponse> Handle(GetMeetingJoinedRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        GetMeetingJoinedResponse response = new GetMeetingJoinedResponse();
        try
        {
            int totalMeeting = 0;
            var meetingUser =  _iMeetingsUserRepository.FindByListSync(x => x.UserId == request.UserId && x.IsmeetingAdmin == false);
            if (meetingUser != null)
            {
                totalMeeting = meetingUser.Count;
            }
            response.Total = totalMeeting;
            response.IsSuccess = true;
            _logger.LogDebug($"Ending method {nameof(Handle)}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}