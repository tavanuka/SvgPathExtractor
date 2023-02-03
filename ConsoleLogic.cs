using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgPathExtractor
{
    public class ConsoleLogic
    {
        private readonly FileService fileService;
        private ConsoleKey? response;
        public ConsoleLogic()
        {
            fileService = new FileService();
            response = new ConsoleKey();
        }


        public async Task Start()
        {
            Console.WriteLine("Svg path extractor");
            do
            {
                Console.Write("Are .svg files located in currently residing folder? (Y/N)");
                response = Console.ReadKey(false).Key;
                if (response is ConsoleKey.N)
                {
                    ReadFilesFromAnotherPath();
                }
                else if (response is ConsoleKey.Y)
                {
                    ReadFilesFromCurrentPath();
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
                    
                    await WriteFilesToAnotherPath();

                }
                else if (response is ConsoleKey.N)
                {
                    await WriteFilesToLocalPath();
                }
            }
            while (response is not ConsoleKey.N && response is not ConsoleKey.Y);

            Console.Write("\noperation done. Press any key to continue...");
            Console.ReadKey(true);
        }


        private void ReadFilesFromAnotherPath()
        {
            Console.Clear();
            bool isValid = false;
            do
            {
                Console.WriteLine("Please enter the folder file path:");
                fileService.FolderPath = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(fileService.FolderPath))
                {
                    Console.WriteLine("Incorrect input. please try again!\n");
                }
                else
                    isValid = true;
            }
            while (!isValid);
            fileService.ReadAllFiles();
        }

        private void ReadFilesFromCurrentPath()
        {
            Console.Clear();
            fileService.ReadAllFiles();
        }

        private void OutputFilesToAnotherPath()
        {
            bool isValid = false;
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
                else
                    isValid = true;
            }
            while (!isValid);
        }

        private async Task WriteFilesToAnotherPath()
        {
            OutputFilesToAnotherPath();
            await fileService.WriteOutputToJson();
        }
        private async Task WriteFilesToLocalPath()
        {
            fileService.CreateNewDirectory();
            await fileService.WriteOutputToJson();
        }
    }
}
