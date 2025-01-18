using AutoMapper;
using VetTail.Application.Common.DTOs.Pets;
using VetTail.Web.Models.Pets;

namespace VetTail.Web.Profiles;

public class PetProfiles : Profile
{
    public PetProfiles()
    {
        CreateMap<CreatePetVM, CreatePetDto>();
        CreateMap<PetDto, UpdatePetVM>();
        CreateMap<UpdatePetVM, UpdatePetDto>();
    }
}
