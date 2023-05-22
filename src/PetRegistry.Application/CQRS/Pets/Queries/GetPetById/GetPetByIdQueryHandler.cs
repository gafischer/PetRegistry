using AutoMapper;
using MediatR;
using PetRegistry.Application.Results;
using PetRegistry.Domain.Interfaces;

namespace PetRegistry.Application.Queries.Pets.GetPetById
{
    public class GetPetByIdQueryHandler :
        IRequestHandler<GetPetByIdQueryRequest, DefaultResult<GetPetByIdQueryResponse>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public GetPetByIdQueryHandler(
            IPetRepository petQueryRepository,
            IMapper mapper)
        {
            _petRepository = petQueryRepository;
            _mapper = mapper;
        }

        public async Task<DefaultResult<GetPetByIdQueryResponse>> Handle(
            GetPetByIdQueryRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var pet = await _petRepository.GetByIdAsync(request.Id);

                if(pet == null)
                {
                    return new DefaultResult<GetPetByIdQueryResponse>(new[] { "Pet not found" });
                }

                var petResponse = _mapper.Map<GetPetByIdQueryResponse>(pet);

                return new DefaultResult<GetPetByIdQueryResponse>(petResponse);
            }
            catch (Exception ex)
            {
                return new DefaultResult<GetPetByIdQueryResponse>(new[] { ex.Message });
            }
        }
    }
}
