namespace MurliAnveshan.Forms
{
    partial class FrmDataBaseInitilization
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
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnRemoveQutotations = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(59, 40);
            this.txtFile.Margin = new System.Windows.Forms.Padding(5);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(434, 26);
            this.txtFile.TabIndex = 0;
            // 
            // btnExtract
            // 
            this.btnExtract.Font = new System.Drawing.Font("Bookman Old Style", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExtract.Location = new System.Drawing.Point(620, 40);
            this.btnExtract.Margin = new System.Windows.Forms.Padding(5);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(108, 26);
            this.btnExtract.TabIndex = 1;
            this.btnExtract.Text = "Extract";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.BtnExtract_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Bookman Old Style", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(523, 40);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(87, 26);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // btnRemoveQutotations
            // 
            this.btnRemoveQutotations.Font = new System.Drawing.Font("Bookman Old Style", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveQutotations.Location = new System.Drawing.Point(536, 90);
            this.btnRemoveQutotations.Margin = new System.Windows.Forms.Padding(5);
            this.btnRemoveQutotations.Name = "btnRemoveQutotations";
            this.btnRemoveQutotations.Size = new System.Drawing.Size(221, 26);
            this.btnRemoveQutotations.TabIndex = 3;
            this.btnRemoveQutotations.Text = "Remove QutotionMarks";
            this.btnRemoveQutotations.UseVisualStyleBackColor = true;
            this.btnRemoveQutotations.Click += new System.EventHandler(this.BtnRemoveQutotations_Click);
            // 
            // FrmDataBaseInitilization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 438);
            this.Controls.Add(this.btnRemoveQutotations);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.txtFile);
            this.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmDataBaseInitilization";
            this.Text = "FrmDataBaseInitilization";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnRemoveQutotations;
    }
}