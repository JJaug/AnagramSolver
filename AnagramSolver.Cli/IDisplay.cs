namespace AnagramSolver.Cli
{
    public interface IDisplay
    {
        public void FormattedPrint(StyleDelegate style, string input);
    }
}