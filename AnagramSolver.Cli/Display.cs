namespace AnagramSolver.Cli
{
    public delegate void PrintDelegate(string input);
    public delegate string StyleDelegate(string input);
    public class Display : IDisplay
    {
        private readonly PrintDelegate _print;
        public Display(PrintDelegate print)
        {
            _print = print;
        }
        public void FormattedPrint(StyleDelegate style, string input)
        {
            var uppercasedInput = style(input);
            _print(uppercasedInput);
        }

    }

}
