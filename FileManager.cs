using System.Diagnostics;
using System.Linq.Expressions;

namespace BetterCD
{
    public class FileManager
    {
        public DirectoryInfo CurrentDirectory { get; set; }
        private readonly string? bcdPath;
        private readonly string _runDirectory;

        public FileManager(string[] args)
        {
            CurrentDirectory = new DirectoryInfo("./");
            _runDirectory = Path.GetFullPath("./");

            switch (args.Length)
            {
                case 0:
                    bcdPath = null;
                    CurrentDirectory = new DirectoryInfo("./");
                    break;
                case 1:
                    bcdPath = args[0];
                    CurrentDirectory = new DirectoryInfo("./");
                    break;
                case 2:
                    bcdPath = args[0];
                    CurrentDirectory = new DirectoryInfo(args[1]);
                    break;
                default:
                    throw new ArgumentException();
            }

            if (!CurrentDirectory.Exists)
            {
                throw new DirectoryNotFoundException(CurrentDirectory.FullName);
            }

            if (bcdPath is not null && !File.Exists(bcdPath))
            {
                File.Create(bcdPath).Close();
            }
        }

        private List<string> GetDirectoreyEntries()
        {
            var subDirectories = CurrentDirectory.GetDirectories();
            var subFiles = CurrentDirectory.GetFiles();

            var directoreyEntries = new List<string>(subDirectories.Length + subFiles.Length);

            if (CurrentDirectory.Parent is null)
            {
                directoreyEntries.AddRange(Directory.GetLogicalDrives());
            }
            else
            {
                directoreyEntries.Add(CurrentDirectory.Parent.FullName);
            }

            foreach (var DirectoreyName in subDirectories)
            {
                directoreyEntries.Add(DirectoreyName.FullName);
            }

            foreach (var fileName in subFiles)
            {
                directoreyEntries.Add(fileName.FullName);
            }

            return directoreyEntries;
        }

        public void NavigateOrOpen(int index)
        {
            var path = Path.Combine(CurrentDirectory.FullName, GetDerictoriesName()[index]);
            if (Directory.Exists(path))
            {
                CurrentDirectory = new DirectoryInfo(path);

            }
            else if (File.Exists(path))
            {
                Process process = new();
                process.StartInfo.FileName = path;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
            else
            {
                Console.WriteLine("not file nor directorey");
                throw new FieldAccessException(path);
            }

        }

        public List<string> GetDerictoriesName()
        {
            var directoreyEntries = GetDirectoreyEntries();

            List<string> result = [];

            foreach (var entryPath in directoreyEntries)
            {
                // string entryName = entryPath == BACK ? BACK : Path.GetFileName(entryPath);
                string entryName = Path.GetRelativePath(CurrentDirectory.FullName, entryPath);
                result.Add(entryName);
            }
            return result;
        }

        public void SaveAndQuit()
        {
            if (bcdPath is null)
            {
                return;
            }

            File.WriteAllText(bcdPath, CurrentDirectory.FullName);
        }

        public void Quit()
        {
            if (bcdPath is null)
            {
                return;
            }

            File.WriteAllText(bcdPath, _runDirectory);
        }
    }

}