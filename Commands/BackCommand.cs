using Terminal.Gui;

namespace BetterCD.Commands
{
    public class BackCommand : IFileManagerCommand
    {

        private readonly FileManager _fileManager;
        private readonly ListView _listView;
        private readonly Label _title;

        public BackCommand(FileManager fileManager, ListView listView, Label title)
        {
            _fileManager = fileManager;
            _listView = listView;
            _title = title;
        }

        public void Excute()
        {
            _fileManager.NavigateOrOpen(0);
            _listView.SetSource(_fileManager.GetDerictoriesName());
            _title.Text = _fileManager.CurrentDirectory.Name;
        }
    }
}