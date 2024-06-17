using Microsoft.EntityFrameworkCore;

using OtakuTracker.Domain.Models;


namespace OtakuTracker.Infrastructure;

public class AnimeDbContext : DbContext
{
    public DbSet<Anime> Animes { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Theme> Themes { get; set; }
    public DbSet<User?> users { get; set; }
    public DbSet<AnimeList> AnimeLists { get; set; }
    public DbSet<AnimeListItem> AnimeListItems { get; set; } 
    public DbSet<Recommendation> Recommendations { get; set;}
    public DbSet<Review> Reviews { get; set; }

    public AnimeDbContext(DbContextOptions<AnimeDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anime>()
            .Property(a => a.AverageRating)
            .HasPrecision(5, 2);

        modelBuilder.Entity<AnimeListItem>()
            .Property(ali => ali.Score)
            .HasPrecision(3, 2);

        modelBuilder.Entity<Review>()
            .Property(r => r.Rating)
            .HasPrecision(3, 2);


        modelBuilder.Entity<Anime>()
            .HasMany(a => a.Genres)
            .WithMany(g => g.Animes)
            .UsingEntity<Dictionary<string, object>>(
                "AnimeGenre",
                j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                j => j.HasOne<Anime>().WithMany().HasForeignKey("AnimeId")
            );


        modelBuilder.Entity<Anime>()
            .HasMany(a => a.Themes)
            .WithMany(t => t.Animes)
            .UsingEntity<Dictionary<string, object>>(
                "AnimeTheme",
                j => j.HasOne<Theme>().WithMany().HasForeignKey("ThemeId"),
                j => j.HasOne<Anime>().WithMany().HasForeignKey("AnimeId")
            );


        modelBuilder.Entity<AnimeList>()
            .HasKey(al => al.UserId);

        modelBuilder.Entity<AnimeList>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<AnimeList>(al => al.UserId);


        modelBuilder.Entity<AnimeListItem>()
            .HasKey(ali => new { ali.AnimeId, ali.UserId });

        modelBuilder.Entity<AnimeListItem>()
            .HasOne<Anime>()
            .WithMany()
            .HasForeignKey(ali => ali.AnimeId);
        modelBuilder.Entity<AnimeListItem>()
            .HasOne<AnimeList>()
            .WithMany(al => al.AnimeItems)
            .HasForeignKey(ali => ali.UserId);

        modelBuilder.Entity<Recommendation>()
            .HasOne<Anime>()
            .WithMany()
            .HasForeignKey(r => r.AnimeId);

        modelBuilder.Entity<Review>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(rev => rev.UserId);

        modelBuilder.Entity<Review>()
            .HasOne<Anime>()
            .WithMany()
            .HasForeignKey(rev => rev.AnimeId);

        base.OnModelCreating(modelBuilder);

    }

}