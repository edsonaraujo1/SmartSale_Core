using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSale.Models
{
    public class UsuarioApp : IdentityUser
    {
        public DateTime DTHR
        {
            get { return __dthr ?? DateTime.Now; }
            set { __dthr = value; }
        }
        private DateTime? __dthr = null;
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Formato inválido em {0}")]
        public DateTime? Nascimento { get; set; }
        [Display(Name = "Gênero")]
        public string Genero { get; set; }
        [Display(Name = "Estado civil")]
        public string EstadoCivil { get; set; }
        public string AvatarUsuario { get; set; }
        public string ImagemFundoPerfil { get; set; }
        public string Bio { get; set; }

        [Display(Name = "Tipo de pessoa")]
        public string Documentacao_TPPessoa { get; set; }
        [Display(Name = "CPF")]
        public string Documentacao_CPF { get; set; }
        [Display(Name = "CNPJ")]
        public string Documentacao_CNPJ { get; set; }
        [Display(Name = "RG")]
        public string Documentacao_RG { get; set; }

        [Display(Name = "Empresa")]
        public string Trabalho_Empresa { get; set; }
        [Display(Name = "Cargo")]
        public string Trabalho_Cargo { get; set; }
        [Display(Name = "Departamento")]
        public string Trabalho_Departamento { get; set; }
        [Display(Name = "Gerente")]
        public string Trabalho_Gerente { get; set; }

        [Display(Name = "Logradouro")]
        public string Contato_Logradouro { get; set; }
        [Display(Name = "Complemento")]
        public string Contato_Complemento { get; set; }
        [Display(Name = "Bairro")]
        public string Contato_Bairro { get; set; }
        [Display(Name = "Cidade")]
        public string Contato_Cidade { get; set; }
        [Display(Name = "CEP")]
        public string Contato_CEP { get; set; }
        [Display(Name = "UF")]
        public string Contato_Estado { get; set; }
        [Display(Name = "País")]
        public string Contato_Pais { get; set; }
        [Display(Name = "Endereço escritório")]
        public string Contato_Escritorio { get; set; }
        [Display(Name = "Website")]
        public string Contato_Website { get; set; }
        [Display(Name = "Fone comercial")]
        public string Contato_FoneComercial { get; set; }
        [Display(Name = "Celular")]
        public string Contato_FoneCelular { get; set; }

        [Display(Name = "Banco")]
        public string Financeiro_Banco_Nome { get; set; }
        [Display(Name = "Agência")]
        public string Financeiro_Banco_Ag { get; set; }
        [Display(Name = "Conta corrente")]
        public string Financeiro_Banco_CC { get; set; }

        public string ContatoAutenticacao_Fone { get; set; }
        public string ContatoAutenticacao_Email { get; set; }
        public string ContatoAutenticacao_FoneAlternativo { get; set; }
        [Display(Name = "Email alternativo")]
        public string ContatoAutenticacao_EmailAlternativo { get; set; }

        public string MidiasSociais_Facebook { get; set; }
        public string MidiasSociais_Twitter { get; set; }
        public string MidiasSociais_Instagram { get; set; }
        public string MidiasSociais_GooglePlus { get; set; }
        public string MidiasSociais_Linkedin { get; set; }
        public string MidiasSociais_Youtube { get; set; }
        public string MidiasSociais_Pinterest { get; set; }

        public string Sistema_FuncaoUsuario { get; set; }

        public bool Sistema_AcessoBloqueado { get; set; }

        public bool TrocaSenhaProxLogin { get; set; }

        public int AppConfiguracoesId { get; set; }
        public AppConfiguracoes AppConfiguracoes { get; set; }
    }
}