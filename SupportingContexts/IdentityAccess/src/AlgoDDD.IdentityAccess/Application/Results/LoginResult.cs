namespace AlgoDDD.IdentityAccess.Application.Results
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public static LoginResult Successful(string token, string refreshToken, string userId, string email, string role)
        {
            return new LoginResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken,
                UserId = userId,
                Email = email,
                Role = role
            };
        }

        public static LoginResult Failed(string errorMessage)
        {
            return new LoginResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
