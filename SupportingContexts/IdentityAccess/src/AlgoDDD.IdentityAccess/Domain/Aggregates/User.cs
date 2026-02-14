using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.IdentityAccess.Domain.ValueObjects;
using AlgoDDD.IdentityAccess.Domain.Events;

namespace AlgoDDD.IdentityAccess.Domain.Aggregates;

public class User : AggregateRoot<Guid>
{
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public string FullName { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsLocked { get; private set; }
    public DateTime? LockedUntil { get; private set; }
    public int FailedLoginAttempts { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }
    
    private User() : base(Guid.Empty)
    {
        // For EF Core - initialize non-nullable properties with defaults
        Email = null!;
        PasswordHash = null!;
        FullName = null!;
        Role = UserRole.Trader;
        IsActive = true;
        IsLocked = false;
        FailedLoginAttempts = 0;
        CreatedAt = DateTime.UtcNow;
    }
    
    private User(Guid id, Email email, PasswordHash passwordHash, string fullName, UserRole role)
        : base(id)
    {
        Id = id;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        Role = role;
        IsActive = true;
        IsLocked = false;
        FailedLoginAttempts = 0;
        CreatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new UserRegisteredEvent(id, email, fullName, role));
    }
    
    public static User Register(string email, string password, string fullName, UserRole role = UserRole.Trader)
    {
        var userEmail = Email.Create(email);
        var passwordHash = PasswordHash.Create(password);
        
        return new User(Guid.NewGuid(), userEmail, passwordHash, fullName, role);
    }
    
    public bool VerifyPassword(string password)
    {
        return PasswordHash.Verify(password);
    }
    
    public LoginResult Login(string password, string ipAddress, string userAgent)
    {
        if (!IsActive)
            return LoginResult.AccountInactive;
            
        if (IsLocked && LockedUntil.HasValue && LockedUntil.Value > DateTime.UtcNow)
            return LoginResult.AccountLocked(LockedUntil.Value);
            
        if (IsLocked && LockedUntil.HasValue && LockedUntil.Value <= DateTime.UtcNow)
        {
            // Lock expired
            IsLocked = false;
            LockedUntil = null;
            FailedLoginAttempts = 0;
        }
        
        if (!VerifyPassword(password))
        {
            FailedLoginAttempts++;
            
            if (FailedLoginAttempts >= 5)
            {
                LockAccount("Too many failed login attempts", TimeSpan.FromMinutes(15));
            }
            
            return LoginResult.InvalidCredentials;
        }
        
        // Successful login
        LastLoginAt = DateTime.UtcNow;
        FailedLoginAttempts = 0;
        
        AddDomainEvent(new UserLoggedInEvent(Id, Email, ipAddress, userAgent));
        
        return LoginResult.Success;
    }
    
    public void ChangePassword(string currentPassword, string newPassword)
    {
        if (!VerifyPassword(currentPassword))
            throw new UnauthorizedAccessException("Current password is incorrect");
            
        PasswordHash = PasswordHash.Create(newPassword);
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new PasswordChangedEvent(Id));
    }
    
    public void LockAccount(string reason, TimeSpan duration)
    {
        IsLocked = true;
        LockedUntil = DateTime.UtcNow.Add(duration);
        
        AddDomainEvent(new UserLockedEvent(Id, reason, LockedUntil.Value));
    }
    
    public void UnlockAccount()
    {
        IsLocked = false;
        LockedUntil = null;
        FailedLoginAttempts = 0;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void AssignRole(UserRole newRole, Guid assignedBy)
    {
        if (Role == newRole)
            return;
            
        Role = newRole;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new UserRoleAssignedEvent(Id, newRole, assignedBy));
    }
    
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void SetRefreshToken(string refreshToken, TimeSpan expiry)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = DateTime.UtcNow.Add(expiry);
    }
    
    public void ClearRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiryTime = null;
    }
}

public class LoginResult
{
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }
    public DateTime? LockedUntil { get; private set; }
    
    private LoginResult(bool isSuccess, string? errorMessage = null, DateTime? lockedUntil = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        LockedUntil = lockedUntil;
    }
    
    public static LoginResult Success => new(true);
    public static LoginResult InvalidCredentials => new(false, "Invalid email or password");
    public static LoginResult AccountInactive => new(false, "Account is deactivated");
    public static LoginResult AccountLocked(DateTime until) => new(false, $"Account locked until {until:yyyy-MM-dd HH:mm:ss} UTC", until);
}
