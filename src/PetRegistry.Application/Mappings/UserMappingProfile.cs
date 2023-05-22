using AutoMapper;
using PetRegistry.Application.CQRS.Users.Commands.CreateUser;
using PetRegistry.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PetRegistry.Application.Mappings
{
    [ExcludeFromCodeCoverage]
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, CreateUserCommandRequest>().ReverseMap();
            CreateMap<User, CreateUserCommandResponse>().ReverseMap();
        }
    }
}
