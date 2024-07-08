using App.EnglishBuddy.Application.Common.AppMessage;
using App.EnglishBuddy.Application.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Services
{
    public class OTPServices : IOTPServices
    {
        public  string GenerateOtp()
        {
            string numbers = AppConstant.Numbers;
            Random random = new Random();
            string otp = string.Empty;
            for (int i = 1; i <= 4; i++)
            {
                int tempval = random.Next(0, numbers.Length);
                otp += tempval;
            }
            return otp;
        }

        
    }
}
