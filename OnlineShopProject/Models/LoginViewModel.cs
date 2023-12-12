using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
        public string ReturnUrl { get; set; }
    }
}
