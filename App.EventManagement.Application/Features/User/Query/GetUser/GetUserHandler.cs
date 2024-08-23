using App.EventManagement.Application.Repositories;
using App.EventManagement.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace App.EventManagement.Application.Features.Query.GetUser;

public sealed class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserHandler> _logger;
    private readonly IMediator _mediator;


    public GetUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<GetUserHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;

    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        GetUserResponse response = new GetUserResponse();
        try
        {
            Users user = await _userRepository.GetById(request.Id, cancellationToken);
            _logger.LogInformation("user" + JsonConvert.SerializeObject(user));
            response = _mapper.Map<GetUserResponse>(user);


            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}