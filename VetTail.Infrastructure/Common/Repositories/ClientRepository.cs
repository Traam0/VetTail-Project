using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Application.Common.Interfaces.repositories;
using VetTail.Domain.Entities;
using VetTail.Infrastructure.Common.Repositories.Generic;
using VetTail.Infrastructure.Data.Persistance;

namespace VetTail.Infrastructure.Common.Repositories;

public class ClientRepository(ApplicationDbContext context) : Repository<Client, Guid>(context), IClientRepository
{


    public async Task<Client?> FindByIdWithPets(Guid id, CancellationToken cancellationToken = default)
    {
        return await base.context.Clients
            .Include(c => c.Pets)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

}
