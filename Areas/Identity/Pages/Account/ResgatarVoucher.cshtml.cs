using SmartSale.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using SmartSale.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace SmartSale.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResgatarVoucherModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;
        public ResgatarVoucherModel(SignInManager<UsuarioApp> signInManager, UserManager<UsuarioApp> userManager, Data.ApplicationDbContext context) { _signInManager = signInManager; _userManager = userManager; _context = context; }

        [BindProperty]
        public ResgatarVoucherViewModelo ResgatarVoucher { get; set; }
        public async Task<IActionResult> OnGet(string u, string p, string c)
        {
            var _mensretorno = "";
            ResgatarVoucher = new ResgatarVoucherViewModelo();

            // SEM PARÂMETROS
            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p) || string.IsNullOrEmpty(c))
            {
                _mensretorno += "";
                //ResgatarVoucher.MensagemRetorno = "Dados inválidos";
                return Page();
            }

            // USUÁRIO AUTENTICADO?
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                //return Redirect("/");
            }

            // JÁ EXISTE USUÁRIO?
            if (_context.Users.FirstOrDefault(x => x.UserName == p) != null)
                return Redirect("/");

            var _pegavoucher =
                _context.Vouchers
                .Where(x => x.UsuarioAppId == u && x.EmailDestino == p && x.CodigoUnico == c && x.STATUS == "A")
                .FirstOrDefault();

            if (_pegavoucher == null)
            {
                ResgatarVoucher.MensagemRetorno = "Esse Voucher não existe em nossa plataforma, ou, já está em uso.";
            }
            else
            {
                if (_pegavoucher.DataLimite < DateTime.Now || _pegavoucher.STATUS != "A")
                {
                    ResgatarVoucher.MensagemRetorno = "Esse Voucher não existe em nossa plataforma, ou, já está em uso.";
                }

                ResgatarVoucher.EhValido = true;

                // CRIA CONTA
                var _config = _context.AppPerfil
                    .Include(x => x.AppConfiguracoes)
                    .Include(x => x.AppConfiguracoes.AppConfiguracoes_Aplicativo)
                    .Include(y => y.AppConfiguracoes.AppConfiguracoes_Azure)
                    .FirstOrDefault();

                var _novaSenha = VerificadoresRetornos.GerarSenha(8);

                var user = new UsuarioApp
                {
                    AppConfiguracoesId = _context.AppConfiguracoes.FirstOrDefault().Id,
                    Nome = "Usuário",
                    Sobrenome = "",
                    Nascimento = new System.DateTime(1978, 2, 14),
                    UserName = _pegavoucher.EmailDestino,
                    NormalizedUserName = _pegavoucher.EmailDestino,
                    Email = _pegavoucher.EmailDestino,
                    NormalizedEmail = _pegavoucher.EmailDestino,
                    EmailConfirmed = true,
                    PasswordHash = _novaSenha,
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/sem-imagem.png",
                    Sistema_FuncaoUsuario = "USUARIO",
                    Bio = "Olá mundo",
                    ContatoAutenticacao_Email = _pegavoucher.EmailDestino,
                    ContatoAutenticacao_EmailAlternativo = _pegavoucher.EmailDestino,
                    ContatoAutenticacao_Fone = "00123456789",
                    ContatoAutenticacao_FoneAlternativo = "00123456789",
                    Contato_Bairro = "NI",
                    Contato_CEP = "00000-000",
                    Contato_Cidade = "NI",
                    Contato_Complemento = "NI",
                    Contato_Escritorio = "NI",
                    Contato_Estado = "NI",
                    Contato_FoneCelular = "",
                    Contato_FoneComercial = "00123456789",
                    Contato_Logradouro = "NI",
                    Contato_Pais = "BRASIL",
                    Contato_Website = "",
                    Genero = "NI",
                    EstadoCivil = "NI",
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    Documentacao_CNPJ = "",
                    Documentacao_CPF = "",
                    Documentacao_RG = "",
                    Documentacao_TPPessoa = "PF",
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = "",
                    Financeiro_Banco_Nome = "",
                    Sistema_AcessoBloqueado = false,
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    PhoneNumber = "00123456789",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "NÃO INFORMADO",
                    Trabalho_Cargo = "NÃO INFORMADO",
                    Trabalho_Departamento = "NÃO INFORMADO",
                    Trabalho_Gerente = "NÃO INFORMADO",
                    TwoFactorEnabled = false,
                    TrocaSenhaProxLogin = true
                };

                var result = await _userManager.CreateAsync(user, _novaSenha);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "USU");

                    // CONTA CRIADA? ALTERA O STATUS DO VOUCHER
                    _pegavoucher.STATUS = "U";
                    _context.Attach(_pegavoucher).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    // FAÇO LOGIN
                    await _signInManager.PasswordSignInAsync(_pegavoucher.EmailDestino, _novaSenha, false, lockoutOnFailure: true);

                    //_mensretorno += "<b style='color:green;font-size:20px;margin-top:35px;'>";
                    //_mensretorno += "Muito bom!!! Sua conta foi criada com sucesso.";
                    //_mensretorno += "</b>";
                    //_mensretorno += "<div style='color:grey;font-size:16px;color:#000;margin:10px auto;'>";
                    //_mensretorno += "Aproveite ao máximo o seu tempo de avaliação em nossa plataforma.<br>Ahhhh... O tempo de avaliação termina em: <b>" + _pegavoucher.DataLimite.ToLongDateString() + " às " + _pegavoucher.DataLimite.ToLongTimeString() + "</b><br><br>";
                    //_mensretorno += "Clique <a href='/Correio/Conversas' class=''>AQUI</a> para acessar nosso Chat.<br>";
                    //_mensretorno += "</div>";
                    //_mensretorno += "<b style='color:red;font-size:25px;margin-top:35px;'>";
                    //_mensretorno += "MUITO IMPORTANTE: A SENHA ABAIXO É PROVISÓRIA.<br>ALTERE-A AGORA <a href='/Identity/Account/Manage/ChangePassword'>CLICANDO AQUI</a>";
                    //_mensretorno += "</b>";
                    //_mensretorno += "<br>";
                    //_mensretorno += $"<b style='color:grey !important;font-size:70px !important;font-weight:lighter !important;'>{_novaSenha}";
                    //_mensretorno += "</b>";
                    //_mensretorno += "<br>";
                    //ResgatarVoucher.MensagemRetorno = _mensretorno;

                    _mensretorno += "<b style='color:green;font-size:30px;margin-top:45px;display:block;'>";
                    _mensretorno += "Sua conta foi criada com sucesso.";
                    _mensretorno += "</b>";
                    _mensretorno += "<div style='color:grey;font-size:16px;color:#000;margin:10px auto;'>";
                    _mensretorno += "Aproveite ao máximo o seu tempo de avaliação em nossa plataforma.<br>Ahhhh... O tempo de avaliação termina em: <b>" + _pegavoucher.DataLimite.ToLongDateString().ToUpper() + " às " + _pegavoucher.DataLimite.ToLongTimeString() + "</b>";
                    _mensretorno += "</div>";
                    _mensretorno += "<b style='color:red;font-size:18px;margin-top:35px;'>";
                    _mensretorno += "MUITO IMPORTANTE: A SENHA ABAIXO É PROVISÓRIA.<br>COPIE A SENHA PROVISÓRIA E CRIE UMA NOVA SENHA <a href='/Identity/Account/Manage/ChangePassword'>CLICANDO AQUI</a>";
                    _mensretorno += "</b>";
                    _mensretorno += "<br>";
                    _mensretorno += $"<b style='color:grey !important;font-size:70px !important;font-weight:lighter !important;margin:15px auto;display:block;'>{_novaSenha}";
                    _mensretorno += "</b>";
                    _mensretorno += "<br>";

                    ResgatarVoucher.MensagemRetorno = _mensretorno;
                }
            }

            return Page();
        }
    }
}