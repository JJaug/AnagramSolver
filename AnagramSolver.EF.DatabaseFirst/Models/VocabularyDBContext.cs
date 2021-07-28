using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class VocabularyDBContext : DbContext
    {
        public VocabularyDBContext()
        {
        }

        public VocabularyDBContext(DbContextOptions<VocabularyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWord> CachedWords { get; set; }
        public virtual DbSet<SearchLog> SearchLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserWord> UserWords { get; set; }
        public virtual DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CachedWord>(entity =>
            {
                entity.HasKey(e => e.Word)
                    .HasName("PK__CachedWo__95B50109050ACE2C");

                entity.ToTable("CachedWord");

                entity.Property(e => e.Word).HasMaxLength(255);

                entity.Property(e => e.Anagram).HasMaxLength(255);
            });

            modelBuilder.Entity<SearchLog>(entity =>
            {
                entity.ToTable("SearchLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserIp)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Pass).HasMaxLength(255);
            });

            modelBuilder.Entity<UserWord>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.WordId })
                    .HasName("UserWords");

                entity.ToTable("UserWord");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WordId).HasColumnName("WordID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserWords)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserWord__UserID__5812160E");

                entity.HasOne(d => d.Word)
                    .WithMany(p => p.UserWords)
                    .HasForeignKey(d => d.WordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserWord__WordID__59063A47");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Word1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Word");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
