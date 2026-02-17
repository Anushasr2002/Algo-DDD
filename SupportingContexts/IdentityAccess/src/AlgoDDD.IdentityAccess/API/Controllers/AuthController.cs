using AlgoDDD.IdentityAccess.Application.Results;
using LoginResult = AlgoDDD.IdentityAccess.Application.Results.LoginResult;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AlgoDDD.IdentityAccess.Application.Commands;
using System.Threading.Tasks;


namespace AlgoDDD.IdentityAccess.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterUserResult>> Register(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}


