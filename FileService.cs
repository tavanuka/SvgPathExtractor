using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace SvgPathExtractor
{
    public class FileService
    {
        public string FolderPath { get; set; }
        public string OutputPath { get; set; } = string.Empty;
        private string[] files { get;  set; }
        private List<FileOutput> regexResults { get; set; }
        public FileService(string folderInput, string folderOutput)
        { 
            folderPath = folderInput;
            outputPath = folderOutput;
            files = Directory.GetFiles(folderPath, "*.svg");
            regexResults = new List<FileOutput>();
        }

        public void CreateNewDirectory()
        {
           OutputPath = Directory.CreateDirectory($"{FolderPath}\\output").FullName;
        }

        public void ReadAllFiles()
        {
            RegexService regex = new RegexService();
            string path;
            string fullPath;
            string name;
            string fileContent;
            for (int id = 0; id < files.Length; id++ )
            {
                if (File.Exists(files[id]))
                {
                    name = Path.GetFileNameWithoutExtension(files[id]);

                    name = Path.GetFileNameWithoutExtension(files[id]);
                    Console.WriteLine($"File {name}.svg exists. reading...");

                    fileContent = File.ReadAllText(files[id]);
                    path = regex.GetSvgPath(fileContent);
                    fullPath = regex.GetPathElementAsString(fileContent);
                    regexResults.Add(new FileOutput { ID = id + 1, Path = path, FullPath = fullPath, Name = name });
                }
            }
        }

        public async Task WriteOutputToJson()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            foreach (var item in regexResults)
            {
                await using FileStream createStream = File.Create($"{OutputPath}\\{item.Name}.json");
                await JsonSerializer.SerializeAsync(createStream, item, options);
            }
        }

    }
}
