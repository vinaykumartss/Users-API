using App.EnglishBuddy.Application.Features.UserFeatures.MeetingInActive;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingCreatedBy;

public sealed class MeetingInActiveHandler : IRequestHandler<MeetingInActiveRequest, MeetingInActiveResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly IMeetingsUsersRepository _iMeetingsUserRepository;
    public MeetingInActiveHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
        IMeetingsUsersRepository iMeetingsUserRepository
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _iMeetingsUserRepository = iMeetingsUserRepository;
    }

    public async Task<MeetingInActiveResponse> Handle(MeetingInActiveRequest request, CancellationToken cancellationToken)
    {
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
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}