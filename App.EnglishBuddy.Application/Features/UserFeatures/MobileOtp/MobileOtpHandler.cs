using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Common.Mail;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Application.Services;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.MobileOtp;

public sealed class MobileOtpHandler : IRequestHandler<MobileOtpRequest, MobileOtpResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOTPServices _iOTPServices;
    private readonly IMapper _mapper;
    private readonly IUserRepository _iUserRepository;
    private readonly IOTPRepository _iOtpRepository;
  
    public MobileOtpHandler(IUnitOfWork unitOfWork,
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

    public async Task<MobileOtpResponse> Handle(MobileOtpRequest request, CancellationToken cancellationToken)
    {
        MobileOtpResponse otp = new MobileOtpResponse();
        try
        {
            string otpRespons =  _iOTPServices.GenerateOtp();
            Users user = await _iUserRepository.FindByUserId(x => x.Mobile.ToLower() == request.Mobile.ToLower(), cancellationToken);
            if (user == null)
            {
                Users otpCreate = new Users()
                {
                    Mobile = request.Mobile,
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Createdby = user?.Id,
                    Updatedby = user?.Id,
                    IsActive = true,
                    Gender = null
                };
               _iUserRepository.Create(otpCreate);
                await _unitOfWork.Save(cancellationToken);
            }
            Users checkUser = await _iUserRepository.FindByUserId(x => x.Mobile.ToLower() == request.Mobile.ToLower(), cancellationToken);
            if (checkUser != null)
            {
                List<Otp> otps = await _iOtpRepository.FindByCondition(x => x.UserId == checkUser.Id, cancellationToken);
                if(otps != null && otps.Count>0)
                {
                    foreach(var x in otps)
                    {
                        _iOtpRepository.Delete(x);
                        await _unitOfWork.Save(cancellationToken);
                    }
                }

                Otp otpCreate = new Otp()
                {
                    UserId = checkUser.Id,
                    OTP = otpRespons,
                    Type = "1",
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Createdby = checkUser.Id ,
                    Updatedby = checkUser.Id ,
                    IsActive=true
                };
                _iOtpRepository.Create(otpCreate);
                await _unitOfWork.Save(cancellationToken);

                otp.Otp = otpRespons;
                otp.IsSuccess = true;

               SendMail.SendMobile(request.Mobile, otpRespons);
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