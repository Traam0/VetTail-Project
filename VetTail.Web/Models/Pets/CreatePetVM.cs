using System;
using System.ComponentModel.DataAnnotations;
using VetTail.Domain.Enums;

namespace VetTail.Web.Models.Pets;

public class CreatePetVM
{
    [Required, MinLength(2)]

    public string Name { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public string Color { get; set; }
    [Required]
    public DateTimeOffset BirthDate { get; set; }
    
    public string? Breed { get; set; }
    [Required]
    public string Specie { get; set; }

    [Required]
    public decimal Weight { get; set; }
    [Required]    
    public string WeightUnit { get; set; }

    public string? MicroChipId { get; set; }

    public Guid OwnerId { get; set; }
}
