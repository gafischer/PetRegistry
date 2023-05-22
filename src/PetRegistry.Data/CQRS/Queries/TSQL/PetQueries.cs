using System.Diagnostics.CodeAnalysis;

namespace PetRegistry.Data.CQRS.Queries.TSQL
{
    [ExcludeFromCodeCoverage]
    public class PetQueries
    {
        public const string GetAllPets = @"
            SELECT  
                    ""Id"",
                    ""Name"",
                    ""Breed"",
                    ""Sex"",
                    ""Description"",
                    ""Neutered"",
                    ""NeuterDate"",
                    ""Specie"",
                    ""Weight"",
                    ""Color"",
                    ""BirthDate""
            FROM
                    ""Pets""
        ";

        public const string GetPetById = @"
            SELECT  
                    ""Id"",
                    ""Name"",
                    ""Breed"",
                    ""Sex"",
                    ""Description"",
                    ""Neutered"",
                    ""NeuterDate"",
                    ""Specie"",
                    ""Weight"",
                    ""Color"",
                    ""BirthDate""
            FROM
                    ""Pets""
            WHERE
                    ""Id"" = @Id
        ";
    }
}
