using App.EnglishBuddy.Application.Features.UserFeatures.OtpTemplate;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;

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
            Domain.Entities.ContactUs contactUs = new()
            {
                FirstName = request.FirstName,
                Mobile= request.Mobile,
                LastName= request.LastName,
                EmailAdress = request.EmailAddress,
                Question = request.Question
            };
            _iContactUsRepository.Update(contactUs);
            await _unitOfWork.Save(cancellationToken);
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