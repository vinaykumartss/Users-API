using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
namespace App.EnglishBuddy.Application.Features.UserFeatures.LoginByUserName;

public sealed class LoginByUserNameHandler : IRequestHandler<LoginByUserNameRequest, LoginByUserNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public LoginByUserNameHandler(IUnitOfWork unitOfWork,
       IUserRepository userRepository,
        IMapper mapper
        )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LoginByUserNameResponse> Handle(LoginByUserNameRequest request, CancellationToken cancellationToken)
    {

        LoginByUserNameResponse response = new LoginByUserNameResponse();
        var paasword = await _userRepository.FindByUserId(x => x.Email == request.Login, cancellationToken);
        try
        {
            if (paasword != null && paasword.Password == request.Password)
            {
                response.IsSuccess = true;
            } else {
                throw new Exception("User is not found");
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