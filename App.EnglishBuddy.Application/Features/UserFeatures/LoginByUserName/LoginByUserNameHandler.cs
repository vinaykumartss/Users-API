using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
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
            } else {
                throw new NotFoundException("User is not found");
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