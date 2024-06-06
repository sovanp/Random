using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace AutomationConverterGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderBrowser.SelectedPath;
                }
            }
        }

        private void btnBrowseCSV_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCSVFilePath.Text = openFileDialog.FileName;
                    LoadCSV(openFileDialog.FileName);
                }
            }
        }

        private void LoadCSV(string filePath)
        {
            var dt = new DataTable();
            using (var sr = new StreamReader(filePath))
            {
                var header = sr.ReadLine();
                if (header == null) return;

                var columns = header.Split(',');
                foreach (var column in columns)
                {
                    dt.Columns.Add(column);
                }

                while (!sr.EndOfStream)
                {
                    var rows = sr.ReadLine().Split(',');
                    dt.Rows.Add(rows);
                }
            }
            dataGridView.DataSource = dt;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;
            string fileExtension = txtFileExtension.Text;
            string csvFilePath = txtCSVFilePath.Text;

            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileExtension) || string.IsNullOrEmpty(csvFilePath))
            {
                MessageBox.Show("Please provide all required inputs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProcessFolder(folderPath, fileExtension, csvFilePath);
        }

        private void ProcessFolder(string folderPath, string fileExtension, string csvFilePath)
        {
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
                                FileName = @"..\..\..\AutomationScriptConverter\bin\Debug\AutomationScriptConverter.exe",
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

            MessageBox.Show("Processing completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
