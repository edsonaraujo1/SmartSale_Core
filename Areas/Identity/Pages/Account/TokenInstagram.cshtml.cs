using SmartSale.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace SmartSale.Areas.Identity.Pages.Account
{
    //[Authorize]
    public class TokenInstagramModel : PageModel
    {
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly Data.ApplicationDbContext _context;
        public TokenInstagramModel(SignInManager<UsuarioApp> signInManager, ILogger<LoginModel> logger, Data.ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        public UsuarioApp Usuario { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "O campo {0} é requerido")]
            [EmailAddress(ErrorMessage = "O valor digitado não corresponde a um email válido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é requerido")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [Display(Name = "Lembrar de mim")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            returnUrl = returnUrl ?? Url.Content("~/");
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            Usuario = new UsuarioApp();
            Usuario = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            ReturnUrl = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var _usu = _context.Users.Where(x => x.UserName.ToLower().Trim() == Input.Email.ToLower().Trim()).FirstOrDefault();
                    if (_usu != null)
                    {
                        if (!_usu.EmailConfirmed)
                        {
                            ModelState.AddModelError(string.Empty, "Email ainda não foi confirmado.");
                            try { await _signInManager.SignOutAsync(); } catch (System.Exception) { }
                            return Page();
                        }
                        if (_usu.Sistema_AcessoBloqueado)
                        {
                            ModelState.AddModelError(string.Empty, "Estamos analisando as informações da sua conta. Logo menos, entramos em contato. Obrigado pela paciência.");
                            try { await _signInManager.SignOutAsync(); } catch (System.Exception) { }
                            return Page();
                        }
                    }

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Dados de login inválidos.");
                    return Page();
                }
            }

            return Page();
        }
    }
}