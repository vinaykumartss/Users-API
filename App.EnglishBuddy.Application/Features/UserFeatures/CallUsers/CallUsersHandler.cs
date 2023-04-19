using App.EnglishBuddy.Application.Common.AppMessage;
using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;

public sealed class CallUsersHandler : IRequestHandler<CallUsersRequest, CallUsersResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICallsRepository _iCallsRepository;
    private readonly IUserRepository _iUserRepository;
    private readonly IMapper _mapper;

    public CallUsersHandler(IUnitOfWork unitOfWork,
        ICallsRepository iCallsRepository,
        IUserRepository iUserRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _iCallsRepository = iCallsRepository;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
    }

    public async Task<CallUsersResponse> Handle(CallUsersRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Calls>(request);
        CallUsersResponse response = new CallUsersResponse();
        response.UserId = request.UserId;
        try
        {
            Calls lstCalls = await _iCallsRepository.FindByUserId(x => x.UserId == user.UserId, cancellationToken);
            if (lstCalls != null && !String.IsNullOrEmpty(lstCalls.OpponentUserId))
            {
                if (lstCalls.SessionId != null)
                {
                    response.SessionId = lstCalls.SessionId;
                    response.OpponentUserId = lstCalls.OpponentUserId;
                    response.IsUserFound = true;
                    response.CallInitiator = lstCalls.CallInitiator;
                    return response;
                }
                else if (request.SessionId == null && !String.IsNullOrEmpty(lstCalls.OpponentUserId))
                {
                    response.SessionId = lstCalls.SessionId;
                    response.OpponentUserId = lstCalls.OpponentUserId;
                    response.IsUserFound = true;
                    response.CallInitiator = lstCalls.CallInitiator;
                    return response;
                }
                else if (request.SessionId != null && lstCalls.OpponentUserId != null && lstCalls.SessionId == null)
                {
                    lstCalls.SessionId = user.SessionId;
                    _iCallsRepository.Update(lstCalls);
                    await _unitOfWork.Save(cancellationToken);
                    user.SessionId = lstCalls.SessionId;
                    Calls updateSessionOponentUser = await _iCallsRepository.FindByUserId(x => x.UserId == request.OpponentUserId, cancellationToken);
                    if (updateSessionOponentUser != null)
                    {
                        updateSessionOponentUser.IsActive = false;
                        updateSessionOponentUser.UpdateDate = DateTime.UtcNow;
                        updateSessionOponentUser.IsSession = false;
                        updateSessionOponentUser.SessionId = user.SessionId;
                        _iCallsRepository.Update(updateSessionOponentUser);
                        await _unitOfWork.Save(cancellationToken);
                    }
                    response.SessionId = lstCalls.SessionId;
                    response.IsUserFound = true;
                    response.CallInitiator = lstCalls.CallInitiator;
                  
                    return response;
                } 
                
                else
                {
                    response.IsUserFound = false;
                }
            }
            else if(lstCalls == null)
            {
                user.IsActive = true;
                user.UpdateDate = DateTime.UtcNow;
                user.CallInitiator = false;
                user.SessionId = null;
                _iCallsRepository.Create(user);

                await _unitOfWork.Save(cancellationToken);
            } 

            List<Calls> results = await _iCallsRepository.FindByCondition(x => x.UserId != user.UserId, cancellationToken);
            if (results.Any())
            {
                if (lstCalls == null)
                {
                    user.OpponentUserId = results[0].UserId;
                    results[0].IsActive = false;
                    user.IsActive = false;
                    user.IsSession = true;
                    user.UpdateDate = DateTime.UtcNow;
                    _iCallsRepository.Update(user);
                    await _unitOfWork.Save(cancellationToken);
                }
                else
                {
                    lstCalls.OpponentUserId = results[0].UserId;
                    results[0].IsActive = false;
                    lstCalls.IsActive = false;
                    lstCalls.IsSession = true;
                    lstCalls.UpdateDate = DateTime.UtcNow;
                    _iCallsRepository.Update(lstCalls);
                    await _unitOfWork.Save(cancellationToken); 
                }
                if (results[0].UserId != null)
                {
                    Calls oponentUser = await _iCallsRepository.GetById(results[0].Id, cancellationToken);
                    if (oponentUser != null && oponentUser.OpponentUserId != null)
                    {
                        oponentUser.IsActive = false;
                        oponentUser.UpdateDate = DateTime.UtcNow;
                        oponentUser.IsSession = true;
                        oponentUser.OpponentUserId = user.UserId;
                        _iCallsRepository.Update(oponentUser);
                        await _unitOfWork.Save(cancellationToken);

                        Guid id = lstCalls?.Id ?? user.Id; 
                        Calls initilator = await _iCallsRepository.GetById(id, cancellationToken);
                        if (initilator != null)
                        {

                            initilator.CallInitiator = true;
                            _iCallsRepository.Update(initilator);
                            await _unitOfWork.Save(cancellationToken);
                        }

                    }
                }
                else
                {
                    response.IsUserFound = false;
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