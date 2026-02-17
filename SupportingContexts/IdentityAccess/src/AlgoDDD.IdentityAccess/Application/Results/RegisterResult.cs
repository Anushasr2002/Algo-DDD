namespace AlgoDDD.IdentityAccess.Application.Results
{
    public class RegisterResult
    {
        public bool Success { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string ErrorMessage { get; set; }

        public static RegisterResult Successful(string userId, string email)
        {
            return new RegisterResult
            {
                Success = true,
                UserId = userId,
                Email = email
            };
        }

        public static RegisterResult Failed(string errorMessage)
        {
            return new RegisterResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
