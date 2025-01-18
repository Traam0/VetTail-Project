using VetTail.Application.Common.DTOs.Clients;
using VetTail.Application.Common.DTOs.Wrappers;

namespace VetTail.Web.Models.Clients;

public class ClientsVM
{
    public PaginatedList<ClientBriefDto> Clients { get; set; }
}
