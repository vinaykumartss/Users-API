using App.EnglishBuddy.Application.Features.UserFeatures.NotificationToAllHandler;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;

public sealed class SaveMeetingsHandler : IRequestHandler<SaveMeetingsRequest, SaveMeetingsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    private readonly IMeetingsUsersRepository _iMeetingsUserRepository;
    private readonly IMediator _iMediator;
    public SaveMeetingsHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository,
        IMeetingsUsersRepository iMeetingsUserRepository,
         IMediator iMediator
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
        _iMeetingsUserRepository = iMeetingsUserRepository;
        _iMediator = iMediator;
    }

    public async Task<SaveMeetingsResponse> Handle(SaveMeetingsRequest request, CancellationToken cancellationToken)
    {
        SaveMeetingsResponse response = new SaveMeetingsResponse();
        try
        {
            var user = _mapper.Map<Meetings>(request);
            user.MeetingId = Guid.NewGuid();
            user.CreatedDate = DateTime.UtcNow;
            user.UpdateDate = DateTime.UtcNow;
            user.UserId= request.UserId;
            
            user.IsActive = true;
            _iMeetingsRepository.Create(user);
            await _unitOfWork.Save(cancellationToken);

            MeetingUsers meetingUsers = new MeetingUsers();
            meetingUsers.IsActive = true;
            meetingUsers.UserId = request.UserId;
            meetingUsers.MeetingId = user.MeetingId;
            meetingUsers.IsmeetingAdmin = true;
            meetingUsers.CreatedDate = DateTime.UtcNow;
            meetingUsers.UpdateDate = DateTime.UtcNow;
            _iMeetingsUserRepository.Create(meetingUsers);
            await _unitOfWork.Save(cancellationToken);
            response.IsSuccess = true;

            response  = _mapper.Map<SaveMeetingsResponse>(request);
            response.MeetingId = user.MeetingId;
            NotificationToAllRequest req =new NotificationToAllRequest();
            await _iMediator.Send(req);
            response.IsSuccess= true;

           
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}