using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebServer.Services;
using WebServer.DataAccess.Repositories;
using WebServer.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebServer.Classes;
using WebServer.DataAccess.DBContexts;

namespace WebServer
{
    public class Program
    {
        private static IConfiguration Configuration;
        
        public static void Main(string[] args)
        {
            var confbuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = confbuilder.Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();
            builder.Services
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter()));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<GetMetallCostsService>();
            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(
                Configuration["ConnectionStrings:DefaultConnection"],
                /*b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName),*/
                o => o.UseNodaTime()));
            #region Repositories
            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IRoleRepository, RoleRepository>();
            builder.Services.AddTransient<IFurnaceRepository, FurnaceRepository>();
            builder.Services.AddTransient<ICalculationHistoryRepository, CalculationHistoryRepository>();

            #endregion
            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new
                    SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                    (Configuration["JWT:Key"]))
                };
            });
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

            app.UseAuthentication();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

    }
}

