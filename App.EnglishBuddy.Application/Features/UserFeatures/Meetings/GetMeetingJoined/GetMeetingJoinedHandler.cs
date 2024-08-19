using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingJoined;

public sealed class GetMeetingJoinedHandler : IRequestHandler<GetMeetingJoinedRequest, GetMeetingJoinedResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly IMeetingsUsersRepository _iMeetingsUserRepository;
    public GetMeetingJoinedHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
        IMeetingsUsersRepository iMeetingsUserRepository
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _iMeetingsUserRepository = iMeetingsUserRepository;
    }

    public async Task<GetMeetingJoinedResponse> Handle(GetMeetingJoinedRequest request, CancellationToken cancellationToken)
    {
        GetMeetingJoinedResponse response = new GetMeetingJoinedResponse();
        try
        {
            int totalMeeting = 0;
            var meetingUser = _iMeetingsUserRepository.FindByListSync(x => x.UserId == request.UserId && x.IsmeetingAdmin == false));
            if (meetingUser != null)
            {
                totalMeeting = meetingUser.Count;
            }
            response.Total = totalMeeting;
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