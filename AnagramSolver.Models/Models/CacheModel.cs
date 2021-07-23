using System.Collections.Generic;

namespace AnagramSolver.Models.Models
{
    public class CacheModel
    {
        public List<string> Caches { get; set; } = new List<string>();
        public bool IsSuccessful { get; set; }
    }
}
