using App.EnglishBuddy.Application.Features.UserFeatures.OtpTemplate;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ContactUs;

public sealed class ContactUsHandler : IRequestHandler<ContactUsRequest, ContactUsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContactUsRepository _iContactUsRepository;
    private readonly IMapper _mapper;
     private readonly ILogger<ContactUsHandler> _logger;
    public ContactUsHandler(IUnitOfWork unitOfWork,
        IContactUsRepository iContactUsRepository,
        IMapper mapper,
        ILogger<ContactUsHandler> logger
        )
    {
        _unitOfWork = unitOfWork;
        _iContactUsRepository = iContactUsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ContactUsResponse> Handle(ContactUsRequest request, CancellationToken cancellationToken)
    {
         _logger.LogDebug($"Statring method {nameof(Handle)}");
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
             _logger.LogDebug($"Ending method {nameof(Handle)}");
        }
        catch (Exception ex)
        {
               _logger.LogError(ex.Message);
            response.IsSuccess = false;
            throw;
        }

        return response;
    }
}