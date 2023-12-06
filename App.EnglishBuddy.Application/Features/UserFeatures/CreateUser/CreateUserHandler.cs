using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
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
    private readonly IMediator _mediator;
    public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<CreateUserHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        CreateUserResponse response = new CreateUserResponse();
        try
        {
            Domain.Entities.Users users = await _userRepository.FindByUserId(x => x.Mobile == request.Mobile, cancellationToken);
            if (users == null)
            {
                var user = _mapper.Map<Users>(request);
               _userRepository.Create(user);
                await _unitOfWork.Save(cancellationToken);
                OTPRequest otpRequest = new OTPRequest()
                {
                    Mobile = request.Mobile
                };
                await _mediator.Send(otpRequest, cancellationToken);
                response.IsSuccess = true;
                response.Id = user.Id;
            }
            else
            {
                if (request.Id == null)
                {
                    throw new BadRequestException("User already exist, please try with other mobile number");
                }
                else
                {
                    users.CreatedDate = DateTime.UtcNow;
                    users.UpdateDate = DateTime.UtcNow;
                    users.FirstName = request.FirstName;
                    users.LastName = request.LastName;
                    users.StateId = request.StateId;
                    users.CityId = request.CityId;
                    if(request.CityName != null)
                    {
                        users.CityName = request.CityName;
                    }
                    if (request.StateName != null)
                    {
                        users.StateName = request.StateName;
                    }
                    _userRepository.Update(users);
                    await _unitOfWork.Save(cancellationToken);
                    response.IsSuccess = true;
                }
            }
        }
        

        catch (BadRequestException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong, please try again");

        }
        return response;
    }
}