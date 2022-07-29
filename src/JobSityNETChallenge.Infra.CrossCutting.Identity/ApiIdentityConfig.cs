using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity;
using NetDevPack.Identity.Data;
using NetDevPack.Identity.Jwt;
using System.Text;

namespace JobSityNETChallenge.Infra.CrossCutting.Identity
{
    public static class ApiIdentityConfig
    {
        public static void AddApiIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Default EF Context for Identity (inside of the NetDevPack.Identity)
            services.AddIdentityEntityFrameworkContextConfiguration(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("JobSityNETChallenge.Infra.CrossCutting.Identity")));

            // Identity configuration
            services
                .AddIdentity<IdentityUser, IdentityRole>(x =>
                {
                    x.Password.RequiredLength = 1;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<NetDevPackAppDbContext>()
                .AddDefaultTokenProviders();

            // JWT configuration
            AddJwtConfiguration(services, configuration, "AppSettings");
        }

        public static IServiceCollection AddJwtConfiguration(IServiceCollection services, IConfiguration configuration, string appJwtSettingsKey = "AppJwtSettings")
        {
            if (services == null) throw new ArgumentException(nameof(services));
            if (configuration == null) throw new ArgumentException(nameof(configuration));

            var appSettingsSection = configuration.GetSection(appJwtSettingsKey);
            services.Configure<AppJwtSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppJwtSettings>();

            if (false)
                SymetricKeyConfiguration(services, appSettings);
            else
            {
                var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = appSettings.Audience,
                            ValidIssuer = appSettings.Issuer
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                // If the request is for our hub...
                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat-room-hub")))
                                {
                                    // Read the token out of the query string
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });
            }

            return services;
        }

        private static void SymetricKeyConfiguration(IServiceCollection services, AppJwtSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = appSettings.Audience,
                        ValidIssuer = appSettings.Issuer
                    };
                });
        }
    }
}