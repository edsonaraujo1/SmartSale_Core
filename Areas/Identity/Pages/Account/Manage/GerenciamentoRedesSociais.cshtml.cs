using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartSale.Data;
using SmartSale.Models;
using SmartSale.Models.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;
namespace SmartSale.Areas.Identity.Pages.Account.Manage
{
    public partial class GerenciamentoRedesSociaisModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private AppPerfil _appperfil = new AppPerfil();
        public GerenciamentoRedesSociaisModel(ApplicationDbContext context) { _context = context; }
        [BindProperty]
        public UserSelfInstaViewModel Modelo { get; set; }
        public bool ExisteCLIENTID { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Modelo = new UserSelfInstaViewModel();
            _appperfil = await _context.AppPerfil.FirstOrDefaultAsync();
            if (!string.IsNullOrEmpty(_appperfil.INSTAGRAM_CLIENTID))
                ExisteCLIENTID = true;

            if (!string.IsNullOrEmpty(_appperfil.INSTAGRAM_CONTA_TOKEN))
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.instagram.com/v1/users/self/?access_token=" + _appperfil.INSTAGRAM_CONTA_TOKEN);
                if (response.IsSuccessStatusCode)
                {
                    var _resultado = await response.Content.ReadAsStringAsync();
                    var _instaself = JsonConvert.DeserializeObject<UserSelfInstaViewModel>(_resultado);
                    Modelo = _instaself;
                }
            }

            return Page();
        }
    }
}