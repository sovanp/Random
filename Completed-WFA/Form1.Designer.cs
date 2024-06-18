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

        // Initialize the components and layout of the form
        private void InitializeComponent()
        {
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.txtFileExtension = new System.Windows.Forms.TextBox();
            this.txtCsvPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.btnBrowseCsv = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblFolderPath = new System.Windows.Forms.Label();
            this.lblFileExtension = new System.Windows.Forms.Label();
            this.lblCsvPath = new System.Windows.Forms.Label();
            this.dgvOutput = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(12, 25);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(260, 20);
            this.txtFolderPath.TabIndex = 0;
            // 
            // txtFileExtension
            // 
            this.txtFileExtension.Location = new System.Drawing.Point(12, 64);
            this.txtFileExtension.Name = "txtFileExtension";
            this.txtFileExtension.Size = new System.Drawing.Size(260, 20);
            this.txtFileExtension.TabIndex = 1;
            // 
            // txtCsvPath
            // 
            this.txtCsvPath.Location = new System.Drawing.Point(12, 103);
            this.txtCsvPath.Name = "txtCsvPath";
            this.txtCsvPath.Size = new System.Drawing.Size(260, 20);
            this.txtCsvPath.TabIndex = 2;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(278, 23);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFolder.TabIndex = 3;
            this.btnBrowseFolder.Text = "Browse...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // btnBrowseCsv
            // 
            this.btnBrowseCsv.Location = new System.Drawing.Point(278, 101);
            this.btnBrowseCsv.Name = "btnBrowseCsv";
            this.btnBrowseCsv.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCsv.TabIndex = 4;
            this.btnBrowseCsv.Text = "Browse...";
            this.btnBrowseCsv.UseVisualStyleBackColor = true;
            this.btnBrowseCsv.Click += new System.EventHandler(this.btnBrowseCsv_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(12, 140);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(341, 23);
            this.btnProcess.TabIndex = 5;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(12, 350);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(341, 130);
            this.txtOutput.TabIndex = 6;
            // 
            // lblFolderPath
            // 
            this.lblFolderPath.AutoSize = true;
            this.lblFolderPath.Location = new System.Drawing.Point(12, 9);
            this.lblFolderPath.Name = "lblFolderPath";
            this.lblFolderPath.Size = new System.Drawing.Size(64, 13);
            this.lblFolderPath.TabIndex = 7;
            this.lblFolderPath.Text = "Folder Path:";
            // 
            // lblFileExtension
            // 
            this.lblFileExtension.AutoSize = true;
            this.lblFileExtension.Location = new System.Drawing.Point(12, 48);
            this.lblFileExtension.Name = "lblFileExtension";
            this.lblFileExtension.Size = new System.Drawing.Size(72, 13);
            this.lblFileExtension.TabIndex = 8;
            this.lblFileExtension.Text = "File Extension:";
            // 
            // lblCsvPath
            // 
            this.lblCsvPath.AutoSize = true;
            this.lblCsvPath.Location = new System.Drawing.Point(12, 87);
            this.lblCsvPath.Name = "lblCsvPath";
            this.lblCsvPath.Size = new System.Drawing.Size(54, 13);
            this.lblCsvPath.TabIndex = 9;
            this.lblCsvPath.Text = "CSV Path:";
            // 
            // dgvOutput
            // 
            this.dgvOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutput.Location = new System.Drawing.Point(12, 169);
            this.dgvOutput.Name = "dgvOutput";
            this.dgvOutput.Size = new System.Drawing.Size(341, 175);
            this.dgvOutput.TabIndex = 10;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(365, 491);
            this.Controls.Add(this.dgvOutput);
            this.Controls.Add(this.lblCsvPath);
            this.Controls.Add(this.lblFileExtension);
            this.Controls.Add(this.lblFolderPath);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnBrowseCsv);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtCsvPath);
            this.Controls.Add(this.txtFileExtension);
            this.Controls.Add(this.txtFolderPath);
            this.Name = "Form1";
            this.Text = "Folder Processor";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
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
        private System.Windows.Forms.Label lblFolderPath;
        private System.Windows.Forms.Label lblFileExtension;
        private System.Windows.Forms.Label lblCsvPath;
        private System.Windows.Forms.DataGridView dgvOutput;
    }
}
