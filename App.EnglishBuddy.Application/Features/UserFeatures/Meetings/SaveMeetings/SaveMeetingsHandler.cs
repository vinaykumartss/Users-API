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
    public SaveMeetingsHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
    }

    public async Task<SaveMeetingsResponse> Handle(SaveMeetingsRequest request, CancellationToken cancellationToken)
    {
        SaveMeetingsResponse response = new SaveMeetingsResponse();
        try
        {
            var user = _mapper.Map<Meetings>(request);
            user.MeetingId = Guid.NewGuid();
            user.StartDate= DateTime.UtcNow;
            user.CreatedDate = DateTime.UtcNow;
            user.UpdateDate = DateTime.UtcNow;
            user.UserId= request.UserId;
            _iMeetingsRepository.Create(user);
            await _unitOfWork.Save(cancellationToken);
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}