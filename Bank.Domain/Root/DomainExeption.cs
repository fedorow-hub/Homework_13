namespace Bank.Domain.Root;

/// <summary>
/// Общее доменное исключение
/// </summary>
[Serializable]
public class DomainExeption : Exception
{
    public DomainExeption() { }
    public DomainExeption(string description)
        :base(description) { }

    public DomainExeption(string description, Exception innerExeption)
        : base(description, innerExeption) { }    
}
