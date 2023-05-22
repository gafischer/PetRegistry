using AutoMapper;
using MediatR;
using PetRegistry.Application.Results;
using PetRegistry.Domain.Interfaces;

namespace PetRegistry.Application.Commands.Pets.UpdatePet
{
    public class UpdatePetCommandHandler :
        IRequestHandler<UpdatePetCommandRequest, DefaultResult<UpdatePetCommandResponse>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public UpdatePetCommandHandler(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<DefaultResult<UpdatePetCommandResponse>> Handle(
            UpdatePetCommandRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var requestValidation = IsRequestValid(request);

                if (!requestValidation.Valid)
                {
                    return new DefaultResult<UpdatePetCommandResponse>(requestValidation.Errors);
                }

                var pet = await _petRepository.GetByIdAsync(request.Id);

                if (pet == null)
                {
                    return new DefaultResult<UpdatePetCommandResponse>(new List<string> { $"Pet with Id {request.Id} not found" });
                }

                _mapper.Map(request, pet);

                if (!pet.Neutered && pet.NeuterDate != null)
                {
                    pet.NeuterDate = null;
                }

                await _petRepository.UpdateAsync(pet);

                return new DefaultResult<UpdatePetCommandResponse>(true);
            }
            catch (Exception ex)
            {
                return new DefaultResult<UpdatePetCommandResponse>(new List<string> { ex.Message });
            }
        }

        private static ValidationResult IsRequestValid(UpdatePetCommandRequest request)
        {
            var errors = new List<string>();

            if (request.Neutered && request.NeuterDate == null)
            {
                errors.Add("Neutered pet must have NeuterDate");
            }

            if (errors.Count > 0)
            {
                return new ValidationResult(false, errors);
            }

            return new ValidationResult(true);
        }
    }
}
