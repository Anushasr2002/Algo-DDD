using MediatR;

namespace AlgoDDD.IdentityAccess.Application.Commands;

public class RegisterUserCommand : IRequest<RegisterUserResult>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}".Trim();
    public string Role { get; set; } = "User"; // Default role
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
}

public class RegisterUserResult
{
    public bool Success { get; set; }
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? Role { get; set; }
    public string? ErrorMessage { get; set; }
    public string[]? Errors { get; set; }
}
