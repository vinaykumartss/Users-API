using App.EventManagement.Application.Common.Token;
using App.EventManagement.Application.Repositories;
using AutoMapper;
using MediatR;
namespace App.EventManagement.Application.Features.UserFeatures.LoginByUserName;

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
        var userInfo = await _userRepository.FindByUserId(x => x.UserName.ToLower() == request.UserName.ToLower() && x.Password == request.Password, cancellationToken);
        try
        {
            if (userInfo != null)
            {
                response.Token = userInfo.UserName;
                response.FullName = $"{userInfo.FirstName} {userInfo.LastName}";
                response.Mobile = userInfo.Mobile;
                response.UserId = userInfo.Id;
                response.IsSuccess = true;
                response.IsOtpVerify = userInfo.IsOtpVerify;
                response.Token = Token.GenerateJwtToken(userInfo);
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