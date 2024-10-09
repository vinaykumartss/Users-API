using System.Net;
using System.Net.Mail;

namespace App.EnglishBuddy.Application.Common.Mail
{
    public  class SendMail
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string emailFromAddress = "vinay.rathore817@gmail.com"; //Sender Email Address  
        static string password = "hqexfhrxzllmoqtz"; //Sender Password  
        static string emailToAddress = "vinayrathore87@gmail.com"; //Receiver Email Address  
        static string subject = "Contact Us";
        static string body = "Hello, This is Email sending test using gmail.";

        public static void SendEmail(string request, string email)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = request;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
        public static async Task SendMobile(string mobile, string otp)
        {
            await Common.Utility.Utility.CallAPIsAsync($"https://2factor.in/API/V1/6da908fb-8491-11ef-8b17-0200cd936042/SMS/{mobile}/{otp}/OTP1", "Get");
        }
    }
}
