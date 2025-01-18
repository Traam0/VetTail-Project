using Microsoft.EntityFrameworkCore;
using VetTail.Application.Common.Interfaces;
using VetTail.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VetTail.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace VetTail.Infrastructure.Data.Persistance;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<string>, string>(options), IApplicationDbContext
{
    public DbSet<Client> Clients { get; set; }

    public DbSet<Pet> Pets { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
