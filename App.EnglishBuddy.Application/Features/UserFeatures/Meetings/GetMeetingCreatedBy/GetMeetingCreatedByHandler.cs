using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingCreatedBy;

public sealed class GetMeetingCreatedByHandler : IRequestHandler<GetMeetingCreatedByRequest, GetMeetingCreatedByResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly ILogger<GetMeetingCreatedByHandler> _logger;
    public GetMeetingCreatedByHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
         ILogger<GetMeetingCreatedByHandler> logger

       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _logger = logger;
    }

    public async Task<GetMeetingCreatedByResponse> Handle(GetMeetingCreatedByRequest request, CancellationToken cancellationToken)
    {
        GetMeetingCreatedByResponse response = new GetMeetingCreatedByResponse();
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        try
        {
            int totalMeeting = 0;
            var meetingUser = _iMeetingsRepository.FindByListSync(x => x.UserId == request.UserId );
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