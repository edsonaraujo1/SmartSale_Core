using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartSale.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
namespace SmartSale.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<UsuarioApp> userManager,
            SignInManager<UsuarioApp> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} é requerido")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha atual")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "{0} é requerido")]
            [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nova senha")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme sua nova senha")]
            [Compare("NewPassword", ErrorMessage = "A nova senha e a senha de confirmação não correspondem.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não é possível carregar o usuário com o ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não é possível carregar o usuário com o ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("Usuário mudou sua senha com sucesso.");
            StatusMessage = "Você alterou a sua senha.";

            return RedirectToPage();
        }
    }
}