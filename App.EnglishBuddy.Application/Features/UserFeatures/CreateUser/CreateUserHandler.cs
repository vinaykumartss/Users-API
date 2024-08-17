using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Common.Mail;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sentry;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IMediator _mediator;
    public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<CreateUserHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        CreateUserResponse response = new CreateUserResponse();
        try
        {
            Domain.Entities.Users users = await _userRepository.FindByUserId(x => x.Email.ToLower() == request.Email.ToLower(), cancellationToken);
            if (users == null)
            {

                var user = _mapper.Map<Users>(request);
                user.IsOtpVerify = false;
                _userRepository.Create(user);
                await _unitOfWork.Save(cancellationToken);
                response.IsSuccess = true;
                response.Id = user.Id;
                response.IsOtpVerify = false;

                var fileContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MailTempate/OtpTemplate.html"));
                OTPRequest oTPRequest = new OTPRequest()
                {
                    Email = request.Email
                };
                var respnse = await _mediator.Send(oTPRequest);
                fileContents = fileContents.Replace("{otp}", respnse.Otp);
                SendMail.SendEmail(fileContents, request.Email);

            }
            else if (users != null && users.IsOtpVerify == false)
            {
                _userRepository.Update(users);
                await _unitOfWork.Save(cancellationToken);

                var fileContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MailTempate/OtpTemplate.html"));
                OTPRequest oTPRequest = new OTPRequest()
                {
                    Email = request.Email
                };
                var respnse = await _mediator.Send(oTPRequest);
                fileContents = fileContents.Replace("{otp}", respnse.Otp);
                SendMail.SendEmail(fileContents, request.Email);
                users.Password = request.Password;
                response.IsOtpVerify = false;
                response.Message = "Otp has been sent to you registered mail Id, Please verify";
                response.IsSuccess = true;
            }
            else if (users != null && users.IsOtpVerify == true)
            {
                response.IsSuccess = true;
                response.IsOtpVerify = true;
                response.Message = "You are already registred login with this mail or do forgot password";
            }
            else
            {
                throw new BadRequestException("Email already exist, please try with other Email");
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