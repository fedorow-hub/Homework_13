using Bank.Domain.Root;

namespace Bank.Domain.Client.ValueObjects;

public class Patronymic : ValueObject
{
    public string Name { get; }

    private Patronymic(string name)
    {
        Name = name;
    }

    public static Patronymic SetName(string name)
    {
        if (IsName(name))
        {
            return new Patronymic(name);
        }
        throw new ArgumentException($"Отчество \"{nameof(name)}\" не корректно");
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
