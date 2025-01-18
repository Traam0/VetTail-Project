using System;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Application.Common.DTOs.Generics;
using VetTail.Application.Common.DTOs.Pets;
using VetTail.Application.Common.DTOs.Wrappers;

namespace VetTail.Application.Common.Interfaces;

public interface IPetService
{
    Task<PetDto> GetPetById(Guid id, CancellationToken cancellationToken = default);
    Task<PaginatedList<PetBriefDto>> GetAllPaginated(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<Guid>> CreatePetAsync(CreatePetDto dto, CancellationToken cancellationToken = default);
    Task<Result<Guid>> UpdatePetAsync(UpdatePetDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeletePetAsync(Guid id, CancellationToken cancellationToken = default);


}
