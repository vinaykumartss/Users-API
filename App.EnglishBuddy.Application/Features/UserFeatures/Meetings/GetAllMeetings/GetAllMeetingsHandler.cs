using App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Sentry;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;

public sealed class GetAllMeetingsHandler : IRequestHandler<GetAllMeetingsRequest, List<GetAllMeetingsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeetingsRepository _iMeetingsRepository;
    public GetAllMeetingsHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IMeetingsRepository iMeetingsRepository
       )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iMeetingsRepository = iMeetingsRepository;
    }

    public async Task<List<GetAllMeetingsResponse>> Handle(GetAllMeetingsRequest request, CancellationToken cancellationToken)
    {
        List<GetAllMeetingsResponse> response = new List<GetAllMeetingsResponse>();
        try
        {
            List<GetAllMeetingsResponse> listMeestings = await _iMeetingsRepository.CallDetails(cancellationToken);
            return listMeestings;
        }
        catch (Exception ex)
        {

            throw;
        }
        return response;
    }
}