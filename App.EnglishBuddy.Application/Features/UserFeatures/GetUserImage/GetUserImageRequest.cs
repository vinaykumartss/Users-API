using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserImage
{

    public class GetUserImageRequest : IRequest<GetUserImageResponse>
    {
        public Guid UserId { get; set; }
      
    }
}