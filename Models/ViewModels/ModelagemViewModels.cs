using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SmartSale.Models.ViewModels
{
    public class Select2BuscaPessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
    public class DropDownItem
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
    }
    public class AppConfig
    {
        public Aplicativo Aplicativo { get; set; }
    }
    public class Aplicativo
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Versao { get; set; }
    }
    public class AppConfig_AppViewModel : AppConfiguracoes_Aplicativo
    {
        [Display(Name = "Imagem Logotipo")]
        public IFormFile App_ImagemLogo { get; set; }
    }
    public class ImagensEnvio : GaleriaPerfilImagem
    {
        public IFormFile Arquivo { get; set; }
    }
    public class UsuarioAppViewModel : UsuarioApp
    {
        //[Display(Name = "Documento digitalizado")]
        //public IFormFile ImagemDocDigitalizado { get; set; }

        [Display(Name = "Foto")]
        public IFormFile ImagemSelfie { get; set; }

        [Required(ErrorMessage = "Campo {0} é requerido")]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua Senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Campo {0} é requerido")]
        [Display(Name = "Li e Aceito os Termos de Uso")]
        public bool Termos { get; set; }
    }
    public class CMSUsuariosViewModel
    {
        public string Id { get; set; }
        public DateTime Criado { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmado { get; set; }
        public bool AcessoBloqueado { get; set; }
        //public string URLDocDigitalizado { get; set; }
        public string URLSelfieDigitalizado { get; set; }
        //public string Financeiro_Investidor_Perfil { get; set; }
    }
    public class DropTema
    {
        public string Chave { get; set; }
        public string Descricao { get; set; }
    }
    public class Calculadora
    {
        public string ChaveId { get; set; }
        public string Titulo { get; set; }
        public bool ExibTitulo { get; set; }
        public string Valor { get; set; }
        public string Lance { get; set; }
        public string ValorMinimoDocs { get; set; }
        public double Reservado { get; set; }
        public int Id { get; set; }
        public int Contador_DataFinal_Ano { get; set; }
        public int Contador_DataFinal_Mes { get; set; }
        public int Contador_DataFinal_Dia { get; set; }
        public int Contador_DataFinal_Hor { get; set; }
        public int Contador_DataFinal_Min { get; set; }
        public int Contador_DataFinal_Seg { get; set; }
    }

    /// <summary>
    /// LANDING PAGES
    /// </summary>
    public class lpROOTViewModel
    {
        public lpViewModel lpViewModel { get; set; }
        public List<lp_menusViewModel> ListaMenus { get; set; }
        public List<lp_caracteristicasViewModel> ListaCaracteristicas { get; set; }
        public List<lp_FAQViewModel> ListaFAQ { get; set; }
    }
    public class lpViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Identificador único")]
        public string chave { get; set; }
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Este campo é requerido")]
        public string titulo { get; set; }
        [Display(Name = "Imagem favicon")]
        public string favicon { get; set; }
        [Display(Name = "META: Lista de Tags")]
        public string meta_chaves { get; set; }
        [Display(Name = "Estilos [CSS]")]
        public string htmlcss_estilo { get; set; }
        [Display(Name = "Scripts [JS]")]
        public string htmlcss_script { get; set; }

        /// <summary>
        /// DOCUMENTACAO DOS BOTS
        /// https://docs.microsoft.com/en-us/azure/bot-service/bot-service-channel-connect-webchat?view=azure-bot-service-3.0#step-1
        /// https://github.com/Microsoft/BotFramework-WebChat/blob/master/README.md
        /// https://github.com/Microsoft/BotBuilder/issues/3087
        /// https://docs.microsoft.com/en-us/azure/bot-service/bot-service-design-pattern-embed-web-site?view=azure-bot-service-4.0
        /// </summary>

        [Display(Name = "BOTS: Ativar")]
        public bool bots_exibir { get; set; }
        [Display(Name = "BOTS: Link Framework")]
        public string bots_src { get; set; }
        [Display(Name = "BOTS: HTML [Style]")]
        public string bots_estilo { get; set; }
        [Display(Name = "BOTS: Frame")]
        public string bots_frame { get; set; }

        [Display(Name = "HERO: Título")]
        public string herotopo_titulo { get; set; }
        [Display(Name = "HERO: Fundo")]
        public string herotopo_fundo { get; set; }
        [Display(Name = "HERO: Texto")]
        public string herotopo_texto { get; set; }
        [Display(Name = "HERO: botão - URL")]
        public string herotopo_botao_hyperlink { get; set; }
        [Display(Name = "HERO: botão - Class")]
        public string herotopo_botao_classe { get; set; }
        [Display(Name = "HERO: botão - Descrição")]
        public string herotopo_botao_descricao { get; set; }

        [Display(Name = "CARACTERÍSTICAS: Título")]
        public string caracteristicas_titulo { get; set; }

        [Display(Name = "FAQ: Título")]
        public string faq_titulo { get; set; }

        [Display(Name = "NEWSLETTER: Título")]
        public string newsletter_titulo { get; set; }
        [Display(Name = "NEWSLETTER: Fundo")]
        public string newsletter_fundo { get; set; }
        [Display(Name = "NEWSLETTER: URL")]
        public string newsletter_botao_hyperlink { get; set; }
        [Display(Name = "NEWSLETTER: Class")]
        public string newsletter_botao_classe { get; set; }
        [Display(Name = "NEWSLETTER: Descrição")]
        public string newsletter_botao_descricao { get; set; }

        [Display(Name = "CONTATO: Lado esquerdo - Título")]
        public string contato_titulo_esq { get; set; }
        [Display(Name = "CONTATO: Lado esquerdo - Texto")]
        public string contato_texto_esq { get; set; }
        [Display(Name = "CONTATO: Lado esquerdo - Informações")]
        public string contato_info_esq { get; set; }
        [Display(Name = "CONTATO: Lado direito - Título")]
        public string contato_titulo_dir { get; set; }
        [Display(Name = "CONTATO: Lado direito - Form")]
        public string contato_form_dir { get; set; }

        [Display(Name = "CALL TO ACTION [Rodapé]: Título")]
        public string calltoact_rodape_titulo { get; set; }
        [Display(Name = "CALL TO ACTION [Rodapé]: Botão - URL")]
        public string calltoact_rodape_botao_hyperlink { get; set; }
        [Display(Name = "CALL TO ACTION [Rodapé]: Botão - Class")]
        public string calltoact_rodape_botao_classe { get; set; }
        [Display(Name = "CALL TO ACTION [Rodapé]: Botão - Descrição")]
        public string calltoact_rodape_botao_descricao { get; set; }

        [Display(Name = "FILE: Hero Fundo")]
        public IFormFile ImagemHeroFundo { get; set; }
        [Display(Name = "FILE: Favicon")]
        public IFormFile ImagemFavicon { get; set; }
        [Display(Name = "FILE: Newsletter")]
        public IFormFile ImagemNewsletterFundo { get; set; }
    }
    /// <summary>
    /// MENUS
    /// <li class="nav-item"><a href="#hero">Home</a></li>
    /// </summary>
    public class lp_menusViewModel
    {
        public int Id { get; set; }
        public int IdlpViewModel { get; set; }
        public lpViewModel lpViewModel { get; set; }
        public int Ordem { get; set; }
        public string Hyperlink { get; set; }
        public string Descricao { get; set; }
    }
    public class lp_caracteristicasViewModel
    {
        public int Id { get; set; }
        public int IdlpViewModel { get; set; }
        public lpViewModel lpViewModel { get; set; }
        public int Ordem { get; set; }
        public string Icone { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
    public class lp_FAQViewModel
    {
        public int Id { get; set; }
        public int IdlpViewModel { get; set; }
        public lpViewModel lpViewModel { get; set; }
        public int Ordem { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
    }

    public class ConversaViewModel : UsuarioApp
    {
        public int conv_id { get; set; }
        public string conv_cnxid { get; set; }
        public DateTime conv_dthr { get; set; }
    }
    public class LUIS_IntentViewModel
    {
        public string intent { get; set; }
        public long score { get; set; }
    }
    public class LUIS_TopScoreIntViewModel
    {
        public string intent { get; set; }
        public long score { get; set; }
    }
    public class LUISViewModel
    {
        public string query { get; set; }
        public LUIS_TopScoreIntViewModel topScoringIntent { get; set; }
        public List<LUIS_IntentViewModel> intents { get; set; }
        public object entities { get; set; }

        /*
        {
          "query": "OLÁ",
          "topScoringIntent": {
            "intent": "BoasVindas",
            "score": 0.952134669
          },
          "intents": [
            {
              "intent": "BoasVindas",
              "score": 0.952134669
            },
            {
              "intent": "Ajuda",
              "score": 0.0220332
            },
            {
              "intent": "None",
              "score": 0.0166839287
            },
            {
              "intent": "QuemSouEu",
              "score": 0.00657591457
            }
          ],

          "entities": []
        } 
        */
    }

    /// <summary>
    /// FRAME PADRAO DE BOTS RENDERIZADOS
    /// 
    /// "userId": "HJmt2u3dZ3v",
    /// "userName": "Visitante",
    /// "botId": "depoisdalinha",
    /// "botIconUrl": "https://docs.botframework.com/static/devportal/client/images/bot-framework-default.png",
    /// "botName": "EndtoEnd",
    /// "secret": "JcrDUU5R8G0.cwA.TOk.CluZuAxUEMcjZYeWTF7030rewomJGbL7hkTH_eIS3hQ",
    /// "iconUrl": "https://docs.botframework.com/static/devportal/client/images/bot-framework-default.png",
    /// "directLineUrl": "https://webchat.botframework.com/v3/directline",
    /// "webSocketEnabled": "true",
    /// "speechTokenEndpoint": "https://api.botframework.com/v3/speechtoken",
    /// "speechEnabled": false,
    /// "cssUrl": "/dwebchat/css/webchat-stable/botchat.css",
    /// "cssFullWindowUrl": "/dwebchat/css/webchat-stable/botchat-fullwindow.css",
    /// "botChatJavaScriptUrl": "/dwebchat/scripts/webchat-stable/botchat.js",
    /// "cognitiveServicesJavaScriptUrl": "/dwebchat/scripts/webchat-stable/CognitiveServices.js" 
    /// 
    /// </summary>
    public class dpslinbotViewModel
    {
        public int Id { get; set; }
        public string TituloPag { get; set; }
        public string TituloChat { get; set; }
        public string faviconPag { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string botId { get; set; }
        public string botIconUrl { get; set; }
        public string botName { get; set; }
        public string secret { get; set; }
        public string iconUrl { get; set; }
        public string directLineUrl { get; set; }
        public string webSocketEnabled { get; set; }
        public string speechTokenEndpoint { get; set; }
        public string speechEnabled { get; set; }
        public string cssUrl { get; set; }
        public string cssFullWindowUrl { get; set; }
        public string botChatJavaScriptUrl { get; set; }
        public string cognitiveServicesJavaScriptUrl { get; set; }
    }

    public class apiLUISViewModel
    {
        public string query { get; set; }
        public apiLUISIntencaoViewModel topScoringIntent { get; set; }
        public List<apiLUISEntidadeViewModel> entities { get; set; }
    }
    public class apiLUISEntidadeViewModel
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public apiLUISEntidadeResolutionViewModel resolution { get; set; }
    }
    public class apiLUISIntencaoViewModel
    {
        public string intent { get; set; }
        public decimal score { get; set; }
    }
    public class apiLUISEntidadeResolutionViewModel
    {
        public List<object> values { get; set; }
    }

    public class ResgatarVoucherViewModelo : Voucher
    {
        public bool EnviarEmail { get; set; }
        public string MensagemRetorno { get; set; }
        public bool EhValido { get; set; }
    }

    public class UserSelfInstaViewModel
    {
        public UserSelfInstaViewCorpoModel data { get; set; }
    }
    public class UserSelfInstaCountsViewModel
    {
        public long media { get; set; }
        public long follows { get; set; }
        public long followed_by { get; set; }
    }
    public class UserSelfInstaViewCorpoModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }
        public string bio { get; set; }
        public string website { get; set; }
        public bool is_business { get; set; }
        public UserSelfInstaCountsViewModel counts { get; set; }
    }
}