using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public virtual ICollection<UserWord> UserWords { get; set; }
    }
}
