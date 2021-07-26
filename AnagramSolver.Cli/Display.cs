using System;

namespace AnagramSolver.Cli
{
    //public delegate void PrintDelegate(string input);
    public delegate Action PrintDelegate(string input);
    //public delegate string StyleDelegate(string input);
    public delegate TResult Func<in T, out TResult>(string input);
    public class Display : IDisplay
    {
        private readonly Action<string> _print;
        public Display(Action<string> print)
        {
            _print = print;
        }
        public void FormattedPrint(Func<string, string> style, string input)
        {
            var uppercasedInput = style(input);
            _print(uppercasedInput);
        }

    }

}
