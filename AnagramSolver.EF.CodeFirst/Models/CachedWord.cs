using System.ComponentModel.DataAnnotations;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class CachedWord
    {
        [Key]
        public string Word { get; set; }
        public string Anagram { get; set; }
    }
}
