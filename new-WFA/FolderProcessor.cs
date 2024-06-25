using System;
using System.IO;
using System.Collections.Generic;
using AutomationScriptConverter;

namespace FolderProcessor
{
    public class Processor
    {
        /// <summary>
        /// Processes each file in the specified folder and applies the script conversion.
        /// </summary>
        /// <param name="folderPath">The folder path containing the files to be processed.</param>
        /// <param name="fileExtension">The file extension to filter files.</param>
        /// <param name="csvPath">The path to the CSV file containing the parameters.</param>
        public void ProcessFolder(string folderPath, string fileExtension, string csvPath)
        {
            var parameterList = LoadParametersFromCsv(csvPath);
            var scriptConverter = new AutoConverter();

            foreach (var file in Directory.GetFiles(folderPath, $"*{fileExtension}", SearchOption.AllDirectories))
            {
                var parameters = FindMatchingParameters(file, parameterList);
                if (parameters == null)
                {
                    Console.WriteLine($"No matching parameters found for file: {file}");
                    continue;
                }

                Console.WriteLine($"Processing file: {file} with parameters {parameters.OldMethodName}, {parameters.NewMethodName}, {parameters.AutomationSetId}");
                scriptConverter.ConvertScript(file, parameters.OldMethodName, parameters.NewMethodName, parameters.AutomationSetId);
                Console.WriteLine("Automation script updated successfully.");
            }
        }

        /// <summary>
        /// Loads the parameters from the CSV file.
        /// </summary>
        /// <param name="csvFilePath">The path to the CSV file.</param>
        /// <returns>A list of parameter sets.</returns>
        private List<ParameterSet> LoadParametersFromCsv(string csvFilePath)
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

        /// <summary>
        /// Finds the matching parameters for the given file.
        /// </summary>
        /// <param name="filePath">The file path of the .pAutomation file.</param>
        /// <param name="parameterList">The list of parameter sets.</param>
        /// <returns>The matching parameter set.</returns>
        private ParameterSet FindMatchingParameters(string filePath, List<ParameterSet> parameterList)
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

    /// <summary>
    /// Represents a set of parameters for script conversion.
    /// </summary>
    public class ParameterSet
    {
        public string OldMethodName { get; set; }
        public string NewMethodName { get; set; }
        public string AutomationSetId { get; set; }
    }
}
