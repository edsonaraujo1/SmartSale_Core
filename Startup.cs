using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSale.Data;
using SmartSale.Hubs;
using SmartSale.Models;
using SmartSale.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
namespace SmartSale
{
    public class Startup
    {
        public static int _LISTAPAGINADATOT = 10;
        public IConfiguration Configuration { get; }
        public static IOptions<AppConfig> AppConfig { get; private set; }
        public Startup(IConfiguration configuration, IOptions<AppConfig> _appconfig)
        {
            Configuration = configuration;
            AppConfig = _appconfig;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(options =>
             {
                 options.CheckConsentNeeded = context => true;
                 options.MinimumSameSitePolicy = SameSiteMode.None;
             });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<UsuarioApp>().AddEntityFrameworkStores<ApplicationDbContext>();
            services
                .AddIdentity<UsuarioApp, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR"), new CultureInfo("pt-BR") };
            });

            services.AddMvc(opts =>
            {
                // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters#filter-scopes-and-order-of-execution
                opts.Filters.Add(new AutoLogAttribute());
            })
            .AddRazorPagesOptions(options =>
            {
                //options.Conventions.AddPageRoute("IndexCMS?handler=DadosBarrasVisitasDia", "api/dadosacessoscms");
                options.Conventions.AddPageRoute("/lpv", "lpv/{chave?}");
                options.Conventions.AddPageRoute("/SalaChat", "SalaChat/{uid?}");
                options.Conventions.AddPageRoute("/Mensagens", "Mensagens/{uid?}");
                options.Conventions.AddPageRoute("/Conversas", "Conversas/{uid?}");
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddOptions();
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            //-----------------------------------------------------------
            // ESSE CARA COMPRIME O CONTEUDO
            //-----------------------------------------------------------
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                        "text/plain",
                        "text/css",
                        "application/javascript",
                        "text/html",
                        "application/xml",
                        "text/xml",
                        "application/json",
                        "text/json",
                        "image/svg+xml",
                        "image/jpg",
                        "image/jpeg",
                        "image/png",
                        "application/x-font-woff",
                        "font/truetype",
                        "font/opentype",
                        "font/woff2",
                        "application/font-woff2",
                        "application/font-woff"
                });
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.WithOrigins("https://localhost:44339")
                        //.WithOrigins("http://house2invest.depoisdalinha.com.br")
                        .AllowCredentials();
                }
            ));

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(30);
            });

            //services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            cultureInfo.NumberFormat.CurrencySymbol = "R$";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            app.UseRequestLocalization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseResponseCompression();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                //routes.MapHub<ChatHub>("/chatHub");
                //routes.MapHub<ChatHub>("/chatHubCMS");
                //routes.MapHub<NotificacoesHub>("/notificacoes", options =>
                //{
                //    // when run in debug mode only WebSockets are allowed
                //    if (Debugger.IsAttached)
                //    {
                //        //options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                //    }
                //});

                routes.MapHub<MensagensHub>("/mensagens");
            });

            //app.UseSignalR(routes => { routes.MapHub<JobProgressHub>("/jobprogress"); });

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });

            app.UseMvc();
        }
    }
}
