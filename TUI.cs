using System.Collections;
using System.Reflection;
using BetterCD.Commands;
using Terminal.Gui;

namespace BetterCD
{
    public class TUI
    {
        private readonly FileManager _fileManager;

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

            CommandManager commandManager = new(_fileManager, listView, title);
            HandleInputs(listView, commandManager);

            Application.Top.Add(listView);
            Application.Top.Add(title);
            Application.Run();
            Application.Shutdown();
        }

        private void HandleInputs(ListView listView, CommandManager commandManager)
        {
            listView.KeyPress += (eventArgs) =>
            {
                if (commandManager.Commands.TryGetValue(eventArgs.KeyEvent.Key, out IFileManagerCommand? command))
                {
                    command.Excute();
                }
            };
        }
    }
}