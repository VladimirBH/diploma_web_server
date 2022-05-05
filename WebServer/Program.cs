using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebServer.Services;
using WebServer.DataAccess.Repositories;
using WebServer.DataAccess.Contracts;
using WebServer.DBContext;
using Microsoft.EntityFrameworkCore;

namespace WebServer
{
    public class Program
    {
        private static IConfiguration Configuration;

        public Program()
        {
            
        }
        /*public Program(IConfiguration configuration) 
        {
            this.Configuration = configuration;
        }*/

        public static void Main(string[] args)
        {
            var confbuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = confbuilder.Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<GetMetallCostsService>();
            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(
                Configuration["ConnectionStrings:DefaultConnection"],
                b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            #region Repositories
            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            #endregion


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "zznob.ru",
                    ValidAudience = "http://localhost:5000/",
                    IssuerSigningKey = new
                    SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                    ("supersecretkeyclientdontthink123"))
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            app.UseHsts();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

    }
}

