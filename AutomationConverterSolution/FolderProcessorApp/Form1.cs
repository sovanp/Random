using System;
using System.IO;
using System.Windows.Forms;
using FolderProcessor;

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
                    DisplayCsvContent(ofd.FileName);
                }
            }
        }

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

            try
            {
                Processor processor = new Processor();
                StringWriter stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                processor.ProcessFolder(folderPath, fileExtension, csvPath);

                txtOutput.Text = stringWriter.ToString();
                MessageBox.Show("Files processed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.ToString();
                MessageBox.Show("Error processing files: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}