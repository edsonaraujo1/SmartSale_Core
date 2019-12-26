using SmartSale.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSale.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly Data.ApplicationDbContext _context;
        public ConfirmEmailModel(Data.ApplicationDbContext context, UserManager<UsuarioApp> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Redirect("/");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            // LOGCENTRAL
            _context.LOGCENTRALs.Add(new LOGCENTRAL()
            {
                ACAO = "ENVIO EMAIL - EMAIL CONFIRMADO",
                INVEST_LancamentoId = 0,
                INVEST_ModeloDocId = 0,
                TP = "",
                URLDOC = "",
                VALOR = 0,
                STATUS = "",
                UsuarioAppId = user.Id
            });
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
