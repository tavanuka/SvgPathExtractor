// See https://aka.ms/new-console-template for more information
using SvgPathExtractor;

string folderInputPath;
string folderOutputPath;
do
{
    Console.WriteLine("Svg path extractor");
    Console.WriteLine("Please enter the folder file path:");
    folderInputPath = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(folderInputPath))
    {
        Console.WriteLine("Incorrect input. please try again!\n");
    }
}
while (string.IsNullOrWhiteSpace(folderInputPath));

do
{
    Console.WriteLine("Where would you like to output the result?");
    Console.Write("Path: ");
    folderOutputPath = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(folderOutputPath))
    {
        Console.WriteLine("Incorrect input. please try again!\n");
    }

}
while (string.IsNullOrWhiteSpace(folderOutputPath));

FileService fileService = new(folderInputPath, folderOutputPath);
fileService.ReadAllFiles();
await fileService.WriteOutputToJson();
Console.Write("\noperation done. Press any key to continue...");
Console.ReadLine();



