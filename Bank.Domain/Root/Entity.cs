﻿using MediatR;

namespace Bank.Domain;

public abstract class Entity
{
    private int? _requestedHashCode;
    public virtual long Id { get; private set; }

    private readonly List<INotification> _domainEvents = new();

    public IReadOnlyCollection<INotification> DomainEvents
        => _domainEvents.AsReadOnly();
    public void AddDomainEvent(INotification eventItem)
        => _domainEvents.Add(eventItem);
    public void RemoveDomainEvent(INotification eventItem)
        => _domainEvents.Remove(eventItem);
    public void ClearDomainEvents()
        => _domainEvents.Clear();
    public bool IsTransient()
        => Id == default;
    protected Entity() { }
    protected Entity(long id) => Id = id;
    public override bool Equals(object obj)
    {
        if (obj is not Entity entity)        
            return false;
        
        if (ReferenceEquals(this, entity))        
            return true;  
        if (GetType() != entity.GetType()) 
            return false;
        if (entity.IsTransient() || IsTransient())        
            return false;
        else
            return Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity a, Entity b)
        => a is null && b is null || a is not null && b is not null && a.Equals(b);

    public static bool operator !=(Entity a, Entity b)
        => !(a == b);

    public override int GetHashCode()
        => (GetType().ToString() + Id).GetHashCode();
}
