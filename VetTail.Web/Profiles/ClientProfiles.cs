using AutoMapper;
using System;
using VetTail.Application.Common.DTOs.Clients;
using VetTail.Domain.Enums;
using VetTail.Web.Models.Clients;

namespace VetTail.Web.Profiles;

public class ClientProfiles: Profile
{
    public ClientProfiles()
    {
        CreateMap<CreateClientVM, CreateClientDto>();
        CreateMap<UpdateClientVM, UpdateClientDto>();
        CreateMap<ClientDto, UpdateClientVM>();
        CreateMap<ClientDto, ClientVM>();
    }
}
