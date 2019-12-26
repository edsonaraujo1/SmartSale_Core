using SmartSale.Models;
using SmartSale.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
namespace SmartSale.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly Data.ApplicationDbContext _context;
        public RegisterModel(UserManager<UsuarioApp> userManager, SignInManager<UsuarioApp> signInManager, ILogger<RegisterModel> logger, IEmailSender emailSender, Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public UsuarioAppViewModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            //-------------------------------------------------------
            // Só para testes
            //-------------------------------------------------------
            //Input = new UsuarioAppViewModel();
            //Input.Email = "leandrodiascarvalho@hotmail.com";
            //Input.Password = "Senha@010203";
            //Input.ConfirmPassword = "Senha@010203";
            //Input.Contato_FoneCelular = "11998814900";
            //Input.ContatoAutenticacao_EmailAlternativo = "depoisdalinha@outlook.com";
            //Input.Termos = true;
            //Input.Nome = "Leandro";
            //Input.Sobrenome = "Leandro";
            //Input.Nascimento = new DateTime(1978, 2, 14, 12, 30, 0);
            //Input.Documentacao_CPF = "21595484809";
            //Input.Documentacao_CNPJ = "14581834000142";
            //Input.Documentacao_RG = "30.017.827-x";
            //Input.Contato_CEP = "08275010";
            //Input.Contato_Logradouro = "Rua Mateus Mendes Pereira";
            //Input.Contato_Bairro = "Jardim Nossa Senhora do Carmo";
            //Input.Contato_Cidade = "São Paulo";
            //Input.Contato_Complemento = "SEM COMPLEMENTO";
            //Input.Contato_Estado = "SP";
            //-------------------------------------------------------

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            var _erroValid = false;

            try
            {
                if (!Input.Termos)
                {
                    ModelState.AddModelError(string.Empty, "Leia e Aceite os Termos de Uso antes de prosseguir");
                    _erroValid = true;
                }
                if (string.IsNullOrEmpty(Input.Email))
                {
                    ModelState.AddModelError(string.Empty, "Preencha o campo EMAIL");
                    _erroValid = true;
                }
                if (string.IsNullOrEmpty(Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Preencha o campo SENHA");
                    _erroValid = true;
                }
                if (string.IsNullOrEmpty(Input.ConfirmPassword))
                {
                    ModelState.AddModelError(string.Empty, "Preencha o campo CONFIRME SUA SENHA");
                    _erroValid = true;
                }
                if (string.IsNullOrEmpty(Input.Nome))
                {
                    ModelState.AddModelError(string.Empty, "Preencha o campo NOME");
                    _erroValid = true;
                }
                if (string.IsNullOrEmpty(Input.Sobrenome))
                {
                    ModelState.AddModelError(string.Empty, "Preencha o campo SOBRENOME");
                    _erroValid = true;
                }
                if (Convert.ToDateTime(Input.Nascimento).Year == 0001)
                {
                    ModelState.AddModelError(string.Empty, "Preencha o campo DATA DE NASCIMENTO");
                    _erroValid = true;
                }
                //if (Input.ImagemDocDigitalizado == null)
                //{
                //    ModelState.AddModelError(string.Empty, "Necessário o envio de um documento com sua foto");
                //    _erroValid = true;
                //}

                if (_erroValid)
                    return Page();

                var _config = _context.AppPerfil
                    .Include(x => x.AppConfiguracoes)
                    .Include(x => x.AppConfiguracoes.AppConfiguracoes_Aplicativo)
                    .Include(y => y.AppConfiguracoes.AppConfiguracoes_Azure)
                    .FirstOrDefault();

                //if (Input.ImagemDocDigitalizado != null)
                //{
                //    var _imagemLogotipo =
                //        await VerificadoresRetornos
                //        .EnviarImagemAzure(Input.ImagemDocDigitalizado, 502000, 0, 0, _config.AppConfiguracoes.AppConfiguracoes_Azure._azureblob_AccountName, _config.AppConfiguracoes.AppConfiguracoes_Azure._azureblob_AccountKey, _config.AppConfiguracoes.AppConfiguracoes_Azure._azureblob_ContainerRaiz);

                //    if (_imagemLogotipo.ToLower().Trim().Contains("blob.core.windows.net"))
                //    {
                //        Input.Sistema_URLFotoDoc = _imagemLogotipo;
                //    }
                //}

                //if (Input.ImagemSelfie != null)
                //{
                //    var _imagemLogotipo =
                //        await VerificadoresRetornos
                //        .EnviarImagemAzure(Input.ImagemSelfie, 502000, 0, 0, _config.AppConfiguracoes.AppConfiguracoes_Azure._azureblob_AccountName, _config.AppConfiguracoes.AppConfiguracoes_Azure._azureblob_AccountKey, _config.AppConfiguracoes.AppConfiguracoes_Azure._azureblob_ContainerRaiz);
                //
                //    if (_imagemLogotipo.ToLower().Trim().Contains("blob.core.windows.net"))
                //    {
                //        Input.AvatarUsuario = _imagemLogotipo;
                //    }
                //}

                var user = new UsuarioApp
                {
                    AppConfiguracoesId = _context.AppConfiguracoes.FirstOrDefault().Id,
                    Nome = Input.Nome,
                    Sobrenome = Input.Sobrenome,
                    Nascimento = Input.Nascimento,
                    UserName = Input.Email,
                    NormalizedUserName = Input.Email,
                    Email = Input.Email,
                    NormalizedEmail = Input.Email,
                    EmailConfirmed = false,
                    PasswordHash = Input.Password,
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/avatar-depois-da-linha.png",
                    Sistema_FuncaoUsuario = "USUARIO",
                    Bio = "Olá mundo",
                    ContatoAutenticacao_Email = Input.Email,
                    ContatoAutenticacao_EmailAlternativo = Input.ContatoAutenticacao_EmailAlternativo,
                    ContatoAutenticacao_Fone = Input.Contato_FoneCelular,
                    ContatoAutenticacao_FoneAlternativo = Input.Contato_FoneComercial,
                    Contato_Bairro = Input.Contato_Bairro,
                    Contato_CEP = Input.Contato_CEP,
                    Contato_Cidade = Input.Contato_Cidade,
                    Contato_Complemento = Input.Contato_Complemento,
                    Contato_Escritorio = Input.Contato_Escritorio,
                    Contato_Estado = Input.Contato_Estado,
                    Contato_FoneCelular = Input.Contato_FoneCelular,
                    Contato_FoneComercial = Input.Contato_FoneComercial,
                    Contato_Logradouro = Input.Contato_Logradouro,
                    Contato_Pais = "BRASIL",
                    Contato_Website = Input.Contato_Website,
                    Genero = Input.Genero,
                    EstadoCivil = Input.EstadoCivil,
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    Documentacao_CNPJ = Input.Documentacao_CNPJ,
                    Documentacao_CPF = Input.Documentacao_CPF,
                    Documentacao_RG = Input.Documentacao_RG,
                    Documentacao_TPPessoa = Input.Documentacao_TPPessoa,
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = "",
                    Financeiro_Banco_Nome = "",
                    Sistema_AcessoBloqueado = true,
                    //Sistema_DataDeclaracaoCienciaTermos = DateTime.Now,
                    //Sistema_DeclaracaoCienciaTermos = Input.Termos,
                    //Sistema_URLComprovanteResidencia = "",
                    //Sistema_URLDeclaracaoResidencia = "",
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    PhoneNumber = Input.Contato_FoneCelular,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "NÃO INFORMADO",
                    Trabalho_Cargo = "NÃO INFORMADO",
                    Trabalho_Departamento = "NÃO INFORMADO",
                    Trabalho_Gerente = "NÃO INFORMADO",
                    TwoFactorEnabled = false,
                    //Sistema_URLFotoDoc = Input.Sistema_URLFotoDoc,
                    //Sistema_URLSelfieDoc = Input.Sistema_URLSelfieDoc,
                    //Financeiro_Investidor_Perfil = Input.Financeiro_Investidor_Perfil
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "USU");
                    _logger.LogInformation("Usuário criado.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code },
                        protocol: Request.Scheme);

                    var _enviou =
                        await VerificadoresRetornos
                        .EnviarEmail(user.Email, callbackUrl, "[CONFIRME SEU EMAIL] SmartSale", string.Format($"Olá!!!{Environment.NewLine}Falta pouco. Por favor, confirme o seu email clicando <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='color:#000 !important;'>AQUI</a> e prove que vc não é um robô."));

                    // LOGCENTRAL
                    _context.LOGCENTRALs.Add(new LOGCENTRAL()
                    {
                        ACAO = "ENVIO EMAIL - CONFIRME SEU EMAIL",
                        INVEST_LancamentoId = 0,
                        INVEST_ModeloDocId = 0,
                        TP = "",
                        URLDOC = "",
                        VALOR = 0,
                        STATUS = "",
                        UsuarioAppId = user.Id
                    });
                    await _context.SaveChangesAsync();

                    if (_enviou == "ok")
                        return Redirect("/Identity/Account/RegisterAgradec?userId=" + user.Id + "&code=" + code);
                    else
                    {
                        ModelState.AddModelError(string.Empty, _enviou);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception erro)
            {
                //ModelState.AddModelError(string.Empty, erro.Message);

                return Redirect("/Identity/Account/RegisterAgradec?userId=3&code=1");
            }

            return Page();
        }
    }
}
