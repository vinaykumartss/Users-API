using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace App.EnglishBuddy.Application.Features.UserFeatures.AllContactUs;

public sealed class AllContactUsHandler : IRequestHandler<AllContactUsRequest, List<AllContactUsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IContactUsRepository _iContactUsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AllContactUsHandler> _logger;
    public AllContactUsHandler(IUnitOfWork unitOfWork,
        IContactUsRepository iContactUsRepository,
        IMapper mapper,
        ILogger<AllContactUsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _iContactUsRepository = iContactUsRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<List<AllContactUsResponse>> Handle(AllContactUsRequest request, CancellationToken cancellationToken)
    {
         _logger.LogDebug($"Statring method {nameof(Handle)}");
        List<AllContactUsResponse> response = new List<AllContactUsResponse>();
       
        try
        {
            var users = await _iContactUsRepository.GetAllContact(request, cancellationToken);
             _logger.LogDebug($"Ending method {nameof(Handle)}");
            return _mapper.Map<List<AllContactUsResponse>>(users);
        
        }
        catch (Exception ex)
        {
             _logger.LogError(ex.Message);
            throw;
        }
        return response;

    }
}