using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateProfile;

public sealed class UpdateProfileHandler : IRequestHandler<UpdateProfileRequest, UpdateProfileResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProfileHandler> _logger;
    private readonly IMediator _mediator;
    public UpdateProfileHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<UpdateProfileHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<UpdateProfileResponse> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        UpdateProfileResponse response = new UpdateProfileResponse();
        try
        {
            Domain.Entities.Users users = await _userRepository.FindByUserId(x => x.Id == request.Id, cancellationToken);
            if (users != null)
            {
                users.StateName = request.State;
                users.CityName = request.City;
                users.FirstName = request.FirstName;
                users.LastName = request.LastName;
                users.CityId = request.CityId;
                users.StateId = request.StateId;
                _userRepository.Update(users);
                await _unitOfWork.Save(cancellationToken);
                response.Id = users.Id;
                response.IsSuccess = true;
                
            }
            else
            {
                throw new BadRequestException("User does not exist, please try agin");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong, please try again");

        }
        return response;
    }
}