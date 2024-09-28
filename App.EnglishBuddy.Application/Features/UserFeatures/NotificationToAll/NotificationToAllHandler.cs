using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.NotificationToAllHandler;

public sealed class NotificationToAllHandler : IRequestHandler<NotificationToAllRequest, NotificationToAllResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<NotificationToAllHandler> _logger;
    private readonly IMediator _mediator;
    public NotificationToAllHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<NotificationToAllHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<NotificationToAllResponse> Handle(NotificationToAllRequest request, CancellationToken cancellationToken)
    {
        NotificationToAllResponse response = new NotificationToAllResponse();
        try
        {
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MailTempate/AccessToken.json");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(fileName),
            });

            // Send notification to all devices
            await SendNotificationToAllDevices("Reload", "MeetingScreen");

            response.AccessToken = "";
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw new Exception(ex.Message);
        }
        
        return response;
    }

    private static async Task SendNotificationToAllDevices(string title, string message)
    {
        var notification = new Notification
        {
            Title = title,
            Body = message,
        };

        var messageToSend = new Message()
        {
            Notification = notification,
            Topic = "all", // Make sure devices are subscribed to this topic
        };

        // Send a message to devices subscribed to the provided topic.
        string response = await FirebaseMessaging.DefaultInstance.SendAsync(messageToSend);
        Console.WriteLine("Successfully sent message: " + response);
    }
}