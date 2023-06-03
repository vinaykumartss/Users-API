using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Features.UserFeatures.CashFreePayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("cashfree")]
    public class CashFreePayment : ControllerBase
    {
        private readonly IMediator _mediator;

        public CashFreePayment(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("order")]
        public async Task<ActionResult<CashFreePaymentResponse>> FindUser(CashFreePaymentRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

       
    }
}