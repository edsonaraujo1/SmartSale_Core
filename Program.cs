using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
namespace SmartSale
{
    public class Program
    {
        public static string _APPIDMEMORIA = "";
        // ###### RESETAR AQUI /// RESETAR NO LUIS ###### 
        // CRIAR UMA CONTA EM: https://luis.ai/home PARA OBTER A CHAVE DE AUTORIZAÇÃO
        public static string _AUTHORINGKEY = "49029bef322a48728f1b7b8561e2d19a";
        // ###### RESETAR AQUI /// RESETAR NO LUIS ###### 
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseApplicationInsights()
            .UseStartup<Startup>();
    }
}