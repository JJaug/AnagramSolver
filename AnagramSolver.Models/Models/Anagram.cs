namespace AnagramSolver.Models.Models
{
    public class Anagram
    {
        public string Type { get; set; }
        public string Word { get; set; }

        public override string ToString()
        {
            return $"{Word}";
        }
    }
}
