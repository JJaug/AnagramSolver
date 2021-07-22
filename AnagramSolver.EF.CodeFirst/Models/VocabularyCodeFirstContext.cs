using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class VocabularyCodeFirstContext : DbContext
    {
        public VocabularyCodeFirstContext()
        {

        }
        public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<SearchLog> SearchLogs { get; set; }
        public DbSet<Word> Words { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LT-LIT-SC-0597\MSSQLSERVER01;Initial Catalog=VocabularyCodeFirst;Integrated Security=True");
        }
    }
}
