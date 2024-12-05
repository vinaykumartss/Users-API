using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Security.Cryptography.X509Certificates;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCallEnd;

public sealed class RandomCallEndHandler : IRequestHandler<RandomCallEndRequest, RandomCallEndResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRandomUsersRepository _iRandomCallsRepository;
    private readonly IMeetingIdsRepository _iIMeetingIdsRepository;
    private readonly IUserRepository _iUserRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RandomCallEndHandler> _logger;
    public RandomCallEndHandler(IUnitOfWork unitOfWork,
        IRandomUsersRepository iRandomCallsRepository,
        IUserRepository iUserRepository,
        IMeetingIdsRepository iIMeetingIdsRepository,
        IMapper mapper,
         ILogger<RandomCallEndHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _iRandomCallsRepository = iRandomCallsRepository;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
        _iIMeetingIdsRepository = iIMeetingIdsRepository;
        _logger = logger;
    }

    public async Task<RandomCallEndResponse> Handle(RandomCallEndRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        CancellationToken token = new CancellationToken();
        RandomCallEndResponse response = new RandomCallEndResponse();
        try
        {
            MeetingIds meetingIds = await _iIMeetingIdsRepository.FindByUserId(x => x.JitsiId == request.MeetingId, token);
            if (meetingIds != null)
            {
                meetingIds.UpdateDate = DateTime.UtcNow;
                meetingIds.CreatedDate = DateTime.UtcNow;
                meetingIds.Status = request.Status;
                _iIMeetingIdsRepository.Update(meetingIds);
              
            }
            RandomUsers user = await _iRandomCallsRepository.FindByUserId(x => x.UserId == request.UserId, token);
            if (user != null)
            {
                user.UpdateDate = DateTime.UtcNow;
                user.CreatedDate = DateTime.UtcNow;
                user.Status = request.Status;
                _iRandomCallsRepository.Update(user);
               
            }
            await _unitOfWork.Save(token);
            _logger.LogDebug($"Ending method {nameof(Handle)}");
            response.Status = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            response.Status = false;
            throw;
           
        }

        return response;
    }
}