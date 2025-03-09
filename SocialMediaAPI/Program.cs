
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Abstraction.Repository;
using SocialMediaAPI.models.Identity;
using SocialMediaAPI.Presistence;
using SocialMediaAPI.Repository;
using SocialMediaAPI.Services;
using SocialMediaAPI.Services.Abstraction;

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

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            //                .AddEntityFrameworkStores<SocialMediaDbContext>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            builder.Services.AddScoped<IAuthunticationServices, AuthunticationServices>(); 

            var app = builder.Build();

            //using var scope = app.Services.CreateScope();
            //var services = scope.ServiceProvider;
            //var userManager = services.GetRequiredService<UserManager<AppUser>>();
            //await SocialMediaDbContetSeed.SeedUserAsync(userManager);

            // Configure the HTTP request pipeline.
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
