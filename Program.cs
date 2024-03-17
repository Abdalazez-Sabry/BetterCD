using BetterCd;

class Hello
{
    public static void Main(string[] args)
    {
        var fileManager = new FileManager(new DirectoryInfo("./"), args);
        fileManager.Run();
    }
}
