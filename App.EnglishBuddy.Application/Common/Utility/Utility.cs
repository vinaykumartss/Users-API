using App.EnglishBuddy.Application.Common.AppMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Common.Utility
{
    public class Utility
    {

        public static string GenerateOtp()
        {
            string numbers = AppConstant.Numbers;
            Random random = new Random();
            string otp = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                int tempval = random.Next(0, numbers.Length);
                otp += tempval;
            }
            return otp;
        }

    }
}
