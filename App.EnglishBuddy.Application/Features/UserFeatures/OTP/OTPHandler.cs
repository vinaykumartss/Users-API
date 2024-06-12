using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Application.Services;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.OTP;

public sealed class OTPHandler : IRequestHandler<OTPRequest, OTPResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOTPServices _iOTPServices;
    private readonly IMapper _mapper;
    private readonly IUserRepository _iUserRepository;
    private readonly IOTPRepository _iOtpRepository;
  
    public OTPHandler(IUnitOfWork unitOfWork,
        IOTPServices iOTPServices,
        IUserRepository iUserRepository,
        IMapper mapper,
        IOTPRepository iOtpRepository)
    {
        _unitOfWork = unitOfWork;
        _iOTPServices = iOTPServices;
        _mapper = mapper;
        _iUserRepository = iUserRepository;
        _iOtpRepository = iOtpRepository;

    }

    public async Task<OTPResponse> Handle(OTPRequest request, CancellationToken cancellationToken)
    {
        OTPResponse otp = new OTPResponse();
        try
        {
            string otpResponse = "1234"; //await _iOTPServices.SendOTP(request.Mobile, request.Code);
            Users user = await _iUserRepository.FindByUserId(x => x.Email == request.Email, cancellationToken);

            if (user != null)
            {
                Otp otps = await _iOtpRepository.FindByUserId(x => x.UserId == user.Id, cancellationToken);
                if (otps != null)
                {
                    otps.OTP = otpResponse;
                    otps.CreatedDate = DateTime.UtcNow;
                    otps.UpdateDate = DateTime.UtcNow;
                    _iOtpRepository.Update(otps);
                }
                else
                {
                    Otp otpCreate = new Otp()
                    {
                        UserId = user.Id,
                        OTP = otpResponse,
                        Type = "1",
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    _iOtpRepository.Create(otpCreate);
                }
                await _unitOfWork.Save(cancellationToken);

            }else
            {
                throw new BadRequestException("Mobile does not exist; please complete the registration.");
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
        return otp;
    }
}