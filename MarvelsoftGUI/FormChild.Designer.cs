namespace MarvelsoftGUI
{
    partial class FormChild
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
            this.marvelsoftCSVReaderControl1 = new MarvelsoftGUI.MarvelsoftCSVReaderControl();
            this.BtnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // marvelsoftCSVReaderControl1
            // 
            this.marvelsoftCSVReaderControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.marvelsoftCSVReaderControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marvelsoftCSVReaderControl1.Location = new System.Drawing.Point(-1, 0);
            this.marvelsoftCSVReaderControl1.Name = "marvelsoftCSVReaderControl1";
            this.marvelsoftCSVReaderControl1.Size = new System.Drawing.Size(599, 516);
            this.marvelsoftCSVReaderControl1.TabIndex = 0;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.IndianRed;
            this.BtnExit.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Location = new System.Drawing.Point(13, 530);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(572, 65);
            this.BtnExit.TabIndex = 1;
            this.BtnExit.Text = "Clo&se Child Window";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // FormChild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 606);
            this.ControlBox = false;
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.marvelsoftCSVReaderControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChild";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Child Window";
            this.ResumeLayout(false);

        }

        #endregion

        private MarvelsoftCSVReaderControl marvelsoftCSVReaderControl1;
        private System.Windows.Forms.Button BtnExit;
    }
}