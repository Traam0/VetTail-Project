using System;

namespace VetTail.Domain.ValueObjects;

public sealed class ContactInfo
{
    public string PhoneNumber { get; private set; }
    public string? Email { get; private set; }

    public ContactInfo(string phoneNumber)
    {
        if(string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException(nameof(phoneNumber), "Phone number cannot be empty.");
        this.PhoneNumber = phoneNumber;
    }

    public ContactInfo(string phoneNumber, string email) : this(phoneNumber)
    {
        if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email), "Email cannot be empty.");
        this.Email = email;
    }

    public override string ToString()
    {
        return this.Email is null ? this.PhoneNumber : $"{this.PhoneNumber} {this.Email}";
    }

    public override bool Equals(object? obj)
    {
        return obj is ContactInfo other && this.PhoneNumber.Equals(other.PhoneNumber) && this.Email == other.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PhoneNumber, Email);
    }
}
