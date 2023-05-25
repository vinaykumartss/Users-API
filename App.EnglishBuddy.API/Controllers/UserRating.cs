using App.EnglishBuddy.Application.Features.UserFeatures.GetUserRating;
using App.EnglishBuddy.Application.Features.UserFeatures.UserRating;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("rating")]
    public class UserRatingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserRatingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetAll")]
        public async Task<ActionResult<List<GetUserRatingResponse>>> GetAll(GetUserRatingRequest request,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("save")]
        public async Task<ActionResult<UserRatingsResponse>> Create(UserRatingsRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}