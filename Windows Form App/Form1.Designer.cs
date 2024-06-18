namespace FolderProcessorApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.txtFileExtension = new System.Windows.Forms.TextBox();
            this.txtCsvPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.btnBrowseCsv = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(12, 12);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(260, 20);
            this.txtFolderPath.TabIndex = 0;
            // 
            // txtFileExtension
            // 
            this.txtFileExtension.Location = new System.Drawing.Point(12, 38);
            this.txtFileExtension.Name = "txtFileExtension";
            this.txtFileExtension.Size = new System.Drawing.Size(260, 20);
            this.txtFileExtension.TabIndex = 1;
            // 
            // txtCsvPath
            // 
            this.txtCsvPath.Location = new System.Drawing.Point(12, 64);
            this.txtCsvPath.Name = "txtCsvPath";
            this.txtCsvPath.Size = new System.Drawing.Size(260, 20);
            this.txtCsvPath.TabIndex = 2;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(278, 10);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFolder.TabIndex = 3;
            this.btnBrowseFolder.Text = "Browse...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // btnBrowseCsv
            // 
            this.btnBrowseCsv.Location = new System.Drawing.Point(278, 62);
            this.btnBrowseCsv.Name = "btnBrowseCsv";
            this.btnBrowseCsv.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCsv.TabIndex = 4;
            this.btnBrowseCsv.Text = "Browse...";
            this.btnBrowseCsv.UseVisualStyleBackColor = true;
            this.btnBrowseCsv.Click += new System.EventHandler(this.btnBrowseCsv_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(12, 90);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(341, 23);
            this.btnProcess.TabIndex = 5;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 119);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(341, 130);
            this.txtOutput.TabIndex = 6;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(365, 261);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnBrowseCsv);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtCsvPath);
            this.Controls.Add(this.txtFileExtension);
            this.Controls.Add(this.txtFolderPath);
            this.Name = "Form1";
            this.Text = "Folder Processor";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.TextBox txtFileExtension;
        private System.Windows.Forms.TextBox txtCsvPath;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Button btnBrowseCsv;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TextBox txtOutput;
    }
}
