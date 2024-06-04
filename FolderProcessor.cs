using System;
using System.IO;
using System.Diagnostics;

class FolderProcessor
{
    static void Main(string[] args)
    {
        // Ensure the correct number of arguments are provided
        if (args.Length != 5)
        {
            Console.WriteLine("Usage: dotnet run <FolderPath> <FileExtension> <OldMethodName> <NewMethodName> <AutomationSetId>");
            return;
        }

        // Assign arguments to variables
        string folderPath = args[0];
        string fileExtension = args[1];
        string oldMethodName = args[2];
        string newMethodName = args[3];
        string automationSetId = args[4];

        // Validate folder path 
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"The folder path {folderPath} does not exist.");
            return;
        }

        ProcessFolder(folderPath, fileExtension, oldMethodName, newMethodName, automationSetId);
    }

    static void ProcessFolder(string folderPath, string fileExtension, string oldMethodName, string newMethodName, string automationSetId)
    {
        string autoScriptConverterPath = @"C:\Users\vn82\Documents\Visual Studio 2015\Projects\AutomationScriptConverter\AutomationScriptConverter\bin\Debug\AutomationScriptConverter.exe";

        foreach (var file in Directory.GetFiles(folderPath, $"*{fileExtension}", SearchOption.AllDirectories))
        {
            Console.WriteLine($"Processing file: {file}");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = autoScriptConverterPath,
                Arguments = $"\"{file}\" \"{oldMethodName}\" \"{newMethodName}\" \"{automationSetId}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine("Standard Output:");
                Console.WriteLine(output);
                Console.WriteLine("Standard Error:");
                Console.WriteLine(error);

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"Error processing File {file}: {error}");
                }
                else
                {
                    Console.WriteLine($"Successfully processed File {file}");
                }
            }
        }
    }
}
