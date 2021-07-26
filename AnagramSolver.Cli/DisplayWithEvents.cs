using System;

namespace AnagramSolver.Cli
{
    public class DisplayWithEvents /*: IDisplay*/
    {
        public EventHandler ClickEvent;
        public void FormattedPrint(Action<string> toFile, string input)
        {
            ClickEvent.Invoke(this, EventArgs.Empty);
        }
    }
}
