using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using RestSharp;
using System.Security.Cryptography.X509Certificates;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCalls;

public sealed class RandomCallsHandler : IRequestHandler<RandomCallsRequest, RandomCallsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRandomUsersRepository _iRandomCallsRepository;
    private readonly IMeetingIdsRepository _iIMeetingIdsRepository;

    private readonly IUserRepository _iUserRepository;
    private readonly IMapper _mapper;

    public RandomCallsHandler(IUnitOfWork unitOfWork,
        IRandomUsersRepository iRandomCallsRepository,
        IUserRepository iUserRepository,
        IMeetingIdsRepository iIMeetingIdsRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _iRandomCallsRepository = iRandomCallsRepository;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
        _iIMeetingIdsRepository = iIMeetingIdsRepository;
    }

    public async Task<RandomCallsResponse> Handle(RandomCallsRequest request, CancellationToken cancellationToken)
    {
        RandomCallsResponse response = new RandomCallsResponse();
        try
        {

             var isMeetingFind = await _iIMeetingIdsRepository.FindByUserId(x => (x.Status == 1 || x.Status == 2), cancellationToken);
            if (isMeetingFind != null)
            {

                if (isMeetingFind.Status == 1 && isMeetingFind.FromUserId != request.UserId)
                {
                    isMeetingFind.Status = 2;
                    isMeetingFind.ToUserId = request.UserId;
                    isMeetingFind.ToToken = request.Token;
                    _iIMeetingIdsRepository.Update(isMeetingFind);
                    await _unitOfWork.Save(cancellationToken);
                    response.Status = 2;
                    response.JistiId = isMeetingFind.JitsiId;
                    response.FromUserId = isMeetingFind.FromUserId;
                    response.ToUserId = isMeetingFind.ToUserId;
                    response.Token = isMeetingFind.FromToken;
                }
                else
                {
                    response.Status = isMeetingFind.Status;
                    response.JistiId = isMeetingFind.JitsiId;
                    response.FromUserId = isMeetingFind.FromUserId;
                    response.ToUserId = isMeetingFind.ToUserId;
                    response.Token = isMeetingFind.ToToken;
                }
               
                
                return response;
            }

            var isMeetingExist = await _iIMeetingIdsRepository.FindByCondition(x => x.Status == 1, cancellationToken);
            if (isMeetingExist.Count == 0)
            {
                Guid id = Guid.NewGuid();
                MeetingIds entity = new MeetingIds()
                {
                    Status = 1,
                    Createdby = request.UserId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    JitsiId = id,
                    FromUserId = request.UserId,
                    FromToken  = request.Token
                };
                _iIMeetingIdsRepository.Create(entity);
                await _unitOfWork.Save(cancellationToken);
                response.Status = 1;
                response.JistiId = id;
                response.FromUserId = request.UserId;
                return response;
            }
            

          
        }
        catch (Exception ex)
        {
            throw;
        }

        return response;
    }
}