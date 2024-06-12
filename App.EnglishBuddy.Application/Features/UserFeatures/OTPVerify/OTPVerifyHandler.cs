using App.EnglishBuddy.Application.Common.AppMessage;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Sentry;

namespace App.EnglishBuddy.Application.Features.UserFeatures.OTP;

public sealed class OTPVerifyHandler : IRequestHandler<OTPVerifyRequest, OTPVerifyResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOTPRepository _iOtpRepository;
    private readonly IUserRepository _iUserRepository;
    public OTPVerifyHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IOTPRepository iOtpRepository, IUserRepository iUserRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iOtpRepository = iOtpRepository;
        _iUserRepository = iUserRepository;
    }

    public async Task<OTPVerifyResponse> Handle(OTPVerifyRequest request, CancellationToken cancellationToken)
    {
        OTPVerifyResponse otp = new OTPVerifyResponse();
        otp.Mobile = request.Email;
        Users user = await _iUserRepository.FindByUserId(x => x.Email == request.Email, cancellationToken);
        if (user !=null)
        {
            //Otp otps = await _iOtpRepository.FindByUserId(x => x.UserId == user.Id & x.OTP == request.OTP, cancellationToken); // TODO
            Otp otps = await _iOtpRepository.FindByUserId(x => x.UserId == user.Id, cancellationToken);
            if (otps != null)
            {
                DateTime dateTime= otps.UpdateDate.HasValue ? otps.UpdateDate.Value : DateTime.UtcNow;
                double result = DateTime.UtcNow.Subtract(dateTime).TotalMinutes;
                
                    otp.IsSuccess = true;
                   
                    Users userInfo = await _iUserRepository.FindByUserId(x => x.Id == otps.UserId, cancellationToken);
                    if (userInfo != null)
                    {
                        otp.EmailId = userInfo.Email;
                        otp.FullName = $"{userInfo.FirstName} {userInfo.LastName}";
                        otp.Mobile = userInfo.Mobile;
                        otp.UserId = otps.UserId;
                    }
                    user.IsOtpVerify = true;
                    _iUserRepository.Update(user);
               
    
            } else
            {
                throw new Exception(AppErrorMessage.OtpInvalid);
            }
        }
        return otp;
    }
}