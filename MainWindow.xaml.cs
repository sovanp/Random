using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace AutomationScriptWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPathTextBox.Text = dialog.SelectedPath;
            }
        }

        private void BrowseCSVButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                CSVFilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = FolderPathTextBox.Text;
            string fileExtension = FileExtensionTextBox.Text;
            string csvFilePath = CSVFilePathTextBox.Text;
            string folderProcessorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\FolderProcessor\\bin\\Debug\\FolderProcessor.exe");

            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileExtension) || string.IsNullOrEmpty(csvFilePath))
            {
                MessageBox.Show("Please provide all inputs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = folderProcessorPath,
                Arguments = $"\"{folderPath}\" \"{fileExtension}\" \"{csvFilePath}\"",
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

                MessageBox.Show($"Standard Output: {output}");
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show($"Standard Error: {error}");
                }

                if (process.ExitCode != 0)
                {
                    MessageBox.Show($"Error processing folder. Exit Code: {process.ExitCode}");
                }
                else
                {
                    MessageBox.Show("Processing completed successfully.");
                }
            }
        }
    }
}
