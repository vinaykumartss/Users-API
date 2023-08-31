using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UsersImages
{

    public class UsersImagesRequest : IRequest<UsersImagesResponse>
    {
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Guid UserId { get; set; }
        
    }
}