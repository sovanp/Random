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
            Console.WriteLine("Usage: FolderProcessor <folderPath> <fileExtension> <csvFilePath>");
            return;
        }

        string folderPath = args[0];
        string fileExtension = args[1];
        string csvFilePath = args[2];

        var csvLines = File.ReadAllLines(csvFilePath);
        foreach (var line in csvLines)
        {
            var columns = line.Split(',');
            if (columns.Length == 3)
            {
                var oldMethodName = columns[0].Trim();
                var newMethodName = columns[1].Trim();
                var automationSetId = columns[2].Trim();

                foreach (var file in Directory.GetFiles(folderPath, $"*{fileExtension}", SearchOption.AllDirectories))
                {
                    Console.WriteLine($"Processing file: {file}");

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = $"run --project \"..\\..\\..\\AutomationScriptConverter\\AutomationScriptConverter.csproj\" \"{file}\" \"{oldMethodName}\" \"{newMethodName}\" \"{automationSetId}\"",
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

        Console.WriteLine("Processing completed successfully.");
    }
}
