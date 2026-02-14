using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.IdentityAccess.Application.Commands;
using AlgoDDD.IdentityAccess.Domain.Models;
using AlgoDDD.IdentityAccess.Domain.Interfaces;

namespace AlgoDDD.IdentityAccess.Application.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _userRepository.GetByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return new RegisterUserResult
                    {
                        Success = false,
                        ErrorMessage = "User with this email already exists"
                    };
                }

                // Create new user
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Role = request.Role,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                // In a real app, you would hash the password here
                user.PasswordHash = request.Password; // This should be hashed!

                await _userRepository.AddAsync(user);

                return new RegisterUserResult
                {
                    Success = true,
                    UserId = user.Id.ToString(), // Important: Convert Guid to string here
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}".Trim(),
                    Role = user.Role
                };
            }
            catch (Exception ex)
            {
                return new RegisterUserResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
