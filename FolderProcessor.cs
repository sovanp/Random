using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;
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

        // Load CSV data into a DataTable
        var dt = new DataTable();
        using (var parser = new TextFieldParser(csvFilePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            string[] headers = parser.ReadFields();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                dt.Rows.Add(fields);
            }
        }

        // Process each file in the directory
        foreach (var file in Directory.GetFiles(folderPath, $"*{fileExtension}", System.IO.SearchOption.AllDirectories))
        {
            Console.WriteLine($"Processing file: {file}");

            // Load the XML document to find the appropriate parameters
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            bool fileProcessed = false;

            foreach (DataRow row in dt.Rows)
            {
                string oldMethodName = row["OldMethodName"].ToString();
                string newMethodName = row["NewMethodName"].ToString();
                string automationSetId = row["AutomationSetId"].ToString();

                // Check if this row's parameters are used in the file
                XmlNodeList instanceNodes = doc.SelectNodes($"//ConnectionBlock[InstanceName/@Value='{oldMethodName}']");
                XmlNodeList componentNodes = doc.SelectNodes($"//OpenSpan.Automation.ConnectableMethod[ComponentName/@Value='{oldMethodName}']");

                if (instanceNodes.Count > 0 || componentNodes.Count > 0)
                {
                    // Run the AutomationScriptConverter with the found parameters
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

                    fileProcessed = true;
                    break; // Exit the loop once the correct parameters are found and used
                }
            }

            if (!fileProcessed)
            {
                Console.WriteLine($"No matching parameters found in CSV for file: {file}");
            }
        }
    }
}
