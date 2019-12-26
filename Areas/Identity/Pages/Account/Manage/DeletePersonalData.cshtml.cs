using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartSale.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
namespace SmartSale.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;

        public DeletePersonalDataModel(
            UserManager<UsuarioApp> userManager,
            SignInManager<UsuarioApp> signInManager,
            ILogger<DeletePersonalDataModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} é requerido")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar dados do usuário ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar dados do usuário ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Senha incorreta.");
                    return Page();
                }
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Ocorreu um erro inesperado ao excluir o usuário com o ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("Usuário com ID '{UserId}' foi excluído.", userId);

            return Redirect("~/");
        }
    }
}