namespace AlgoDDD.IdentityAccess.Application.Results
{
    public class LoginResult
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        public required string UserId { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string ErrorMessage { get; set; }
        public bool Success { get; set; }

        public static LoginResult Successful(string token, string refreshToken, string userId, string email, string role)
        {
            return new LoginResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken,
                UserId = userId,
                Email = email,
                Role = role,
                ErrorMessage = string.Empty // ✅ required property set
            };
        }

        public static LoginResult Failed(string errorMessage)
        {
            return new LoginResult
            {
                Success = false,
                Token = string.Empty,       // ✅ required property set
                RefreshToken = string.Empty,
                UserId = string.Empty,
                Email = string.Empty,
                Role = string.Empty,
                ErrorMessage = errorMessage
            };
        }
    }
}
