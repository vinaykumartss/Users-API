using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetRandomCallTracking;

public sealed class GetRandomCallTrackingHandler : IRequestHandler<GetRandomCallTrackingRequest, GetRandomCallTrackingResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRandomCallTrackingRepository _iRandomCallTrackingRepository;
    private readonly IMapper _mapper;

    public GetRandomCallTrackingHandler(IUnitOfWork unitOfWork,
        IRandomCallTrackingRepository iRandomCallTrackingRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _iRandomCallTrackingRepository = iRandomCallTrackingRepository;
        _mapper = mapper;
    }
    public async Task<GetRandomCallTrackingResponse> Handle(GetRandomCallTrackingRequest request, CancellationToken cancellationToken)
    {
        GetRandomCallTrackingResponse response = new GetRandomCallTrackingResponse();
        try
        {
            var result = await _iRandomCallTrackingRepository.FindByCondition(x => x.UserId == request.UserId && x.CreatedDate == DateTime.Now.Date, cancellationToken);
            if (result != null && result.Count > 0)
            {
                response.IsSuccess = true;
                response.Minutes = result.Sum(x => x.Minutes);
                response.UserId = request.UserId;
            }
            else
            {
                response.IsSuccess = true;
                response.Minutes = 0;
                response.UserId = request.UserId;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return response;

    }
}