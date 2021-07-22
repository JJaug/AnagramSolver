namespace AnagramSolver.EF.CodeFirst.Models
{
    public partial class UserWord
    {
        public long UserId { get; set; }
        public long WordId { get; set; }

        public virtual User User { get; set; }
        public virtual Word Word { get; set; }
    }
}
