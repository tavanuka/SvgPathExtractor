// See https://aka.ms/new-console-template for more information
using SvgPathExtractor;

FileService fileService = new();
ConsoleKey? response;
Console.WriteLine("Svg path extractor");
do
{
    Console.Write("Are .svg files located in currently residing folder? (Y/N)");
    response = Console.ReadKey(false).Key;
    if (response is ConsoleKey.N)
    {
        ReadFilesFromAnotherPath(fileService);
    }
    else if (response is ConsoleKey.Y)
    {
        ReadFilesFromCurrentPath(fileService);
    }
}
while (response is not ConsoleKey.N && response is not ConsoleKey.Y);

response = null;

do
{
    Console.WriteLine("Do you want to save output anywhere else? (Y/N)");
    response = Console.ReadKey(false).Key;
    if (response is ConsoleKey.Y)
    {
        OutputFilesToAnotherPath();
        await WriteFilesToAnotherPath(fileService);

    }
    else if (response is ConsoleKey.N)
    {
        await WriteFilesToLocalPath(fileService);
    }
}
while (response is not ConsoleKey.N && response is not ConsoleKey.Y);

Console.Write("\noperation done. Press any key to continue...");
Console.ReadKey(true);

void ReadFilesFromAnotherPath(FileService fileService)
{
    Console.Clear();
    do
    {
        Console.WriteLine("Please enter the folder file path:");
        fileService.FolderPath = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(fileService.FolderPath))
        {
            Console.WriteLine("Incorrect input. please try again!\n");
        }
    }
    while (string.IsNullOrWhiteSpace(fileService.FolderPath));
    fileService.ReadAllFiles();
}

void ReadFilesFromCurrentPath(FileService fileService)
{
    Console.Clear();
    fileService.ReadAllFiles();
}

void OutputFilesToAnotherPath()
{
    Console.Clear();
    do
    {
        Console.WriteLine("Where would you like to output the result?");
        Console.Write("Path: ");
        fileService.OutputPath = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(fileService.OutputPath))
        {
            Console.WriteLine("Incorrect input. please try again!\n");
        }
    }
    while (string.IsNullOrWhiteSpace(fileService.OutputPath));
}

async Task WriteFilesToAnotherPath(FileService fileService)
{
    await fileService.WriteOutputToJson();
}
async Task WriteFilesToLocalPath(FileService fileService)
{
    fileService.CreateNewDirectory();
    await fileService.WriteOutputToJson();
}



