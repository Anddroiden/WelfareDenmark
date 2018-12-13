using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WelfareDenmark.Models;

namespace WelfareDenmark.Areas.Identity.Pages.Account {
    [Authorize(Policy = PolicyConstants.CanCreatePatient)]
    public class RegisterModel : PageModel {
        private readonly ILogger<RegisterModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger
        ) {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null) {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid) {
                var user = new ApplicationUser {UserName = Input.Email, Email = Input.Email};

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddClaimAsync(user, new Claim(PolicyConstants.IsPatient, "true"));
                    var consultant = await _userManager.GetUserAsync(User);
                    consultant.Patients.Add(user);
                    await _userManager.UpdateAsync(consultant);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public class InputModel {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 1)] //password length
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}