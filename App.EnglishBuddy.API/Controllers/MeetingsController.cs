using App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingsUsers;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUserRating;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers;
using App.EnglishBuddy.Application.Features.UserFeatures.UserRating;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("meetings")]
    public class MeetingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MeetingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAll/{isActive}")]
        public async Task<ActionResult<List<GetAllMeetingsResponse>>> GetAll(bool isActive,CancellationToken cancellationToken)
        {
            GetAllMeetingsRequest request = new GetAllMeetingsRequest()
            {
                IsActive = isActive,
            };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("save")]
        public async Task<ActionResult<SaveMeetingsResponse>> Create(SaveMeetingsRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("meetingusers")]
        public async Task<ActionResult<SaveMeetingsResponse>> Create(SaveMeetingsUsersRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("meetingusers")]
        public async Task<ActionResult<SaveMeetingsResponse>> Get(GetMeetingsUsersRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}