using System;
using VetTail.Domain.Enums;

namespace VetTail.Application.Common.DTOs.Pets;

public class UpdatePetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public string Color { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public string Breed { get; set; }
    public string Specie { get; set; }

    public decimal Weight { get; set; }
    public string WeightUnit { get; set; }

    public string MicroChipId { get; set; }
}
