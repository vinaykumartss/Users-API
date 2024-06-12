using App.EnglishBuddy.Application.Features.UserFeatures.OtpTemplate;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUser;
using App.EnglishBuddy.Application.Features.UserFeatures.UsersImages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using App.EnglishBuddy.Application.Features.UserFeatures.ContactUs;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("contact")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost("create")]
        public async Task<ActionResult<ContactUsResponse>> Create(ContactUsRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        
    }
}