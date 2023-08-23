using System.ComponentModel.DataAnnotations;

namespace FE_Client.Form
{
    public class LoginForm
    {
        [Required,EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
