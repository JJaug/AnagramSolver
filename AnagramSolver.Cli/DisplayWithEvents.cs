using System;

namespace AnagramSolver.Cli
{
    public class DisplayWithEvents : IDisplay
    {
        public EventHandler ClickEvent;
        private readonly Action<string> _print;
        public DisplayWithEvents(Action<string> print)
        {
            _print = print;
        }
        public void FormattedPrint(Func<string, string> letterUp, string input)
        {

            ClickEvent.Invoke(this, EventArgs.Empty);
            var uppercasedInput = letterUp(input);
            _print(uppercasedInput);
        }
    }
}
