using Terminal.Gui;

namespace BetterCD.Commands
{
    public class CommandManager
    {
        public Dictionary<Key, IFileManagerCommand> Commands { get; }

        public CommandManager(FileManager fileManager, ListView listView, Label title)
        {
            Commands = new() {
                { Key.k, new UpCommand(listView, Key.k) },
                { Key.j, new DownCommand(listView, Key.j) },
                { Key.l, new OpenCommand(fileManager, listView, title) },
                { Key.h, new BackCommand(fileManager, listView, title) },
                { Key.q, new QuitCommand() },
                { Key.s, new SaveQuitCommand(fileManager) },
            };
        }
    }
}