using System.Collections.Generic;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class User
    {
        public User()
        {
            UserWords = new HashSet<UserWord>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }

        public virtual ICollection<UserWord> UserWords { get; set; }
    }
}
