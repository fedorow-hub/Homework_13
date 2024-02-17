namespace Bank.Domain.Root;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }
        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object> GetEqalityComponents();
    public sealed override bool Equals(object? obj)
    {
        if(obj == null || obj.GetType() != GetType())
        {
            return false;
        }      

        var other = (ValueObject)obj;
        return GetEqalityComponents().SequenceEqual(other.GetEqalityComponents());
    }

    public sealed override int GetHashCode()
        => GetEqalityComponents()
        .Select(x => x.GetHashCode())
        .Aggregate((x, y) => x ^ y);

    public override string ToString()
    {
        return base.ToString() ?? throw new InvalidOperationException();
    }
}
