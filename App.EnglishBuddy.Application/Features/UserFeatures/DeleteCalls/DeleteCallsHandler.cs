using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed class DeleteCallsHandler : IRequestHandler<DeleteCallsRequest, DeleteCallsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICallsRepository _iCallsRepository;
    private readonly IMapper _mapper;

    public DeleteCallsHandler(IUnitOfWork unitOfWork, ICallsRepository iCallsRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _iCallsRepository = iCallsRepository;
        _mapper = mapper;
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