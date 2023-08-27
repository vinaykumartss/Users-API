using App.EnglishBuddy.Application.Features.UserFeatures.ContactUs;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUser;
using App.EnglishBuddy.Application.Features.UserFeatures.UsersImages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("contact")]
    public class ContactUsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactUsController(IMediator mediator)
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