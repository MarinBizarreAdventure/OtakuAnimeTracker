using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Infrastructure;

public partial class OtakutrackerContext : DbContext
{
    public OtakutrackerContext()
    {
    }

    public OtakutrackerContext(DbContextOptions<OtakutrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anime> Animes { get; set; }

    public virtual DbSet<AnimeGenre> AnimeGenres { get; set; }

    public virtual DbSet<AnimeList> AnimeLists { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WatchingStatus> WatchingStatuses { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=otakutracker;Username=datasync;Password=ra5hoxetRami5");

        // => optionsBuilder.UseNpgsql("Host=localhost;Database=otakutracker;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anime>(entity =>
        {
            entity.HasKey(e => e.AnimeId).HasName("anime_pkey");

            entity.ToTable("anime");

            entity.Property(e => e.AnimeId).HasColumnName("anime_id");
            entity.Property(e => e.Aired).HasColumnName("aired");
            entity.Property(e => e.Completed).HasColumnName("completed");
            entity.Property(e => e.Dropped).HasColumnName("dropped");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EnglishName).HasColumnName("english_name");
            entity.Property(e => e.Episodes).HasColumnName("episodes");
            entity.Property(e => e.Favorites).HasColumnName("favorites");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.JapaneseName).HasColumnName("japanese_name");
            entity.Property(e => e.Licensors).HasColumnName("licensors");
            entity.Property(e => e.Members).HasColumnName("members");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OnHold).HasColumnName("on_hold");
            entity.Property(e => e.PlanToWatch).HasColumnName("plan_to_watch");
            entity.Property(e => e.Popularity).HasColumnName("popularity");
            entity.Property(e => e.Premiered).HasColumnName("premiered");
            entity.Property(e => e.Producers).HasColumnName("producers");
            entity.Property(e => e.Ranked).HasColumnName("ranked");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Score1).HasColumnName("score_1");
            entity.Property(e => e.Score10).HasColumnName("score_10");
            entity.Property(e => e.Score2).HasColumnName("score_2");
            entity.Property(e => e.Score3).HasColumnName("score_3");
            entity.Property(e => e.Score4).HasColumnName("score_4");
            entity.Property(e => e.Score5).HasColumnName("score_5");
            entity.Property(e => e.Score6).HasColumnName("score_6");
            entity.Property(e => e.Score7).HasColumnName("score_7");
            entity.Property(e => e.Score8).HasColumnName("score_8");
            entity.Property(e => e.Score9).HasColumnName("score_9");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.Studios).HasColumnName("studios");
            entity.Property(e => e.Synopsis).HasColumnName("synopsis");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Watching).HasColumnName("watching");
        });

        modelBuilder.Entity<AnimeGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("anime_genre_pkey");

            entity.ToTable("anime_genre");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnimeId).HasColumnName("anime_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Anime).WithMany(p => p.AnimeGenres)
                .HasForeignKey(d => d.AnimeId)
                .HasConstraintName("anime_genre_anime_id_fkey");

            entity.HasOne(d => d.Genre).WithMany(p => p.AnimeGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("anime_genre_genre_id_fkey");
        });

        modelBuilder.Entity<AnimeList>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.AnimeId }).HasName("anime_list_pkey");

            entity.ToTable("anime_list");

            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.AnimeId).HasColumnName("anime_id");
            entity.Property(e => e.MyFinishDate).HasColumnName("my_finish_date");
            entity.Property(e => e.MyLastUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("my_last_updated");
            entity.Property(e => e.MyRewatching).HasColumnName("my_rewatching");
            entity.Property(e => e.MyRewatchingEp).HasColumnName("my_rewatching_ep");
            entity.Property(e => e.MyStartDate).HasColumnName("my_start_date");
            entity.Property(e => e.MyTags).HasColumnName("my_tags");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.WatchedEpisodes).HasColumnName("watched_episodes");
            entity.Property(e => e.WatchingStatus).HasColumnName("watching_status");

            entity.HasOne(d => d.Anime).WithMany(p => p.AnimeLists)
                .HasForeignKey(d => d.AnimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("anime_list_anime_id_fkey");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.AnimeLists)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("anime_list_username_fkey");

            entity.HasOne(d => d.WatchingStatusNavigation).WithMany(p => p.AnimeLists)
                .HasForeignKey(d => d.WatchingStatus)
                .HasConstraintName("anime_list_watching_status_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("genre_pkey");

            entity.ToTable("genre");

            entity.HasIndex(e => e.GenreName, "genre_genre_name_key").IsUnique();

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.GenreName).HasColumnName("genre_name");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("review_pkey");

            entity.ToTable("review");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.AnimeId).HasColumnName("anime_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewDate).HasColumnName("review_date");
            entity.Property(e => e.ReviewText).HasColumnName("review_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Anime).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AnimeId)
                .HasConstraintName("review_anime_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("review_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("nextval('users_user_id_seq1'::regclass)")
                .HasColumnName("user_id");
            entity.Property(e => e.AccessRank).HasColumnName("access_rank");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.JoinDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("join_date");
            entity.Property(e => e.LastOnline)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_online");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.StatsEpisodes).HasColumnName("stats_episodes");
            entity.Property(e => e.StatsMeanScore).HasColumnName("stats_mean_score");
            entity.Property(e => e.StatsRewatched).HasColumnName("stats_rewatched");
            entity.Property(e => e.UserCompleted).HasColumnName("user_completed");
            entity.Property(e => e.UserDaysSpentWatching).HasColumnName("user_days_spent_watching");
            entity.Property(e => e.UserDropped).HasColumnName("user_dropped");
            entity.Property(e => e.UserOnhold).HasColumnName("user_onhold");
            entity.Property(e => e.UserPlantowatch).HasColumnName("user_plantowatch");
            entity.Property(e => e.UserWatching).HasColumnName("user_watching");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<WatchingStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("watching_status_pkey");

            entity.ToTable("watching_status");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("status_id");
            entity.Property(e => e.StatusDescription).HasColumnName("status_description");
        });
        modelBuilder.HasSequence("users_user_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
