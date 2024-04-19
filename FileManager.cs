using System.Diagnostics;
using System.Linq.Expressions;

namespace BetterCD
{
    public class FileManager
    {
        public DirectoryInfo CurrentDirectory { get; set; }
        private string? bcdPath;

        public FileManager(string[] args)
        {
            CurrentDirectory = new DirectoryInfo("./");

            HandleArgs(args);

            if (!CurrentDirectory.Exists)
            {
                throw new DirectoryNotFoundException(CurrentDirectory.FullName);
            }
        }

        private void HandleArgs(string[] args)
        {
            bcdPath = args.Length switch
            {
                0 => null,
                1 => args[0],
                2 => args[0],
                _ => throw new ArgumentException()
            };

            CurrentDirectory = args.Length switch
            {
                0 => new DirectoryInfo("./"),
                1 => new DirectoryInfo("./"),
                2 => new DirectoryInfo(args[1]),
                _ => throw new ArgumentException()
            };
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
            if (bcdPath is not null)
            {
                File.WriteAllText(bcdPath, CurrentDirectory.FullName);
            }
        }
    }
}