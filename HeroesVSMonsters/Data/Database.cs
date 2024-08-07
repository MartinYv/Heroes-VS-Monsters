namespace HeroesVSMonsters.Data
{
    using HeroesVSMonsters.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class Database : DbContext
    {
        public Database()
        {
            
        }
        public Database(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=DESKTOP-8N6PVG5;Database=HeroesVSMonsters;Integrated Security=True;Encrypt=False");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}