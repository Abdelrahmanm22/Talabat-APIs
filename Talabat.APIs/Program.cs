using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Contexts;

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

            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //make it generic too
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            #region validation error
            builder.Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState
                                                .Where(p => p.Value.Errors.Count() > 0)
                                                .SelectMany(p => p.Value.Errors)
                                                .Select(e=>e.ErrorMessage)
                                                .ToList();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            #endregion
            ///validation error///////


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
                #region Data Seeding
                await StoreContextSeed.SeedAsync(dbContext);

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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
