#nullable disable
namespace NailManagement.Models
{
    public class AdminUserConfig
    {
        /// <summary>
        /// To store the initial admin username for the application
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// To store the initial admin email for the application
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// To store the initial admin password for the application
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// To store the initial admin role for the application
        /// </summary>
        public string Role { get; set; }
    }
}
