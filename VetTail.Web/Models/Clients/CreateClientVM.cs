using System;
using System.ComponentModel.DataAnnotations;
using VetTail.Domain.Enums;

namespace VetTail.Web.Models.Clients;

public class CreateClientVM
{
    [Required, MinLength(3)]
    public string FirstName { get; set; }

    [Required, MinLength(3)]
    public string LastName { get; set; }

    [MinLength(3)]
    public string? MiddleName { get; set; }

    [Required, MinLength(10), MaxLength(13)]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(150)]
    public string? Street { get; set; }

    [MaxLength(50)]
    public string? City { get; set; }
    public string? ZipCode { get; set; }

    public DateOnly? BirthDate { get; set; }

    [Required]
    public Gender Gender { get; set; }
}
