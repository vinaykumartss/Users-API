using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingsUsers;

public sealed class GetMeetingsUsersHandler : IRequestHandler<GetMeetingsUsersRequest, GetMeetingsUsersResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsUsersRepository _iMeetingsRepository;
    public GetMeetingsUsersHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsUsersRepository iMeetingsRepository
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
    }

    public async Task<GetMeetingsUsersResponse> Handle(GetMeetingsUsersRequest request, CancellationToken cancellationToken)
    {
        GetMeetingsUsersResponse response = new GetMeetingsUsersResponse();
        try
        {
            var isMeetingExist = await _iMeetingsRepository.FindByCondition(x => x.MeetingId == request.MeetingId, cancellationToken);
            if(isMeetingExist.Count>0)
            {
                response.TotalUsers = isMeetingExist.Count;
            } else
            {
                response.TotalUsers = 0;
            }
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