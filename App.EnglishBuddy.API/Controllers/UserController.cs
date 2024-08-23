using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Features.UserFeatures.ForgotPassword;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUserImage;
using App.EnglishBuddy.Application.Features.UserFeatures.LoginByUserName;
using App.EnglishBuddy.Application.Features.UserFeatures.Password;
using App.EnglishBuddy.Application.Features.UserFeatures.UpdateProfile;
using App.EnglishBuddy.Application.Features.UserFeatures.UsersImages;
using App.EnglishBuddy.Application.Features.UserFeatures.AccessToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("list")]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAll(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CreateUserResponse>> Get(Guid id,
            CancellationToken cancellationToken)
        {
            GetUserRequest request = new GetUserRequest()
            {
                Id = id
            };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("/upload/{userId}")]
        public async Task<ActionResult<CreateUserResponse>> Upload(UsersImagesRequest file, Guid userId,
            CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(file, cancellationToken);
            return Ok(response);
        }

        [HttpGet("/upload/{userId}")]
        public async Task<ActionResult<CreateUserResponse>> GetImage(Guid userId,
           CancellationToken cancellationToken)
        {
            GetUserImageRequest file = new GetUserImageRequest()
            {
                UserId = userId
            };
            var response = await _mediator.Send(file, cancellationToken);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginByUserNameResponse>> LoginByUserName(LoginByUserNameRequest login,
          CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(login, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update-password")]
        public async Task<ActionResult<PasswordResponse>> SetPassword(PasswordRequest request,
          CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }



        [HttpPost("forgot-password")]
        public async Task<ActionResult<UpdateProfileResponse>> Update(ForgotPasswordRequest request,
          CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{id}/update-profile")]
        public async Task<ActionResult<UpdateProfileResponse>> UpdateProfile(Guid id, UpdateProfileRequest request,
          CancellationToken cancellationToken)
        {
            request.Id = id;
            UpdateProfileResponse response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("fcmtoken/{userid}")]
        public async Task<ActionResult<UpdateProfileResponse>> Vinay(Guid userid, FcmTokenRequest request,
        CancellationToken cancellationToken)
        {
            request.UserId = userid;
            FcmTokenResponse response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("accessToken")]
        public async Task<ActionResult<AccessTokenResponse>> SetPassword(
           CancellationToken cancellationToken)
        {
            AccessTokenRequest request= new AccessTokenRequestAccessTokenRequest();
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }


    }
}