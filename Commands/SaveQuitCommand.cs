using Terminal.Gui;

namespace BetterCD.Commands
{
    public class SaveQuitCommand : IFileManagerCommand
    {
        private readonly FileManager _fileManager;
        public SaveQuitCommand(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public void Excute()
        {
            Application.RequestStop();
            _fileManager.SaveAndQuit();
        }
    }
}