using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.IdentityAccess.Domain.ValueObjects;

public class PasswordHash : ValueObject
{
    public string Hash { get; }
    
    private PasswordHash(string hash)
    {
        Hash = hash;
    }
    
    public static PasswordHash Create(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            throw new ArgumentException("Password cannot be empty", nameof(plainPassword));
            
        if (plainPassword.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters", nameof(plainPassword));
            
        // Hash the password using BCrypt
        string hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
        return new PasswordHash(hash);
    }
    
    public static PasswordHash FromHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash cannot be empty", nameof(hash));
            
        return new PasswordHash(hash);
    }
    
    public bool Verify(string plainPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, Hash);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
    }
    
    public override string ToString() => "[PROTECTED]";
}
