using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;

public sealed record GetAllUserRequest : IRequest<List<GetAllUserResponse>>;