namespace Bank.Domain.Worker;

public abstract class Worker
{
    public RoleDataAccess DataAccess { get; protected set; }
}

