using App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingsUsers;
using App.EnglishBuddy.Application.Features.UserFeatures.MeetingInActive;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers;
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

        [HttpGet("getAll")]
        public async Task<ActionResult<List<GetAllMeetingsResponse>>> GetAll(CancellationToken cancellationToken)
        {
            GetAllMeetingsRequest request = new GetAllMeetingsRequest()
            {
                IsActive = true,
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

        [HttpGet("meetingusers/{meetingId}")]
        public async Task<ActionResult<SaveMeetingsResponse>> Get(Guid meetingId,
            CancellationToken cancellationToken)
        {
            GetMeetingsUsersRequest request = new GetMeetingsUsersRequest()
            {
                MeetingId = meetingId
            };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("leftmeeting")]
        public async Task<ActionResult<SaveMeetingsResponse>> InActive(MeetingInActiveRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}