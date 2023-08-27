using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Features.UserFeatures.RandomCallEnd;
using App.EnglishBuddy.Application.Features.UserFeatures.RandomCalls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("calls")]
    public class RandomCallsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RandomCallsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("finduser")]
        public async Task<ActionResult<RandomCallsResponse>> FindUser(RandomCallsRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("update/status")]
        public async Task<ActionResult<RandomCallEndResponse>> Status(RandomCallEndRequest request,
           CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}