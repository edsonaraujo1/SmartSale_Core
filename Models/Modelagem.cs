using System;
using System.ComponentModel.DataAnnotations;
namespace SmartSale.Models
{
    public class SEO_Visita
    {
        [Key]
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;
        public string IdUsu { get; set; }
    }
    public class AppPerfil
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Configuração padrão")]
        public int AppConfiguracoesId { get; set; }
        public AppConfiguracoes AppConfiguracoes { get; set; }

        [Display(Name = "[Instagram] AppId")]
        public string INSTAGRAM_CLIENTID { get; set; }
        [Display(Name = "[Instagram] Redirect")]
        public string INSTAGRAM_REDIRECTURI { get; set; }

        [Display(Name = "[Instagram] UID")]
        public string INSTAGRAM_CONTA_UID { get; set; }
        [Display(Name = "[Instagram] TOKEN")]
        public string INSTAGRAM_CONTA_TOKEN { get; set; }
    }
    public class AppConfiguracoes
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Display(Name = "Permitir a criação de novos usuários?")]
        public bool NovosUsuarios { get; set; }
        [Display(Name = "Somente o Administrador poderá criar novos usuários?")]
        public bool NovosUsuarios_NivelADM { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Descrição do perfil")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Link para a exibição")]
        public string URLExibicao { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Tema")]
        public string Tema { get; set; }
        [Display(Name = "Caminho absoluto do Tema")]
        public string CaminhoAbsolutoTema { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Aplicação")]
        public int AppConfiguracoes_AplicativoId { get; set; }
        public AppConfiguracoes_Aplicativo AppConfiguracoes_Aplicativo { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Azure")]
        public int AppConfiguracoes_AzureId { get; set; }
        public AppConfiguracoes_Azure AppConfiguracoes_Azure { get; set; }
    }
    public class AppConfiguracoes_Aplicativo
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Display(Name = "Ícone do aplicativo")]
        public string LogotipoEmpresa { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Nome da empresa")]
        public string Empresa { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "CPF/CNPJ da empresa")]
        public string CPFCNPJ { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Telefone principal")]
        public string Fone { get; set; }
        [Display(Name = "Telefone recados")]
        public string FoneRecados { get; set; }
        [Display(Name = "Celular")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Email para contato")]
        public string EmailContato { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Email para suporte")]
        public string EmailSuporte { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Email para vendas")]
        public string EmailVendas { get; set; }
    }
    public class AppConfiguracoes_Azure
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Conta")]
        public string _azureblob_AccountName { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Chave")]
        public string _azureblob_AccountKey { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Container")]
        public string _azureblob_ContainerRaiz { get; set; }

        [Display(Name = "Usar simulador de armazenamento local?")]
        public bool UsarSimuladorLocal { get; set; }
        [Display(Name = "Endereço armazenamento local Blob")]
        public string EnderecoArmazLocal { get; set; }

        [Display(Name = "Authoring Key")]
        public string _luis_AuthoringKey { get; set; }
        /// <summary>
        /// WestUS, WestEurope, AustraliaEast 
        /// </summary>
        [Display(Name = "Região")]
        public string _luis_Region { get; set; }
        [Display(Name = "AppId")]
        public string _luis_AppID { get; set; }
    }
    public class GaleriaPerfilAlbum
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Perfil")]
        public int AppPerfilId { get; set; }
        public AppPerfil AppPerfil { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
    public class GaleriaPerfilImagem
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Álbum")]
        public int GaleriaPerfilAlbumId { get; set; }
        public GaleriaPerfilAlbum GaleriaPerfilAlbum { get; set; }

        public string Descricao { get; set; }
        public string Url { get; set; }
    }
    public class LOGCENTRAL
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        public int INVEST_LancamentoId { get; set; }
        public int INVEST_ModeloDocId { get; set; }
        public string UsuarioAppId { get; set; }
        public string URLDOC { get; set; }
        public string TP { get; set; }
        public double VALOR { get; set; }
        public string ACAO { get; set; }
        public string STATUS { get; set; }
    }
    public class LOGACOESUSU
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;
        public string UsuarioAppId { get; set; }
        public string scrollLeft { get; set; }
        public string scrollTop { get; set; }
        public string Url { get; set; }
    }
    public class tblHubConversa
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;
        public string De { get; set; }
        public string DeCnxId { get; set; }
        public string Para { get; set; }
        public string ParaCnxId { get; set; }
        public string Mensagem { get; set; }
        public bool Lida { get; set; }
    }
    public class tblHubCliente
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;
        public string Email { get; set; }
        public string CnxId { get; set; }
        public string Status { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Usuário")]
        public string UsuarioAppId { get; set; }
        public UsuarioApp UsuarioApp { get; set; }
    }
    public class PreferSalaVIP
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Usuário")]
        public string UsuarioAppId { get; set; }
        public UsuarioApp UsuarioApp { get; set; }

        public bool CabecalhoFixo { get; set; }
        public bool BarrasFixas { get; set; }
        public bool TopoFixo { get; set; }
        public bool RodapeFixo { get; set; }
        public bool FormatoCaixa { get; set; }
        public bool MenuFixo { get; set; }

        public string Tema { get; set; }
    }
    public class tblUsuSalaVIP
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;
        public string CnxId { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Usuário")]
        public string UsuarioAppId { get; set; }
        public UsuarioApp UsuarioApp { get; set; }

        public string Status { get; set; }
    }
    public class ContatoPaginaInicial
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }
        public string Mensagem { get; set; }
    }
    public class lp
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHRCRIACAO
        {
            get { return __dthrcriacao ?? DateTime.Now; }
            set { __dthrcriacao = value; }
        }
        private DateTime? __dthrcriacao = null;

        public string chave { get; set; }
        public string titulo { get; set; }
        public string favicon { get; set; }

        public string meta_chaves { get; set; }
        public string htmlcss_estilo { get; set; }
        public string htmlcss_script { get; set; }

        public bool bots_exibir { get; set; }
        public string bots_src { get; set; }
        public string bots_estilo { get; set; }
        public string bots_frame { get; set; }

        public string herotopo_titulo { get; set; }
        public string herotopo_fundo { get; set; }
        public string herotopo_texto { get; set; }
        public string herotopo_botao_hyperlink { get; set; }
        public string herotopo_botao_classe { get; set; }
        public string herotopo_botao_descricao { get; set; }

        public string caracteristicas_titulo { get; set; }

        public string newsletter_titulo { get; set; }
        public string newsletter_fundo { get; set; }
        public string newsletter_botao_hyperlink { get; set; }
        public string newsletter_botao_classe { get; set; }
        public string newsletter_botao_descricao { get; set; }

        public string faq_titulo { get; set; }

        public string contato_titulo_esq { get; set; }
        public string contato_texto_esq { get; set; }
        public string contato_info_esq { get; set; }
        public string contato_titulo_dir { get; set; }
        public string contato_form_dir { get; set; }

        public string calltoact_rodape_titulo { get; set; }
        public string calltoact_rodape_botao_hyperlink { get; set; }
        public string calltoact_rodape_botao_classe { get; set; }
        public string calltoact_rodape_botao_descricao { get; set; }
    }
    public class lp_menus
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHRCRIACAO
        {
            get { return __dthrcriacao ?? DateTime.Now; }
            set { __dthrcriacao = value; }
        }
        private DateTime? __dthrcriacao = null;
        public int Idlp { get; set; }
        //public lp lp { get; set; }
        public int Ordem { get; set; }
        public string Hyperlink { get; set; }
        public string Descricao { get; set; }
    }
    public class dpslinbot
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHRCRIACAO
        {
            get { return __dthrcriacao ?? DateTime.Now; }
            set { __dthrcriacao = value; }
        }
        private DateTime? __dthrcriacao = null;
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
    public class lp_caracteristicas
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHRCRIACAO
        {
            get { return __dthrcriacao ?? DateTime.Now; }
            set { __dthrcriacao = value; }
        }
        private DateTime? __dthrcriacao = null;
        public int Idlp { get; set; }
        //public lp lp { get; set; }
        public int Ordem { get; set; }
        public string Icone { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
    public class lp_FAQ
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHRCRIACAO
        {
            get { return __dthrcriacao ?? DateTime.Now; }
            set { __dthrcriacao = value; }
        }
        private DateTime? __dthrcriacao = null;
        public int Idlp { get; set; }
        //public lp lp { get; set; }
        public int Ordem { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
    }
    public class lp_contatos
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHRCRIACAO
        {
            get { return __dthrcriacao ?? DateTime.Now; }
            set { __dthrcriacao = value; }
        }
        private DateTime? __dthrcriacao = null;
        public int Idlp { get; set; }
        public string CorpoContato { get; set; }
    }

    public class Voucher
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Criado por")]
        public string UsuarioAppId { get; set; }
        public UsuarioApp UsuarioApp { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Email destino")]
        public string EmailDestino { get; set; }

        /// <summary>
        /// NC (Nova conta com acesso limitado por tempo)
        /// </summary>
        [Display(Name = "TIPO")]
        public string TP { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "Expira em")]
        public DateTime DataLimite { get; set; }

        [Display(Name = "Código do Voucher")]
        public string CodigoUnico { get; set; }

        /// <summary>
        /// A,U,X (Aberto, Em Uso, Expirado)
        /// </summary>
        [Display(Name = "STATUS")]
        public string STATUS { get; set; }
    }

    public class OBJLuisApp
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        public string name { get; set; }
        public string description { get; set; }
        public string culture { get; set; }
        public string usageScenario { get; set; }
        public string domain { get; set; }
        public string initialVersionId { get; set; }

        public string IdAppCloud { get; set; }
    }
    public class OBJLuisAppIntent
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "APP LUIS")]
        public int OBJLuisAppId { get; set; }
        public OBJLuisApp OBJLuisApp { get; set; }
        public string name { get; set; }
        public string appVersionId { get; set; }
        public string IdIntent { get; set; }
    }
    public class OBJLuisAppIntentUt
    {
        [Key]
        public int Id { get; set; }
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name = "APP LUIS")]
        public int OBJLuisAppIntentId { get; set; }
        public OBJLuisAppIntent OBJLuisAppIntent { get; set; }
        public string text { get; set; }
    }
}