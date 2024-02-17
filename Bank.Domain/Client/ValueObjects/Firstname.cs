using Bank.Domain.Root;

namespace Bank.Domain.Client.ValueObjects;

public class Firstname : ValueObject
{
    public string Name { get; }

    private Firstname(string name)
    {
        Name = name;
    }

    public static Firstname SetName(string name)
    {
        if (IsName(name))
        {
            return new Firstname(name);
        }
        throw new ArgumentException($"Имя \"{nameof(name)}\" не корректно");
    }

    public static bool IsName(string name)
    {
        if (name.Length <= 0) return false;
        return char.IsUpper(name[0]) && name.Length > 2;
    }

    protected override IEnumerable<object> GetEqalityComponents()
    {
        yield return Name;
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}
