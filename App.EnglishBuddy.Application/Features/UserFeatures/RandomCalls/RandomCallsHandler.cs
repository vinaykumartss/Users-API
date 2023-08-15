using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCalls;

public sealed class RandomCallsHandler : IRequestHandler<RandomCallsRequest, RandomCallsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRandomCallsRepository _iRandomCallsRepository;
    private readonly IUserRepository _iUserRepository;
    private readonly IMapper _mapper;

    public RandomCallsHandler(IUnitOfWork unitOfWork,
        IRandomCallsRepository iRandomCallsRepository,
        IUserRepository iUserRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _iRandomCallsRepository = iRandomCallsRepository;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
    }

    public async Task<RandomCallsResponse> Handle(RandomCallsRequest request, CancellationToken cancellationToken)
    {
        RandomCallsResponse response = new RandomCallsResponse();
        try
        {
            var randomCalls = await _iRandomCallsRepository.FindByCondition(x => x.Status == 0 && x.FromUserId == request.UserId, cancellationToken);
            if (randomCalls != null && randomCalls.Count >0)
            {
                response.FromUserId = request.UserId;
                response.ToUserId = randomCalls[0].FromUserId;
                response.JistiId = randomCalls[0].JitsiId;
                response.CallId = randomCalls[0].Id;

                randomCalls[0].Status = 2;
                foreach (var randomCall in randomCalls)
                {
                    _iRandomCallsRepository.Update(randomCall);
                    await _unitOfWork.Save(cancellationToken);
                }
            } else
            {
                App.EnglishBuddy.Domain.Entities.RandomCalls entity = new App.EnglishBuddy.Domain.Entities.RandomCalls()
                {
                    FromUserId = request.UserId,
                    Status =0,
                    Createdby= request.UserId,
                    IsActive=true,
                    JitsiId = new Guid(),
                    CreatedDate = DateTime.UtcNow
                };
                _iRandomCallsRepository.Create(entity);
                await _unitOfWork.Save(cancellationToken);
            }

        }
        catch (Exception ex)
        {
            throw;
        }

        return response;
    }
}