using AutoMapper;
using MediatR;
using PetRegistry.Application.Results;
using PetRegistry.Domain.Entities;
using PetRegistry.Domain.Interfaces;

namespace PetRegistry.Application.Commands.Pets.CreatePet
{
    public class CreatePetCommandHandler :
        IRequestHandler<CreatePetCommandRequest, DefaultResult<CreatePetCommandResponse>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public CreatePetCommandHandler(
            IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<DefaultResult<CreatePetCommandResponse>> Handle(
            CreatePetCommandRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var requestValidation = IsRequestValid(request);

                if (!requestValidation.Valid)
                {
                    return new DefaultResult<CreatePetCommandResponse>(requestValidation.Errors);
                }

                var petEntity = _mapper.Map<Pet>(request);

                var newPet = await _petRepository.AddAsync(petEntity);

                var petResponse = _mapper.Map<CreatePetCommandResponse>(newPet);

                return new DefaultResult<CreatePetCommandResponse>(petResponse);
            }
            catch (Exception ex)
            {
                return new DefaultResult<CreatePetCommandResponse>(ex.Message);
            }
        }

        private static ValidationResult IsRequestValid(CreatePetCommandRequest request)
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
