using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Domain.Entities.Identifier;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;
using ETicaretAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using ETicaretAPI.Application.Exceptions;

namespace ETicaretAPI.Persistance.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<Domain.Entities.Identifier.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler; 
        readonly IConfiguration _configuration;
        readonly SignInManager<Domain.Entities.Identifier.AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
            _signInManager = signInManager;
        }


        public async Task<Token> GoogleLoginAsync(string idToken,int accessTokenLifeTime)
        {


            var settings = new GoogleJsonWebSignature.ValidationSettings()

            {
                Audience = new List<string> {_configuration["ExternalLoginSettings:Google:Client_ID"] }
            };


            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            Domain.Entities.Identifier.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, payload.Name, payload.Email, info, accessTokenLifeTime);


          
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identifier.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new UserNotFoundException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);

               return token;    
            }

            throw new Exception();
        }





        async Task<Token> CreateUserExternalAsync(AppUser user, string name, string email, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        FullName = name
                    };

                    var identityResult = await _userManager.CreateAsync(user);

                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);

                return token;
            }
            else
                throw new Exception("Invalid external auth");
        }

       
    }
}
