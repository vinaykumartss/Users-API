using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Features.UserFeatures.MeetingInActive;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingCreatedBy;

public sealed class MeetingInActiveHandler : IRequestHandler<MeetingInActiveRequest, MeetingInActiveResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly IMeetingsUsersRepository _iMeetingsUserRepository;
    private readonly ILogger<MeetingInActiveHandler> _logger;
    public MeetingInActiveHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
        IMeetingsUsersRepository iMeetingsUserRepository,
         ILogger<MeetingInActiveHandler> logger
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _iMeetingsUserRepository = iMeetingsUserRepository;
        _logger = logger;

    }

    public async Task<MeetingInActiveResponse> Handle(MeetingInActiveRequest request, CancellationToken cancellationToken)
    {

        _logger.LogDebug($"Statring method {nameof(Handle)}");
        MeetingInActiveResponse response = new MeetingInActiveResponse();
        try
        {
            var meetingUser = await _iMeetingsUserRepository.FindByUserId(x => x.MeetingId == request.Meeting && x.UserId == request.UserId, cancellationToken);
            var meetings = await _iMeetingsRepository.FindByUserId(x => x.MeetingId == request.Meeting, cancellationToken);
            if (meetings != null && meetings.UserId == request.UserId)
            {
                meetings.IsActive = false;
                _iMeetingsRepository.Update(meetings);
                if (meetingUser != null)
                {
                    meetingUser.IsActive = false;
                    _iMeetingsUserRepository.Update(meetingUser);
                }
            }
            else
            {
                if (meetingUser != null)
                {
                    meetingUser.IsActive = false;
                    _iMeetingsUserRepository.Update(meetingUser);
                }
            }
            await _unitOfWork.Save(cancellationToken);
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