namespace AutomationConverterGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileExtension = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCSVFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowseCSV = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder Path:";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(85, 10);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(391, 20);
            this.txtFolderPath.TabIndex = 1;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(482, 8);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFolder.TabIndex = 2;
            this.btnBrowseFolder.Text = "Browse...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "File Extension:";
            // 
            // txtFileExtension
            // 
            this.txtFileExtension.Location = new System.Drawing.Point(85, 39);
            this.txtFileExtension.Name = "txtFileExtension";
            this.txtFileExtension.Size = new System.Drawing.Size(100, 20);
            this.txtFileExtension.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "CSV File:";
            // 
            // txtCSVFilePath
            // 
            this.txtCSVFilePath.Location = new System.Drawing.Point(85, 66);
            this.txtCSVFilePath.Name = "txtCSVFilePath";
            this.txtCSVFilePath.Size = new System.Drawing.Size(391, 20);
            this.txtCSVFilePath.TabIndex = 6;
            // 
            // btnBrowseCSV
            // 
            this.btnBrowseCSV.Location = new System.Drawing.Point(482, 64);
            this.btnBrowseCSV.Name = "btnBrowseCSV";
            this.btnBrowseCSV.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCSV.TabIndex = 7;
            this.btnBrowseCSV.Text = "Browse...";
            this.btnBrowseCSV.UseVisualStyleBackColor = true;
            this.btnBrowseCSV.Click += new System.EventHandler(this.btnBrowseCSV_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 103);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(16, 132);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(541, 306);
            this.dataGridView.TabIndex = 9;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(569, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBrowseCSV);
            this.Controls.Add(this.txtCSVFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFileExtension);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Automation Converter GUI";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFileExtension;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCSVFilePath;
        private System.Windows.Forms.Button btnBrowseCSV;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}
