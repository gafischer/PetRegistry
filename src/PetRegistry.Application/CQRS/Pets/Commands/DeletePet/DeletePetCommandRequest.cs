using MediatR;
using PetRegistry.Application.Results;

namespace PetRegistry.Application.CQRS.Pets.Commands.DeletePet
{
    public class DeletePetCommandRequest : IRequest<DefaultResult<DeletePetCommandResponse>>
    {
        public DeletePetCommandRequest()
        {
        }

        public int Id { get; set; }
    }
}
