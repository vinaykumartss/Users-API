using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Common.Mail;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ForgotPassword;

public sealed class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ForgotPasswordHandler> _logger;
    private readonly IMediator _mediator;
    public ForgotPasswordHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<ForgotPasswordHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        ForgotPasswordResponse response = new ForgotPasswordResponse();
        try
        {
            Domain.Entities.Users users = await _userRepository.FindByUserId(x => x.Email.ToLower() == request.Email.ToLower(), cancellationToken);
            if (users != null)
            {
                var fileContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MailTempate/OtpTemplate.html"));
                OTPRequest oTPRequest = new OTPRequest()
                {
                    Email = request.Email
                };
                var respnse = await _mediator.Send(oTPRequest);
                fileContents = fileContents.Replace("{otp}", respnse.Otp);
                SendMail.SendEmail(fileContents, request.Email);
                response.IsSuccess = true;
                response.Id = users.Id;
            }
            else
            {
                throw new BadRequestException("Email does not exist, please try agin");
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
        return response;
    }
}