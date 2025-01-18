using System;

namespace VetTail.Domain.ValueObjects;

public sealed class MicroChipId
{
    public string Value { get; private set; }

    public MicroChipId(string value)
    {
        if(string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value), "Microchip value cannot be empty.");
        this.Value = value;
    }

    public override string ToString() => this.Value;
    
    public override bool Equals(object? obj)
    {
        return obj is MicroChipId other && this.Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Value);
    }

    public static implicit operator string(MicroChipId chip) => chip.Value;

    public static implicit operator MicroChipId(string value) => new MicroChipId(value);
    
    public static bool operator ==(MicroChipId left, MicroChipId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MicroChipId left, MicroChipId right)
    {
        return !left.Equals(right);
    }
}
