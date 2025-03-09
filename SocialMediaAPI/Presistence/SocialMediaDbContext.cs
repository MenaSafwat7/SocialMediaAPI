using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.models;
using SocialMediaAPI.models.Identity;

namespace SocialMediaAPI.Presistence
{
    public class SocialMediaDbContext : IdentityDbContext<AppUser, IdentityRole<string>, string>
    {
        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options): base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialMediaDbContext).Assembly);
            var cascadeFK = modelBuilder
            .Model
            .GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

            foreach (var fk in cascadeFK)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<AppUser> users { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Notifications> notifications { get; set; }
        public DbSet<Messages> messages { get; set; }
        public DbSet<Likes> likes { get; set; }
        public DbSet<Comments> Comments { get; set; }


    }
}
