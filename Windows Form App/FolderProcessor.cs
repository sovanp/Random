using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

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
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
        {
            parameterList = csv.GetRecords<ParameterSet>().ToList();
        }
        return parameterList;
    }

    static void ProcessFolder(string folderPath, string fileExtension, List<ParameterSet> parameterList)
    {
        string automationScriptConverterPath = @"C:\Users\vn82\Documents\Visual Studio 2015\Projects\AutomationConverterSolution\AutomationScriptConverter\bin\Debug\AutomationScriptConverter.exe";
        
        int paramIndex = 0;
        foreach (var file in Directory.GetFiles(folderPath, $"*{fileExtension}", SearchOption.AllDirectories))
        {
            if (paramIndex >= parameterList.Count)
            {
                Console.WriteLine("Not enough parameters in the CSV file to process all files.");
                break;
            }

            var parameters = parameterList[paramIndex++];
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
}

public class ParameterSet
{
    public string OldMethodName { get; set; }
    public string NewMethodName { get; set; }
    public string AutomationSetId { get; set; }
}
