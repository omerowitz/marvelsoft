namespace MarvelsoftGUI
{
    partial class MarvelsoftCSVReaderControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnBrowse = new System.Windows.Forms.Button();
            this.TxtFilename = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CsvFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ProductListView = new System.Windows.Forms.ListView();
            this.LblProcess = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DropDownPerPage = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblCurrentPage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblTotalRecords = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnLast = new System.Windows.Forms.Button();
            this.BtnFirst = new System.Windows.Forms.Button();
            this.BtnPrev = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.LblTotalPages = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Location = new System.Drawing.Point(462, 101);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(115, 37);
            this.BtnBrowse.TabIndex = 0;
            this.BtnBrowse.Text = "Bro&wse...";
            this.BtnBrowse.UseVisualStyleBackColor = true;
            this.BtnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // TxtFilename
            // 
            this.TxtFilename.Location = new System.Drawing.Point(19, 116);
            this.TxtFilename.Name = "TxtFilename";
            this.TxtFilename.ReadOnly = true;
            this.TxtFilename.Size = new System.Drawing.Size(437, 21);
            this.TxtFilename.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select a special &Marvelsoft CSV file previously parsed and prepared using the co" +
    "nsole app:";
            // 
            // CsvFileDialog
            // 
            this.CsvFileDialog.AddExtension = false;
            this.CsvFileDialog.DefaultExt = "csv";
            this.CsvFileDialog.Filter = "CSV Files (.csv)|*.csv";
            this.CsvFileDialog.ReadOnlyChecked = true;
            this.CsvFileDialog.RestoreDirectory = true;
            this.CsvFileDialog.ShowHelp = true;
            this.CsvFileDialog.SupportMultiDottedExtensions = true;
            this.CsvFileDialog.Title = "Select CSV file...";
            this.CsvFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.CsvFileDialog_FileOk);
            // 
            // ProductListView
            // 
            this.ProductListView.HideSelection = false;
            this.ProductListView.Location = new System.Drawing.Point(19, 156);
            this.ProductListView.Name = "ProductListView";
            this.ProductListView.Size = new System.Drawing.Size(558, 289);
            this.ProductListView.TabIndex = 3;
            this.ProductListView.UseCompatibleStateImageBehavior = false;
            this.ProductListView.Visible = false;
            this.ProductListView.DoubleClick += new System.EventHandler(this.ProductListView_DoubleClick);
            // 
            // LblProcess
            // 
            this.LblProcess.AutoSize = true;
            this.LblProcess.Location = new System.Drawing.Point(19, 140);
            this.LblProcess.Name = "LblProcess";
            this.LblProcess.Size = new System.Drawing.Size(92, 13);
            this.LblProcess.TabIndex = 4;
            this.LblProcess.Text = "Processing files...";
            this.LblProcess.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(599, 79);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(482, 39);
            this.label2.TabIndex = 7;
            this.label2.Text = "Marvelsoft CSV reader v0.1.1";
            // 
            // DropDownPerPage
            // 
            this.DropDownPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DropDownPerPage.FormatString = "N0";
            this.DropDownPerPage.FormattingEnabled = true;
            this.DropDownPerPage.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100",
            "200",
            "500",
            "1000"});
            this.DropDownPerPage.Location = new System.Drawing.Point(76, 477);
            this.DropDownPerPage.MaxLength = 4;
            this.DropDownPerPage.Name = "DropDownPerPage";
            this.DropDownPerPage.Size = new System.Drawing.Size(55, 21);
            this.DropDownPerPage.TabIndex = 8;
            this.DropDownPerPage.Visible = false;
            this.DropDownPerPage.SelectedIndexChanged += new System.EventHandler(this.DropDownPerPage_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 481);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Per page:";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 481);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Page:";
            this.label4.Visible = false;
            // 
            // LblCurrentPage
            // 
            this.LblCurrentPage.AutoSize = true;
            this.LblCurrentPage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCurrentPage.Location = new System.Drawing.Point(165, 481);
            this.LblCurrentPage.Name = "LblCurrentPage";
            this.LblCurrentPage.Size = new System.Drawing.Size(28, 13);
            this.LblCurrentPage.TabIndex = 11;
            this.LblCurrentPage.Text = "{0}";
            this.LblCurrentPage.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 481);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "pages, of";
            this.label5.Visible = false;
            // 
            // LblTotalRecords
            // 
            this.LblTotalRecords.AutoSize = true;
            this.LblTotalRecords.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalRecords.Location = new System.Drawing.Point(295, 481);
            this.LblTotalRecords.Name = "LblTotalRecords";
            this.LblTotalRecords.Size = new System.Drawing.Size(28, 13);
            this.LblTotalRecords.TabIndex = 13;
            this.LblTotalRecords.Text = "{2}";
            this.LblTotalRecords.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(335, 481);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "records.";
            this.label6.Visible = false;
            // 
            // BtnLast
            // 
            this.BtnLast.Location = new System.Drawing.Point(527, 477);
            this.BtnLast.Name = "BtnLast";
            this.BtnLast.Size = new System.Drawing.Size(50, 23);
            this.BtnLast.TabIndex = 15;
            this.BtnLast.Text = "Last";
            this.BtnLast.UseVisualStyleBackColor = true;
            this.BtnLast.Visible = false;
            this.BtnLast.Click += new System.EventHandler(this.BtnLast_Click);
            // 
            // BtnFirst
            // 
            this.BtnFirst.Enabled = false;
            this.BtnFirst.Location = new System.Drawing.Point(383, 477);
            this.BtnFirst.Name = "BtnFirst";
            this.BtnFirst.Size = new System.Drawing.Size(50, 23);
            this.BtnFirst.TabIndex = 16;
            this.BtnFirst.Text = "First";
            this.BtnFirst.UseVisualStyleBackColor = true;
            this.BtnFirst.Visible = false;
            this.BtnFirst.Click += new System.EventHandler(this.BtnFirst_Click);
            // 
            // BtnPrev
            // 
            this.BtnPrev.Enabled = false;
            this.BtnPrev.Location = new System.Drawing.Point(431, 477);
            this.BtnPrev.Name = "BtnPrev";
            this.BtnPrev.Size = new System.Drawing.Size(50, 23);
            this.BtnPrev.TabIndex = 17;
            this.BtnPrev.Text = "Prev";
            this.BtnPrev.UseVisualStyleBackColor = true;
            this.BtnPrev.Visible = false;
            this.BtnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Location = new System.Drawing.Point(479, 477);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(50, 23);
            this.BtnNext.TabIndex = 18;
            this.BtnNext.Text = "Next";
            this.BtnNext.UseVisualStyleBackColor = true;
            this.BtnNext.Visible = false;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // LblTotalPages
            // 
            this.LblTotalPages.AutoSize = true;
            this.LblTotalPages.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalPages.Location = new System.Drawing.Point(208, 481);
            this.LblTotalPages.Name = "LblTotalPages";
            this.LblTotalPages.Size = new System.Drawing.Size(28, 13);
            this.LblTotalPages.TabIndex = 19;
            this.LblTotalPages.Text = "{1}";
            this.LblTotalPages.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 481);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "of";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(19, 448);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(336, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Double click on any item in the list to show more information about it.";
            this.label7.Visible = false;
            // 
            // MarvelsoftCSVReaderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.LblTotalPages);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.BtnPrev);
            this.Controls.Add(this.BtnFirst);
            this.Controls.Add(this.BtnLast);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LblTotalRecords);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LblCurrentPage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DropDownPerPage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LblProcess);
            this.Controls.Add(this.ProductListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtFilename);
            this.Controls.Add(this.BtnBrowse);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MarvelsoftCSVReaderControl";
            this.Size = new System.Drawing.Size(599, 516);
            this.Load += new System.EventHandler(this.MarvelsoftCSVReaderControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnBrowse;
        private System.Windows.Forms.TextBox TxtFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog CsvFileDialog;
        private System.Windows.Forms.ListView ProductListView;
        private System.Windows.Forms.Label LblProcess;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DropDownPerPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblCurrentPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblTotalRecords;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnLast;
        private System.Windows.Forms.Button BtnFirst;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.Label LblTotalPages;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}
