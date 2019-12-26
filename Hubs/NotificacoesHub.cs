using SmartSale.Data;
using SmartSale.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SmartSale.Hubs
{
    public class ClienteAtivo
    {
        public string Id { get; set; }
        public string IdCNX { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Avatar { get; set; }
        public string Empresa { get; set; }
    }

    //[Authorize]
    public class NotificacoesHub : Hub
    {
        public static HashSet<string> ConexoesAtivas = new HashSet<string>();
        public static List<ClienteAtivo> ClientesAtivos = new List<ClienteAtivo>();
        private readonly ApplicationDbContext _context;
        public NotificacoesHub(ApplicationDbContext context) { _context = context; }

        public override async Task OnConnectedAsync()
        {
            var _contextoConexao = Context;
            var _quem =
                _context.Users
                .FirstOrDefault(x => x.UserName == _contextoConexao.User.Identity.Name);

            await Quem("ENTROU", _quem.Id);

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var _contextoConexao = Context;
            var _quem =
                _context.Users
                .FirstOrDefault(x => x.UserName == _contextoConexao.User.Identity.Name);

            await Quem("SAIU", _quem.Id);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task Quem(string status, string idusu)
        {
            try
            {
                var _usu =
                    _context.Users
                    .Where(x => x.Id == idusu)
                    .FirstOrDefault();

                if (_usu.Sistema_FuncaoUsuario != "BOT")
                {
                    //----------------------
                    // LOG
                    //----------------------
                    var _novocliente = new tblHubCliente()
                    {
                        CnxId = Context.ConnectionId,
                        Email = _usu.Email,
                        Status = status,
                        UsuarioAppId = _usu.Id
                    };
                    _context.tblHubClientes.Add(_novocliente);
                    await _context.SaveChangesAsync();

                    //-----------------------------------------------
                    // **** ESSA TASK SERVE PARA "ENTROU/SAIU"
                    // LIMPA ANTES DE CRIAR / LIMPA A SALA AO SAIR!!!
                    //-----------------------------------------------
                    var _limpa =
                        _context.tblUsuSalaVIPs
                        .Where(x => x.UsuarioAppId == _usu.Id);

                    _context.tblUsuSalaVIPs.RemoveRange(_limpa);
                    await _context.SaveChangesAsync();

                    //------------------------
                    // ALTERA FLAG: TÁ ONLINE? 
                    //------------------------
                    if (status.ToLower().Trim() == "entrou")
                    {
                        //----------------------
                        // TÁ ONLINE?
                        //----------------------
                        var _novoususalavip = new tblUsuSalaVIP()
                        {
                            CnxId = Context.ConnectionId,
                            UsuarioAppId = _usu.Id
                        };
                        _context.tblUsuSalaVIPs.Add(_novoususalavip);
                        await _context.SaveChangesAsync();
                    }

                    // cnxid, de, para, mens, status
                    //await Clients
                    //    .AllExcept(Context.ConnectionId)
                    //    .SendAsync
                    //    (
                    //        "quemsoueu",
                    //        Context.ConnectionId,
                    //        idusu,
                    //        "",
                    //        _usu.Nome + " " + _usu.Sobrenome,
                    //        status
                    //    );

                    await Clients.All.SendAsync("quemsoueu", Context.ConnectionId, idusu, _usu.Id, _usu.Nome + " " + _usu.Sobrenome, status);
                    //await Clients.All.SendAsync("listaclienteschat", _usu.Id);
                }
            }
            catch (Exception erro)
            {
                string _erro = erro.Message;
            }
        }
        public async Task EnviarMensagem(string de, string para, string mens)
        {
            await Clients.All.SendAsync("enviarmens", Context.ConnectionId, de, para, mens);
            //await Clients.Client(para).SendAsync("enviarmens", Context.ConnectionId, de, para, mens);
        }
        public async Task QuadroMens(string para, string mens)
        {
            await Clients.Client(para).SendAsync("quadromens", Context.ConnectionId, para.Replace("@", "").Replace(".", ""), mens);
            //await Clients.Client(para).SendAsync("enviarmens", Context.ConnectionId, de, para, mens);
        }
        public async Task ForceLogoff(string para, string mens)
        {
            await Clients.Client(para).SendAsync("forcelogoff", Context.ConnectionId, para.Replace("@", "").Replace(".", ""), mens);
            //await Clients.Client(para).SendAsync("enviarmens", Context.ConnectionId, de, para, mens);
        }
        public async Task Teclando(string de, string para)
        {
            await Clients.All.SendAsync("teclando", de, para);
        }

        public async Task Teste(string de, string para, string mens, string cnxde, string cnxpara)
        {
            //var _cnxid = "";
            try
            {
                //var _pegacnxpara = await _context.tblUsuSalaVIPs.FirstOrDefaultAsync(x => x.UsuarioAppId == para);
                //if (_pegacnxpara != null)
                //{
                //    _cnxid = _pegacnxpara.CnxId;
                //}

            }
            catch (Exception) { }

            //await Clients.All.SendAsync("enviarmens", Context.ConnectionId, de, para, mens);
            //if (!string.IsNullOrEmpty(_cnxid))
            await Clients.Client(cnxpara).SendAsync("teste", de, para, mens, cnxde, cnxpara);
        }

        //public async Task ListarClientesAtivos(string email)
        //{
        //    var _listacli = "";

        //    //var _contextoConexao = Context;
        //    //var id = Context.ConnectionId;
        //    //foreach (var item in ClientesAtivos.Where(x => x.Email != email))
        //    //{
        //    //    _listacli += $"<option val='{item.Id}'>{item.Email}</option>";
        //    //}
        //    foreach (var item in ClientesAtivos)
        //    {
        //        _listacli += $"<option value='{item.Id}'>{item.Email}</option>";
        //    }
        //    await Clients.All.SendAsync("listaclientes", _listacli);
        //    //await Clients.Client(_contextoConexao.ConnectionId).SendAsync("listaclientes", _listacli);
        //}
        //public async Task ListarClientesChat(string email, string chave)
        //{
        //    var _listacli = "";

        //    ////-------------------------
        //    //// ACOES EM CHAT DIA DE HJ
        //    ////-------------------------
        //    //var _listaAcoesChatHJ =
        //    //    _context.tblHubClientes
        //    //    .Include(x => x.UsuarioApp)
        //    //    .Where(x => x.Email.ToLower().Trim() != chave.ToLower().Trim())
        //    //    .Where(x => x.DTHR.Date == DateTime.Now.Date);

        //    //-------------------------
        //    // LISTA DISTINTA DE EMAILS
        //    //-------------------------
        //    //var _listaDistintaUsuEmail =
        //    //    _listaAcoesChatHJ
        //    //    .ToList()
        //    //    .OrderByDescending(x => x.DTHR)
        //    //    .GroupBy(x => x.Email)
        //    //    .Select(x => new
        //    //    {
        //    //        x.Key,
        //    //        x.FirstOrDefault().Status,
        //    //        x.FirstOrDefault().UsuarioApp,
        //    //        x.FirstOrDefault().Id
        //    //    });

        //    ////-------------------------
        //    //// LISTA DISTINTA DE EMAILS
        //    ////-------------------------
        //    //var _ususChat =
        //    //    _context.tblHubClientes
        //    //    .Include(x => x.UsuarioApp)
        //    //    .Where(x => x.Email != email)
        //    //    .Distinct(x => x.Email)
        //    //    .Where(x => x.DTHR.ToShortDateString() == DateTime.Now.ToShortDateString())
        //    //    //.GroupBy(g => g.Email)
        //    //    //.Select(s => s.First())
        //    //    .OrderByDescending(x => x.DTHR)
        //    //    .ToList();

        //    //var _ususacc = new List<string>();
        //    foreach (var item in _context.tblUsuSalaVIPs.Include(x => x.UsuarioApp).OrderByDescending(x => x.DTHR))
        //    {
        //        //if (item.Email != email)
        //        //{
        //        //if (item.Status.ToLower().Trim() == "entrou")
        //        //{
        //        //if (_ususacc.Where(x => x == item.UsuarioApp.Id).Count() == 0)
        //        //{
        //        //_ususacc.Add(item.UsuarioApp.Id);

        //        _listacli += $"<a href='#' class='usr'";
        //        _listacli += $"    data-chat-id='chat_{item.Id}'";
        //        _listacli += $"    data-chat-fname='{item.UsuarioApp.Nome}'";
        //        _listacli += $"    data-chat-lname='{item.UsuarioApp.Sobrenome}'";
        //        _listacli += $"    data-chat-status='online'";
        //        _listacli += $"    data-chat-alertmsg=''";
        //        _listacli += $"    data-chat-alertshow='false'";
        //        _listacli += $"    data-rel='popover-hover'";
        //        _listacli += $"    data-placement='right'";
        //        _listacli += $"    data-html='true'";
        //        _listacli += $"    data-content=\"";
        //        _listacli += $"	    <div class='usr-card'>";
        //        _listacli += $"		    <img src='{item.UsuarioApp.AvatarUsuario}' alt='{item.UsuarioApp.Nome} {item.UsuarioApp.Sobrenome}'>";
        //        _listacli += $"			<div class='usr-card-content'>";
        //        _listacli += $"			    <h3>{item.UsuarioApp.Nome} {item.UsuarioApp.Sobrenome}</h3>";
        //        _listacli += $"				<p>{item.UsuarioApp.Trabalho_Empresa}</p>";
        //        _listacli += $"	        </div>";
        //        _listacli += $"		</div>";
        //        _listacli += $"	\">";
        //        _listacli += $"	<i></i>{item.UsuarioApp.Nome} {item.UsuarioApp.Sobrenome}";
        //        _listacli += $"</a>";

        //        //}
        //        //}
        //        //}
        //    }

        //    var _saida = new
        //    {
        //        lista = _listacli,
        //        chave
        //    };

        //    //await Clients.All.SendAsync("listaclienteschat", _saida, chave);
        //    //await Clients.All.SendAsync("listaclienteschat", chave);
        //}
    }
}