using Microsoft.EntityFrameworkCore;
using Roommates.Infrastructure.Models;
using File = Roommates.Infrastructure.Models.File;

namespace Roommates.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public const string SCHEMA_NAME = "roomates";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA_NAME);

            modelBuilder.Entity<User>()
                .HasMany(e => e.EmailVerifications)
                .WithOne(e => e.User);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
    }
}
