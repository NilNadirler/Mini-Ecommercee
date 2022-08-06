using ETicaretAPI.Application.Features.Commands.AppUser;
using ETicaretAPI.Application.Features.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommanRequest loginUserCommanRequest)
        {

            LoginUserCommandResponse response = await _mediator.Send(loginUserCommanRequest);
            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {

            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
    }
}
