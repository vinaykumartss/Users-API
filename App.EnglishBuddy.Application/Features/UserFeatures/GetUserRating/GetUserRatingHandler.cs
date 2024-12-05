using App.EnglishBuddy.Application.Common.Utility;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserRating;

public sealed class UserRatingHandler : IRequestHandler<GetUserRatingRequest, GetUserRatingResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITotalRatingsRepository _iIRatingsRepository;
    private readonly ILogger<UserRatingHandler> _logger;
    public UserRatingHandler(IUnitOfWork unitOfWork,
        IMapper mapper, ITotalRatingsRepository iIRatingsRepository,
         ILogger<UserRatingHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _iIRatingsRepository = iIRatingsRepository;
    }

    public async Task<GetUserRatingResponse> Handle(GetUserRatingRequest request, CancellationToken cancellationToken)
    {
       GetUserRatingResponse ratings = new  GetUserRatingResponse();
        try
        {
            _logger.LogDebug($"Statring method {nameof(Handle)}");
            var result = await _iIRatingsRepository.FindByUserId(x => x.UserId == request.UserId, cancellationToken);
            if (result != null)
            {
                ratings.UserId = result.UserId;
                ratings.UserRating = result.TotalRating;
            }
            _logger.LogDebug($"Ending method {nameof(Handle)}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
        return ratings;
    }
}