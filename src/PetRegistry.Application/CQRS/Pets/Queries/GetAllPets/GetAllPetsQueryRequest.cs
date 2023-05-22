using System.Diagnostics.CodeAnalysis;
using MediatR;
using PetRegistry.Application.Results;

namespace PetRegistry.Application.Queries.Pets.GetAllPets
{

    [ExcludeFromCodeCoverage]
    public class GetAllPetsQueryRequest : IRequest<DefaultResult<IEnumerable<GetAllPetsQueryResponse>>>
    { 
    }
}
