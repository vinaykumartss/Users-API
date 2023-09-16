using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Sentry;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers;

public sealed class SaveMeetingsUsersHandler : IRequestHandler<SaveMeetingsUsersRequest, SaveMeetingsUsersResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsUsersRepository _iMeetingsRepository;
    public SaveMeetingsUsersHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsUsersRepository iMeetingsRepository
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
    }

    public async Task<SaveMeetingsUsersResponse> Handle(SaveMeetingsUsersRequest request, CancellationToken cancellationToken)
    {
        SaveMeetingsUsersResponse response = new SaveMeetingsUsersResponse();
        try
        {
            if (request.Isactive)
            {
                var user = _mapper.Map<MeetingUsers>(request);
                _iMeetingsRepository.Create(user);
                await _unitOfWork.Save(cancellationToken);
                response.IsSuccess = true;
            }
            else
            {
                var meetings = await _iMeetingsRepository.FindByUserId(x => x.MeetingId == request.MeetingId, cancellationToken);
                if(meetings != null)
                {
                    meetings.IsActive = false;
                    _iMeetingsRepository.Update(meetings);
                    await _unitOfWork.Save(cancellationToken);
                    response.IsSuccess = true;
                }
            }

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}