using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetRegistry.API.Controllers.Base;
using PetRegistry.Application.CQRS.Users.Commands.CreateUser;
using PetRegistry.Application.CQRS.Users.Commands.SignIn;
using PetRegistry.Application.CQRS.Users.Commands.VerifyCode;
using PetRegistry.Domain.Entities;

namespace PetRegistry.API.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ILogger<UserController> logger, IMediator mediator) : base(logger, mediator)
        {
            /* 
             * TODO Routes
             * register
             * verify code 
             * signin
             * forgot password
             * validate reset password token
             * change password
             * resend verify code
             * remove user lockout
             * get user by id
             */
        }

        [HttpPost("Register", Name = "CreateUser")]
        [ProducesResponseType(typeof(CreateUserCommandResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest createUserCommandRequest)
        {
            var createUserCommandResponse = await _mediator.Send(createUserCommandRequest);

            if (!createUserCommandResponse.Success)
            {
                return BadRequest(createUserCommandResponse);
            }

            return Created("", createUserCommandResponse);
        }

        [HttpPost("VerifyCode", Name = "VerifyCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeUserCommandRequest verifyCodeUserCommandRequest)
        {
            var verifyCodeUserCommandResponse = await _mediator.Send(verifyCodeUserCommandRequest);

            if (!verifyCodeUserCommandResponse.Success)
            {
                return BadRequest(verifyCodeUserCommandResponse);
            }

            return Ok();
        }

        [HttpPost("SignIn", Name = "SignIn")]
        [ProducesResponseType(typeof(SignInUserCommandResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignIn([FromBody] SignInUserCommandRequest signInUserCommandRequest)
        {
            var signInUserCommandResponse = await _mediator.Send(signInUserCommandRequest);

            if (!signInUserCommandResponse.Success)
            {
                return BadRequest(signInUserCommandResponse);
            }

            return Ok(signInUserCommandResponse);
        }
    }
}
