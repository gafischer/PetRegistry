using MediatR;
using PetRegistry.Application.Results;
using PetRegistry.Domain.Interfaces;

namespace PetRegistry.Application.CQRS.Pets.Commands.DeletePet
{
    internal class DeletePetCommandHandler :
        IRequestHandler<DeletePetCommandRequest, DefaultResult<DeletePetCommandResponse>>
    {
        private readonly IPetRepository _petRepository;

        public DeletePetCommandHandler(
            IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<DefaultResult<DeletePetCommandResponse>> Handle(
            DeletePetCommandRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var petEntity = await _petRepository.GetByIdAsync(request.Id);

                if (petEntity == null)
                {
                    return new DefaultResult<DeletePetCommandResponse>(new[] { "Pet not found" });
                }

                await _petRepository.DeleteAsync(petEntity);

                return new DefaultResult<DeletePetCommandResponse>(true);
            }
            catch (Exception ex)
            {
                return new DefaultResult<DeletePetCommandResponse>(new[] { ex.Message });
            }
        }
    }
}
