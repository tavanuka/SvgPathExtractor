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
        private string[]? Files { get; set; }
        private List<FileOutput> RegexResults { get; set; }

        public FileService()
        {
            FolderPath = Directory.GetCurrentDirectory();
            RegexResults = new List<FileOutput>();
        }

        public void CreateNewDirectory()
        {
            string localPath = Directory.GetCurrentDirectory();
            OutputPath = Directory.CreateDirectory($"{localPath}\\output").FullName;
        }

        public void ReadAllFiles()
        {
            Files = Directory.GetFiles(FolderPath, "*.svg");
            RegexService regex = new RegexService();
            string path;
            string fullPath;
            string name;
            string fileContent;
            for (int id = 0; id < Files.Length; id++ )
            {
                if (File.Exists(Files[id]))
                {
                    name = Path.GetFileNameWithoutExtension(Files[id]);

                    Console.WriteLine($"File {name}.svg exists. reading...");

                    fileContent = File.ReadAllText(Files[id]);
                    path = regex.GetSvgPath(fileContent);
                    fullPath = regex.GetPathElementAsString(fileContent);
                    RegexResults.Add(new FileOutput { ID = id + 1, Path = path, FullPath = fullPath, Name = name });
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
            foreach (var item in RegexResults)
            {
                await using FileStream createStream = File.Create($"{OutputPath}\\{item.Name}.json");
                await JsonSerializer.SerializeAsync(createStream, item, options);
            }
        }

    }
}
