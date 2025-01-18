using System;

namespace VetTail.Domain.ValueObjects;

public sealed class Weight
{
    public decimal Value { get; private set; }
    public string Unit { get; private set; }

    public Weight(decimal value, string unit)
    {
        if(value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Weight value must be greater than zero.");
        if(string.IsNullOrEmpty(unit)) throw new ArgumentNullException(nameof(unit), "Weight unit cannot be empty.");
        this.Value = value;
        this.Unit = unit;
    }


    public override string ToString()
    {
        return $"{this.Value} {this.Unit}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Weight other && this.Value.Equals(other.Value) && this.Unit.Equals(other.Unit);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Value, this.Unit);
    }
}
