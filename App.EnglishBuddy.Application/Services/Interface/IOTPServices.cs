using App.EnglishBuddy.Application.Common.AppMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Services
{
    public interface IOTPServices
    {
        string GenerateOtp();
        Task<string> SendOTP(string? mobileNumber);
    }
}
