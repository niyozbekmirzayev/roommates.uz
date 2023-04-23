using Microsoft.EntityFrameworkCore;
using Roommates.Domain.Models.Files;
using Roommates.Domain.Models.Locations;
using Roommates.Domain.Models.Posts;
using Roommates.Domain.Models.Roommates;
using Roommates.Domain.Models.Users;
using File = Roommates.Domain.Models.Files.File;

namespace Roommates.Data
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

            modelBuilder.Entity<Post>()
                .HasMany(e => e.LikedByUsers)
                .WithMany(e => e.LikedPosts);

            modelBuilder.Entity<User>()
                .HasMany(e => e.OwnPosts)
                .WithOne(e => e.CreatedByUser);

            modelBuilder.Entity<User>()
                .HasMany(e => e.EmailVerifications)
                .WithOne(e => e.User);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }
    }
}
