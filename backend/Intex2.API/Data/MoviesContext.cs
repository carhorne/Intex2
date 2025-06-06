﻿using Microsoft.EntityFrameworkCore;

namespace Intex2.API.Data
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<MoviesRating> MoviesRatings { get; set; }
        public virtual DbSet<MoviesTitle> MoviesTitles { get; set; }
        public virtual DbSet<MoviesUser> MoviesUsers { get; set; }

        // Add DbSet properties for the new models
        public virtual DbSet<MoviesHomePageRecommendations> MoviesHomePageRecommendations { get; set; }
        public virtual DbSet<MoviesUserRecommendations> MoviesUserRecommendations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration.
            optionsBuilder.UseSqlite("Data Source=Movies.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesRating>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ShowId }); // Composite primary key

                entity.ToTable("movies_ratings");
                entity.Property(e => e.starRating).HasColumnName("rating");
                entity.Property(e => e.ShowId).HasColumnName("show_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                
            });

            modelBuilder.Entity<MoviesTitle>(entity =>
            {
                entity.HasKey(e => e.ShowId); // Define primary key
                entity.ToTable("movies_titles");
                entity.Property(e => e.AnimeSeriesInternationalTvShows).HasColumnName("Anime Series International TV Shows");
                entity.Property(e => e.BritishTvShowsDocuseriesInternationalTvShows).HasColumnName("British TV Shows Docuseries International TV Shows");
                entity.Property(e => e.Cast).HasColumnName("cast");
                entity.Property(e => e.ComediesDramasInternationalMovies).HasColumnName("Comedies Dramas International Movies");
                entity.Property(e => e.ComediesInternationalMovies).HasColumnName("Comedies International Movies");
                entity.Property(e => e.ComediesRomanticMovies).HasColumnName("Comedies Romantic Movies");
                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.CrimeTvShowsDocuseries).HasColumnName("Crime TV Shows Docuseries");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Director).HasColumnName("director");
                entity.Property(e => e.DocumentariesInternationalMovies).HasColumnName("Documentaries International Movies");
                entity.Property(e => e.DramasInternationalMovies).HasColumnName("Dramas International Movies");
                entity.Property(e => e.DramasRomanticMovies).HasColumnName("Dramas Romantic Movies");
                entity.Property(e => e.Duration).HasColumnName("duration");
                entity.Property(e => e.FamilyMovies).HasColumnName("Family Movies");
                entity.Property(e => e.HorrorMovies).HasColumnName("Horror Movies");
                entity.Property(e => e.InternationalMoviesThrillers).HasColumnName("International Movies Thrillers");
                entity.Property(e => e.InternationalTvShowsRomanticTvShowsTvDramas).HasColumnName("International TV Shows Romantic TV Shows TV Dramas");
                entity.Property(e => e.KidsTv).HasColumnName("Kids' TV");
                entity.Property(e => e.LanguageTvShows).HasColumnName("Language TV Shows");
                entity.Property(e => e.NatureTv).HasColumnName("Nature TV");
                entity.Property(e => e.Rating).HasColumnName("rating");
                entity.Property(e => e.RealityTv).HasColumnName("Reality TV");
                entity.Property(e => e.ReleaseYear).HasColumnName("release_year");
                entity.Property(e => e.ShowId).HasColumnName("show_id");
                entity.Property(e => e.TalkShowsTvComedies).HasColumnName("Talk Shows TV Comedies");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.TvAction).HasColumnName("TV Action");
                entity.Property(e => e.TvComedies).HasColumnName("TV Comedies");
                entity.Property(e => e.TvDramas).HasColumnName("TV Dramas");
                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<MoviesUser>(entity =>
            {
                entity.HasNoKey().ToTable("movies_users");
                entity.Property(e => e.Age).HasColumnName("age");
                entity.Property(e => e.AmazonPrime).HasColumnName("Amazon Prime");
                entity.Property(e => e.AppleTv).HasColumnName("Apple TV+");
                entity.Property(e => e.City).HasColumnName("city");
                entity.Property(e => e.Disney).HasColumnName("Disney+");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Gender).HasColumnName("gender");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Paramount).HasColumnName("Paramount+");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.State).HasColumnName("state");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Zip).HasColumnName("zip");
            });

            // Add model configurations for the new models if necessary
            modelBuilder.Entity<MoviesHomePageRecommendations>(entity =>
            {
                entity.HasNoKey().ToTable("homepage_recommendations"); // Correct table name
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Genre).HasColumnName("genre");
                entity.Property(e => e.ShowId).HasColumnName("show_id");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Score).HasColumnName("score");
            });

            modelBuilder.Entity<MoviesUserRecommendations>(entity =>
            {
                entity.HasNoKey().ToTable("user_movie_recommendations"); // Correct table name
                entity.Property(e => e.ShowId).HasColumnName("show_id");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Score).HasColumnName("score");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.SourceShowId).HasColumnName("source_show_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

