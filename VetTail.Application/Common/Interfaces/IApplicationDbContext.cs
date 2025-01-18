using VetTail.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VetTail.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Client> Clients { get; }
    DbSet<Pet> Pets { get; }
}
