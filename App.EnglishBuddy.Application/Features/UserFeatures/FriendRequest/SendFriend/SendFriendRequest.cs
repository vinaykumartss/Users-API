using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FriendRequest.SendFriend
{
    public class SendFriendRequest : IRequest<SendFriendResponse>
    {
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }
       
    }
}