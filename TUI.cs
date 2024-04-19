using System.Collections;
using System.Reflection;
using BetterCD.Commands;
using Terminal.Gui;

namespace BetterCD
{
    public class TUI
    {
        private readonly FileManager _fileManager;
        private CommandManager _commandManager;

        public TUI(string[] args)
        {
            _fileManager = new FileManager(args);
        }

        public void Run()
        {
            Application.Init();

            var currentDirectories = _fileManager.GetDerictoriesName();

            var title = new Label(_fileManager.CurrentDirectory.Name)
            {
                Border = new Border() { BorderStyle = BorderStyle.Rounded },
                Width = Dim.Fill(),

            };
            var listView = new ListView(currentDirectories)
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                Y = 2
            };

            _commandManager = new CommandManager(_fileManager, listView, title);
            HandleInputs(listView);

            Application.Top.Add(listView);
            Application.Top.Add(title);
            Application.Run();
            Application.Shutdown();
        }

        private void HandleInputs(ListView listView)
        {
            listView.KeyPress += (eventArgs) =>
            {
                if (_commandManager.Commands.TryGetValue(eventArgs.KeyEvent.Key, out IFileManagerCommand? command))
                {
                    command.Excute();
                }
            };
        }
    }
}