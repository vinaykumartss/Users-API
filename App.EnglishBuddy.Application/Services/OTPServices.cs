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
            for (int i = 0; i < 5; i++)
            {
                int tempval = random.Next(0, numbers.Length);
                otp += tempval;
            }
            return otp;
        }

        public async Task<string> SendOTP(string? mobileNumber, int code)
        {
            string otp = GenerateOtp();
            string otpUrls = string.Format(AppUrls.MessageAPI, mobileNumber, code, otp);
            string message  = await Utility.CallAPIsAsync(otpUrls, MethodType.Get.ToString());
            return otp;
        }
    }
}
