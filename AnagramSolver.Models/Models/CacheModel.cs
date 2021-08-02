using System.Collections.Generic;

namespace AnagramSolver.Models.Models
{
    public class CacheModel
    {
        public HashSet<AnagramModel> Caches { get; set; } = new HashSet<AnagramModel>();
        public bool IsSuccessful { get; set; }
    }
}
