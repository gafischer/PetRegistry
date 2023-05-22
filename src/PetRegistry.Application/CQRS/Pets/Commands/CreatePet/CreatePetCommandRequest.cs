using MediatR;
using PetRegistry.Application.CQRS.Pets;
using PetRegistry.Application.Results;

namespace PetRegistry.Application.Commands.Pets.CreatePet
{
    public class CreatePetCommandRequest : PetDTO, IRequest<DefaultResult<CreatePetCommandResponse>>
    {
    }
}
