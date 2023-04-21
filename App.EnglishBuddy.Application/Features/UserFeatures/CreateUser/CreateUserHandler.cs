using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
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