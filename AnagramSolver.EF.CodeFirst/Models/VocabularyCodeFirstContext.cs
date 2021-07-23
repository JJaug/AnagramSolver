using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class VocabularyCodeFirstContext : DbContext
    {
        public VocabularyCodeFirstContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<SearchLog> SearchLogs { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<UserWord> UserWords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LT-LIT-SC-0597\MSSQLSERVER01;Initial Catalog=VocabularyCodeFirst;Integrated Security=True");

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserWord>()
        //        .HasKey(c => new { c.UserId, c.WordId });

        //    modelBuilder.Entity<User>()
        //        .HasMany(c => c.UserWords)
        //        .HasForeignKey(c => c.UserId);

        //    modelBuilder.Entity<Word>()
        //        .HasMany(c => c.UserWords)
        //        .IsRequired()
        //        .HasForeignKey(c => c.WordId);
        //}
    }
}
