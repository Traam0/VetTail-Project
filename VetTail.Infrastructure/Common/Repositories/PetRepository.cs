using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Application.Common.Interfaces.repositories;
using VetTail.Domain.Entities;
using VetTail.Infrastructure.Common.Repositories.Generic;
using VetTail.Infrastructure.Data.Persistance;

namespace VetTail.Infrastructure.Common.Repositories;

public class PetRepository(ApplicationDbContext context) : Repository<Pet, Guid>(context), IPetRepository
{
    public Task<Pet?> FindByIdWithOwner(Guid id, CancellationToken cancellationToken = default)
    {
        return base.context.Pets.Include(p => p.Client).ThenInclude(navigationPropertyPath: c => c.Pets.Where(p => p.Id != id)).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
