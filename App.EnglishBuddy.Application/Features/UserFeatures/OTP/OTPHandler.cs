using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Application.Services;
using AutoMapper;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.OTP;

public sealed class OTPHandler : IRequestHandler<OTPRequest, OTPResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOTPServices _iOTPServices;
    private readonly IMapper _mapper;
    public OTPHandler(IUnitOfWork unitOfWork,
        IOTPServices iOTPServices,
        IUserRepository iUserRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _iOTPServices = iOTPServices;
        _mapper = mapper;
    }

    public async Task<OTPResponse> Handle(OTPRequest request, CancellationToken cancellationToken)
    {
        OTPResponse otp = new OTPResponse();
        await _iOTPServices.SendOTP(request.Mobile);
        return otp;
    }
}