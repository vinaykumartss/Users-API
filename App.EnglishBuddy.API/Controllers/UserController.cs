using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUserImage;
using App.EnglishBuddy.Application.Features.UserFeatures.LoginByUserName;
using App.EnglishBuddy.Application.Features.UserFeatures.Password;
using App.EnglishBuddy.Application.Features.UserFeatures.UpdateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.UsersImages;
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

        [HttpGet]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUserRequest(), cancellationToken);
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
        public async Task<ActionResult<CreateUserResponse>> GetImage( Guid userId,
           CancellationToken cancellationToken)
        {
            GetUserImageRequest file = new GetUserImageRequest()
            {
                UserId = userId
            };
            var response = await _mediator.Send(file, cancellationToken);
            return Ok(response);
        }

        [HttpGet("/{loginId}/{password}")]
        public async Task<ActionResult<LoginByUserNameResponse>> LoginByUserName(string? loginId, string? password, 
          CancellationToken cancellationToken)
        {
            LoginByUserNameRequest file = new LoginByUserNameRequest()
            {
                Password = password,
                Login   = loginId
            };
            var response = await _mediator.Send(file, cancellationToken);
            return Ok(response);
        }

        [HttpPut("setPassword/{loginId}")]
        public async Task<ActionResult<PasswordResponse>> SetPassword(PasswordRequest request,
          CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<UpdateUserResponse>> Update(UpdateUserRequest request,
          CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}