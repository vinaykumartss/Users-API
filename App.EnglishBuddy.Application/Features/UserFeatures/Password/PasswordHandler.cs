using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
namespace App.EnglishBuddy.Application.Features.UserFeatures.Password;

public sealed class PasswordHandler : IRequestHandler<PasswordRequest, PasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public PasswordHandler(IUnitOfWork unitOfWork,
       IUserRepository userRepository,
        IMapper mapper
        )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PasswordResponse> Handle(PasswordRequest request, CancellationToken cancellationToken)
    {
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
                }
                
            } else {
                throw new Exception("User is not found, Please try with correct user");
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }
        return response;
    }
}