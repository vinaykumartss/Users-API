using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;

public sealed class GetAllMeetingsHandler : IRequestHandler<GetAllMeetingsRequest, List<GetAllMeetingsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly ILogger<GetAllMeetingsHandler> _logger;
    public GetAllMeetingsHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
         ILogger<GetAllMeetingsHandler> logger
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _logger = logger;
    }

    public async Task<List<GetAllMeetingsResponse>> Handle(GetAllMeetingsRequest request, CancellationToken cancellationToken)
    {
       
        List<GetAllMeetingsResponse> response = new List<GetAllMeetingsResponse>();
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        try
        {
            List<GetAllMeetingsResponse> listMeestings = await _iMeetingsRepository.CallDetails(cancellationToken);
            _logger.LogDebug($"Statring method {nameof(Handle)}");
            return listMeestings;
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
        return response;
    }
}