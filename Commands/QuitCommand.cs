using Terminal.Gui;

namespace BetterCD.Commands
{
    public class QuitCommand : IFileManagerCommand
    {
        public void Excute()
        {
            Application.RequestStop();
        }
    }
}