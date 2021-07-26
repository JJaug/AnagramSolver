namespace AnagramSolver.Cli
{
    public interface IDisplay
    {
        public void FormattedPrint(Func<string, string> style, string input);
    }
}