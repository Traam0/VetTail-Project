using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using VetTail.Application.Common.DTOs.Clients;
using VetTail.Application.Common.DTOs.Generics;
using VetTail.Application.Common.DTOs.Wrappers;

namespace VetTail.Application.Common.Interfaces;

public interface IClientService
{
    Task<ClientDto> GetClientByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ClientMacroDto>> GetAllClientsMacroAsync(CancellationToken cancellation = default);
    Task<PaginatedList<ClientBriefDto>> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<Guid>> CreateClientAsync(CreateClientDto dto, CancellationToken cancellationToken = default);
    Task<Result<Guid>> UpdateClientAsync(UpdateClientDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteClientAsync(Guid id, CancellationToken cancellationToken = default);
}
