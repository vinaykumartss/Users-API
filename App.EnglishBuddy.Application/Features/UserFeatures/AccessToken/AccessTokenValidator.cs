﻿using App.EnglishBuddy.Application.Common.AppMessage;
using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;

public sealed class AccessTokenValidator : AbstractValidator<AccessTokenRequest>
{
    public AccessTokenValidator()
    {
        
    }
}