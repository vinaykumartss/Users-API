using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.LoginByUserName;

public sealed class LoginByUserNameHandler : IRequestHandler<LoginByUserNameRequest, LoginByUserNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<LoginByUserNameHandler> _logger;
    public LoginByUserNameHandler(IUnitOfWork unitOfWork,
       IUserRepository userRepository,
        IMapper mapper,
         ILogger<LoginByUserNameHandler> logger
        )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LoginByUserNameResponse> Handle(LoginByUserNameRequest request, CancellationToken cancellationToken)
    {

        _logger.LogDebug($"Statring method {nameof(Handle)}");
        LoginByUserNameResponse response = new LoginByUserNameResponse();
        var userInfo = await _userRepository.FindByUserId(x => x.Email.ToLower() == request.Email.ToLower() && x.Password == request.Password, cancellationToken);
        try
        {
            if (userInfo != null)
            {
                response.EmailId = userInfo.Email;
                response.FullName = $"{userInfo.FirstName} {userInfo.LastName}";
                response.Mobile = userInfo.Mobile;
                response.UserId = userInfo.Id;
                response.IsSuccess = true;
                response.IsOtpVerify = userInfo.IsOtpVerify;
                response.FcmToken = userInfo.FcmToken;
                _logger.LogDebug($"Ending method {nameof(Handle)}");
            } else {
                throw new NotFoundException("User is not found");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            response.IsSuccess = false;
            throw;
        }

        return response;
    }
}