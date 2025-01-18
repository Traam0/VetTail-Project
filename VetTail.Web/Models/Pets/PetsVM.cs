using VetTail.Application.Common.DTOs.Pets;
using VetTail.Application.Common.DTOs.Wrappers;

namespace VetTail.Web.Models.Pets;

public class PetsVM
{
    public PaginatedList<PetBriefDto> Pets { get; set; }
}
