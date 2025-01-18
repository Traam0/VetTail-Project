using AutoMapper;
using VetTail.Application.Common.DTOs.Pets;
using VetTail.Domain.Entities;
using VetTail.Domain.ValueObjects;

namespace VetTail.Application.Common.Profiles;

public class PetProfiles : Profile
{
    public PetProfiles()
    {
        CreateMap<Pet, PetBriefDto>()
            .ForMember(dest => dest.Breed, options => options.MapFrom(src => string.IsNullOrEmpty(src.Specie.Breed) ? "N/A" : src.Specie.Breed))
            .ForMember(dest => dest.Specie, options => options.MapFrom(src => src.Specie.Species))
            .ForMember(dest => dest.RegisterationDate, options => options.MapFrom(src => src.CreatedAt));

        CreateMap<Pet, PetDto>()
            .ForMember(dest => dest.Breed, options => options.MapFrom(src => string.IsNullOrEmpty(src.Specie.Breed) ? "N/A" : src.Specie.Breed))
            .ForMember(dest => dest.Specie, options => options.MapFrom(src => src.Specie.Species))
            .ForMember(dest => dest.Weight, options => options.MapFrom(src => src.Weight.Value))
            .ForMember(dest => dest.WeightUnit, options => options.MapFrom(src => src.Weight.Unit))
            .ForMember(dest => dest.MicroChipId, options => options.MapFrom(src => src.MicroChipId ?? "N/A"))
            .ForMember(dest => dest.Owner, options => options.MapFrom(src => src.Client));

        CreateMap<CreatePetDto, Pet>()
            .ForMember(dest => dest.Weight, options => options.MapFrom(src => new Weight(src.Weight, src.WeightUnit)))
            .ForMember(
                dest => dest.Specie,
                options => options.MapFrom(src => string.IsNullOrEmpty(src.Breed) ?
                    new BreedSpecie(src.Specie) :
                    new BreedSpecie(src.Specie, src.Breed)
                )
            )
            .ForMember(dest => dest.MicroChipId, options => options.MapFrom(src => src.MicroChipId))
            .ForMember(dest => dest.ClientId, options => options.MapFrom(src => src.OwnerId));

        CreateMap<UpdatePetDto, Pet>()
           .ForMember(dest => dest.Weight, options => options.MapFrom(src => new Weight(src.Weight, src.WeightUnit)))
           .ForMember(
               dest => dest.Specie,
               options => options.MapFrom(src => string.IsNullOrEmpty(src.Breed) ?
                   new BreedSpecie(src.Specie) :
                   new BreedSpecie(src.Specie, src.Breed)
               )
           )
           .ForMember(dest => dest.MicroChipId, options => options.MapFrom(src => src.MicroChipId));
    }
}
