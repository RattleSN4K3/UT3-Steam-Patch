namespace UT3SteamPatch
{
    partial class Main
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

        #region

        private void InitializeComponent()
        {
            this.RichTextEULA = new System.Windows.Forms.RichTextBox();
            this.ButtonAccept = new System.Windows.Forms.Button();
            this.ButtonDecline = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DialogFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RichTextEULA
            // 
            this.RichTextEULA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextEULA.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.RichTextEULA, 3);
            this.RichTextEULA.Location = new System.Drawing.Point(3, 3);
            this.RichTextEULA.Name = "RichTextEULA";
            this.RichTextEULA.ReadOnly = true;
            this.RichTextEULA.Size = new System.Drawing.Size(658, 393);
            this.RichTextEULA.TabIndex = 0;
            this.RichTextEULA.Text = "";
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonAccept.Location = new System.Drawing.Point(484, 409);
            this.ButtonAccept.Margin = new System.Windows.Forms.Padding(10);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(75, 23);
            this.ButtonAccept.TabIndex = 1;
            this.ButtonAccept.Text = "Accept";
            this.ButtonAccept.UseVisualStyleBackColor = true;
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // ButtonDecline
            // 
            this.ButtonDecline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDecline.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonDecline.Location = new System.Drawing.Point(579, 409);
            this.ButtonDecline.Margin = new System.Windows.Forms.Padding(10);
            this.ButtonDecline.Name = "ButtonDecline";
            this.ButtonDecline.Size = new System.Drawing.Size(75, 23);
            this.ButtonDecline.TabIndex = 2;
            this.ButtonDecline.Text = "Decline";
            this.ButtonDecline.UseVisualStyleBackColor = true;
            this.ButtonDecline.Click += new System.EventHandler(this.ButtonDecline_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ButtonDecline, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.RichTextEULA, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonAccept, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(664, 442);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // DialogFolder
            // 
            this.DialogFolder.ShowNewFolderButton = false;
            // 
            // Main
            // 
            this.AcceptButton = this.ButtonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonDecline;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "End User License Agreement";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextEULA;
        private System.Windows.Forms.Button ButtonAccept;
        private System.Windows.Forms.Button ButtonDecline;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FolderBrowserDialog DialogFolder;
    }
}

