using AlgoDDD.IdentityAccess.Domain.ValueObjects;

namespace AlgoDDD.IdentityAccess;

public static class TestCompilation
{
    public static void Test()
    {
        var email = Email.Create("test@example.com");
        var password = PasswordHash.Create("Test123!");
        var role = UserRole.Trader;
        
        Console.WriteLine($"Email: {email}");
        Console.WriteLine($"Password Hash: {password}");
        Console.WriteLine($"Role: {role}");
    }
}
