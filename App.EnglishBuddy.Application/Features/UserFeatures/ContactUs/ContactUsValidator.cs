using App.EnglishBuddy.Application.Features.UserFeatures.OtpTemplate;
using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ContactUs;

public sealed class ContactUsValidator : AbstractValidator<ContactUsRequest>
{
    public ContactUsValidator()
    {
        
    }
}