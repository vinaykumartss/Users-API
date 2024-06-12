using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Common.Mail;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

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
            Domain.Entities.Users users = await _userRepository.FindByUserId(x => x.Email == request.Email, cancellationToken);
            if (users == null)
            {
                var user = _mapper.Map<Users>(request);
                user.IsOtpVerify = true;
                _userRepository.Create(user);
                await _unitOfWork.Save(cancellationToken);
                response.IsSuccess = true;
                response.Id = user.Id;

                //var fileContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MailTempate/OtpTemplate.html"));
                //fileContents = fileContents.Replace("/", @"\");
                //string name = request.FirstName + " " + request?.LastName;
                //fileContents = fileContents.Replace("@Name", name);
                //fileContents = fileContents.Replace("@Mobile", "Test");
                //fileContents = fileContents.Replace("@Question", "Test");
                //fileContents = fileContents.Replace("@Email", "Test");
                //fileContents = fileContents.Replace("@Comapnay", "India.com");
                //SendMail.SendEmail(fileContents);
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