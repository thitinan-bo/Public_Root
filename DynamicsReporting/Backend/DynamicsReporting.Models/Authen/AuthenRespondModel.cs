 

namespace DynamicsReporting.Models.Authen
{
    public class AuthenResponseModel
    {
        //public bool IsAuthenticated { get; set; }
        //public string? Token { get; set; }   // JWT หรือ Session Token
        //public string? RefreshToken { get; set; }
        //public DateTime? ExpireAt { get; set; }

 
        //public string? Role { get; set; }
        public int? UserId { get; set; }
        public string? DisplayName { get; set; }
        public string? BranchCode { get; set; }
        public string? BranchName { get; set; }
        public string? DefaultServer { get; set; }

        public bool IsAuthenticated { get; set; } = false;

    }
}
