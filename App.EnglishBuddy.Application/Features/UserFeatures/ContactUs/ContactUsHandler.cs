using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using App.EnglishBuddy.Domain.Entities;
using Sentry;
using System.Linq.Dynamic.Core.Tokenizer;
using App.EnglishBuddy.Application.Common.Mail;
using System.Text.RegularExpressions;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ContactUs;

public sealed class ContactUsHandler : IRequestHandler<ContactUsRequest, ContactUsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContactUsRepository _iContactUsRepository;
    private readonly IMapper _mapper;

    public ContactUsHandler(IUnitOfWork unitOfWork,
        IContactUsRepository iContactUsRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _iContactUsRepository = iContactUsRepository;
        _mapper = mapper;

    }

    public async Task<ContactUsResponse> Handle(ContactUsRequest request, CancellationToken cancellationToken)
    {
        ContactUsResponse response = new ContactUsResponse();
        try
        {
            Domain.Entities.ContactUs contactUs = new Domain.Entities.ContactUs()
            {
                FirstName = request.FirstName,
                Mobile= request.Mobile,
                LastName= request.LastName,
                EmailAdress = request.EmailAddress,
                Question = request.Question
            };
            _iContactUsRepository.Update(contactUs);
            await _unitOfWork.Save(cancellationToken);
            var fileContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MailTempate/ContactUs.html"));
            fileContents = fileContents.Replace("/", @"\");
            string name = request.FirstName + " " + request?.LastName;
            fileContents = fileContents.Replace("@Name", name);
            fileContents = fileContents.Replace("@Mobile", request?.Mobile);
            fileContents = fileContents.Replace("@Question", request?.Question);
            fileContents = fileContents.Replace("@Email", request.EmailAddress);
            fileContents = fileContents.Replace("@Comapnay", "India.com");
            SendMail.SendEmail(fileContents);
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }

        return response;
    }
}