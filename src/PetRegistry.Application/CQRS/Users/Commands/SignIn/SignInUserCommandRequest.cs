using MediatR;
using PetRegistry.Application.Results;

namespace PetRegistry.Application.CQRS.Users.Commands.SignIn
{
    public class SignInUserCommandRequest : IRequest<DefaultResult<SignInUserCommandResponse>>
    {
        public string UsernameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
