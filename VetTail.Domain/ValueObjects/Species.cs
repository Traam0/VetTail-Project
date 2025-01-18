using System;

namespace VetTail.Domain.ValueObjects;

public sealed class BreedSpecie
{
    public string Species { get; private set; }
    public string? Breed { get; private set; }

    public BreedSpecie(string species)
    {
        if(string.IsNullOrEmpty(species)) throw new ArgumentNullException(nameof(species), "Species cannot be empty.");
        this.Species = species;
    }

    public BreedSpecie(string species, string breed) : this(species)
    {
        if(string.IsNullOrEmpty(breed)) throw new ArgumentNullException(nameof(breed), "Breed cannot be empty.");
        this.Breed = breed;
    }

    public override string ToString()
    {
        return this.Breed is not null ? $"{this.Species} ({this.Breed})" : this.Species;
    }

    public override bool Equals(object? obj)
    {
        return obj is BreedSpecie other && this.Species.Equals(other.Species) && this.Breed == other.Breed;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Species, Breed);
    }
}
