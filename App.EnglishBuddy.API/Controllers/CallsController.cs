using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("calls")]
    public class CallsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CallsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("find")]
        public async Task<ActionResult<CallUsersResponse>> FindUser(CallUsersRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<CallUsersResponse>> Delete(DeleteCallsRequest request,
          CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

    }
}