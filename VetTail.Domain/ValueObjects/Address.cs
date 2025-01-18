using System;

namespace VetTail.Domain.ValueObjects;

public sealed class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string? ZipCode { get; private set; }

    public Address(string street, string city)
    {
        if(string.IsNullOrEmpty(street)) throw new ArgumentNullException(nameof(street), "Street cannot be empty.");
        this.Street = street;

        if (string.IsNullOrEmpty(city)) throw new ArgumentNullException(nameof(city), "City cannot be empty.");
        this.City = city;
    }

    public Address(string street, string city, string zipCode) : this(street, city)
    {
        if(string.IsNullOrEmpty(zipCode)) throw new ArgumentNullException(nameof(zipCode), "Zip code cannot be empty.");
        this.ZipCode = zipCode;
    }

    public override string ToString()
    {
        return this.ZipCode is not null ? $"{this.Street}, {this.City} {this.ZipCode}" : $"{this.Street}, {this.City}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Address other && this.Street.Equals(other.Street) && this.City.Equals(other.City);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Street, this.City, this.ZipCode);
    }
}