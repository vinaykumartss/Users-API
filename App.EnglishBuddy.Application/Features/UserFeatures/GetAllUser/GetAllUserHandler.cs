using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;

public sealed class GetAllUserHandler : IRequestHandler<GetAllUserRequest, List<GetAllUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AccessTokenHandler> _logger;
    public GetAllUserHandler(IUserRepository userRepository, IMapper mapper, ILogger<AccessTokenHandler> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        try
        {
            var users = await _userRepository.GetAllUsers(request, cancellationToken);
            _logger.LogDebug($"Ending method {nameof(Handle)}");
            return users;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}