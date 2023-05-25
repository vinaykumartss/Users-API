using App.EnglishBuddy.Application.Common.Utility;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UserRating;

public sealed class UserRatingHandler : IRequestHandler<UserRatingsRequest, UserRatingsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRatingsRepository _iIRatingsRepository;
    private readonly ITotalRatingsRepository _iTotalRatingsRepository;
    public UserRatingHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IRatingsRepository iIRatingsRepository,
         ITotalRatingsRepository iTotalRatingsRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iIRatingsRepository = iIRatingsRepository;
        _iTotalRatingsRepository = iTotalRatingsRepository;
    }

    public async Task<UserRatingsResponse> Handle(UserRatingsRequest request, CancellationToken cancellationToken)
    {
        UserRatingsResponse ratings = new UserRatingsResponse();
        try
        {
            Ratings result = new Ratings()
            {
                Rating = request.Rating,
                UserId = request.UserId,
                IsActive=true,
                CreatedDate = DateTime.UtcNow
            };
            _iIRatingsRepository.Create(result);

            var totalRatingresult = await _iTotalRatingsRepository.FindByUserId(x => x.UserId == request.UserId, cancellationToken);

            if(totalRatingresult == null)
            {

                TotalRatings totalresult = new TotalRatings()
                {
                    TotalRating = request.Rating,
                    UserId = request.UserId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };
                _iTotalRatingsRepository.Create(totalresult);
            } else
            {
                totalRatingresult.TotalRating = totalRatingresult.TotalRating + request.Rating;
                totalRatingresult.UpdateDate = DateTime.UtcNow;
                totalRatingresult.CreatedDate = DateTime.UtcNow;
                _iTotalRatingsRepository.Update(totalRatingresult);
            }
            await _unitOfWork.Save(cancellationToken);
            ratings.IsSuccess = true;
        }
        catch (Exception ex)
        {
            ratings.IsSuccess = false;
            throw;
        }
        return ratings;
    }
}