namespace MurliAnveshan
{
    partial class MainForm
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnBuildIndex = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.grbAvyakthMurliCategory = new System.Windows.Forms.GroupBox();
            this.rdbDrills = new System.Windows.Forms.RadioButton();
            this.rdbAvyakthTitles = new System.Windows.Forms.RadioButton();
            this.rdbAvyakthAll = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbAvyakthMurlis = new System.Windows.Forms.RadioButton();
            this.rdbSakarMurlis = new System.Windows.Forms.RadioButton();
            this.grbSakarMurliCategory = new System.Windows.Forms.GroupBox();
            this.rdbSlogans = new System.Windows.Forms.RadioButton();
            this.rdbBlessings = new System.Windows.Forms.RadioButton();
            this.rdbSakarQuestionAnswers = new System.Windows.Forms.RadioButton();
            this.rdbSakarTitles = new System.Windows.Forms.RadioButton();
            this.rdbDharnaPoints = new System.Windows.Forms.RadioButton();
            this.grbAvyakthMurliCategory.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grbSakarMurliCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(597, 23);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.txtSearch.ForeColor = System.Drawing.Color.Navy;
            this.txtSearch.Location = new System.Drawing.Point(340, 20);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(236, 33);
            this.txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(248, 29);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(90, 16);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search Term: ";
            // 
            // btnBuildIndex
            // 
            this.btnBuildIndex.Location = new System.Drawing.Point(800, 79);
            this.btnBuildIndex.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuildIndex.Name = "btnBuildIndex";
            this.btnBuildIndex.Size = new System.Drawing.Size(133, 28);
            this.btnBuildIndex.TabIndex = 3;
            this.btnBuildIndex.Text = "Build Index";
            this.btnBuildIndex.UseVisualStyleBackColor = true;
            this.btnBuildIndex.Click += new System.EventHandler(this.btnBuildIndex_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Font = new System.Drawing.Font("Tiro Devanagari Hindi", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.Navy;
            this.richTextBox1.Location = new System.Drawing.Point(13, 167);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(920, 330);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // btnClear
            // 
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.Location = new System.Drawing.Point(832, 127);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // grbAvyakthMurliCategory
            // 
            this.grbAvyakthMurliCategory.Controls.Add(this.rdbDrills);
            this.grbAvyakthMurliCategory.Controls.Add(this.rdbAvyakthTitles);
            this.grbAvyakthMurliCategory.Controls.Add(this.rdbAvyakthAll);
            this.grbAvyakthMurliCategory.Location = new System.Drawing.Point(303, 111);
            this.grbAvyakthMurliCategory.Name = "grbAvyakthMurliCategory";
            this.grbAvyakthMurliCategory.Size = new System.Drawing.Size(338, 49);
            this.grbAvyakthMurliCategory.TabIndex = 3;
            this.grbAvyakthMurliCategory.TabStop = false;
            this.grbAvyakthMurliCategory.Text = "Search In : ";
            // 
            // rdbDrills
            // 
            this.rdbDrills.AutoSize = true;
            this.rdbDrills.Location = new System.Drawing.Point(149, 20);
            this.rdbDrills.Name = "rdbDrills";
            this.rdbDrills.Size = new System.Drawing.Size(59, 20);
            this.rdbDrills.TabIndex = 2;
            this.rdbDrills.Text = "Drills";
            this.rdbDrills.UseVisualStyleBackColor = true;
            // 
            // rdbAvyakthTitles
            // 
            this.rdbAvyakthTitles.AutoSize = true;
            this.rdbAvyakthTitles.Checked = true;
            this.rdbAvyakthTitles.Location = new System.Drawing.Point(49, 20);
            this.rdbAvyakthTitles.Name = "rdbAvyakthTitles";
            this.rdbAvyakthTitles.Size = new System.Drawing.Size(59, 20);
            this.rdbAvyakthTitles.TabIndex = 1;
            this.rdbAvyakthTitles.TabStop = true;
            this.rdbAvyakthTitles.Text = "Titles";
            this.rdbAvyakthTitles.UseVisualStyleBackColor = true;
            // 
            // rdbAvyakthAll
            // 
            this.rdbAvyakthAll.AutoSize = true;
            this.rdbAvyakthAll.Location = new System.Drawing.Point(249, 21);
            this.rdbAvyakthAll.Name = "rdbAvyakthAll";
            this.rdbAvyakthAll.Size = new System.Drawing.Size(41, 20);
            this.rdbAvyakthAll.TabIndex = 3;
            this.rdbAvyakthAll.Text = "All";
            this.rdbAvyakthAll.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbAvyakthMurlis);
            this.groupBox2.Controls.Add(this.rdbSakarMurlis);
            this.groupBox2.Location = new System.Drawing.Point(303, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search : ";
            // 
            // rdbAvyakthMurlis
            // 
            this.rdbAvyakthMurlis.AutoSize = true;
            this.rdbAvyakthMurlis.Checked = true;
            this.rdbAvyakthMurlis.Location = new System.Drawing.Point(47, 18);
            this.rdbAvyakthMurlis.Name = "rdbAvyakthMurlis";
            this.rdbAvyakthMurlis.Size = new System.Drawing.Size(115, 20);
            this.rdbAvyakthMurlis.TabIndex = 1;
            this.rdbAvyakthMurlis.TabStop = true;
            this.rdbAvyakthMurlis.Text = "Avyakth Murlis";
            this.rdbAvyakthMurlis.UseVisualStyleBackColor = true;
            // 
            // rdbSakarMurlis
            // 
            this.rdbSakarMurlis.AutoSize = true;
            this.rdbSakarMurlis.Location = new System.Drawing.Point(193, 19);
            this.rdbSakarMurlis.Name = "rdbSakarMurlis";
            this.rdbSakarMurlis.Size = new System.Drawing.Size(102, 20);
            this.rdbSakarMurlis.TabIndex = 0;
            this.rdbSakarMurlis.Text = "Sakar Murlis";
            this.rdbSakarMurlis.UseVisualStyleBackColor = true;
            // 
            // grbSakarMurliCategory
            // 
            this.grbSakarMurliCategory.Controls.Add(this.rdbSlogans);
            this.grbSakarMurliCategory.Controls.Add(this.rdbBlessings);
            this.grbSakarMurliCategory.Controls.Add(this.rdbSakarQuestionAnswers);
            this.grbSakarMurliCategory.Controls.Add(this.rdbSakarTitles);
            this.grbSakarMurliCategory.Controls.Add(this.rdbDharnaPoints);
            this.grbSakarMurliCategory.Location = new System.Drawing.Point(170, 111);
            this.grbSakarMurliCategory.Name = "grbSakarMurliCategory";
            this.grbSakarMurliCategory.Size = new System.Drawing.Size(604, 49);
            this.grbSakarMurliCategory.TabIndex = 3;
            this.grbSakarMurliCategory.TabStop = false;
            this.grbSakarMurliCategory.Text = "Search In : ";
            // 
            // rdbSlogans
            // 
            this.rdbSlogans.AutoSize = true;
            this.rdbSlogans.Location = new System.Drawing.Point(505, 20);
            this.rdbSlogans.Name = "rdbSlogans";
            this.rdbSlogans.Size = new System.Drawing.Size(72, 20);
            this.rdbSlogans.TabIndex = 5;
            this.rdbSlogans.Text = "Slogans";
            this.rdbSlogans.UseVisualStyleBackColor = true;
            // 
            // rdbBlessings
            // 
            this.rdbBlessings.AutoSize = true;
            this.rdbBlessings.Location = new System.Drawing.Point(400, 20);
            this.rdbBlessings.Name = "rdbBlessings";
            this.rdbBlessings.Size = new System.Drawing.Size(83, 20);
            this.rdbBlessings.TabIndex = 4;
            this.rdbBlessings.Text = "Blessings";
            this.rdbBlessings.UseVisualStyleBackColor = true;
            // 
            // rdbSakarQuestionAnswers
            // 
            this.rdbSakarQuestionAnswers.AutoSize = true;
            this.rdbSakarQuestionAnswers.Location = new System.Drawing.Point(110, 20);
            this.rdbSakarQuestionAnswers.Name = "rdbSakarQuestionAnswers";
            this.rdbSakarQuestionAnswers.Size = new System.Drawing.Size(137, 20);
            this.rdbSakarQuestionAnswers.TabIndex = 2;
            this.rdbSakarQuestionAnswers.Text = "Question-Answers";
            this.rdbSakarQuestionAnswers.UseVisualStyleBackColor = true;
            // 
            // rdbSakarTitles
            // 
            this.rdbSakarTitles.AutoSize = true;
            this.rdbSakarTitles.Checked = true;
            this.rdbSakarTitles.Location = new System.Drawing.Point(29, 20);
            this.rdbSakarTitles.Name = "rdbSakarTitles";
            this.rdbSakarTitles.Size = new System.Drawing.Size(59, 20);
            this.rdbSakarTitles.TabIndex = 1;
            this.rdbSakarTitles.TabStop = true;
            this.rdbSakarTitles.Text = "Titles";
            this.rdbSakarTitles.UseVisualStyleBackColor = true;
            // 
            // rdbDharnaPoints
            // 
            this.rdbDharnaPoints.AutoSize = true;
            this.rdbDharnaPoints.Location = new System.Drawing.Point(269, 21);
            this.rdbDharnaPoints.Name = "rdbDharnaPoints";
            this.rdbDharnaPoints.Size = new System.Drawing.Size(109, 20);
            this.rdbDharnaPoints.TabIndex = 3;
            this.rdbDharnaPoints.Text = "DharnaPoints";
            this.rdbDharnaPoints.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClear;
            this.ClientSize = new System.Drawing.Size(945, 509);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grbAvyakthMurliCategory);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnBuildIndex);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.grbSakarMurliCategory);
            this.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.grbAvyakthMurliCategory.ResumeLayout(false);
            this.grbAvyakthMurliCategory.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grbSakarMurliCategory.ResumeLayout(false);
            this.grbSakarMurliCategory.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnBuildIndex;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox grbAvyakthMurliCategory;
        private System.Windows.Forms.RadioButton rdbAvyakthTitles;
        private System.Windows.Forms.RadioButton rdbAvyakthAll;
        private System.Windows.Forms.RadioButton rdbDrills;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbAvyakthMurlis;
        private System.Windows.Forms.RadioButton rdbSakarMurlis;
        private System.Windows.Forms.GroupBox grbSakarMurliCategory;
        private System.Windows.Forms.RadioButton rdbSakarQuestionAnswers;
        private System.Windows.Forms.RadioButton rdbSakarTitles;
        private System.Windows.Forms.RadioButton rdbDharnaPoints;
        private System.Windows.Forms.RadioButton rdbSlogans;
        private System.Windows.Forms.RadioButton rdbBlessings;
    }
}

