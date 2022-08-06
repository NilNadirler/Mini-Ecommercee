using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommanRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async  Task<LoginUserCommandResponse> Handle(LoginUserCommanRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 15);

            return new LoginUserCommandResponse()
            {
                Token = token
            };


        }
    }
}
