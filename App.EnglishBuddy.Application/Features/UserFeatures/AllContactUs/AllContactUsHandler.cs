using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace App.EnglishBuddy.Application.Features.UserFeatures.AllContactUs;

public sealed class AllContactUsHandler : IRequestHandler<AllContactUsRequest, List<AllContactUsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IContactUsRepository _iContactUsRepository;
    private readonly IMapper _mapper;

    public AllContactUsHandler(IUnitOfWork unitOfWork,
        IContactUsRepository iContactUsRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _iContactUsRepository = iContactUsRepository;
        _mapper = mapper;
    }
    public async Task<List<AllContactUsResponse>> Handle(AllContactUsRequest request, CancellationToken cancellationToken)
    {
        List<AllContactUsResponse> response = new List<AllContactUsResponse>();
        try
        {
            var users = await _iContactUsRepository.GetAllContact(request, cancellationToken);
            return _mapper.Map<List<AllContactUsResponse>>(users);
        }
        catch (Exception ex)
        {
            throw;
        }
        return response;

    }
}