using System;
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

        /// <summary>
        /// Event handler for Browse Folder button click.
        /// </summary>
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

        /// <summary>
        /// Event handler for Browse CSV button click.
        /// </summary>
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

        /// <summary>
        /// Displays the content of the CSV file in the DataGridView.
        /// </summary>
        /// <param name="csvFilePath">The path to the CSV file.</param>
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

        /// <summary>
        /// Event handler for Process button click.
        /// </summary>
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

            Processor processor = new Processor();
            processor.ProcessFolder(folderPath, fileExtension, csvPath);
        }
    }
}
