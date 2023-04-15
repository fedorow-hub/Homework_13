namespace Bank.Application.Common.Exeptions;

public class ApplicationExeption : Exception
{
    public ApplicationExeption()
    {
        
    }

    public ApplicationExeption(string description)
        : base(description)
    {
        
    }

    public ApplicationExeption(string description, Exception innerExeption)
        : base(description, innerExeption)
    {
        
    }
}
