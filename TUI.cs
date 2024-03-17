using System.Collections;
using Terminal.Gui;

namespace BetterCD 
{
    public class TUI
    {
        private readonly FileManager fileManager;
        public TUI(string[] args)
        {
            fileManager = new FileManager(args);
        }

        public void Run()
        {
            Application.Init();

            var currentDirectories = fileManager.GetDerictoriesName();

            var title = new Label(fileManager.CurrentDirectory.Name) { 
                Border = new Border(){ BorderStyle = BorderStyle.Rounded} ,
                Width = Dim.Fill(), 

            };
            var lv = new ListView(currentDirectories)
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                Y = 2
            };


            HandleInputs(lv, currentDirectories, title);

            Application.Top.Add(lv);
            Application.Top.Add(title);
            Application.Run();
            Application.Shutdown();
            fileManager.QuitAndSave();
        }

        private void HandleInputs(ListView lv, List<string> currentDirectories, Label title) {
            bool isClickingEnter = false;
            lv.KeyDown += (key) => {
                if (!isClickingEnter && key.KeyEvent.Key == Key.Enter) {
                    isClickingEnter = true;
                    var selected = lv.SelectedItem;
                    fileManager.NavigateOrOpen(currentDirectories[lv.SelectedItem]);
                    currentDirectories = fileManager.GetDerictoriesName();
                    lv.SetSource(currentDirectories);
                    title.Text = fileManager.CurrentDirectory.Name;
                }
            };
            lv.KeyUp += (key) => {
                if (isClickingEnter && key.KeyEvent.Key == Key.Enter) {
                    isClickingEnter = false;
                }
            };
        }
    }
}