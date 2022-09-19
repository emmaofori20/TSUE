using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
using TSUE.Models.Data;
using TSUE.Services;
using TSUE.Services.IServices;
using TSUE.Utils;

namespace TSUE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<TSUEProjectDbContext>(options => options.UseSqlServer(
            //      Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<BirdTsueDBContext>(options => options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection1")));


            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<TSUEProjectDbContext>();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            // implements the cookie event handler
            services.AddTransient<CookieEventHandler>();

            // demo version of a state management to keep track of logout notifications
            services.AddSingleton<LogoutSessionManager>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie(options =>
                {
                    options.EventsType = typeof(CookieEventHandler);
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Configuration.GetSection("IdentityServer")["Authority"];
                    options.RequireHttpsMetadata = false;

                    options.ClientId = Configuration.GetSection("IdentityServer")["ClientId"];
                    options.ClientSecret = Configuration.GetSection("IdentityServer")["Secret"];

                    options.ResponseType = "code";

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    //options.Scope.Add("scope1");
                    options.Scope.Add("offline_access");
                    //options.Scope.Add("271ffa58-680a-42b9-918e-9468228c3f42_api");

                    // not mapped by default
                    options.ClaimActions.MapJsonKey("website", "website");

                    // keeps id_token smaller
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    NameClaimType = "name",
                    //    RoleClaimType = "role"
                    //};

                    //CustomHandlers cHandler = new CustomHandlers(adminDbContext);

                    options.Events = new OpenIdConnectEvents
                    {
                        //OnTicketReceived = cHandler.InitializeUserClaims
                    };
                });

            services.AddControllersWithViews();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IDocumentTypeService, DocumentTypeService>();
            services.AddTransient<IAnalyticService, AnalyticService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
