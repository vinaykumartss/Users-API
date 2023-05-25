using App.EnglishBuddy.Domain.Entities;
using MediatR;
using System.Linq.Dynamic.Core;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallList
{
    public class CallListRequest : IRequest<CallListResponse>
    {
        public Guid Id { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}