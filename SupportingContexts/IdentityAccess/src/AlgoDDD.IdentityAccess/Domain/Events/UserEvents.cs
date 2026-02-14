using AlgoDDD.SharedKernel.Domain.Events;
using AlgoDDD.IdentityAccess.Domain.ValueObjects;


namespace AlgoDDD.IdentityAccess.Domain.Events;

public class UserRegisteredEvent : DomainEventBase
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }
    public UserRole Role { get; }
    
    public UserRegisteredEvent(Guid userId, string email, string fullName, UserRole role)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
        Role = role;
    }
}

public class UserLoggedInEvent : DomainEventBase
{
    public Guid UserId { get; }
    public string Email { get; }
    public string IpAddress { get; }
    public string UserAgent { get; }
    
    public UserLoggedInEvent(Guid userId, string email, string ipAddress, string userAgent)
    {
        UserId = userId;
        Email = email;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
}

public class PasswordChangedEvent : DomainEventBase
{
    public Guid UserId { get; }
    public DateTime ChangedAt { get; }
    
    public PasswordChangedEvent(Guid userId)
    {
        UserId = userId;
        ChangedAt = DateTime.UtcNow;
    }
}

public class UserLockedEvent : DomainEventBase
{
    public Guid UserId { get; }
    public string Reason { get; }
    public DateTime LockedUntil { get; }
    
    public UserLockedEvent(Guid userId, string reason, DateTime lockedUntil)
    {
        UserId = userId;
        Reason = reason;
        LockedUntil = lockedUntil;
    }
}

public class UserRoleAssignedEvent : DomainEventBase
{
    public Guid UserId { get; }
    public UserRole Role { get; }
    public Guid AssignedBy { get; }
    
    public UserRoleAssignedEvent(Guid userId, UserRole role, Guid assignedBy)
    {
        UserId = userId;
        Role = role;
        AssignedBy = assignedBy;
    }
}

