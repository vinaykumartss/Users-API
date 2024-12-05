using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCallTrackings;

public sealed class RandomCallTrackingHandler : IRequestHandler<RandomCallTrackingRequest, RandomCallTrackingResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRandomCallTrackingRepository _iRandomCallTrackingRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RandomCallTrackingHandler> _logger;

    public RandomCallTrackingHandler(IUnitOfWork unitOfWork,
        IRandomCallTrackingRepository iRandomCallTrackingRepository,
        IMapper mapper,
         ILogger<RandomCallTrackingHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _iRandomCallTrackingRepository = iRandomCallTrackingRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<RandomCallTrackingResponse> Handle(RandomCallTrackingRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
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
            _logger.LogDebug($"Ending method {nameof(Handle)}");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
        return response;

    }
}