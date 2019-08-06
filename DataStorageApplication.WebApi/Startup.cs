using Authorization.Module;
using DataStorageApplication.WebApi.ExceptionMiddlewares;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Module;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DataStorageApplication.WebApi
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
           services.Configure<DatabaseOptions>(Configuration.GetSection("DatabaseConfiguration"));
           var securityKey = Configuration.GetSection("Security:SecurityKey").Value;
           services.Configure<Security>(option => option.SecurityKey = securityKey);

           services.ConfigureDocumentDatabase();
           services.ConfigureUserCredentialDatabase(Configuration);
           ConfigureAuthorization(services, securityKey);

           services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddlewareBase>();
            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureAuthorization(IServiceCollection services, string secretKey)
        {
            var encodedkey = Encoding.ASCII.GetBytes(secretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = "http://localhost:59555",
                   ValidAudience = "http://localhost:59555",
                   IssuerSigningKey = new SymmetricSecurityKey(encodedkey)
               };
           });
        }
    }
}