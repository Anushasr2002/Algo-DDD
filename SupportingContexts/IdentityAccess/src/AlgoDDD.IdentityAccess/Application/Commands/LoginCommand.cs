using MediatR;

namespace AlgoDDD.IdentityAccess.Application.Commands;

public class LoginCommand : IRequest<LoginResult>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
}
