﻿using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sentry;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers;

public sealed class SaveMeetingsUsersHandler : IRequestHandler<SaveMeetingsUsersRequest, SaveMeetingsUsersResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly IMeetingsUsersRepository _iMeetingsUserRepository;
    private readonly ILogger<SaveMeetingsUsersHandler> _logger;
    public SaveMeetingsUsersHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
        IMeetingsUsersRepository iMeetingsUserRepository,
        ILogger<SaveMeetingsUsersHandler> logger
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _iMeetingsUserRepository = iMeetingsUserRepository;
          _logger = logger;
    }
    public async Task<SaveMeetingsUsersResponse> Handle(SaveMeetingsUsersRequest request, CancellationToken cancellationToken)
    {

        _logger.LogDebug($"Statring method {nameof(Handle)}");
        SaveMeetingsUsersResponse response = new SaveMeetingsUsersResponse();
        try
        {
            if (request != null && request.MeetingId != Guid.Empty)
            {
                if (request.IsmeetingAdmin == true)
                {
                    if (request.Isactive)
                    {
                        MeetingUsers meetingUsers = await _iMeetingsUserRepository.FindByUserId(x => x.Id == request.MeetingId && x.UserId == request.UserId, cancellationToken);
                        if (meetingUsers != null)
                        {
                              meetingUsers.IsmeetingAdmin = true;
                            meetingUsers.IsActive = request.Isactive;
                            _iMeetingsUserRepository.Update(meetingUsers);
                        }
                        else
                        {
                            var user = _mapper.Map<MeetingUsers>(request);
                            user.IsActive = request.Isactive;
                            user.UpdateDate = DateTime.UtcNow;
                            user.CreatedDate = DateTime.UtcNow;
                            user.IsmeetingAdmin = true;
                            _iMeetingsUserRepository.Create(user);
                        }

                        await _unitOfWork.Save(cancellationToken);
                        response.IsSuccess = true;
                    }
                    else
                    {
                        CancellationToken cancellation = CancellationToken.None;
                        Meetings meetings = await _iMeetingsRepository.FindByUserId(x => x.Id == request.MeetingId, cancellation);
                        if (meetings != null)
                        {
                            meetings.IsActive = false;
                            meetings.UpdateDate = DateTime.UtcNow;
                            meetings.CreatedDate = DateTime.UtcNow;
                            _iMeetingsRepository.Update(meetings);
                            await _unitOfWork.Save(cancellation);
                            response.IsSuccess = true;
                        }
                    }
                }
                else if (request.IsmeetingAdmin == false)
                {
                    CancellationToken cancellation = CancellationToken.None;
                    MeetingUsers meetingUsers = await _iMeetingsUserRepository.FindByUserId(x => x.MeetingId == request.MeetingId && x.UserId == request.UserId, cancellation);
                    if (meetingUsers != null)
                    {
                        meetingUsers.IsmeetingAdmin = false;
                        meetingUsers.IsActive = request.Isactive;
                        _iMeetingsUserRepository.Update(meetingUsers);
                    }
                    else
                    {
                        var user = _mapper.Map<MeetingUsers>(request);
                        user.IsActive = request.Isactive;
                        user.UpdateDate = DateTime.UtcNow;
                        user.CreatedDate = DateTime.UtcNow;
                        user.IsmeetingAdmin=false;
                        _iMeetingsUserRepository.Create(user);
                    }

                    await _unitOfWork.Save(cancellation);
                    response.IsSuccess = true;

                }
            }
            _logger.LogDebug($"Ending method {nameof(Handle)}");

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}