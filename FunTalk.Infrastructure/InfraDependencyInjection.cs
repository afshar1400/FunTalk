using FunTalk.Application.Common.Interface;
using FunTalk.Infrastructure.Data;
using FunTalk.Infrastructure.Identity;
using FunTalk.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Infrastructure
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services ,IConfiguration configuration)
        {
            //for db
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=JokeShare.db"));
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            //for IApplicationdbContext
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IIdentityService, IdentityService>();

            //for jwt
            var JwtTokenConfig = configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.AddSingleton(JwtTokenConfig);

            services.AddScoped<IJwtManager, JwtManager>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = JwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenConfig.Secret)),
                    ValidAudience = JwtTokenConfig.Audience,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(2)
                };
            });
            return services;
        }
    }
}
