using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class SearchLog
    {
        public string UserIp { get; set; }
        public string Word { get; set; }
        public int? Anagrams { get; set; }
        public int? SearchTime { get; set; }
        public DateTime CreatedAt { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
