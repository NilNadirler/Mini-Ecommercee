using ETicaretAPI.Application.Features.Commands.CreateUser;
using ETicaretAPI.Application.Features.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);

            return Ok(response);    
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommanRequest loginUserCommanRequest)     
        {

            LoginUserCommandResponse response = await _mediator.Send(loginUserCommanRequest);
            return Ok(response);
        }
    }
}
