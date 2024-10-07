using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FriendRequest.AcceptFriend
{
    public class AcceptFriendRequest : IRequest<AcceptFriendResponse>
    {
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }

        public bool IsStatus { get; set; }

    }
}