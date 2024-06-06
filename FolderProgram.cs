using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.FileIO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: FolderProcessor <FolderPath> <FileExtension> <CSVFilePath>");
            return;
        }

        string folderPath = args[0];
        string fileExtension = args[1];
        string csvFilePath = args[2];
        string projectDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\AutomationScriptConverter\\bin\\Debug\\AutomationScriptConverter.exe");

        foreach (var file in Directory.GetFiles(folderPath, $"*{fileExtension}", SearchOption.AllDirectories))
        {
            Console.WriteLine($"Processing file: {file}");

            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length == 3)
                    {
                        string oldMethodName = fields[0];
                        string newMethodName = fields[1];
                        string automationSetId = fields[2];

                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = projectDirectory,
                            Arguments = $"\"{file}\" \"{oldMethodName}\" \"{newMethodName}\" \"{automationSetId}\"",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };

                        using (Process process = Process.Start(startInfo))
                        {
                            process.WaitForExit();

                            string output = process.StandardOutput.ReadToEnd();
                            string error = process.StandardError.ReadToEnd();

                            Console.WriteLine($"Standard Output: {output}");
                            if (!string.IsNullOrEmpty(error))
                            {
                                Console.WriteLine($"Standard Error: {error}");
                            }

                            if (process.ExitCode != 0)
                            {
                                Console.WriteLine($"Error processing file. Exit Code: {process.ExitCode}");
                            }
                        }
                    }
                }
            }
        }
    }
}
