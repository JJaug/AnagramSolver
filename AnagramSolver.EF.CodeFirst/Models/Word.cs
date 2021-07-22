using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class Word
    {
        public string Word1 { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
