using System;
using VetTail.Domain.Enums;
using VetTail.Domain.Entities;
using System.Collections.Generic;
using VetTail.Domain.ValueObjects;
using VetTail.Application.Common.DTOs.Pets;

namespace VetTail.Application.Common.DTOs.Clients;

public class ClientDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string? ZipCode { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public IEnumerable<PetBriefDto> Pets { get; set; } = [];
}
