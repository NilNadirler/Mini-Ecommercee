using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Entities.Identifier;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {

        readonly UserManager<Domain.Entities.Identifier.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identifier.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result= await _userManager.CreateAsync(new Domain.Entities.Identifier.AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,    
                Email = request.Email,  
                FullName = request.FullName,
               
            }, request.Password);

            CreateUserCommandResponse response = new()
            {
                Succeeded = result.Succeeded
            };

            if (result.Succeeded)
                response.Message = "Success";

            else
            {
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}<br>";
            }
               


            return response;
            //throw new UserCreateFailedException();
        }
    }
}
