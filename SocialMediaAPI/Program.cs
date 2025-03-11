
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Abstraction.Repository;
using SocialMediaAPI.MiddleWares;
using SocialMediaAPI.models.Identity;
using SocialMediaAPI.Presistence;
using SocialMediaAPI.Repository;
using SocialMediaAPI.Services;
using SocialMediaAPI.Services.Abstraction;
using System.Runtime.CompilerServices;

namespace SocialMediaAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SocialMediaDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<SocialMediaDbContext>()
                            .AddDefaultTokenProviders();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            builder.Services.AddScoped<IAuthunticationServices, AuthunticationServices>(); 

            var app = builder.Build();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            //using var scope = app.Services.CreateScope();
            //var services = scope.ServiceProvider;
            //var userManager = services.GetRequiredService<UserManager<AppUser>>();
            //await SocialMediaDbContetSeed.SeedUserAsync(userManager);

            // Configure the HTTP request pipeline.

            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status200OK; // Return 200 OK
            //        context.Response.ContentType = "application/json";

            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //        if (exceptionHandlerPathFeature?.Error != null)
            //        {
            //            await context.Response.WriteAsync(new
            //            {
            //                Message = "An error occurred, but the request was handled gracefully."
            //            }.ToString());
            //        }
            //    });
            //});


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
