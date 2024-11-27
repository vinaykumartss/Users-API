using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed class DeleteCallsHandler : IRequestHandler<DeleteCallsRequest, DeleteCallsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICallsRepository _iCallsRepository;
    private readonly IMapper _mapper;
   private readonly ILogger<DeleteCallsHandler> _logger;
    public DeleteCallsHandler(IUnitOfWork unitOfWork, ICallsRepository iCallsRepository, IMapper mapper,ILogger<DeleteCallsHandler> _logger)
    {
        _unitOfWork = unitOfWork;
        _iCallsRepository = iCallsRepository;
        _mapper = mapper;
        _logger = _logger;
    }
    
    public async Task<DeleteCallsResponse> Handle(DeleteCallsRequest request, CancellationToken cancellationToken)
    {
        DeleteCallsResponse result = new DeleteCallsResponse();

        //if (request.IsDeleted)
        //{
        //    //List<Calls> calls = await _iCallsRepository.GetAll(cancellationToken);
        //    //foreach (var call in calls)
        //    //{
        //    //     _iCallsRepository.Delete(call);
        //    //    await _unitOfWork.Save(cancellationToken);
        //    //}
        //    result.Result = null;
        //} else
        //{
        //    result.Result = await _iCallsRepository.GetAll(cancellationToken);
        //}

        return result;
    }
}