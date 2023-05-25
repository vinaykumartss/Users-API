namespace App.EnglishBuddy.Application.Services
{
    public interface IOTPServices
    {
        string GenerateOtp();
        Task<string> SendOTP(string? mobileNumber, int code);
    }
}
