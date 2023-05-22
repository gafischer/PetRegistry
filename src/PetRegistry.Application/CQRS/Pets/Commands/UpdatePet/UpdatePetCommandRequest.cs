using MediatR;
using PetRegistry.Application.CQRS.Pets;
using PetRegistry.Application.Results;
using PetRegistry.Shared;

namespace PetRegistry.Application.Commands.Pets.UpdatePet
{
    public class UpdatePetCommandRequest : PetDTO, IRequest<DefaultResult<UpdatePetCommandResponse>>
    {
        public UpdatePetCommandRequest()
        {
        }
    }
}
