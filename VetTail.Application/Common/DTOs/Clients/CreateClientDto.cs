using System;
using VetTail.Domain.Enums;

namespace VetTail.Application.Common.DTOs.Clients;

public class CreateClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string? ZipCode { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; }
}
