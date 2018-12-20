using System.ComponentModel.DataAnnotations;

namespace WelfareDenmark.APIControllers {
    public class LoginDto {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}