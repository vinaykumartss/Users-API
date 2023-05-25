using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallList
{
    public class CallsDetailsResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? TotalMinutes { get; set; }
        public DateTime? Date { get; set; }
    }
}
