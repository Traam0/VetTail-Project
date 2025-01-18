using System;

namespace VetTail.Domain.ValueObjects;

public sealed class Name
{
    public string FirstName { get; private set; }
    public string? MiddleName{ get; private set; }
    public string LastName { get; private set; }

    public Name(string firstName, string lastName)
    {
        if(string.IsNullOrEmpty(firstName)) throw new ArgumentNullException(nameof(firstName), "First name cannot be empty.");
        this.FirstName = firstName.Trim();
        this.LastName = lastName.Trim();

    }

    public Name(string firstName, string lastName, string? middleName): this(firstName, lastName)
    {        
        this.MiddleName = middleName?.Trim();
    }

    public override string ToString()
    {
        return this.MiddleName is not null ? $"{this.LastName} {this.MiddleName} {this.FirstName}" : $"{this.LastName} {this.FirstName}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Name other && this.FirstName.Equals(other.FirstName) && this.LastName.Equals(other.LastName) && this.MiddleName == other.MiddleName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, MiddleName);
    }
}
