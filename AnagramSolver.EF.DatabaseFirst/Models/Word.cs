using System;
using System.Collections.Generic;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class Word
    {
        public int Id { get; set; }
        public string Word1 { get; set; }
    }
}
