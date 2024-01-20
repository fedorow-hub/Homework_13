namespace Bank.Domain.Client.ValueObjects;

public sealed class PassportSerie : ValueObject
{
    public string Serie { get; }
    private PassportSerie(string serie)
    {
        Serie = serie;
    }

    public static PassportSerie SetSerie(string serie)
    {
        if (!IsSeries(serie))
        {
            throw new ArgumentException($"Номер \"{nameof(serie)}\" не является серией паспорта");
        }
        return new PassportSerie(serie);
    }

    public static bool IsSeries(string value)
    {
        if (value.Length < 2 || value.Length > 4)
        {
            return false;
        }
        return true;
    }

    protected override IEnumerable<object> GetEqalityComponents()
    {
        yield return Serie;
    }

    public override string ToString()
    {
        return $"{Serie}";
    }
}
