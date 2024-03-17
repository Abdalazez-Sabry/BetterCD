using System.Diagnostics;

namespace BetterCd
{
    public class FileManager 
    {
        public DirectoryInfo CurrentDirectory { get; set; }

        private const string QUIT = "q";
        private string? bcdPath;
        private bool isRunning = false;

        public FileManager(DirectoryInfo curernt, string[] args) 
        {
            CurrentDirectory = curernt;

            HandleArgs(args);

            if (!CurrentDirectory.Exists) 
            {
                throw new DirectoryNotFoundException(CurrentDirectory.FullName);
            }
        }

        private void HandleArgs(string[] args) {
            bcdPath = args.Length switch 
            {
                0 => null,
                1 => args[0],
                _ => throw new ArgumentException()
            };
        }

        private List<string> GetDirectoreyEntries() 
        {
            // var subDirectories = Directory.GetDirectories(CurrentDirectory.FullName);
            var subDirectories = CurrentDirectory.GetDirectories();
            var subFiles = CurrentDirectory.GetFiles();

            var directoreyEntries = new List<string>(subDirectories.Length + subFiles.Length);
            
            if (CurrentDirectory.Parent is null) {
                directoreyEntries.AddRange(Directory.GetLogicalDrives());
            } else {
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

        private void NavigateOrOpen(string path) 
        {
            Console.WriteLine(path);
            if (Directory.Exists(path)) 
            {
                CurrentDirectory = new DirectoryInfo(path);

            } else if (File.Exists(path)) 
            {
                Process process = new();
                process.StartInfo.FileName = path;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            } else 
            {
                throw new FieldAccessException(path);
            }

        }

        private void PrintFileNamesInDirectory(IList<string> directoreyEntries) 
        {
            var currentOption = 0 ; 
            Console.WriteLine($"{CurrentDirectory.FullName}");

            foreach (var entryPath in directoreyEntries)
            {
                // string entryName = entryPath == BACK ? BACK : Path.GetFileName(entryPath);
                string entryName = Path.GetRelativePath(CurrentDirectory.FullName, entryPath);
                Console.WriteLine($"{currentOption} -> {entryName}");
                currentOption++;
            }

            Console.WriteLine();
        }

        private void QuitAndSave() {
            if (bcdPath is not null) {
                File.WriteAllText(bcdPath, CurrentDirectory.FullName);
                isRunning = false;
            }
        }

        private void ReadOption(List<string> directoreyEntries) 
        {
            var optionStr = Console.ReadLine() ?? throw new KeyNotFoundException();

            if (optionStr == QUIT) 
            {
                QuitAndSave();
                return;
            }

            if (!int.TryParse(optionStr, out int option)) 
            {
                throw new NotFiniteNumberException(optionStr);
            }

            if (option < 0 || option >= directoreyEntries.Count) 
            {
                throw new IndexOutOfRangeException();
            }

            NavigateOrOpen(directoreyEntries[option]);

        }

        public void Run() 
        {
            isRunning = true;

            while (isRunning)
            {
                List<string> naviationPaths = GetDirectoreyEntries();
                PrintFileNamesInDirectory(naviationPaths);
                ReadOption(naviationPaths);
            }
        }
    }
}