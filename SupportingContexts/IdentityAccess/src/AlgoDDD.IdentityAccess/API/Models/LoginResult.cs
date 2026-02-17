using System;

namespace AlgoDDD.IdentityAccess.API.Models;

public class LoginResult
{
   public class Message
{
    public bool Success { get; set; }

    // Required values for a successful login
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }

    // User identity details
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public required string Role { get; set; }

    // Error handling
    public string? ErrorMessage { get; set; }
    public string[]? Errors { get; set; }
}

}
