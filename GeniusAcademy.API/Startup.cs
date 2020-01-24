using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeniusAcademy.Data;
using GeniusAcademy.EF.Respsitory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace GeniusAcademy.API
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


            //Enable CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            //Add Context
            services.AddDbContext<DataContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //Add Test-Repo
            services.AddScoped<EfCoreTestRepository>();
            //Add Identity classes
            services.AddIdentity<IdentityUser, IdentityRole>(
                option =>
                {
                    option.Password.RequireDigit = false;
                    option.Password.RequiredLength = 6;
                    option.Password.RequireLowercase = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            //Add Authentication method( JWT token with SigningKey )
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                Options.SaveToken = true;
                Options.RequireHttpsMetadata = true;
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    //ValidAudience = Configuration["Jwt:Site"],
                    //ValidIssuer = Configuration["Jwt:Site"],
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"]))
                };
            });

            services.AddMvc()
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
              .AddJsonOptions(options =>
              {
                  var resolver = options.SerializerSettings.ContractResolver;
                  if (resolver != null)
                      (resolver as DefaultContractResolver).NamingStrategy = null;
              });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

           

            app.UseCors("CorsPolicy");

            

            app.UseAuthentication();

            //app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
