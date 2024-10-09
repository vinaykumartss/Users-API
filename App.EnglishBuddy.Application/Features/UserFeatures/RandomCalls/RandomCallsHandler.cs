using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using cashfreepg.Model;
using MediatR;
using RestSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
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

    private static Dictionary<Guid, RandomCallsMatch> dictPeople = new Dictionary<Guid, RandomCallsMatch>();
    private static Dictionary<Guid, Guid> pairedPeople = new Dictionary<Guid, Guid>();
    public List<string> lstPeople = new List<string>();
    private static Random _random = new Random();

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
        bool isFound = false;
        RandomCallsResponse response = new RandomCallsResponse();
        try
        {
            int count = _myList.Count;
            if (!dictPeople.ContainsKey(request.UserId))
            {
                dictPeople.Add(request.UserId, new RandomCallsMatch { FromId = request.UserId, Status = 1 });
                count = count + 1;
            }
            if (lstPeople.Count < 2)
            {
                throw new BadRequestException("Not enough people to connect.");
            }
            foreach(KeyValuePair<string,book> kvp in dictPeople) {
                if (kvp.Value.toUserId == request.UserId)
                {
                    kvp.Value.toUserId =2;
                    isFound =true;
                    myDic.Remove(kvp.Value.UserId);
                    myDic.Remove(kvp.Value.toUserId);
                }
            if(isFound == false)
            {
                string toUserId = GetRandomPerson(request.UserId.ToString());
                if (toUserId != null)
                {
                    dictPeople[request.UserId] = new RandomCallsMatch { ToId = Guid.Parse(toUserId??string.Empty), MeetingId =Guid.NewGuid()};
                }
            } 
            
            
}
            
            




            //var person2 = GetRandomPerson(person1);

            //// Return the result as a JSON response
            //new { Person1 = person1, Person2 = person2 });

            //int count = _myList.Count;
            //_record = true;
            //lock (_myList)
            //{
            //    if (_record)
            //    {
            //        if (!_myList.ContainsKey(request.UserId))
            //        {
            //            _myList.Add(request.UserId, new RandomCallsMatch { FromId = request.UserId, Status = 1, ToId = null, Order = count + 1 });
            //        }
            //        if (_myList.Count >= 1)
            //        {
            //            var dict = _myList.Where(x => x.Key != request.UserId).OrderBy(x => x.Value.Order)?.FirstOrDefault().Key;
            //           // if(_myList.Count)
            //        }


            //        _record = false;
            //    }
            //}
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

    private string GetRandomPerson(string excludePerson = null)
    {
        lstPeople = dictPeople.Select(x => x.Key.ToString()).ToList<string>();
        string selectedPerson;
        do
        {
            selectedPerson = lstPeople[_random.Next(lstPeople.Count)];
        } while (selectedPerson == excludePerson);  // Avoid selecting the same person twice
        return selectedPerson;
    }
}