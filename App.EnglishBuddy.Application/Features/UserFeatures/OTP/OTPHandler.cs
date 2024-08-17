using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Common.Mail;
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
            string otpRespons =  _iOTPServices.GenerateOtp();
            Users user = await _iUserRepository.FindByUserId(x => x.Email.ToLower() == request.Email.ToLower(), cancellationToken);

            if (user != null)
            {
                List<Otp> otps = await _iOtpRepository.FindByCondition(x => x.UserId == user.Id, cancellationToken);
                if(otps != null)
                {
                    foreach(var x in otps)
                    {
                        _iOtpRepository.Delete(x);
                        await _unitOfWork.Save(cancellationToken);
                    }
                }

                Otp otpCreate = new Otp()
                {
                    UserId = user.Id,
                    OTP = otpRespons,
                    Type = "1",
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Createdby = user.Id ,
                    Updatedby = user.Id ,
                    IsActive=true
                };
                _iOtpRepository.Create(otpCreate);
                await _unitOfWork.Save(cancellationToken);

                otp.Otp = otpRespons;
                otp.IsSuccess = true;

                if(request.IsResend)
                {
                    var fileContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MailTempate/OtpTemplate.html"));
                   
                    fileContents = fileContents.Replace("{otp}", otpRespons);
                    SendMail.SendEmail(fileContents, request.Email);
                }
            }
            else
            {
                throw new BadRequestException("EMail does not exist, Please try again");
            }
        }
        
        catch (Exception ex)
        {
            otp.IsSuccess = false;
            throw new Exception("Something went wrong, please try again");

        }
        return otp;
    }
}