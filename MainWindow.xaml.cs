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

        private async void ProcessButton_Click(object sender, RoutedEventArgs e)
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
        
            try
            {
                using (Process process = new Process { StartInfo = startInfo, EnableRaisingEvents = true })
                {
                    var outputBuilder = new System.Text.StringBuilder();
                    var errorBuilder = new System.Text.StringBuilder();
        
                    process.OutputDataReceived += (s, ev) => { if (ev.Data != null) outputBuilder.AppendLine(ev.Data); };
                    process.ErrorDataReceived += (s, ev) => { if (ev.Data != null) errorBuilder.AppendLine(ev.Data); };
        
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
        
                    await Task.Run(() => process.WaitForExit());
        
                    string output = outputBuilder.ToString();
                    string error = errorBuilder.ToString();
        
                    if (!string.IsNullOrEmpty(output))
                    {
                        MessageBox.Show($"Standard Output: {output}", "Output", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
        
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Standard Error: {error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
        
                    if (process.ExitCode != 0)
                    {
                        MessageBox.Show($"Error processing folder. Exit Code: {process.ExitCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Processing completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
