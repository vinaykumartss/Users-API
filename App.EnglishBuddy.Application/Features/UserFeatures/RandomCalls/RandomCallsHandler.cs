using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using cashfreepg.Model;
using MediatR;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<RandomCallsHandler> _logger;

    public RandomCallsHandler(IUnitOfWork unitOfWork,
        IRandomUsersRepository iRandomCallsRepository,
        IUserRepository iUserRepository,
        IMeetingIdsRepository iIMeetingIdsRepository,
        IMapper mapper,
        ILogger<RandomCallsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _iRandomCallsRepository = iRandomCallsRepository;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
        _iIMeetingIdsRepository = iIMeetingIdsRepository;
        _logger = logger;
    }

    public async Task<RandomCallsResponse> Handle(RandomCallsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
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
            if (dictPeople.Count < 2)
            {
                throw new BadRequestException("Not enough people to connect.");
            }
            
           

            foreach (KeyValuePair<Guid, RandomCallsMatch> kvp in dictPeople)
            {
                if (kvp.Value.ToId == request.UserId)
                {
                    kvp.Value.Status = 2;
                    isFound = true;
                    dictPeople.Remove(kvp.Value.FromId);
                    dictPeople.Remove(kvp.Value.ToId);

                    response.Status = 2;
                    response.JistiId = kvp.Value.MeetingId;
                    response.FromUserId = kvp.Value.FromId;
                    response.ToUserId = kvp.Value.ToId;

                }
                else if (kvp.Value.FromId == request.UserId && kvp.Value.ToId != request.UserId)
                {
                    kvp.Value.Status = 2;
                    response.Status = 2;
                    response.JistiId = kvp.Value.MeetingId;
                    response.FromUserId = request.UserId;
                    response.ToUserId = kvp.Value.ToId;
                }
            }

            if (isFound == false)
            {
                string toUserId = GetRandomPerson(request.UserId.ToString());
                Guid meetingId = Guid.NewGuid();
                if (toUserId != null)
                {
                    dictPeople[request.UserId] = new RandomCallsMatch { ToId = Guid.Parse(toUserId ?? string.Empty), MeetingId = meetingId };
                }

                response.Status = 2;
                response.JistiId = meetingId;
                response.FromUserId = request.UserId;
                response.ToUserId = Guid.Parse(toUserId ?? string.Empty);
            }
            _logger.LogDebug($"Ending method {nameof(Handle)}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
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