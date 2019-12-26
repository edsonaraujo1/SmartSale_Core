using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartSale.Data;
using SmartSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace SmartSale.Hubs
{
    public static class ManipuladorDeConexoes
    {
        public static HashSet<string> Conectados = new HashSet<string>();
    }

    ////[Authorize]
    public class MensagensHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<UsuarioApp> _signInManager;
        public MensagensHub(SignInManager<UsuarioApp> signInManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
            //var _quemonline = _context.Users.Where(x => x.UserName == Context.User.Identity.Name).FirstOrDefault();
            //if (_quemonline.Sistema_FuncaoUsuario == "BOT")
            //{
            //    if (!_signInManager.)
            //    {
            //        var result = _signInManager.PasswordSignInAsync(_quemonline.Email, "S3nh4Bot@010203", true, lockoutOnFailure: true);
            //    }
            //}
        }
        UsuarioApp RetornaUsuLogado()
        {
            try
            {
                var _contextoConexao = Context;
                if (_contextoConexao.User.Identity.IsAuthenticated)
                {
                    return _context
                        .Users
                        .FirstOrDefault
                        (
                            x => x.UserName == _contextoConexao.User.Identity.Name
                        );
                }
            }
            catch (Exception) { }

            return new UsuarioApp();
        }
        tblUsuSalaVIP RetornaUsuSalaVIP(string uid)
        {
            var _contextoConexao = Context;
            return _context.tblUsuSalaVIPs
                .Include(x => x.UsuarioApp)
                .FirstOrDefault(x => x.UsuarioAppId == uid);
        }

        public override async Task OnConnectedAsync()
        {
            ManipuladorDeConexoes.Conectados.Add(Context.ConnectionId);
            await Quem("ENTROU");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ManipuladorDeConexoes.Conectados.Remove(Context.ConnectionId);
            await Quem("SAIU");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task Quem(string status)
        {
            try
            {
                var _quem = RetornaUsuLogado();
                var _contextoConexao = Context;
                ////----------------------
                //// LOG
                ////----------------------
                //var _novocliente = new tblHubCliente()
                //{
                //    CnxId = _contextoConexao.ConnectionId,
                //    Email = _quem.Email,
                //    Status = status,
                //    UsuarioAppId = _quem.Id
                //};
                //_context.tblHubClientes.Add(_novocliente);
                //await _context.SaveChangesAsync();

                //-------------------------
                // AQUI VISITANTE NÃO ENTRA
                //-------------------------
                if (!string.IsNullOrEmpty(_quem.UserName))
                {
                    // EXISTE?
                    var _existesalaVIP = await _context.tblUsuSalaVIPs
                        .FirstOrDefaultAsync(x => x.UsuarioAppId == _quem.Id);

                    // NÃO!!!!
                    if (_existesalaVIP == null)
                    {
                        // CRIA REGISTRO
                        var _novoususalavip = new tblUsuSalaVIP()
                        {
                            CnxId = _contextoConexao.ConnectionId,
                            UsuarioAppId = _quem.Id,
                            Status = status
                        };
                        _context.tblUsuSalaVIPs.Add(_novoususalavip);
                        await _context.SaveChangesAsync();
                    }
                    // SIM
                    else
                    {
                        _existesalaVIP.DTHR = DateTime.Now;
                        _existesalaVIP.CnxId = _contextoConexao.ConnectionId;
                        _existesalaVIP.Status = status;
                        _context.Attach(_existesalaVIP).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    //---------------------------
                    // VISITANTE!! TRATO OS NULLS
                    //---------------------------
                    _quem.Id = "offline";
                    _quem.Nome = "visitante";
                    _quem.Sobrenome = "n: " + ManipuladorDeConexoes.Conectados.Count.ToString();
                }

                // AVISA À TODOS
                await Clients
                    .All
                    // AVISA À TODOS, EXCETO VOCÊ
                    //.AllExcept(_contextoConexao.ConnectionId)
                    .SendAsync
                    (
                        "quem",
                        _contextoConexao.ConnectionId,
                        _quem.Id,
                        _quem.Nome + " " + _quem.Sobrenome,
                        status,
                        ManipuladorDeConexoes.Conectados.Count
                    );
            }
            catch (Exception erro) { string _erro = erro.Message; }
        }
        public async Task Teclando(string de, string para)
        {
            var _de = RetornaUsuLogado();
            var _usupara = RetornaUsuSalaVIP(para);
            if (_de != null && _usupara != null)
            {
                //await Clients.All.SendAsync("teclando", $"{_de.Nome} {_de.Sobrenome}", $"{_usupara.UsuarioApp.Nome} {_usupara.UsuarioApp.Sobrenome}");
                await Clients.Client(_usupara.CnxId)
                    .SendAsync("teclando", $"{_de.Nome} {_de.Sobrenome}", $"{_usupara.UsuarioApp.Nome} {_usupara.UsuarioApp.Sobrenome}", _de.Id, _usupara.UsuarioAppId);
            }

            //await Clients
            //    //.Client(_usupara.CnxId)
            //    .All
            //    .SendAsync("teclando", de, para);
        }

        //public async Task Correio(string cnxid_para, string id_para, string mens_para)
        //{
        //    var _contextoConexao = Context;
        //    await Clients
        //        .Client(cnxid_para)
        //        .SendAsync("correio", cnxid_para, id_para, mens_para);

        //    //await Clients.All.SendAsync("correio", cnxid_para, id_para, mens_para);
        //    //await Clients.Client(para).SendAsync("enviarmens", Context.ConnectionId, de, para, mens);
        //}
    }
    //public class CustomUserIdProvider : IUserIdProvider
    //{
    //    public virtual string GetUserId(HubConnectionContext connection)
    //    {
    //        return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
    //    }
    //}
}