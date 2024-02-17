using Bank.Domain.Root;

namespace Bank.Domain.Client.ValueObjects;

public sealed class PassportSeries : ValueObject
{
    public string Series { get; }
    private PassportSeries(string series)
    {
        Series = series;
    }

    public static PassportSeries SetSeries(string series)
    {
        if (!IsSeries(series))
        {
            throw new ArgumentException($"Номер \"{nameof(series)}\" не является серией паспорта");
        }
        return new PassportSeries(series);
    }

    public static bool IsSeries(string value)
    {
        return value.Length is >= 2 and <= 4;
    }

    protected override IEnumerable<object> GetEqalityComponents()
    {
        yield return Series;
    }

    public override string ToString()
    {
        return $"{Series}";
    }
}
