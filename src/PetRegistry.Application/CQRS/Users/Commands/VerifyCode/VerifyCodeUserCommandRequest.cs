using MediatR;
using PetRegistry.Application.Results;

namespace PetRegistry.Application.CQRS.Users.Commands.VerifyCode
{
    public class VerifyCodeUserCommandRequest : IRequest<DefaultResult<VerifyCodeUserCommandResponse>>
    {
        public string? Email { get; set; }
        public string? Code { get; set; }
    }
}
