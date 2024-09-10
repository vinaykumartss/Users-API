using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;

public sealed class AccessTokenHandler : IRequestHandler<AccessTokenRequest, AccessTokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AccessTokenHandler> _logger;
    private readonly IMediator _mediator;
    public AccessTokenHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,
        IMapper mapper, ILogger<AccessTokenHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<AccessTokenResponse> Handle(AccessTokenRequest request, CancellationToken cancellationToken)
    {
        AccessTokenResponse response = new AccessTokenResponse();
        try
        {
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MailTempate/AccessToken.json");
            string scopes = "https://www.googleapis.com/auth/firebase.messaging";
            var bearertoken = string.Empty;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                bearertoken = GoogleCredential
                  .FromStream(stream) // Loads key file
                  .CreateScoped(scopes) // Gathers scopes requested
                  .UnderlyingCredential // Gets the credentials
                  .GetAccessTokenForRequestAsync().Result; // Gets the Access Token
            }
            response.AccessToken = bearertoken;
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw new Exception(ex.Message);
        }
        
        return response;
    }
}