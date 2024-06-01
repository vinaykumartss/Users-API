using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic.FileIO;
using RestSharp;
using Sentry;
using System.Security.Cryptography.X509Certificates;



namespace App.EnglishBuddy.Application.Features.UserFeatures.Password;

public sealed class PasswordHandler : IRequestHandler<PasswordRequest, PasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public PasswordHandler(IUnitOfWork unitOfWork,
       IUserRepository userRepository,
        IMapper mapper
        )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PasswordResponse> Handle(PasswordRequest request, CancellationToken cancellationToken)
    {
       
        PasswordResponse response = new PasswordResponse();
        var paasword = await _userRepository.FindByUserId(x => x.Email == request.Login, cancellationToken);
        try
        {
            if (paasword != null)
            {
                paasword.Password = request.Password;
                _userRepository.Update(paasword);
                await _unitOfWork.Save(cancellationToken);
            } else {
                throw new Exception("User is not found");
            }
            
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            throw;
        }

        return response;
    }
}