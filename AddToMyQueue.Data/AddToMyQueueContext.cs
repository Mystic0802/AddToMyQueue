using AddToMyQueue.Data.Models;
using AddToMyQueue.Data.Models.Spotify;
using Microsoft.EntityFrameworkCore;

namespace AddToMyQueue.Data
{
    public class AddToMyQueueContext : DbContext
    {
        public AddToMyQueueContext(DbContextOptions<AddToMyQueueContext> options) :
            base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<UserSpotifyAccount>()
            .HasKey(nameof(UserSpotifyAccount.UserId), nameof(UserSpotifyAccount.SpotifyId));
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<SpotifyAccount> SpotifyAccounts { get; set; }
        public DbSet<UserSpotifyAccount> UserSpotifyAccounts { get; set; }
    }
}
