using System;
using VetTail.Domain.Entities;
using VetTail.Domain.Enums;
using VetTail.Domain.ValueObjects;

namespace VetTail.Application.Common.DTOs.Clients;

public class ClientBriefDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
}
