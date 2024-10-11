using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCallTrackings;

public sealed class RandomCallTrackingHandler : IRequestHandler<RandomCallTrackingRequest, RandomCallTrackingResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRandomCallTrackingRepository _iRandomCallTrackingRepository;
    private readonly IMapper _mapper;

    public RandomCallTrackingHandler(IUnitOfWork unitOfWork,
        IRandomCallTrackingRepository iRandomCallTrackingRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _iRandomCallTrackingRepository = iRandomCallTrackingRepository;
        _mapper = mapper;
    }
    public async Task<RandomCallTrackingResponse> Handle(RandomCallTrackingRequest request, CancellationToken cancellationToken)
    {
        RandomCallTrackingResponse response = new RandomCallTrackingResponse();
        try
        {
            RandomCallingTracking randomCallingTracking = new RandomCallingTracking()
            {
                UserId = request.UserId,
                Minutes = request.Minutes,
                ToUserId = request.ToUserId,
                CreatedDate = DateTime.Now.Date
            };
            _iRandomCallTrackingRepository.Create(randomCallingTracking);
            await _unitOfWork.Save(cancellationToken);

        }
        catch (Exception ex)
        {
            throw;
        }
        return response;

    }
}