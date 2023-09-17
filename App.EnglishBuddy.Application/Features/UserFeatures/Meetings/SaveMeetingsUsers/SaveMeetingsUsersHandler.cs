using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Sentry;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers;

public sealed class SaveMeetingsUsersHandler : IRequestHandler<SaveMeetingsUsersRequest, SaveMeetingsUsersResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly IMeetingsUsersRepository _iMeetingsUserRepository;
    public SaveMeetingsUsersHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
        IMeetingsUsersRepository iMeetingsUserRepository
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _iMeetingsUserRepository = iMeetingsUserRepository;
    }
    public async Task<SaveMeetingsUsersResponse> Handle(SaveMeetingsUsersRequest request, CancellationToken cancellationToken)
    {
        SaveMeetingsUsersResponse response = new SaveMeetingsUsersResponse();
        try
        {
            if (request.Isactive)
            {
                var user = _mapper.Map<MeetingUsers>(request);
                user.UpdateDate = DateTime.UtcNow;
                user.CreatedDate = DateTime.UtcNow;
                _iMeetingsUserRepository.Create(user);
                await _unitOfWork.Save(cancellationToken);
                response.IsSuccess = true;
            }
            else
            {
                CancellationToken cancellation = new CancellationToken(false);
                var meetings = await _iMeetingsUserRepository.FindByUserId(x => x.MeetingId == request.MeetingId, cancellation);
                if (meetings != null)
                {
                    meetings.IsActive = false;
                    meetings.UpdateDate = DateTime.UtcNow;
                    meetings.CreatedDate = DateTime.UtcNow;
                    _iMeetingsUserRepository.Update(meetings);
                    await _unitOfWork.Save(cancellation);
                    response.IsSuccess = true;
                }
                var userMeeting = await _iMeetingsRepository.FindByUserId(x => x.Id == request.MeetingId, cancellation);
                if (userMeeting != null && userMeeting.UserId == request.UserId)
                {
                    userMeeting.IsActive = false;
                    userMeeting.UpdateDate = DateTime.UtcNow;
                    userMeeting.CreatedDate = DateTime.UtcNow;
                    userMeeting.StartDate = DateTime.UtcNow;
                    _iMeetingsRepository.Update(userMeeting);
                    await _unitOfWork.Save(cancellation);
                }
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}