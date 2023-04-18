using Microsoft.EntityFrameworkCore;
using Roommates.Domain.Models.Locations;
using Roommates.Domain.Models.Posts;
using Roommates.Domain.Models.Files;
using Roommates.Domain.Models.Roommates;
using File = Roommates.Domain.Models.Files.File;

namespace Roommates.Data
{
    public class RoommatesDbContext : DbContext
    {
        public RoommatesDbContext(DbContextOptions<RoommatesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(e => e.LikedByRoommates)
                .WithMany(e => e.LikedPosts);

            modelBuilder.Entity<Roommate>()
                .HasMany(e => e.OwnPosts)
                .WithOne(e => e.CreatedByRoommate);
        }

        public DbSet<Roommate> Roommates { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
