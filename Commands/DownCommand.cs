using Terminal.Gui;

namespace BetterCD.Commands
{
    public class DownCommand : IFileManagerCommand
    {

        public DownCommand(ListView listView, Key key)
        {
            listView.AddKeyBinding(key, [Command.LineDown]);
        }

        public void Excute()
        {
        }
    }
}