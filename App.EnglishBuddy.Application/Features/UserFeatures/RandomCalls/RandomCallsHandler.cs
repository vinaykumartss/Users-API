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
            var isMeetingExist = await _iIMeetingIdsRepository.FindByCondition(x => x.Status == 1 || x.Status == 0, cancellationToken);
            if (isMeetingExist.Count == 0)
            {
                MeetingIds entity = new MeetingIds()
                {
                    Status = 0,
                    Createdby = request.UserId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    JitsiId = Guid.NewGuid()
                };
                _iIMeetingIdsRepository.Create(entity);
                await _unitOfWork.Save(cancellationToken);
            }
            var user = await _iRandomCallsRepository.FindByCondition(x => x.UserId == request.UserId && x.Status !=2, cancellationToken);

            if (user.Count() == 0)
            {
                RandomUsers entity = new RandomUsers()
                {
                    Status = 0,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UserId = request.UserId,
                };
                _iRandomCallsRepository.Create(entity);
                await _unitOfWork.Save(cancellationToken);

            }
            var userCheckAll = await _iRandomCallsRepository.FindByCondition(x=>x.UserId == request.UserId, cancellationToken);

            var userCheck = userCheckAll.FirstOrDefault();
            if (userCheck != null && userCheck.Status != 2)
            {
                var allMeetings = await _iIMeetingIdsRepository.FindByCondition(x => x.Status == 1 || x.Status == 0, cancellationToken);
                var allMeetingsWithStatusOne = allMeetings.Where(x => x.Status == 1);
                foreach (var meeting in allMeetingsWithStatusOne)
                {
                    if (meeting.FromUserId == request.UserId)
                    {
                        response.Status = 1;
                        response.JistiId = meeting.JitsiId;
                        return response;
                    }
                    if (meeting.ToUserId == null)
                    {

                        meeting.Status = 2;
                        meeting.CreatedDate = DateTime.UtcNow;
                        meeting.ToUserId = request.UserId;
                        _iIMeetingIdsRepository.Update(meeting);
                        await _unitOfWork.Save(cancellationToken);

                       

                        var fromUserId = meeting.FromUserId.HasValue ? meeting.FromUserId.Value : new Guid();
                        var fromUser = await _iRandomCallsRepository.FindByUserId(x => x.UserId == fromUserId, cancellationToken);
                        fromUser.Status = 2;
                        fromUser.CreatedDate = DateTime.UtcNow;
                        _iRandomCallsRepository.Update(fromUser);
                        await _unitOfWork.Save(cancellationToken);

                        response.Status = 2;
                        response.JistiId = meeting.JitsiId;
                        response.FromUserId = meeting.FromUserId;
                        response.ToUserId = request.UserId;
                        response.JistiId = meeting.JitsiId;

                        return response;
                    }
                }

                var allMeetingsWithStatusZero = allMeetings.Where(x => x.Status == 0);
                foreach (var meeting in allMeetingsWithStatusZero)
                {

                    meeting.Status = 1;
                    meeting.CreatedDate = DateTime.UtcNow;
                    meeting.FromUserId = request.UserId;
                    _iIMeetingIdsRepository.Update(meeting);
                    await _unitOfWork.Save(cancellationToken);


                    userCheck.Status = 1;
                    userCheck.CreatedDate = DateTime.UtcNow;

                    _iRandomCallsRepository.Update(userCheck);
                    await _unitOfWork.Save(cancellationToken);


                    response.Status = 1;
                    response.JistiId = meeting.JitsiId;
                    return response;
                }
            } else
            {
                var allMeetings = await _iIMeetingIdsRepository.FindByCondition(x => x.Status == 2 && (x.FromUserId == request.UserId || x.ToUserId == request.UserId), cancellationToken);
                foreach (var meeting in allMeetings)
                {
                    if(meeting.FromUserId == request.UserId)
                    {
                        response.Status = 2;
                        response.JistiId = meeting.JitsiId;
                        response.FromUserId = request.UserId;
                        response.ToUserId = meeting.ToUserId;
                        response.JistiId = meeting.JitsiId;

                    }
                    else if(meeting.ToUserId == request.UserId)
                    {
                        response.Status = 2;
                        response.JistiId = meeting.JitsiId;
                        response.FromUserId = meeting.FromUserId;
                        response.ToUserId = request.UserId;
                        response.JistiId = meeting.JitsiId;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        return response;
    }
}