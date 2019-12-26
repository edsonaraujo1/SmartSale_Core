using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartSale.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
namespace SmartSale.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly IEmailSender _emailSender;
        public IndexModel(UserManager<UsuarioApp> userManager, SignInManager<UsuarioApp> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [Display(Name = "Usuário")]
        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Phone]
            [Display(Name = "Celular")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não pude carregar dados do usuário ID '{_userManager.GetUserId(User)}'.");
            }
            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            Username = userName;
            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber
            };
            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

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
                return NotFound($"Não pude carregar dados do usuário ID '{_userManager.GetUserId(User)}'.");
            }
            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Ocorreu um erro inesperado ao configurar o email para o usuário com o ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Ocorreu um erro inesperado ao definir o número de telefone do usuário com o ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Seu perfil foi atualizado";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
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

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { userId, code }, protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(email, "Confirme seu email", $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");
            StatusMessage = "E-mail de verificação enviado. Por favor verifique seu email.";

            return RedirectToPage();
        }
    }
}