using System.Diagnostics;

namespace Arentheym.ParkingBarrier.Domain.Core;

/// <summary>
/// Abstract class for a DDD-style ValueObject.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Operator overload for ==.
    /// </summary>
    /// <param name="left">Left object to compare.</param>
    /// <param name="right">Right object to compare.</param>
    /// <returns>True if equal, false if not.</returns>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        ArgumentNullException.ThrowIfNull(left);
        return left.Equals(right);
    }

    /// <summary>
    /// Operator overload for !=.
    /// </summary>
    /// <param name="left">Left object to compare.</param>
    /// <param name="right">Right object to compare.</param>
    /// <returns>True if not equal, false if equal.</returns>
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        ArgumentNullException.ThrowIfNull(left);
        return !left.Equals(right);
    }

    /// <summary>
    /// Compare if objects are equal.
    /// </summary>
    /// <param name="obj">The other <see cref="object"/> to compare.</param>
    /// <returns>True if equal, false if not.</returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueObject);
    }

    /// <summary>
    /// Compare if objects are equal.
    /// </summary>
    /// <param name="other">The other <see cref="ValueObject"/> to compare.</param>
    /// <returns>True if equal, false if not.</returns>
    public bool Equals(ValueObject? other)
    {
        if (other is null || other.GetType() != GetType())
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        IEnumerator<object> thisValues = GetEqualityValues().GetEnumerator();
        IEnumerator<object> otherValues = other.GetEqualityValues().GetEnumerator();

        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (thisValues.Current is null ^ otherValues.Current is null)
            {
                return false;
            }

            if (thisValues.Current?.Equals(otherValues.Current) == false)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Calculate hash-code for the ValueObject.
    /// </summary>
    /// <returns>The calculated hash-code.</returns>
    public override int GetHashCode()
    {
        return GetEqualityValues().Select(value => value?.GetHashCode() ?? 0).Aggregate((total, next) => total ^ next);
    }

    /// <summary>
    /// Retrieve all the properties that need to be evaluated for the equals functionality.
    /// </summary>
    /// <returns>A list of objects to compare.</returns>
    protected abstract IEnumerable<object> GetEqualityValues();
}
