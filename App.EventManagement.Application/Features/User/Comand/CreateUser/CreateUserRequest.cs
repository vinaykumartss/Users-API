﻿using MediatR;

namespace App.EventManagement.Application.Features.Comand.CreateUser
{
    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public Guid? Id { get; set; }
    }
}