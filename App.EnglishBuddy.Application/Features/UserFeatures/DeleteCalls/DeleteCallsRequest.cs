using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser
{

    public class DeleteCallsRequest : IRequest<DeleteCallsResponse>
    {
      
        public bool IsDeleted { get; set; }
      
    }
}