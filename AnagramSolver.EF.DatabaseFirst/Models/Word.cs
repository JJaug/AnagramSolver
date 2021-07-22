using System.Collections.Generic;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class Word
    {
        public Word()
        {
            UserWords = new HashSet<UserWord>();
        }

        public string Word1 { get; set; }
        public long Id { get; set; }

        public virtual ICollection<UserWord> UserWords { get; set; }
    }
}
