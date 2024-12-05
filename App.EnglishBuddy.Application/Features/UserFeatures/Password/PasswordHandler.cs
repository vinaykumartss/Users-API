using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.Password;

public sealed class PasswordHandler : IRequestHandler<PasswordRequest, PasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PasswordHandler> _logger;
    public PasswordHandler(IUnitOfWork unitOfWork,
       IUserRepository userRepository,
        IMapper mapper,
        ILogger<PasswordHandler> logger
        )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PasswordResponse> Handle(PasswordRequest request, CancellationToken cancellationToken)
    {

        _logger.LogDebug($"Statring method {nameof(Handle)}");
        PasswordResponse response = new PasswordResponse();
        var paasword = await _userRepository.FindByUserId(x => x.Id == request.Id, cancellationToken);
        try
        {
            if (paasword != null)
            {
                if(request.IsForgotPassword)
                {
                    paasword.Password = request.NewPassword;
                    _userRepository.Update(paasword);
                    await _unitOfWork.Save(cancellationToken);
                    response.IsSuccess = true;
                } else if(paasword.Password == request.CurrentPassword)
                {
                    paasword.Password = request.NewPassword;
                    _userRepository.Update(paasword);
                    await _unitOfWork.Save(cancellationToken);
                    response.IsSuccess = true;
                }else
                {
                    throw new Exception("Current Password is not correct, Please try with correct Password");
                }
                
            } else {
                throw new Exception("User is not found,Please try with correct user");
            }
            _logger.LogDebug($"Ending method {nameof(Handle)}");
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