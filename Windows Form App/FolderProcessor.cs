using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

class FolderProcessor
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: FolderProcessor <FolderPath> <FileExtension> <CsvFilePath>");
            return;
        }

        string folderPath = args[0];
        string fileExtension = args[1];
        string csvFilePath = args[2];

        var parameterList = LoadParametersFromCsv(csvFilePath);
        ProcessFolder(folderPath, fileExtension, parameterList);
    }

    static List<ParameterSet> LoadParametersFromCsv(string csvFilePath)
    {
        var parameterList = new List<ParameterSet>();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue; // Skip header
                }
                var values = line.Split(',');
                if (values.Length == 3)
                {
                    parameterList.Add(new ParameterSet
                    {
                        OldMethodName = values[0],
                        NewMethodName = values[1],
                        AutomationSetId = values[2]
                    });
                }
            }
        }
        return parameterList;
    }

    static void ProcessFolder(string folderPath, string fileExtension, List<ParameterSet> parameterList)
    {
        string automationScriptConverterPath = @"C:\Users\vn82\Documents\Visual Studio 2015\Projects\AutomationConverterSolution\AutomationScriptConverter\bin\Debug\AutomationScriptConverter.exe";

        foreach (var file in Directory.GetFiles(folderPath, $"*{fileExtension}", SearchOption.AllDirectories))
        {
            var parameters = FindMatchingParameters(file, parameterList);
            if (parameters == null)
            {
                Console.WriteLine($"No matching parameters found for file: {file}");
                continue;
            }

            Console.WriteLine($"Processing file: {file} with parameters {parameters.OldMethodName}, {parameters.NewMethodName}, {parameters.AutomationSetId}");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = automationScriptConverterPath,
                Arguments = $"\"{file}\" \"{parameters.OldMethodName}\" \"{parameters.NewMethodName}\" \"{parameters.AutomationSetId}\"",
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
                    Console.WriteLine($"Error processing file {file}: {error}");
                }
                else
                {
                    Console.WriteLine($"Successfully processed file: {file}");
                }
            }
        }
    }

    static ParameterSet FindMatchingParameters(string filePath, List<ParameterSet> parameterList)
    {
        var fileContent = File.ReadAllText(filePath);
        foreach (var parameters in parameterList)
        {
            if (fileContent.Contains(parameters.OldMethodName))
            {
                return parameters;
            }
        }
        return null;
    }
}

public class ParameterSet
{
    public string OldMethodName { get; set; }
    public string NewMethodName { get; set; }
    public string AutomationSetId { get; set; }
}
