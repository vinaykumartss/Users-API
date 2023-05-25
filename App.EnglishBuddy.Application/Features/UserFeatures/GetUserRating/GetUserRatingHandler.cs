using App.EnglishBuddy.Application.Common.Utility;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserRating;

public sealed class UserRatingHandler : IRequestHandler<GetUserRatingRequest, GetUserRatingResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITotalRatingsRepository _iIRatingsRepository;
    public UserRatingHandler(IUnitOfWork unitOfWork,
        IMapper mapper, ITotalRatingsRepository iIRatingsRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iIRatingsRepository = iIRatingsRepository;
    }

    public async Task<GetUserRatingResponse> Handle(GetUserRatingRequest request, CancellationToken cancellationToken)
    {
       GetUserRatingResponse ratings = new  GetUserRatingResponse();
        try
        {
          var result = await _iIRatingsRepository.FindByUserId(x => x.UserId == request.UserId, cancellationToken);
            if (result != null)
            {
                ratings.UserId = result.UserId;
                ratings.UserRating = result.TotalRating;
            }
        }
        catch (Exception )
        {
            throw;
        }
        return ratings;
    }
}