using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace FolderProcessorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Event handler for Browse Folder button click
        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtFolderPath.Text = fbd.SelectedPath;
                }
            }
        }

        // Event handler for Browse CSV button click
        private void btnBrowseCsv_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    txtCsvPath.Text = ofd.FileName;
                    DisplayCsvContent(ofd.FileName);
                }
            }
        }

        // Display the content of the CSV file in the DataGridView
        private void DisplayCsvContent(string csvFilePath)
        {
            dgvOutput.Rows.Clear();
            dgvOutput.Columns.Clear();
            try
            {
                using (var reader = new StreamReader(csvFilePath))
                {
                    bool isFirstLine = true;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (isFirstLine)
                        {
                            foreach (var header in values)
                            {
                                dgvOutput.Columns.Add(header, header);
                            }
                            isFirstLine = false;
                        }
                        else
                        {
                            dgvOutput.Rows.Add(values);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading CSV file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for Process button click
        private void btnProcess_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;
            string fileExtension = txtFileExtension.Text;
            string csvPath = txtCsvPath.Text;

            if (string.IsNullOrWhiteSpace(folderPath) || string.IsNullOrWhiteSpace(fileExtension) || string.IsNullOrWhiteSpace(csvPath))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProcessFolder(folderPath, fileExtension, csvPath);
        }

        // Process the files in the specified folder
        private void ProcessFolder(string folderPath, string fileExtension, string csvPath)
        {
            // Update this path to the correct location on the manager's machine
            string folderProcessorPath = @"C:\Users\vn82\Documents\Visual Studio 2015\Projects\AutomationConverterSolution\FolderProcessor\bin\Debug\FolderProcessor.exe";
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = folderProcessorPath,
                Arguments = $"\"{folderPath}\" \"{fileExtension}\" \"{csvPath}\"",
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

                txtOutput.AppendText("Standard Output:\n" + output + "\n");
                txtOutput.AppendText("Standard Error:\n" + error + "\n");

                if (process.ExitCode != 0)
                {
                    MessageBox.Show($"Error processing files: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Files processed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
