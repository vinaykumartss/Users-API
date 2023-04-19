using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers
{

    public class CallUsersRequest : IRequest<CallUsersResponse>
    {
        public string? UserId { get; set; }
        public string? OpponentUserId { get; set; }

        public Guid? SessionId { get; set; }
        public bool IsCallIntiator { get; set; }

       
    }
}