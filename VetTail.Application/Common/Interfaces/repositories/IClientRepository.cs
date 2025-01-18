using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Domain.Common.Interfaces;
using VetTail.Domain.Entities;

namespace VetTail.Application.Common.Interfaces.repositories;

public interface IClientRepository: IRepository<Client, Guid>
{
    Task<Client?> FindByIdWithPets(Guid id, CancellationToken cancellationToken = default);
}
