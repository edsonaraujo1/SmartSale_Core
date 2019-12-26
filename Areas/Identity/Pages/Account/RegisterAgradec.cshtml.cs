using SmartSale.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
namespace SmartSale.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterAgradecModel : PageModel
    {
        private readonly UserManager<UsuarioApp> _userManager;
        public RegisterAgradecModel(UserManager<UsuarioApp> userManager) { _userManager = userManager; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Redirect("/");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Problemas com o usuário ID: '{userId}'.");
            }

            //var result = await _userManager.ConfirmEmailAsync(user, code);
            //if (!result.Succeeded)
            //{
            //    throw new InvalidOperationException($"Não foi confirmado o envio de email para o usuário ID: '{userId}':");
            //}

            return Page();
        }
    }
}