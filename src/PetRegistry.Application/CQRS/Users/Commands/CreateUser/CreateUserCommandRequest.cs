using MediatR;
using PetRegistry.Application.Results;

namespace PetRegistry.Application.CQRS.Users.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<DefaultResult<CreateUserCommandResponse>>
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
