using ChapApp.Business.Core.Repositorys;
using ChatApp.Business.Core.Authentication;
using ChatApp.Business.Core.DbContexts;
using ChatApp.Business.Core.Services;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Repositorys;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ChatApp.Business.Core.Repositorys;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.API.MIP
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


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatApp.API.MIP", Version = "v1" });
            });

            services.AddDbContext<ChatAppContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IUserService, UserService>();

            services.AddScoped<IGroupUserService, GroupUserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IGroupUserRepository, EFGroupUserRepository>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IGroupRepository, EFGroupRepository>();
            services.AddScoped<IMessageRepository, EFMessageRepository>();

            services.AddScoped<IJWTAuthService, JWTAuthService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatApp.API.MIP v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<JWTMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
