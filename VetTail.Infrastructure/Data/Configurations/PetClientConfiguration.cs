using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetTail.Domain.Entities;

namespace VetTail.Infrastructure.Data.Configurations;

public class PetClientConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("vet_pets");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(p => p.Weight, weight =>
        {
            weight.Property(w => w.Value).IsRequired();
            weight.Property(w => w.Unit).IsRequired();  
        });

        builder.OwnsOne(p => p.MicroChipId, m =>
        {
            m.Property(m => m.Value);
        });

        builder.OwnsOne(p => p.Specie, sp =>
        {
            sp.Property(s => s.Species).IsRequired();   
            sp.Property(s => s.Breed);
        });
    }
}
