namespace POS_Application.Server.Models
{
    public class User
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    #region Token Management

    public class TokenInfo
    {
        public string Token { get; set; }
        public string EmployeeId { get; set; }
        public bool IsValid { get; set; }
    }

    #region Login

    public class LoginRequest
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public string EmployeeId { get; set; }
        public string Role { get; set; }
    }

    #endregion Login

    #region Logout

    public class LogoutRequest
    {
        public string Token { get; set; }
    }

    public class LogoutResponse
    {
        public string Message { get; set; }
    }
    #endregion Logout

    #region SessionCheck

    public class SessionInfo
    {
        public bool IsValid { get; set; }
        public string EmployeeId { get; set; }
        public string Role { get; set; }
    }
    #endregion SessionCheck

    #endregion Token Management
}
