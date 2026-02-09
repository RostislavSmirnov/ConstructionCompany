using Application.Features.AuthFeatures.Login;
using Application.Features.AuthFeatures.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionCompanyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            string token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            string? token = await _mediator.Send(command);
            if (token is null)
                return Unauthorized("Неверный логин или пароль");
            return Ok(new { Token = token });
        }
    }
}
