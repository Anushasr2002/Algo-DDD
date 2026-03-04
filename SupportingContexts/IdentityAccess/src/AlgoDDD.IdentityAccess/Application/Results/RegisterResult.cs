namespace AlgoDDD.IdentityAccess.Application.Results
{
    public class RegisterResult
    {
        public required string UserId { get; set; }
        public required string Email { get; set; }
        public required string ErrorMessage { get; set; }
        public bool Success { get; set; }

        public static RegisterResult Successful(string userId, string email)
        {
            return new RegisterResult
            {
                Success = true,
                UserId = userId,
                Email = email,
                ErrorMessage = string.Empty // ✅ required property set
            };
        }

        public static RegisterResult Failed(string errorMessage)
        {
            return new RegisterResult
            {
                Success = false,
                UserId = string.Empty,      // ✅ required property set
                Email = string.Empty,       // ✅ required property set
                ErrorMessage = errorMessage
            };
        }
    }
}
