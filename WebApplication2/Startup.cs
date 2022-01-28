using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication2.Databases;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplication2
{
    public class Startup
    {
        public static string Secret = "AVSQOFMU/JbYTOZVKqXAtPDLWogsokM/kDmSoEV5zIud+M1kE0WRmflCPkSu5gfCjqD5Ctx3in658EQf99Fu0JeYIOg+8CuRAbwE7+RpP6RlUCWCl15/pZ6T5zIhkWsHY44r2UJ6xUTKBWkoDfU6V4y1XvaP+tOOa5cp/eNn7QTJeRdiTEt927Ubcx95+ZEidOjuB565EbCjQc1gRpndKPTd9/51HLQ7UHaDy7gw8zbeeh6jz8syaEyTVt7q6XtSu0pclrCXFUjoNJgdWJpA38yaOl9mLIG6qTfa3z46cu/F+uxmhtCleaMpIkYteG43sg0LmvQCXT1dXa/aw8iC";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(option =>
            {
                option.UseMySql(Configuration.GetConnectionString("Database"))
                    .UseLazyLoadingProxies();
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                var key = Convert.FromBase64String(Secret);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateActor = false,
                    ValidateLifetime = false,
                    ValidateTokenReplay = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}