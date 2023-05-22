using System.Diagnostics.CodeAnalysis;
using MediatR;
using PetRegistry.Application.Results;

namespace PetRegistry.Application.Queries.Pets.GetPetById
{

    [ExcludeFromCodeCoverage]
    public class GetPetByIdQueryRequest : IRequest<DefaultResult<GetPetByIdQueryResponse>>
    {
        public int Id { get; }

        public GetPetByIdQueryRequest()
        { 
        }

        public GetPetByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
