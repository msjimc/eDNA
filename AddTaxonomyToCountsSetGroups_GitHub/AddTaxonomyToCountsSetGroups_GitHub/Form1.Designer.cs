namespace AddTaxonomyToCountsSetGroups_GitHub
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
            this.btnGetXML = new System.Windows.Forms.Button();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.lblUpdate2 = new System.Windows.Forms.Label();
            this.lblUpdate3 = new System.Windows.Forms.Label();
            this.lblUpdate4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnCounts = new System.Windows.Forms.Button();
            this.btnCombine = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBlastFile = new System.Windows.Forms.Label();
            this.lblCounts = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetXML
            // 
            this.btnGetXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetXML.Location = new System.Drawing.Point(399, 40);
            this.btnGetXML.Name = "btnGetXML";
            this.btnGetXML.Size = new System.Drawing.Size(75, 23);
            this.btnGetXML.TabIndex = 0;
            this.btnGetXML.Text = "Blast data";
            this.btnGetXML.UseVisualStyleBackColor = true;
            this.btnGetXML.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoEllipsis = true;
            this.lblUpdate.Location = new System.Drawing.Point(13, 16);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(461, 33);
            this.lblUpdate.TabIndex = 1;
            this.lblUpdate.Text = "Pending";
            // 
            // lblUpdate2
            // 
            this.lblUpdate2.AutoEllipsis = true;
            this.lblUpdate2.Location = new System.Drawing.Point(13, 49);
            this.lblUpdate2.Name = "lblUpdate2";
            this.lblUpdate2.Size = new System.Drawing.Size(461, 43);
            this.lblUpdate2.TabIndex = 2;
            this.lblUpdate2.Text = "Pending";
            // 
            // lblUpdate3
            // 
            this.lblUpdate3.AutoEllipsis = true;
            this.lblUpdate3.Location = new System.Drawing.Point(13, 92);
            this.lblUpdate3.Name = "lblUpdate3";
            this.lblUpdate3.Size = new System.Drawing.Size(461, 43);
            this.lblUpdate3.TabIndex = 3;
            this.lblUpdate3.Text = "Pending";
            // 
            // lblUpdate4
            // 
            this.lblUpdate4.AutoEllipsis = true;
            this.lblUpdate4.Location = new System.Drawing.Point(16, 135);
            this.lblUpdate4.Name = "lblUpdate4";
            this.lblUpdate4.Size = new System.Drawing.Size(458, 43);
            this.lblUpdate4.TabIndex = 4;
            this.lblUpdate4.Text = "Pending";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(403, 388);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Save to";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(322, 388);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Open";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCounts
            // 
            this.btnCounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCounts.Enabled = false;
            this.btnCounts.Location = new System.Drawing.Point(399, 89);
            this.btnCounts.Name = "btnCounts";
            this.btnCounts.Size = new System.Drawing.Size(75, 23);
            this.btnCounts.TabIndex = 7;
            this.btnCounts.Text = "Counts data";
            this.btnCounts.UseVisualStyleBackColor = true;
            this.btnCounts.Click += new System.EventHandler(this.btnCounts_Click);
            // 
            // btnCombine
            // 
            this.btnCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCombine.Enabled = false;
            this.btnCombine.Location = new System.Drawing.Point(399, 126);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(75, 23);
            this.btnCombine.TabIndex = 8;
            this.btnCombine.Text = "Combine";
            this.btnCombine.UseVisualStyleBackColor = true;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCounts);
            this.groupBox1.Controls.Add(this.btnCombine);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblBlastFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCounts);
            this.groupBox1.Controls.Add(this.btnGetXML);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 164);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select data files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select the file containing the aggergated BLAST results.";
            // 
            // lblBlastFile
            // 
            this.lblBlastFile.Location = new System.Drawing.Point(13, 45);
            this.lblBlastFile.Name = "lblBlastFile";
            this.lblBlastFile.Size = new System.Drawing.Size(300, 16);
            this.lblBlastFile.TabIndex = 10;
            this.lblBlastFile.Text = "Not set";
            // 
            // lblCounts
            // 
            this.lblCounts.Location = new System.Drawing.Point(13, 94);
            this.lblCounts.Name = "lblCounts";
            this.lblCounts.Size = new System.Drawing.Size(300, 16);
            this.lblCounts.TabIndex = 12;
            this.lblCounts.Text = "Not set";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(300, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Select the read count data generated by \'Aggregator\' software";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(315, 28);
            this.label5.TabIndex = 15;
            this.label5.Text = "Press the \'Combine\' button to retreve the taxomonic data and combine it with the " +
    "read count data based on the BLAST results.";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblUpdate);
            this.groupBox2.Controls.Add(this.lblUpdate2);
            this.groupBox2.Controls.Add(this.lblUpdate3);
            this.groupBox2.Controls.Add(this.lblUpdate4);
            this.groupBox2.Location = new System.Drawing.Point(4, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 180);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuit.Location = new System.Drawing.Point(20, 369);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 11;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 408);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Add taxonomy to read counts";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetXML;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.Label lblUpdate2;
        private System.Windows.Forms.Label lblUpdate3;
        private System.Windows.Forms.Label lblUpdate4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnCounts;
        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCounts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBlastFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnQuit;
    }
}

