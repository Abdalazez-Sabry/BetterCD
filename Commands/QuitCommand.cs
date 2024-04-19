using Terminal.Gui;

namespace BetterCD.Commands
{
    public class QuitCommand : IFileManagerCommand
    {
        private readonly FileManager _fileManager;

        public QuitCommand(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public void Excute()
        {
            Application.RequestStop();
            _fileManager.Quit();
        }
    }
}