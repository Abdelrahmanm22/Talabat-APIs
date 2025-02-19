using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Contexts;
using Talabat.Repository.Contexts.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });

            ////Cleaning Up Program Class
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            ////Cleaning Up Program Class





            #endregion

            var app = builder.Build();

            #region Update-Database
            //AppDbContext dbContext = new AppDbContext(); //invalid
            //await dbContext.Database.MigrateAsync();
            using var Scope = app.Services.CreateScope(); //group of services that lifetime scooped
            var Services = Scope.ServiceProvider; //catch services its self
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {

                var dbContext = Services.GetRequiredService<AppDbContext>(); //ASK CLR for Creating Object From DbContext Explicitly
                await dbContext.Database.MigrateAsync(); //Update - Database

                ///Identity Auto update database
                var IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                ///Identity Auto update database

                #region Data Seeding
                await StoreContextSeed.SeedAsync(dbContext);
                await AppIdentityDbContextSeed.SeedUserAsync(Services.GetRequiredService<UserManager<AppUser>>());
                #endregion
            }
            catch (Exception ex) {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occured During Appling The Migration");
            }


            #endregion

            

            #region Configure - Configure the HTTP request pipeline.


            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();
                ////Cleaning Up Program Class
                app.UseSwaggerMiddlewares();
                ////Cleaning Up Program Class
            }



            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
