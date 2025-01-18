using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetTail.Domain.Entities;

namespace VetTail.Infrastructure.Data.Configurations;

public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("vet_clients");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(c => c.Name, name =>
        {
            name.Property(n => n.FirstName).HasMaxLength(50).IsRequired();
            name.Property(n => n.LastName).HasMaxLength(50).IsRequired();
            name.Property(n => n.MiddleName).HasMaxLength(50);
        });

       builder.OwnsOne(c => c.ContactInfo, contact =>
       {
           contact.Property(ci => ci.PhoneNumber).HasMaxLength(15).IsRequired();
           contact.Property(ci => ci.Email).HasMaxLength(50);
       });


        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.Street).IsRequired();
            address.Property(a => a.City).IsRequired();
            address.Property(a => a.ZipCode);
        });


        builder.HasMany(c => c.Pets)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.ClientId);
    }
}
