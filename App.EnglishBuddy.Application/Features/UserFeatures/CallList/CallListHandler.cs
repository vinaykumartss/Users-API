using App.EnglishBuddy.Application.Common.Utility;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallList;

public sealed class CallListHandler : IRequestHandler<CallListRequest, CallListResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICallInfoRepository _iCallInfoRepository;
    public CallListHandler(IUnitOfWork unitOfWork,
        IMapper mapper, ICallInfoRepository iCallInfoRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iCallInfoRepository = iCallInfoRepository;
    }

    public async Task<CallListResponse> Handle(CallListRequest request, CancellationToken cancellationToken)
    {
        CallListResponse callListResponse = new CallListResponse();
        List<CallsDetailsResponse> result= await _iCallInfoRepository.CallDetails(request.Id, cancellationToken);
        
        callListResponse.Response = null;
        return callListResponse;
    }
}