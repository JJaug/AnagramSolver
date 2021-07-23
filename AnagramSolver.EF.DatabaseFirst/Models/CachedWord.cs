#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class CachedWord
    {
        public string Word { get; set; }
        public string Anagram { get; set; }
    }
}
