using AutoMapper;
using MediatR;
using PetRegistry.Application.Results;
using PetRegistry.Domain.Interfaces;

namespace PetRegistry.Application.Queries.Pets.GetAllPets
{
    public class GetAllPetsQueryHandler :
        IRequestHandler<GetAllPetsQueryRequest, DefaultResult<IEnumerable<GetAllPetsQueryResponse>>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;


        public GetAllPetsQueryHandler(IPetRepository petQueryRepository, IMapper mapper)
        {
            _petRepository = petQueryRepository;
            _mapper = mapper;
        }

        public async Task<DefaultResult<IEnumerable<GetAllPetsQueryResponse>>> Handle(GetAllPetsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var pets = await _petRepository.GetAllAsync();
                var petResponse = _mapper.Map<IEnumerable<GetAllPetsQueryResponse>>(pets);

                return new DefaultResult<IEnumerable<GetAllPetsQueryResponse>>(petResponse);
            }
            catch (Exception ex)
            {
                return new DefaultResult<IEnumerable<GetAllPetsQueryResponse>>(new[] { ex.Message });
            }
        }
    }
}
