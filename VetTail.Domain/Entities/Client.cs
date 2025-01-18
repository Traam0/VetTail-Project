using System;
using System.Collections.Generic;
using VetTail.Domain.Common.Abstractions;
using VetTail.Domain.Enums;
using VetTail.Domain.ValueObjects;

namespace VetTail.Domain.Entities;

public sealed class Client : AuditableEntity
{
    public Guid Id { get; set; }
    public Name Name { get; set; }
    public ContactInfo ContactInfo { get; set; }
    public Address Address { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; }

    public ICollection<Pet>? Pets { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
}
