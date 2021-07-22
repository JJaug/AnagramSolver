using System.Data.Entity;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class UserWordsContext : System.Data.Entity.DbContext
    {
        public UserWordsContext() : base()
        {

        }
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
