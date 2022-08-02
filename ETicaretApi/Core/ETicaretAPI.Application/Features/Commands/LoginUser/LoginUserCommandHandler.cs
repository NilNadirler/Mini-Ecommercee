using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Entities.Identifier;
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

        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async  Task<LoginUserCommandResponse> Handle(LoginUserCommanRequest request, CancellationToken cancellationToken)
        {
           AppUser user =  await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if(user == null)
              user =  await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
                throw new UserNotFoundException();

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
               Token token = _tokenHandler.CreateAccessToken(5);

                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }

            return new LoginUserErrorCommandResponse()
            {
                Message = "Kullanici Adi hatali"
            };

        }
    }
}
