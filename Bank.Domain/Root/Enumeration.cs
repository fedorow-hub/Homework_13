using System.Reflection;

namespace Bank.Domain.Root;

public abstract class Enumeration : IComparable
{
    public string Name { get; }
    public int Id { get; }
    protected Enumeration(int id, string name)
    {
        Name = name;
        Id = id;
    }

    public sealed override string ToString()
    {
        return Name;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
        .Select(f=>f.GetValue(null))
        .Cast<T>();

    public sealed override bool Equals(object? obj)
    {
        if(obj is not Enumeration otherValue)
        {
            return false;
        }
        var typeMatches = GetType() ==obj.GetType();
        var valueMatches = Id.Equals(otherValue.Id);
        return typeMatches && valueMatches;
    }

    protected bool Equals(Enumeration other)
    {
        return Name == other.Name && Id == other.Id;
    }

    public sealed override int GetHashCode()
    {
        return HashCode.Combine(Name, Id);
    }

    public int CompareTo(object? obj)
    {
        return Id.CompareTo(((Enumeration)obj!).Id);
    }
}