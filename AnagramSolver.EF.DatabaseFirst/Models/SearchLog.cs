using System;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class SearchLog
    {
        public string UserIp { get; set; }
        public string Word { get; set; }
        public int? Anagrams { get; set; }
        public int? SearchTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
    }
}
