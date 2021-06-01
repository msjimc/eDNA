namespace FilterDatarRNAData_GitHub
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudFrom = new System.Windows.Forms.NumericUpDown();
            this.nudToo = new System.Windows.Forms.NumericUpDown();
            this.nudPercentIdentity = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudTaxonomyLevel = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.btnFiltered = new System.Windows.Forms.Button();
            this.rboPercent = new System.Windows.Forms.RadioButton();
            this.rboProportions = new System.Windows.Forms.RadioButton();
            this.rboAbsolute = new System.Windows.Forms.RadioButton();
            this.cboTaxonomyName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPercentIdentity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaxonomyLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(296, 102);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select raw data file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enter sequence size range";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "to";
            // 
            // nudFrom
            // 
            this.nudFrom.Location = new System.Drawing.Point(191, 24);
            this.nudFrom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFrom.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFrom.Name = "nudFrom";
            this.nudFrom.Size = new System.Drawing.Size(75, 20);
            this.nudFrom.TabIndex = 6;
            this.nudFrom.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // nudToo
            // 
            this.nudToo.Location = new System.Drawing.Point(296, 24);
            this.nudToo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudToo.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudToo.Name = "nudToo";
            this.nudToo.Size = new System.Drawing.Size(75, 20);
            this.nudToo.TabIndex = 7;
            this.nudToo.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // nudPercentIdentity
            // 
            this.nudPercentIdentity.Location = new System.Drawing.Point(296, 50);
            this.nudPercentIdentity.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.nudPercentIdentity.Name = "nudPercentIdentity";
            this.nudPercentIdentity.Size = new System.Drawing.Size(75, 20);
            this.nudPercentIdentity.TabIndex = 8;
            this.nudPercentIdentity.Value = new decimal(new int[] {
            98,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Percent identity to blast hit";
            // 
            // nudTaxonomyLevel
            // 
            this.nudTaxonomyLevel.Location = new System.Drawing.Point(296, 76);
            this.nudTaxonomyLevel.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudTaxonomyLevel.Name = "nudTaxonomyLevel";
            this.nudTaxonomyLevel.Size = new System.Drawing.Size(75, 20);
            this.nudTaxonomyLevel.TabIndex = 10;
            this.nudTaxonomyLevel.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudTaxonomyLevel.ValueChanged += new System.EventHandler(this.nudTaxonomyLevel_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Taxonomic level of Identity";
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(296, 131);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Number of UTOs: not set";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Results";
            // 
            // lblResults
            // 
            this.lblResults.Location = new System.Drawing.Point(12, 199);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(359, 84);
            this.lblResults.TabIndex = 15;
            this.lblResults.Text = "Not set";
            // 
            // btnFiltered
            // 
            this.btnFiltered.Enabled = false;
            this.btnFiltered.Location = new System.Drawing.Point(296, 160);
            this.btnFiltered.Name = "btnFiltered";
            this.btnFiltered.Size = new System.Drawing.Size(75, 23);
            this.btnFiltered.TabIndex = 16;
            this.btnFiltered.Text = "Filtered";
            this.btnFiltered.UseVisualStyleBackColor = true;
            this.btnFiltered.Click += new System.EventHandler(this.btnFiltered_Click);
            // 
            // rboPercent
            // 
            this.rboPercent.AutoSize = true;
            this.rboPercent.Checked = true;
            this.rboPercent.Location = new System.Drawing.Point(197, 134);
            this.rboPercent.Name = "rboPercent";
            this.rboPercent.Size = new System.Drawing.Size(62, 17);
            this.rboPercent.TabIndex = 21;
            this.rboPercent.TabStop = true;
            this.rboPercent.Text = "Percent";
            this.rboPercent.UseVisualStyleBackColor = true;
            // 
            // rboProportions
            // 
            this.rboProportions.AutoSize = true;
            this.rboProportions.Location = new System.Drawing.Point(106, 134);
            this.rboProportions.Name = "rboProportions";
            this.rboProportions.Size = new System.Drawing.Size(78, 17);
            this.rboProportions.TabIndex = 20;
            this.rboProportions.Text = "Proportions";
            this.rboProportions.UseVisualStyleBackColor = true;
            // 
            // rboAbsolute
            // 
            this.rboAbsolute.AutoSize = true;
            this.rboAbsolute.Location = new System.Drawing.Point(15, 134);
            this.rboAbsolute.Name = "rboAbsolute";
            this.rboAbsolute.Size = new System.Drawing.Size(66, 17);
            this.rboAbsolute.TabIndex = 19;
            this.rboAbsolute.Text = "Absolute";
            this.rboAbsolute.UseVisualStyleBackColor = true;
            // 
            // cboTaxonomyName
            // 
            this.cboTaxonomyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTaxonomyName.Enabled = false;
            this.cboTaxonomyName.FormattingEnabled = true;
            this.cboTaxonomyName.Items.AddRange(new object[] {
            "Kingdom        ",
            "Subkingdom     ",
            "Infrakingdom   ",
            "Superdivision  ",
            "Superphylum    ",
            "Division       ",
            "Phylum         ",
            "Subdivision    ",
            "Subphylum      ",
            "Infradivision  ",
            "Infraphylum    ",
            "Parvdivision   ",
            "Parvphylum     ",
            "Superclass     ",
            "Class          ",
            "Subclass       ",
            "Infraclass     ",
            "Superorder     ",
            "Order          ",
            "Suborder       ",
            "Infraorder     ",
            "Section        ",
            "Subsection     ",
            "Superfamily    ",
            "Family         ",
            "Subfamily    ",
            "Tribe",
            "Subtribe",
            "Genus",
            "Subgenus",
            "Species "});
            this.cboTaxonomyName.Location = new System.Drawing.Point(169, 75);
            this.cboTaxonomyName.Name = "cboTaxonomyName";
            this.cboTaxonomyName.Size = new System.Drawing.Size(121, 21);
            this.cboTaxonomyName.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 292);
            this.Controls.Add(this.cboTaxonomyName);
            this.Controls.Add(this.rboPercent);
            this.Controls.Add(this.rboProportions);
            this.Controls.Add(this.rboAbsolute);
            this.Controls.Add(this.btnFiltered);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudTaxonomyLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudPercentIdentity);
            this.Controls.Add(this.nudToo);
            this.Controls.Add(this.nudFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Filter sequences by Taxonomy";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPercentIdentity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaxonomyLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudFrom;
        private System.Windows.Forms.NumericUpDown nudToo;
        private System.Windows.Forms.NumericUpDown nudPercentIdentity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudTaxonomyLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Button btnFiltered;
        private System.Windows.Forms.RadioButton rboPercent;
        private System.Windows.Forms.RadioButton rboProportions;
        private System.Windows.Forms.RadioButton rboAbsolute;
        private System.Windows.Forms.ComboBox cboTaxonomyName;
    }
}

