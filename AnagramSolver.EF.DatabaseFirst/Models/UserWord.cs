using System;
using System.Collections.Generic;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class UserWord
    {
        public long UserId { get; set; }
        public long WordId { get; set; }

        public virtual User User { get; set; }
        public virtual Word Word { get; set; }
    }
}
