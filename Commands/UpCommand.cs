using Terminal.Gui;

namespace BetterCD.Commands
{
    public class UpCommand : IFileManagerCommand
    {
        public UpCommand(ListView listView, Key key)
        {
            listView.AddKeyBinding(key, [Command.LineUp]);
        }

        public void Excute()
        {

        }
    }
}