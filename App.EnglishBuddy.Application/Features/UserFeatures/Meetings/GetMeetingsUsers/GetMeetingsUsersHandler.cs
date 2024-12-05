using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingsUsers;

public sealed class GetMeetingsUsersHandler : IRequestHandler<GetMeetingsUsersRequest, GetMeetingsUsersResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsUsersRepository _iMeetingsRepository;
    private readonly ILogger<GetMeetingsUsersHandler> _logger;
    public GetMeetingsUsersHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsUsersRepository iMeetingsRepository,
        ILogger<GetMeetingsUsersHandler> logger
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _logger = logger;
    }

    public async Task<GetMeetingsUsersResponse> Handle(GetMeetingsUsersRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        GetMeetingsUsersResponse response = new GetMeetingsUsersResponse();
        try
        {
            var isMeetingExist = await _iMeetingsRepository.FindByCondition(x => x.MeetingId == request.MeetingId && x.IsActive ==true, cancellationToken);
            if(isMeetingExist.Count>0)
            {
                response.TotalUsers = isMeetingExist.Count;
            } else
            {
                response.TotalUsers = 0;
            }
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