using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;
    public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<CreateUserHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger= logger;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hi");
        CreateUserResponse response = new CreateUserResponse();
        try
        {
            var user = _mapper.Map<Users>(request);
            _userRepository.Create(user);
            await _unitOfWork.Save(cancellationToken);
            response.IsSuccess = true;
            response.Id = user.Id;
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}