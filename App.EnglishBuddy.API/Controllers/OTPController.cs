using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Features.UserFeatures.MobileOtp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("otp")]
    public class OTPController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OTPController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send")]
        public async Task<ActionResult<OTPResponse>> Create(OTPRequest request,
            CancellationToken cancellationToken)
        {
            request.IsResend = true;
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("mobileOtp")]
        public async Task<ActionResult<MobileOtpResponse>> MobileOtp(MobileOtpRequest request,
            CancellationToken cancellationToken)
        {
            request.IsResend = true;
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        [HttpPost("verify")]
        public async Task<ActionResult<OTPResponse>> Verify(OTPVerifyRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}