using System.Text.RegularExpressions;
using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.IdentityAccess.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }
    
    private Email(string value)
    {
        Value = value.ToLowerInvariant();
    }
    
    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
            
        // Basic email validation
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!regex.IsMatch(email))
            throw new ArgumentException("Invalid email format", nameof(email));
            
        return new Email(email);
    }
    
    public static implicit operator string(Email email) => email.Value;
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public override string ToString() => Value;
}
