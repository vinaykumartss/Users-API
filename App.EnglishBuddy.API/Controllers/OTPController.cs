using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("OTP")]
    public class OTPController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OTPController(IMediator mediator)
        {
            _mediator = mediator;
        }

       

        [HttpGet("sendotp/{mobile}")]
        public async Task<ActionResult<OTPResponse>> Create(string mobile,
            CancellationToken cancellationToken)
        {
            OTPRequest request = new OTPRequest()
            {
                Mobile = mobile
            };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}