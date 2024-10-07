using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using cashfreepg.Model;
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
    private static readonly object _lock = new();

    private static Dictionary<Guid, RandomCallsMatch> _myList = new Dictionary<Guid, RandomCallsMatch>();
    private static bool _record;
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
            int count = _myList.Count;
            _record = true;
            lock (_myList)
            {
                if (_record)
                {
                    if (!_myList.ContainsKey(request.UserId))
                    {
                        _myList.Add(request.UserId, new RandomCallsMatch { FromId = request.UserId, Status = 1, ToId = null, Order = count + 1 });
                    }
                    if (_myList.Count >= 1)
                    {
                        var dict = _myList.Where(x => x.Key != request.UserId).OrderBy(x => x.Value.Order)?.FirstOrDefault().Key;
                       // if(_myList.Count)
                    }


                    _record = false;
                }
            }
            //

            //lock (_lock)
            //{

            //    var allstatus = _iIMeetingIdsRepository.FindByListSync(x => (x.Status == 1 || x.Status == 2 || x.Status ==3));
            //    var isMeetingFind = allstatus.Where(x => (x.Status == 1 || x.Status == 2)).FirstOrDefault();
            //    if (isMeetingFind != null)
            //    {
            //        if (isMeetingFind.Status == 1 && isMeetingFind.FromUserId != request.UserId)
            //        {
            //            isMeetingFind.Status = 2;
            //            isMeetingFind.ToUserId = request.UserId;
            //            isMeetingFind.ToToken = request.Token;
            //            _iIMeetingIdsRepository.Update(isMeetingFind);
            //            _unitOfWork.Save(cancellationToken);
            //            response.Status = 2;
            //            response.JistiId = isMeetingFind.JitsiId;
            //            response.FromUserId = isMeetingFind.FromUserId;
            //            response.ToUserId = isMeetingFind.ToUserId;
            //            response.Token = isMeetingFind.FromToken;
            //            return response;
            //        }
            //        else
            //        {
            //            response.Status = isMeetingFind.Status;
            //            response.JistiId = isMeetingFind.JitsiId;
            //            response.FromUserId = isMeetingFind.FromUserId;
            //            response.ToUserId = isMeetingFind.ToUserId;
            //            response.Token = isMeetingFind.ToToken;
            //            return response;
            //        }
            //    }
            //    var allConnectedstatus = allstatus.Where(x => x.Status == 3).FirstOrDefault();
            //    if (allConnectedstatus != null && (allConnectedstatus.FromUserId == request.UserId || allConnectedstatus.ToUserId == request.UserId)) {

            //        response.Status = allConnectedstatus.Status;
            //        response.JistiId = allConnectedstatus.JitsiId;
            //        response.FromUserId = allConnectedstatus.FromUserId;
            //        response.ToUserId = allConnectedstatus.ToUserId;
            //        response.Token = allConnectedstatus.ToToken;
            //        return response;
            //    }

            //    var isMeetingExist = allstatus.Where(x => x.Status == 1).FirstOrDefault();
            //    if (isMeetingExist == null)
            //    {
            //        Guid id = Guid.NewGuid();
            //        MeetingIds entity = new MeetingIds()
            //        {
            //            Status = 1,
            //            Createdby = request.UserId,
            //            IsActive = true,
            //            CreatedDate = DateTime.UtcNow,
            //            JitsiId = id,
            //            FromUserId = request.UserId,
            //            FromToken = request.Token
            //        };
            //        _iIMeetingIdsRepository.Create(entity);
            //        _unitOfWork.Save(cancellationToken);
            //        response.Status = 1;
            //        response.JistiId = id;
            //        response.FromUserId = request.UserId;
            //        return response;
            //    }
            //}

        }
        catch (Exception ex)
        {
            throw;
        }

        return await Task.FromResult(response);
    }
}