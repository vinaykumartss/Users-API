using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FriendRequest.GetAllFriend
{
    public class GetAllFriendRequest : IRequest<GetAllFriendResponse>
    {
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }
       
    }
}