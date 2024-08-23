﻿namespace App.EventManagement.Application.Features.UserFeatures.LoginByUserName;

public sealed record LoginByUserNameResponse
{
    public string? Mobile { get; set; }
    public bool IsSuccess { get; set; }

    public Guid? UserId { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public bool? IsOtpVerify { get; set; }
    public string? Token { get; set; }

}