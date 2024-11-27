using App.EnglishBuddy.Application.Common.Utility;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallList;

public sealed class CallListHandler : IRequestHandler<CallListRequest, CallListResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICallInfoRepository _iCallInfoRepository;
    private readonly ILogger<CallListHandler> _logger;
    public CallListHandler(IUnitOfWork unitOfWork,
        IMapper mapper, ICallInfoRepository iCallInfoRepository,
        ILogger<CallListHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iCallInfoRepository = iCallInfoRepository;
        _logger = logger;
    }

    public async Task<CallListResponse> Handle(CallListRequest request, CancellationToken cancellationToken)
    {
        CallListResponse callListResponse = new CallListResponse();
        try
        {
         _logger.LogDebug($"Statring method {nameof(Handle)}");
        List<CallsDetailsResponse> result= await _iCallInfoRepository.CallDetails(request.Id, cancellationToken);
        callListResponse.Response = null;
        _logger.LogDebug($"Ending method {nameof(Handle)}");
      
        } catch(Exception ex)
        {
            _logger.LogError(ex.Message);
        }
          return callListResponse;
    }
}