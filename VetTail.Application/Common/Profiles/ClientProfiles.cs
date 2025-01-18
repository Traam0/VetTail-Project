using AutoMapper;
using VetTail.Application.Common.DTOs.Clients;
using VetTail.Domain.Entities;
using VetTail.Domain.ValueObjects;

namespace VetTail.Application.Common.Profiles;

public sealed class ClientProfiles : Profile
{
    public ClientProfiles()
    {
        base.CreateMap<Client, ClientBriefDto>()
            .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.Name.FirstName))
            .ForMember(dest => dest.MiddleName, options => options.MapFrom(src => src.Name.MiddleName ?? "N/A"))
            .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.Name.LastName))
            .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.ContactInfo.PhoneNumber))
            .ForMember(dest => dest.Email, options => options.MapFrom(src => src.ContactInfo.Email ?? "N/A"));

        base.CreateMap<Client, ClientDto>()
            .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.Name.FirstName))
            .ForMember(dest => dest.MiddleName, options => options.MapFrom(src => src.Name.MiddleName ?? "N/A"))
            .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.Name.LastName))
            .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.ContactInfo.PhoneNumber))
            .ForMember(dest => dest.Email, options => options.MapFrom(src => src.ContactInfo.Email ?? "N/A"))
            .ForMember(dest => dest.Street, options => options.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.City, options => options.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.ZipCode, options => options.MapFrom(src => src.Address.ZipCode ?? "N/A"));

        base.CreateMap<CreateClientDto, Client>()
            .ForMember(
                dest => dest.Name, 
                options => options.MapFrom(src => string.IsNullOrEmpty(src.MiddleName) ?
                    new Name(src.FirstName, src.LastName) :
                    new Name(src.FirstName, src.LastName, src.MiddleName)
                )
            )
            .ForMember(
                dest => dest.ContactInfo, 
                options => options.MapFrom(src => string.IsNullOrEmpty(src.Email) ?
                    new ContactInfo(src.PhoneNumber) :
                    new ContactInfo(src.PhoneNumber, src.Email)
                )
            )
            .ForMember(
                dest => dest.Address,
                options => options.MapFrom(src => string.IsNullOrEmpty(src.ZipCode) ?
                    new Address(src.Street, src.City) :
                    new Address(src.Street, src.City, src.ZipCode)
                )
            );

        base.CreateMap<Client, ClientMacroDto>()
            .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name.ToString()));

        base.CreateMap<UpdateClientDto, Client>()
           .ForMember(
               dest => dest.Name,
               options => options.MapFrom(src => string.IsNullOrEmpty(src.MiddleName) ?
                   new Name(src.FirstName, src.LastName) :
                   new Name(src.FirstName, src.LastName, src.MiddleName)
               )
           )
           .ForMember(
               dest => dest.ContactInfo,
               options => options.MapFrom(src => string.IsNullOrEmpty(src.Email) ?
                   new ContactInfo(src.PhoneNumber) :
                   new ContactInfo(src.PhoneNumber, src.Email)
               )
           )
           .ForMember(
               dest => dest.Address,
               options => options.MapFrom(src => string.IsNullOrEmpty(src.ZipCode) ?
                   new Address(src.Street, src.City) :
                   new Address(src.Street, src.City, src.ZipCode)
               )
           );
    }
}
