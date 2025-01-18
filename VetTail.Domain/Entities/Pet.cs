using System;
using VetTail.Domain.Common.Abstractions;
using VetTail.Domain.Enums;
using VetTail.Domain.ValueObjects;

namespace VetTail.Domain.Entities;

public sealed class Pet : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public string Color { get; set; }
    public Weight Weight { get; set; }
    public BreedSpecie Specie { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public MicroChipId? MicroChipId { get; set; }

    public Guid ClientId { get; set; }
    public Client? Client { get; set; }

}
