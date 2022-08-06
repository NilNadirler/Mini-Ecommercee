﻿using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Domain.Entities.Identifier;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance.Services
{
    internal class UserService : IUserService
    {

        readonly UserManager<Domain.Entities.Identifier.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new Domain.Entities.Identifier.AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,

            }, model.Password);

            CreateUserResponse response = new()
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
        }
    }
}
