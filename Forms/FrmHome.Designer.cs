using MurliAnveshan.Controls;

namespace MurliAnveshan
{
    partial class FrmHome
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
            this.components = new System.ComponentModel.Container();
            this.grbSakarMurliCategory = new System.Windows.Forms.GroupBox();
            this.rdbSlogans = new MaterialSkin.Controls.MaterialRadioButton();
            this.rdbBlessings = new MaterialSkin.Controls.MaterialRadioButton();
            this.rdbSakarQuestionAnswers = new MaterialSkin.Controls.MaterialRadioButton();
            this.rdbSakarTitles = new MaterialSkin.Controls.MaterialRadioButton();
            this.rdbDharnaPoints = new MaterialSkin.Controls.MaterialRadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSearch = new MaterialSkin.Controls.MaterialButton();
            this.btnClear = new MaterialSkin.Controls.MaterialButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbSakarMurlis = new MaterialSkin.Controls.MaterialRadioButton();
            this.rdbAvyakthMurlis = new MaterialSkin.Controls.MaterialRadioButton();
            this.grbAvyakthMurliCategory = new System.Windows.Forms.GroupBox();
            this.rdbAvyakthAll = new MaterialSkin.Controls.MaterialRadioButton();
            this.rdbAvyakthTitles = new MaterialSkin.Controls.MaterialRadioButton();
            this.btnBuildIndex = new MaterialSkin.Controls.MaterialButton();
            this.cmbLanguages = new System.Windows.Forms.ComboBox();
            this.txtSearch = new SelfControls.Controls.SelfTextBox3();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnExport = new MaterialSkin.Controls.MaterialButton();
            this.paginationControl = new PaginationControl();
            this.grbSakarMurliCategory.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbAvyakthMurliCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSakarMurliCategory
            // 
            this.grbSakarMurliCategory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbSakarMurliCategory.Controls.Add(this.rdbSlogans);
            this.grbSakarMurliCategory.Controls.Add(this.rdbBlessings);
            this.grbSakarMurliCategory.Controls.Add(this.rdbSakarQuestionAnswers);
            this.grbSakarMurliCategory.Controls.Add(this.rdbSakarTitles);
            this.grbSakarMurliCategory.Controls.Add(this.rdbDharnaPoints);
            this.grbSakarMurliCategory.Location = new System.Drawing.Point(145, 130);
            this.grbSakarMurliCategory.Name = "grbSakarMurliCategory";
            this.grbSakarMurliCategory.Size = new System.Drawing.Size(655, 45);
            this.grbSakarMurliCategory.TabIndex = 4;
            this.grbSakarMurliCategory.TabStop = false;
            this.grbSakarMurliCategory.Text = "Search In : ";
            // 
            // rdbSlogans
            // 
            this.rdbSlogans.Depth = 0;
            this.rdbSlogans.Location = new System.Drawing.Point(531, 17);
            this.rdbSlogans.Margin = new System.Windows.Forms.Padding(0);
            this.rdbSlogans.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbSlogans.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbSlogans.Name = "rdbSlogans";
            this.rdbSlogans.Ripple = true;
            this.rdbSlogans.Size = new System.Drawing.Size(100, 22);
            this.rdbSlogans.TabIndex = 4;
            this.rdbSlogans.Text = "Slogans";
            this.rdbSlogans.UseVisualStyleBackColor = true;
            // 
            // rdbBlessings
            // 
            this.rdbBlessings.Depth = 0;
            this.rdbBlessings.Location = new System.Drawing.Point(416, 17);
            this.rdbBlessings.Margin = new System.Windows.Forms.Padding(0);
            this.rdbBlessings.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbBlessings.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbBlessings.Name = "rdbBlessings";
            this.rdbBlessings.Ripple = true;
            this.rdbBlessings.Size = new System.Drawing.Size(100, 22);
            this.rdbBlessings.TabIndex = 3;
            this.rdbBlessings.Text = "Blessings";
            this.rdbBlessings.UseVisualStyleBackColor = true;
            // 
            // rdbSakarQuestionAnswers
            // 
            this.rdbSakarQuestionAnswers.Depth = 0;
            this.rdbSakarQuestionAnswers.Location = new System.Drawing.Point(98, 17);
            this.rdbSakarQuestionAnswers.Margin = new System.Windows.Forms.Padding(0);
            this.rdbSakarQuestionAnswers.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbSakarQuestionAnswers.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbSakarQuestionAnswers.Name = "rdbSakarQuestionAnswers";
            this.rdbSakarQuestionAnswers.Ripple = true;
            this.rdbSakarQuestionAnswers.Size = new System.Drawing.Size(158, 22);
            this.rdbSakarQuestionAnswers.TabIndex = 1;
            this.rdbSakarQuestionAnswers.Text = "Question-Answers";
            this.rdbSakarQuestionAnswers.UseVisualStyleBackColor = true;
            // 
            // rdbSakarTitles
            // 
            this.rdbSakarTitles.Checked = true;
            this.rdbSakarTitles.Depth = 0;
            this.rdbSakarTitles.Location = new System.Drawing.Point(24, 17);
            this.rdbSakarTitles.Margin = new System.Windows.Forms.Padding(0);
            this.rdbSakarTitles.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbSakarTitles.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbSakarTitles.Name = "rdbSakarTitles";
            this.rdbSakarTitles.Ripple = true;
            this.rdbSakarTitles.Size = new System.Drawing.Size(59, 20);
            this.rdbSakarTitles.TabIndex = 0;
            this.rdbSakarTitles.TabStop = true;
            this.rdbSakarTitles.Text = "Titles";
            this.rdbSakarTitles.UseVisualStyleBackColor = true;
            // 
            // rdbDharnaPoints
            // 
            this.rdbDharnaPoints.Depth = 0;
            this.rdbDharnaPoints.Location = new System.Drawing.Point(271, 18);
            this.rdbDharnaPoints.Margin = new System.Windows.Forms.Padding(0);
            this.rdbDharnaPoints.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbDharnaPoints.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbDharnaPoints.Name = "rdbDharnaPoints";
            this.rdbDharnaPoints.Ripple = true;
            this.rdbDharnaPoints.Size = new System.Drawing.Size(130, 22);
            this.rdbDharnaPoints.TabIndex = 2;
            this.rdbDharnaPoints.Text = "DharnaPoints";
            this.rdbDharnaPoints.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 197);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(920, 308);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.AutoSize = false;
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSearch.Depth = 0;
            this.btnSearch.HighEmphasis = true;
            this.btnSearch.Icon = null;
            this.btnSearch.Location = new System.Drawing.Point(676, 27);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSearch.Size = new System.Drawing.Size(120, 30);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSearch.UseAccentColor = false;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.AutoSize = false;
            this.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClear.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Title;
            this.btnClear.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClear.Depth = 0;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.HighEmphasis = true;
            this.btnClear.Icon = null;
            this.btnClear.Location = new System.Drawing.Point(832, 145);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClear.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClear.Name = "btnClear";
            this.btnClear.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnClear.UseAccentColor = false;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.rdbSakarMurlis);
            this.groupBox1.Controls.Add(this.rdbAvyakthMurlis);
            this.groupBox1.Location = new System.Drawing.Point(289, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search : ";
            // 
            // rdbSakarMurlis
            // 
            this.rdbSakarMurlis.Depth = 0;
            this.rdbSakarMurlis.Enabled = false;
            this.rdbSakarMurlis.Font = new System.Drawing.Font("Bookman Old Style", 6.25F);
            this.rdbSakarMurlis.Location = new System.Drawing.Point(199, 18);
            this.rdbSakarMurlis.Margin = new System.Windows.Forms.Padding(0);
            this.rdbSakarMurlis.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbSakarMurlis.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbSakarMurlis.Name = "rdbSakarMurlis";
            this.rdbSakarMurlis.Ripple = true;
            this.rdbSakarMurlis.Size = new System.Drawing.Size(140, 22);
            this.rdbSakarMurlis.TabIndex = 1;
            this.rdbSakarMurlis.TabStop = true;
            this.rdbSakarMurlis.Text = "Sakar Murlis";
            this.rdbSakarMurlis.UseVisualStyleBackColor = true;
            // 
            // rdbAvyakthMurlis
            // 
            this.rdbAvyakthMurlis.Checked = true;
            this.rdbAvyakthMurlis.Depth = 0;
            this.rdbAvyakthMurlis.Font = new System.Drawing.Font("Bookman Old Style", 6.25F);
            this.rdbAvyakthMurlis.Location = new System.Drawing.Point(31, 18);
            this.rdbAvyakthMurlis.Margin = new System.Windows.Forms.Padding(0);
            this.rdbAvyakthMurlis.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbAvyakthMurlis.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbAvyakthMurlis.Name = "rdbAvyakthMurlis";
            this.rdbAvyakthMurlis.Ripple = true;
            this.rdbAvyakthMurlis.Size = new System.Drawing.Size(140, 22);
            this.rdbAvyakthMurlis.TabIndex = 0;
            this.rdbAvyakthMurlis.TabStop = true;
            this.rdbAvyakthMurlis.Text = "Avyakth Murlis";
            this.rdbAvyakthMurlis.UseVisualStyleBackColor = true;
            // 
            // grbAvyakthMurliCategory
            // 
            this.grbAvyakthMurliCategory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbAvyakthMurliCategory.Controls.Add(this.rdbAvyakthAll);
            this.grbAvyakthMurliCategory.Controls.Add(this.rdbAvyakthTitles);
            this.grbAvyakthMurliCategory.Location = new System.Drawing.Point(289, 130);
            this.grbAvyakthMurliCategory.Name = "grbAvyakthMurliCategory";
            this.grbAvyakthMurliCategory.Size = new System.Drawing.Size(366, 45);
            this.grbAvyakthMurliCategory.TabIndex = 3;
            this.grbAvyakthMurliCategory.TabStop = false;
            this.grbAvyakthMurliCategory.Text = "Search In : ";
            // 
            // rdbAvyakthAll
            // 
            this.rdbAvyakthAll.Depth = 0;
            this.rdbAvyakthAll.Location = new System.Drawing.Point(228, 17);
            this.rdbAvyakthAll.Margin = new System.Windows.Forms.Padding(0);
            this.rdbAvyakthAll.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbAvyakthAll.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbAvyakthAll.Name = "rdbAvyakthAll";
            this.rdbAvyakthAll.Ripple = true;
            this.rdbAvyakthAll.Size = new System.Drawing.Size(62, 22);
            this.rdbAvyakthAll.TabIndex = 2;
            this.rdbAvyakthAll.TabStop = true;
            this.rdbAvyakthAll.Text = "All";
            this.rdbAvyakthAll.UseVisualStyleBackColor = true;
            // 
            // rdbAvyakthTitles
            // 
            this.rdbAvyakthTitles.Checked = true;
            this.rdbAvyakthTitles.Depth = 0;
            this.rdbAvyakthTitles.Location = new System.Drawing.Point(82, 17);
            this.rdbAvyakthTitles.Margin = new System.Windows.Forms.Padding(0);
            this.rdbAvyakthTitles.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdbAvyakthTitles.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdbAvyakthTitles.Name = "rdbAvyakthTitles";
            this.rdbAvyakthTitles.Ripple = true;
            this.rdbAvyakthTitles.Size = new System.Drawing.Size(80, 22);
            this.rdbAvyakthTitles.TabIndex = 0;
            this.rdbAvyakthTitles.TabStop = true;
            this.rdbAvyakthTitles.Text = "Titles";
            this.rdbAvyakthTitles.UseVisualStyleBackColor = true;
            // 
            // btnBuildIndex
            // 
            this.btnBuildIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuildIndex.AutoSize = false;
            this.btnBuildIndex.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBuildIndex.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Title;
            this.btnBuildIndex.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnBuildIndex.Depth = 0;
            this.btnBuildIndex.HighEmphasis = true;
            this.btnBuildIndex.Icon = null;
            this.btnBuildIndex.Location = new System.Drawing.Point(813, 27);
            this.btnBuildIndex.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnBuildIndex.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBuildIndex.Name = "btnBuildIndex";
            this.btnBuildIndex.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnBuildIndex.Size = new System.Drawing.Size(120, 30);
            this.btnBuildIndex.TabIndex = 14;
            this.btnBuildIndex.Text = "Build Index";
            this.btnBuildIndex.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnBuildIndex.UseAccentColor = false;
            this.btnBuildIndex.UseVisualStyleBackColor = false;
            this.btnBuildIndex.Click += new System.EventHandler(this.BtnBuildIndex_Click);
            // 
            // cmbLanguages
            // 
            this.cmbLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLanguages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cmbLanguages.FormattingEnabled = true;
            this.cmbLanguages.Items.AddRange(new object[] {
            "English",
            "Hindi",
            "Kannada"});
            this.cmbLanguages.Location = new System.Drawing.Point(659, 31);
            this.cmbLanguages.Name = "cmbLanguages";
            this.cmbLanguages.Size = new System.Drawing.Size(121, 24);
            this.cmbLanguages.TabIndex = 17;
            this.cmbLanguages.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSearch.AnimateReadOnly = false;
            this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSearch.BackgroundColor = System.Drawing.Color.White;
            this.txtSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtSearch.BorderRadius = 4;
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtSearch.Font = new System.Drawing.Font("Tiro Devanagari Hindi", 13.8F);
            this.txtSearch.ForegroundColor = System.Drawing.Color.Navy;
            this.txtSearch.HideSelection = true;
            this.txtSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSearch.LeadingIcon = null;
            this.txtSearch.Location = new System.Drawing.Point(289, 17);
            this.txtSearch.MaximumSize = new System.Drawing.Size(500, 50);
            this.txtSearch.MaxLength = 32767;
            this.txtSearch.MinimumSize = new System.Drawing.Size(100, 40);
            this.txtSearch.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PlaceholderForegroundColor = System.Drawing.Color.Blue;
            this.txtSearch.PlaceHolderText = "";
            this.txtSearch.PrefixSuffixText = null;
            this.txtSearch.ReadOnly = false;
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.ShortcutsEnabled = true;
            this.txtSearch.Size = new System.Drawing.Size(366, 40);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TabStop = false;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSearch.TrailingIcon = null;
            this.txtSearch.UseSystemPasswordChar = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Noto Sans", 11F);
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox1.Location = new System.Drawing.Point(661, 77);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 27);
            this.textBox1.TabIndex = 18;
            this.textBox1.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.AutoSize = false;
            this.btnExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExport.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Title;
            this.btnExport.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnExport.Depth = 0;
            this.btnExport.HighEmphasis = true;
            this.btnExport.Icon = null;
            this.btnExport.Location = new System.Drawing.Point(832, 97);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnExport.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnExport.Name = "btnExport";
            this.btnExport.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 19;
            this.btnExport.Text = "Export";
            this.btnExport.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnExport.UseAccentColor = false;
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // paginationControl
            // 
            this.paginationControl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.paginationControl.CurrentPage = 1;
            this.paginationControl.Location = new System.Drawing.Point(307, 512);
            this.paginationControl.Name = "paginationControl";
            this.paginationControl.PageSize = 0;
            this.paginationControl.Size = new System.Drawing.Size(443, 45);
            this.paginationControl.TabIndex = 16;
            this.paginationControl.Text = "R";
            this.paginationControl.TotalPages = 0;
            // 
            // FrmHome
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClear;
            this.ClientSize = new System.Drawing.Size(945, 559);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmbLanguages);
            this.Controls.Add(this.paginationControl);
            this.Controls.Add(this.btnBuildIndex);
            this.Controls.Add(this.grbAvyakthMurliCategory);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.grbSakarMurliCategory);
            this.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmHome";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.FrmHome_Load);
            this.grbSakarMurliCategory.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grbAvyakthMurliCategory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox grbSakarMurliCategory;
        private MaterialSkin.Controls.MaterialRadioButton rdbSakarQuestionAnswers;
        private MaterialSkin.Controls.MaterialRadioButton rdbSakarTitles;
        private MaterialSkin.Controls.MaterialRadioButton rdbDharnaPoints;
        private MaterialSkin.Controls.MaterialRadioButton rdbSlogans;
        private MaterialSkin.Controls.MaterialRadioButton rdbBlessings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private SelfControls.Controls.SelfTextBox3 txtSearch;
        private MaterialSkin.Controls.MaterialButton btnSearch;
        private MaterialSkin.Controls.MaterialButton btnClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private MaterialSkin.Controls.MaterialRadioButton rdbAvyakthMurlis;
        private MaterialSkin.Controls.MaterialRadioButton rdbSakarMurlis;
        private System.Windows.Forms.GroupBox grbAvyakthMurliCategory;
        private MaterialSkin.Controls.MaterialRadioButton rdbAvyakthAll;
        private MaterialSkin.Controls.MaterialRadioButton rdbAvyakthTitles;
        private MaterialSkin.Controls.MaterialButton btnBuildIndex;
        private PaginationControl paginationControl;
        private System.Windows.Forms.ComboBox cmbLanguages;
        private System.Windows.Forms.TextBox textBox1;
        private MaterialSkin.Controls.MaterialButton btnExport;
    }
}

