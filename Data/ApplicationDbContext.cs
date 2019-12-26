using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartSale.Models;
using System;
using System.Linq;
namespace SmartSale.Data
{
    public class ApplicationDbContext : IdentityDbContext<UsuarioApp>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /* ENTIDADES */
        public DbSet<SEO_Visita> SEO_Visitas { get; set; }
        public DbSet<AppPerfil> AppPerfil { get; set; }
        public DbSet<AppConfiguracoes> AppConfiguracoes { get; set; }
        public DbSet<AppConfiguracoes_Aplicativo> AppConfiguracoes_Aplicativo { get; set; }
        public DbSet<AppConfiguracoes_Azure> AppConfiguracoes_Azure { get; set; }
        public DbSet<GaleriaPerfilAlbum> GaleriaPerfilAlbum { get; set; }
        public DbSet<GaleriaPerfilImagem> GaleriaPerfilImagem { get; set; }
        public DbSet<LOGCENTRAL> LOGCENTRALs { get; set; }
        public DbSet<LOGACOESUSU> LOGACOESUSUs { get; set; }
        public DbSet<tblHubConversa> tblHubConversas { get; set; }
        public DbSet<tblHubCliente> tblHubClientes { get; set; }
        public DbSet<PreferSalaVIP> PreferSalaVIPs { get; set; }
        public DbSet<tblUsuSalaVIP> tblUsuSalaVIPs { get; set; }
        public DbSet<ContatoPaginaInicial> ContatosPaginaInicial { get; set; }
        public DbSet<lp> lp { get; set; }
        public DbSet<lp_menus> lp_menus { get; set; }
        public DbSet<lp_caracteristicas> lp_caracteristicas { get; set; }
        public DbSet<lp_FAQ> lp_FAQ { get; set; }
        public DbSet<dpslinbot> dpslinbot { get; set; }
        public DbSet<lp_contatos> lp_contatos { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<OBJLuisApp> OBJLuisApps { get; set; }
        public DbSet<OBJLuisAppIntent> OBJLuisAppIntents { get; set; }
        public DbSet<OBJLuisAppIntentUt> OBJLuisAppIntentUts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfiguracoes_Aplicativo>().HasIndex(x => new { x.CPFCNPJ }).IsUnique();
            modelBuilder.Entity<AppConfiguracoes_Aplicativo>().HasIndex(x => new { x.Empresa }).IsUnique();
            modelBuilder.Entity<AppConfiguracoes>().HasIndex(x => new { x.Descricao }).IsUnique();
            modelBuilder.Entity<AppConfiguracoes>().HasIndex(x => new { x.URLExibicao }).IsUnique();
            modelBuilder.Entity<AppConfiguracoes>().HasIndex(x => new { x.Descricao, x.URLExibicao, x.AppConfiguracoes_AplicativoId, x.AppConfiguracoes_AzureId }).IsUnique();
            modelBuilder.Entity<AppPerfil>().HasIndex(x => new { x.AppConfiguracoesId }).IsUnique();

            //--------------------------------------------------------------------------------
            // DESABILITA DELETE EM CASCATA
            //--------------------------------------------------------------------------------
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            //    .SelectMany(t => t.GetForeignKeys())
            //    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            //foreach (var fk in cascadeFKs)
            //    fk.DeleteBehavior = DeleteBehavior.Restrict;
            //--------------------------------------------------------------------------------

            base.OnModelCreating(modelBuilder);

            /* ROLES FIXAS */
            const string _roleIdSIS = "59c39ee9-024a-41c1-bae8-7859879aa9f2";
            const string _roleIdADM = "d3f04340-133b-4fe5-964f-725cf3482cbb";
            const string _roleIdSUPERVISOR = "730f160e-5178-4f3c-a955-2c12abc4d1f9";
            const string _roleIdADMCONTA = "a59726df-21c5-44c8-96ba-a4a2244dea3c";
            const string _roleIdAPI = "ced87445-4121-45ff-b05e-6965f5014bc8";
            const string _roleIdMANUTAPP = "8f0a2fbc-ec7d-47e2-b307-0d5085e57275";
            const string _roleIdSUPORTE = "e42f1601-ac85-4384-8fd4-f3c4345b56db";
            const string _roleIdCOMUNIDADE = "c91c9806-bd59-4a8e-ade6-13bc0427602c";
            const string _roleIdFINANCEIRO = "257310a1-a0bf-4625-ae12-a31c2940e5af";
            const string _roleIdMARKETING = "acbfb305-1c76-4a54-8c40-36ff8ac5312e";
            const string _roleIdMIDIAS = "eb7726bc-7c17-446c-a22a-72df87922494";
            const string _roleIdVENDA = "99d47b3f-b8f7-435a-b6a0-7dd2e80f4d30";
            const string _roleIdDESENVOLVEDOR = "16b933cf-e693-47d5-9957-d217467d17a6";
            const string _roleIdUSU = "96c373b4-8cc5-4a63-9f9a-81884a5efe75";

            /* USUÁRIOS FIXOS */
            const string _userIdSIS = "b30b4d21-0f99-435e-acec-a2c672697b0f";
            const string _userIdSISH2I = "b1aadecb-4357-457d-bfea-27e153505497";
            const string _userIdSISH2I_Vitor = "4cb6d3d8-493a-415c-a5d7-cc6860aa8691";
            const string _userIdSISH2I_Gui = "bbf7b44d-faa0-4276-b163-295b780a062c";
            const string _userIdSISH2I_Regis = "2751483e-2c65-4629-84b5-abc20c940c1c";
            const string _userIdSISBOT_E2E = "15421006-d35d-4b4b-8925-3d79defe40b4";
            const string _userIdSISBOT_FIRMA = "0fbbe7b6-54ad-460c-8600-f9d009387f81";

            /* REGISTROS FIXOS DO MÓDULO CONFIGURAÇÕES */
            const int _appconfigAppId = 1;
            const int _appconfigAzureId = 1;
            const int _appconfigId = 1;
            const int _appperfilId = 1;

            //x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x
            // CONFIGURAÇÕES PADRÃO DE CADA APLICATIVO
            //x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x
            //-----------------------------------------
            // APLICATIVO
            //-----------------------------------------
            modelBuilder.Entity<AppConfiguracoes_Aplicativo>()
                .HasData(new AppConfiguracoes_Aplicativo()
                {
                    Bairro = "VILA MAZZEI",
                    Celular = "(11) 9737-4068",
                    CEP = "02311-030",
                    Cidade = "SAO PAULO",
                    Complemento = "",
                    CPFCNPJ = "28.255.469/0001-90",
                    EmailContato = "reginaldo.diniz@grupoendtoend.com.br",
                    EmailSuporte = "reginaldo.diniz@grupoendtoend.com.br",
                    EmailVendas = "reginaldo.diniz@grupoendtoend.com.br",
                    Empresa = "END TO END",
                    Estado = "SP",
                    Fone = "(11) 9737-4068",
                    FoneRecados = "(11) 9737-4068",
                    Id = _appconfigAppId,
                    LogotipoEmpresa = "/images/avatar-e2e.png",
                    Logradouro = "MANUEL VALENTE, 128"
                });
            //-----------------------------------------
            // AZURE
            //-----------------------------------------
            modelBuilder.Entity<AppConfiguracoes_Azure>()
                .HasData(new AppConfiguracoes_Azure()
                {
                    Id = _appconfigAzureId,
                    // ###### RESETAR AQUI /// RESETAR NA NUVEM ###### 
                    // CRIAR UMA CONTA DE ARMAZENAMENTO EM: https://portal.azure.com/ PARA OBTER A CHAVE
                    // DE AUTORIZAÇÃO
                    _azureblob_AccountKey = "nCwjSLQNYeJgO//dFndQDRB3rybEo0egQXCUWmj+b7Oldv9xmaUIb7CAwHl+T08hSewE5H4HCVK3Hhi5WxwkkQ==",
                    // ###### RESETAR AQUI /// RESETAR NA NUVEM ###### 
                    _azureblob_AccountName = "dimweb",
                    _azureblob_ContainerRaiz = "armazseguro",
                    EnderecoArmazLocal = "",
                    UsarSimuladorLocal = false
                });
            //-----------------------------------------
            // CONFIG. PRINCIPAL
            //-----------------------------------------
            modelBuilder.Entity<AppConfiguracoes>()
                .HasData(new AppConfiguracoes()
                {
                    Id = _appconfigId,
                    AppConfiguracoes_AplicativoId = _appconfigAppId,
                    AppConfiguracoes_AzureId = _appconfigAzureId,
                    Descricao = "Tema padrão",
                    Tema = "_Layout",
                    URLExibicao = "padrao",
                    CaminhoAbsolutoTema = "",
                    NovosUsuarios = false,
                    NovosUsuarios_NivelADM = true
                });
            //-----------------------------------------
            // PERFIL PADRAO
            //-----------------------------------------
            modelBuilder.Entity<AppPerfil>()
                .HasData(new AppPerfil()
                {
                    Id = _appperfilId,
                    AppConfiguracoesId = _appconfigId
                });

            /* INSERÇÃO DE ROLES */
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Id = _roleIdSIS, Name = "SIS", NormalizedName = "SIS" },
                new IdentityRole { Id = _roleIdADM, Name = "ADM", NormalizedName = "ADM" },
                new IdentityRole { Id = _roleIdSUPERVISOR, Name = "SUPERVISOR", NormalizedName = "SUPERVISOR" },
                new IdentityRole { Id = _roleIdADMCONTA, Name = "ADMCONTA", NormalizedName = "ADMCONTA" },
                new IdentityRole { Id = _roleIdAPI, Name = "API", NormalizedName = "API" },
                new IdentityRole { Id = _roleIdMANUTAPP, Name = "MANUTAPP", NormalizedName = "MANUTAPP" },
                new IdentityRole { Id = _roleIdSUPORTE, Name = "SUPORTE", NormalizedName = "SUPORTE" },
                new IdentityRole { Id = _roleIdCOMUNIDADE, Name = "COMUNIDADE", NormalizedName = "COMUNIDADE" },
                new IdentityRole { Id = _roleIdFINANCEIRO, Name = "FINANCEIRO", NormalizedName = "FINANCEIRO" },
                new IdentityRole { Id = _roleIdMARKETING, Name = "MARKETING", NormalizedName = "MARKETING" },
                new IdentityRole { Id = _roleIdMIDIAS, Name = "MIDIAS", NormalizedName = "MIDIAS" },
                new IdentityRole { Id = _roleIdVENDA, Name = "VENDA", NormalizedName = "VENDA" },
                new IdentityRole { Id = _roleIdDESENVOLVEDOR, Name = "DESENVOLVEDOR", NormalizedName = "DESENVOLVEDOR" },
                new IdentityRole { Id = _roleIdUSU, Name = "USU", NormalizedName = "USU" });

            /* INSERÇÃO DE USUÁRIOS */
            var hasher = new PasswordHasher<UsuarioApp>();
            modelBuilder.Entity<UsuarioApp>().HasData(
                new UsuarioApp
                {
                    Id = _userIdSIS,
                    AppConfiguracoesId = _appconfigId,
                    Sistema_AcessoBloqueado = false,
                    UserName = "adm@depoisdalinha.com.br",
                    NormalizedUserName = "adm@depoisdalinha.com.br",
                    Email = "adm@depoisdalinha.com.br",
                    NormalizedEmail = "adm@depoisdalinha.com.br",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "S3nh4DepoisDaLinha@010203"),
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/avatar-boodah.jpg",
                    Sistema_FuncaoUsuario = "SISTEMA",
                    Nome = "Depois",
                    Sobrenome = "da Linha",
                    Bio = "Depois da Linha",
                    ContatoAutenticacao_Email = "leandrodiascarvalho@hotmail.com",
                    ContatoAutenticacao_EmailAlternativo = "sistemasolarium@gmail.com",
                    ContatoAutenticacao_Fone = "+5511998814900",
                    ContatoAutenticacao_FoneAlternativo = "+551126917524",
                    Contato_Bairro = "JD. NOSSA SENHORA DO CARMO",
                    Contato_CEP = "08275010",
                    Contato_Cidade = "SÃO PAULO",
                    Contato_Complemento = "SEM COMPLEMENTO",
                    Contato_Escritorio = "RUA MATEUS MENDES PEREIRA, 312, JD. NOSSA SENHORA DO CARMO, SP, 08275-010",
                    Contato_Estado = "SP",
                    Contato_FoneCelular = "+5511998814900",
                    Contato_FoneComercial = "+551126917524",
                    Contato_Logradouro = "RUA MATEUS MENDES PEREIRA, 312",
                    Contato_Pais = "BRASIL",
                    Contato_Website = "https://www.depoisdalinha.com.br",
                    Genero = "MASCULINO",
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    Nascimento = new DateTime(1978, 2, 14),
                    PhoneNumber = "+5511998814900",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "DEPOIS DA LINHA",
                    Trabalho_Cargo = "SEO E DESENVOLVEDOR",
                    Trabalho_Departamento = "GERÊNCIA E DESENVOLVIMENTO",
                    Trabalho_Gerente = "LEANDRO M. CARVALHO",
                    TwoFactorEnabled = false,
                    Documentacao_CNPJ = "14.581.834/0001-42",
                    Documentacao_CPF = "000.000.000-00",
                    Documentacao_RG = "00.000.000-x",
                    Documentacao_TPPessoa = "PF",
                    EstadoCivil = "CASADO",
                    Financeiro_Banco_Nome = "",
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = ""
                },
                new UsuarioApp
                {
                    Id = _userIdSISH2I,
                    AppConfiguracoesId = _appconfigId,
                    Sistema_AcessoBloqueado = false,
                    UserName = "adm@smartsale.com.br",
                    NormalizedUserName = "adm@smartsale.com.br",
                    Email = "adm@smartsale.com.br",
                    NormalizedEmail = "adm@smartsale.com.br",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "S3nh4SmartSale@010203"),
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/avatar-e2e.png",
                    Sistema_FuncaoUsuario = "SISTEMA",
                    Nome = "End",
                    Sobrenome = "To End",
                    Bio = "EndToEnd",
                    ContatoAutenticacao_Email = "reginaldo.diniz@grupoendtoend.com.br",
                    ContatoAutenticacao_EmailAlternativo = "reginaldo.diniz@grupoendtoend.com.br",
                    ContatoAutenticacao_Fone = "+551197374068",
                    ContatoAutenticacao_FoneAlternativo = "+551197374068",
                    Contato_Bairro = "VILA MAZZEI",
                    Contato_CEP = "02311030",
                    Contato_Cidade = "SÃO PAULO",
                    Contato_Complemento = "SEM COMPLEMENTO",
                    Contato_Escritorio = "MANUEL VALENTE, 128, VILA MAZZEI, SP, 02311-030",
                    Contato_Estado = "SP",
                    Contato_FoneCelular = "+551197374068",
                    Contato_FoneComercial = "+551197374068",
                    Contato_Logradouro = "MANUEL VALENTE, 128",
                    Contato_Pais = "BRASIL",
                    Contato_Website = "https://grupoendtoend.com.br/",
                    Genero = "MASCULINO",
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    Nascimento = new System.DateTime(1973, 9, 14),
                    PhoneNumber = "+551197374068",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "ENDTOEND",
                    Trabalho_Cargo = "SEO",
                    Trabalho_Departamento = "GERÊNCIA",
                    Trabalho_Gerente = "REGINALDO DINIZ",
                    TwoFactorEnabled = false,
                    Documentacao_CNPJ = "00.000.000/0000-00",
                    Documentacao_CPF = "000.000.000-00",
                    Documentacao_RG = "00.000.000-0",
                    Documentacao_TPPessoa = "PF",
                    EstadoCivil = "",
                    Financeiro_Banco_Nome = "",
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = "",
                    //Sistema_DeclaracaoCienciaTermos = true,
                    //Sistema_DataDeclaracaoCienciaTermos = DateTime.Now,
                    //Sistema_URLComprovanteResidencia = "",
                    //Sistema_URLDeclaracaoResidencia = "",
                    //Sistema_URLFotoDoc = "",
                    //Sistema_URLSelfieDoc = "",
                    //Financeiro_Investidor_Perfil = "SISTEMA"
                },
                new UsuarioApp
                {
                    Id = _userIdSISH2I_Vitor,
                    AppConfiguracoesId = _appconfigId,
                    Sistema_AcessoBloqueado = false,
                    UserName = "vitor@agenciafirma.com.br",
                    NormalizedUserName = "vitor@agenciafirma.com.br",
                    Email = "vitor@agenciafirma.com.br",
                    NormalizedEmail = "vitor@agenciafirma.com.br",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "S3nh4SmartSale@010203"),
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/vitor.jpg",
                    Sistema_FuncaoUsuario = "SISTEMA",
                    Nome = "Vitor",
                    Sobrenome = "Horvath",
                    Bio = "Agência Firma",
                    ContatoAutenticacao_Email = "vitor@agenciafirma.com.br",
                    ContatoAutenticacao_EmailAlternativo = "vitor@agenciafirma.com.br",
                    ContatoAutenticacao_Fone = "+5511000000000",
                    ContatoAutenticacao_FoneAlternativo = "+5511000000000",
                    Contato_Bairro = "BAIRRO",
                    Contato_CEP = "00000000",
                    Contato_Cidade = "SÃO PAULO",
                    Contato_Complemento = "SEM COMPLEMENTO",
                    Contato_Escritorio = "LOGRADOURO, 1",
                    Contato_Estado = "SP",
                    Contato_FoneCelular = "+5511000000000",
                    Contato_FoneComercial = "+5511000000000",
                    Contato_Logradouro = "LOGRADOURO, 1",
                    Contato_Pais = "BRASIL",
                    Contato_Website = "https://www.agenciafirma.com.br",
                    Genero = "MASCULINO",
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    Nascimento = new DateTime(1978, 2, 14),
                    PhoneNumber = "+5511000000000",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "Agência Firma",
                    Trabalho_Cargo = "",
                    Trabalho_Departamento = "",
                    Trabalho_Gerente = "",
                    TwoFactorEnabled = false,
                    Documentacao_CNPJ = "00.000.000/0000-00",
                    Documentacao_CPF = "000.000.000-00",
                    Documentacao_RG = "00.000.000-0",
                    Documentacao_TPPessoa = "PF",
                    EstadoCivil = "",
                    Financeiro_Banco_Nome = "",
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = "",
                    //Sistema_DeclaracaoCienciaTermos = true,
                    //Sistema_DataDeclaracaoCienciaTermos = DateTime.Now,
                    //Sistema_URLComprovanteResidencia = "",
                    //Sistema_URLDeclaracaoResidencia = "",
                    //Sistema_URLFotoDoc = "",
                    //Sistema_URLSelfieDoc = "",
                    //Financeiro_Investidor_Perfil = "SISTEMA"
                },
                new UsuarioApp
                {
                    Id = _userIdSISH2I_Gui,
                    AppConfiguracoesId = _appconfigId,
                    Sistema_AcessoBloqueado = false,
                    UserName = "guilherme@agenciafirma.com.br",
                    NormalizedUserName = "guilherme@agenciafirma.com.br",
                    Email = "guilherme@agenciafirma.com.br",
                    NormalizedEmail = "guilherme@agenciafirma.com.br",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "S3nh4SmartSale@010203"),
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/guilherme.jpg",
                    Sistema_FuncaoUsuario = "SISTEMA",
                    Nome = "Guilherme",
                    Sobrenome = "Corbo",
                    Bio = "Agência Firma",
                    ContatoAutenticacao_Email = "guilherme@agenciafirma.com.br",
                    ContatoAutenticacao_EmailAlternativo = "guilherme@agenciafirma.com.br",
                    ContatoAutenticacao_Fone = "+5511000000000",
                    ContatoAutenticacao_FoneAlternativo = "+5511000000000",
                    Contato_Bairro = "BAIRRO",
                    Contato_CEP = "00000000",
                    Contato_Cidade = "SÃO PAULO",
                    Contato_Complemento = "SEM COMPLEMENTO",
                    Contato_Escritorio = "LOGRADOURO, 1",
                    Contato_Estado = "SP",
                    Contato_FoneCelular = "+5511000000000",
                    Contato_FoneComercial = "+5511000000000",
                    Contato_Logradouro = "LOGRADOURO, 1",
                    Contato_Pais = "BRASIL",
                    Contato_Website = "https://www.agenciafirma.com.br",
                    Genero = "MASCULINO",
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    Nascimento = new DateTime(1978, 2, 14),
                    PhoneNumber = "+5511000000000",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "Agência Firma",
                    Trabalho_Cargo = "",
                    Trabalho_Departamento = "",
                    Trabalho_Gerente = "",
                    TwoFactorEnabled = false,
                    Documentacao_CNPJ = "00.000.000/0000-00",
                    Documentacao_CPF = "000.000.000-00",
                    Documentacao_RG = "00.000.000-0",
                    Documentacao_TPPessoa = "PF",
                    EstadoCivil = "",
                    Financeiro_Banco_Nome = "",
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = "",
                    //Sistema_DeclaracaoCienciaTermos = true,
                    //Sistema_DataDeclaracaoCienciaTermos = DateTime.Now,
                    //Sistema_URLComprovanteResidencia = "",
                    //Sistema_URLDeclaracaoResidencia = "",
                    //Sistema_URLFotoDoc = "",
                    //Sistema_URLSelfieDoc = "",
                    //Financeiro_Investidor_Perfil = "SISTEMA"
                },
                new UsuarioApp
                {
                    Id = _userIdSISH2I_Regis,
                    AppConfiguracoesId = _appconfigId,
                    Sistema_AcessoBloqueado = false,
                    UserName = "reginaldo@smartsale.com.br",
                    NormalizedUserName = "reginaldo@smartsale.com.br",
                    Email = "reginaldo@smartsale.com.br",
                    NormalizedEmail = "reginaldo@smartsale.com.br",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "S3nh4SmartSale@010203"),
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/regis.jpg",
                    Sistema_FuncaoUsuario = "SISTEMA",
                    Nome = "Reginaldo",
                    Sobrenome = "Diniz",
                    Bio = "EndToEnd",
                    ContatoAutenticacao_Email = "reginaldo.diniz@grupoendtoend.com.br",
                    ContatoAutenticacao_EmailAlternativo = "reginaldo.diniz@grupoendtoend.com.br",
                    ContatoAutenticacao_Fone = "+551197374068",
                    ContatoAutenticacao_FoneAlternativo = "+551197374068",
                    Contato_Bairro = "VILA MAZZEI",
                    Contato_CEP = "02311030",
                    Contato_Cidade = "SÃO PAULO",
                    Contato_Complemento = "SEM COMPLEMENTO",
                    Contato_Escritorio = "MANUEL VALENTE, 128, VILA MAZZEI, SP, 02311-030",
                    Contato_Estado = "SP",
                    Contato_FoneCelular = "+551197374068",
                    Contato_FoneComercial = "+551197374068",
                    Contato_Logradouro = "MANUEL VALENTE, 128",
                    Contato_Pais = "BRASIL",
                    Contato_Website = "https://grupoendtoend.com.br/",
                    Genero = "MASCULINO",
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    Nascimento = new System.DateTime(1973, 9, 14),
                    PhoneNumber = "+551197374068",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "ENDTOEND",
                    Trabalho_Cargo = "SEO",
                    Trabalho_Departamento = "GERÊNCIA",
                    Trabalho_Gerente = "REGINALDO DINIZ",
                    TwoFactorEnabled = false,
                    Documentacao_CNPJ = "00.000.000/0000-00",
                    Documentacao_CPF = "000.000.000-00",
                    Documentacao_RG = "00.000.000-0",
                    Documentacao_TPPessoa = "PF",
                    EstadoCivil = "",
                    Financeiro_Banco_Nome = "",
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = ""
                },
                new UsuarioApp
                {
                    Id = _userIdSISBOT_E2E,
                    AppConfiguracoesId = _appconfigId,
                    Sistema_AcessoBloqueado = false,
                    UserName = "bote2e@endtoend.com.br",
                    NormalizedUserName = "bote2e@endtoend.com.br",
                    Email = "bote2e@endtoend.com.br",
                    NormalizedEmail = "bote2e@endtoend.com.br",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "S3nh4Bot@010203"),
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/bot-e2e-avatar-chat.png",
                    Sistema_FuncaoUsuario = "BOT",
                    Nome = "Bot",
                    Sobrenome = "E2E",
                    Bio = "Ser ou não ser, eis a questão",
                    ContatoAutenticacao_Email = "bote2e@endtoend.com.br",
                    ContatoAutenticacao_EmailAlternativo = "bote2e@endtoend.com.br",
                    ContatoAutenticacao_Fone = "+5511000000000",
                    ContatoAutenticacao_FoneAlternativo = "+5511000000000",
                    Contato_Bairro = "",
                    Contato_CEP = "",
                    Contato_Cidade = "SÃO PAULO",
                    Contato_Complemento = "",
                    Contato_Escritorio = "",
                    Contato_Estado = "SP",
                    Contato_FoneCelular = "+5511000000000",
                    Contato_FoneComercial = "+5511000000000",
                    Contato_Logradouro = "",
                    Contato_Pais = "BRASIL",
                    Contato_Website = "http://www.endtoend.com.br",
                    Genero = "INDEFINIDO",
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    Nascimento = new DateTime(1978, 2, 14),
                    PhoneNumber = "+5511000000000",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "END TO END",
                    Trabalho_Cargo = "ROBÔ",
                    Trabalho_Departamento = "ATENDIMENTO",
                    Trabalho_Gerente = "REGINALDO DINIZ",
                    TwoFactorEnabled = false,
                    Documentacao_CNPJ = "00.000.000/0000-00",
                    Documentacao_CPF = "000.000.000-00",
                    Documentacao_RG = "00.000.000-0",
                    Documentacao_TPPessoa = "NP",
                    EstadoCivil = "SOLTEIRO",
                    Financeiro_Banco_Nome = "",
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = "",
                    //Sistema_DeclaracaoCienciaTermos = true,
                    //Sistema_DataDeclaracaoCienciaTermos = DateTime.Now,
                    //Sistema_URLComprovanteResidencia = "",
                    //Sistema_URLDeclaracaoResidencia = "",
                    //Sistema_URLFotoDoc = "",
                    //Sistema_URLSelfieDoc = "",
                    //Financeiro_Investidor_Perfil = "BOT"
                },
                new UsuarioApp
                {
                    Id = _userIdSISBOT_FIRMA,
                    AppConfiguracoesId = _appconfigId,
                    Sistema_AcessoBloqueado = false,
                    UserName = "botfirma@endtoend.com.br",
                    NormalizedUserName = "botfirma@endtoend.com.br",
                    Email = "botfirma@endtoend.com.br",
                    NormalizedEmail = "botfirma@endtoend.com.br",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "S3nh4Bot@010203"),
                    SecurityStamp = string.Empty,
                    AvatarUsuario = "/images/bot-firma-avatar-chat.png",
                    Sistema_FuncaoUsuario = "BOT",
                    Nome = "Bot",
                    Sobrenome = "Firma",
                    Bio = "Ser ou não ser, eis a questão",
                    ContatoAutenticacao_Email = "botfirma@endtoend.com.br",
                    ContatoAutenticacao_EmailAlternativo = "botfirma@endtoend.com.br",
                    ContatoAutenticacao_Fone = "+5511998814900",
                    ContatoAutenticacao_FoneAlternativo = "+551126917524",
                    Contato_Bairro = "",
                    Contato_CEP = "",
                    Contato_Cidade = "SÃO PAULO",
                    Contato_Complemento = "",
                    Contato_Escritorio = "",
                    Contato_Estado = "SP",
                    Contato_FoneCelular = "+5511000000000",
                    Contato_FoneComercial = "+5511000000000",
                    Contato_Logradouro = "",
                    Contato_Pais = "BRASIL",
                    Contato_Website = "https://www.depoisdalinha.com.br",
                    Genero = "INDEFINIDO",
                    ImagemFundoPerfil = "/images/fundo_padrao_perfil.png",
                    MidiasSociais_Facebook = "",
                    MidiasSociais_GooglePlus = "",
                    MidiasSociais_Instagram = "",
                    MidiasSociais_Linkedin = "",
                    MidiasSociais_Pinterest = "",
                    MidiasSociais_Twitter = "",
                    MidiasSociais_Youtube = "",
                    Nascimento = new DateTime(1978, 2, 14),
                    PhoneNumber = "+555511000000000",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    Trabalho_Empresa = "FIRMA",
                    Trabalho_Cargo = "ROBÔ",
                    Trabalho_Departamento = "ATENDIMENTO",
                    Trabalho_Gerente = "FIRMA",
                    TwoFactorEnabled = false,
                    Documentacao_CNPJ = "00.000.000/0000-00",
                    Documentacao_CPF = "000.000.000-00",
                    Documentacao_RG = "00.000.000-0",
                    Documentacao_TPPessoa = "NP",
                    EstadoCivil = "SOLTEIRO",
                    Financeiro_Banco_Nome = "",
                    Financeiro_Banco_Ag = "",
                    Financeiro_Banco_CC = "",
                    //Sistema_DeclaracaoCienciaTermos = true,
                    //Sistema_DataDeclaracaoCienciaTermos = DateTime.Now,
                    //Sistema_URLComprovanteResidencia = "",
                    //Sistema_URLDeclaracaoResidencia = "",
                    //Sistema_URLFotoDoc = "",
                    //Sistema_URLSelfieDoc = "",
                    //Financeiro_Investidor_Perfil = "BOT"
                });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string> { RoleId = _roleIdSIS, UserId = _userIdSIS },
                new IdentityUserRole<string> { RoleId = _roleIdSIS, UserId = _userIdSISH2I },
                new IdentityUserRole<string> { RoleId = _roleIdSIS, UserId = _userIdSISH2I_Vitor },
                new IdentityUserRole<string> { RoleId = _roleIdSIS, UserId = _userIdSISH2I_Gui },
                new IdentityUserRole<string> { RoleId = _roleIdSIS, UserId = _userIdSISH2I_Regis },
                new IdentityUserRole<string> { RoleId = _roleIdSIS, UserId = _userIdSISBOT_E2E },
                new IdentityUserRole<string> { RoleId = _roleIdSIS, UserId = _userIdSISBOT_FIRMA });
        }
    }
}