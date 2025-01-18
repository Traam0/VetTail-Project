using System.Threading.Tasks;
using System.Threading;
using System;
using VetTail.Domain.Entities;
using VetTail.Domain.Common.Interfaces;

namespace VetTail.Application.Common.Interfaces.repositories;

public interface IPetRepository : IRepository<Pet, Guid>
{
    Task<Pet?> FindByIdWithOwner(Guid id, CancellationToken cancellationToken = default);

}
