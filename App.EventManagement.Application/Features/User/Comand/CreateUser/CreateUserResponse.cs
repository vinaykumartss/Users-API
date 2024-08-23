namespace App.EventManagement.Application.Features.Comand.CreateUser;

public sealed record CreateUserResponse
{
    public Guid Id { get; set; }
    public bool IsSuccess { get; set; }

    public bool IsOtpVerify { get; set; }

    public string Message { get; set; }
}



