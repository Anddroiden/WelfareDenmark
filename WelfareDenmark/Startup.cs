using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlite("Data Source=app.db");
            });
            services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtKey);
            services.AddAuthorization(options => {
                options.AddPolicy(PolicyConstants.IsPatient,
                    policy => policy.RequireClaim(PolicyConstants.IsPatient, "true"));
                options.AddPolicy(PolicyConstants.CanCreatePatient,
                    policy => policy.RequireClaim(PolicyConstants.CanCreatePatient, "true"));
            });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication().AddCookie().AddJwtBearer(x => {
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IServiceProvider serviceProvider) {
            UseAmericanCultureInfo();
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseAuthentication();
            app.UseMvc(routes => {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider);
        }

        private static void UseAmericanCultureInfo() {
            var cultureInfo = CultureInfo.GetCultureInfo("en-US");

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        private async void CreateRoles(IServiceProvider serviceProvider) {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var email = "someone@somewhere.com";

            var testUser = await userManager.FindByEmailAsync(email);

            if (testUser == null) {
                var administrator = new ApplicationUser {Email = email, UserName = email};

                var newUser = await userManager.CreateAsync(administrator, "_AStrongP@ssword!");

                if (newUser.Succeeded)
                    await userManager.AddClaimAsync(administrator, new Claim(PolicyConstants.CanCreatePatient, "true"));
            }
        }
    }

    public class AppSettings {
        public string JwtKey { get; set; }
        public double JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}