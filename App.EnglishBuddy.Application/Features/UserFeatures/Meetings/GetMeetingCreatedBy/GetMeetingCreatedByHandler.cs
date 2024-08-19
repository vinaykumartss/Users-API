using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingCreatedBy;

public sealed class GetMeetingCreatedByHandler : IRequestHandler<GetMeetingCreatedByRequest, GetMeetingCreatedByResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly IMeetingsUsersRepository _iMeetingsUserRepository;
    public GetMeetingCreatedByHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
        IMeetingsUsersRepository iMeetingsUserRepository
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _iMeetingsUserRepository = iMeetingsUserRepository;
    }

    public async Task<GetMeetingCreatedByResponse> Handle(GetMeetingCreatedByRequest request, CancellationToken cancellationToken)
    {
        GetMeetingCreatedByResponse response = new GetMeetingCreatedByResponse();
        try
        {
            int totalMeeting = 0;
            var meetingUser = _iMeetingsUserRepository.FindByListSync(x => x.UserId == request.UserId && x.IsmeetingAdmin ==true);
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