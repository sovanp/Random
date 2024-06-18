using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FolderProcessorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

        private void btnBrowseCsv_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    txtCsvPath.Text = ofd.FileName;
                }
            }
        }

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

        private void ProcessFolder(string folderPath, string fileExtension, string csvPath)
        {
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
