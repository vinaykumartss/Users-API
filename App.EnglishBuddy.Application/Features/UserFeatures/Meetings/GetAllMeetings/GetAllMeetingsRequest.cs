using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings
{
    public class GetAllMeetingsRequest : IRequest<List<GetAllMeetingsResponse>>
    {
       public Boolean IsActive { get; set; }
      
    }
}