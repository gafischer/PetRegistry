using AutoMapper;
using PetRegistry.Application.Commands.Pets.CreatePet;
using PetRegistry.Application.Commands.Pets.UpdatePet;
using PetRegistry.Application.Queries.Pets.GetAllPets;
using PetRegistry.Application.Queries.Pets.GetPetById;
using PetRegistry.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PetRegistry.Application.Mappings
{
    [ExcludeFromCodeCoverage]
    public class PetMappingProfile : Profile
    {
        public PetMappingProfile()
        {
            CreateMap<Pet, CreatePetCommandRequest>().ReverseMap();
            CreateMap<Pet, CreatePetCommandResponse>().ReverseMap();

            CreateMap<Pet, UpdatePetCommandRequest>().ReverseMap();
            CreateMap<Pet, UpdatePetCommandResponse>().ReverseMap();

            CreateMap<Pet, GetPetByIdQueryResponse>().ReverseMap();

            CreateMap<Pet, GetAllPetsQueryResponse>().ReverseMap();
        }
    }
}
