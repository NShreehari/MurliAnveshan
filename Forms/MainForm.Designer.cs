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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ParentNavigationControl = new System.Windows.Forms.Panel();
            this.BottomNavigationControl = new System.Windows.Forms.FlowLayoutPanel();
            this.mLogout = new System.Windows.Forms.Button();
            this.NavigationControl = new System.Windows.Forms.FlowLayoutPanel();
            this.mBookmarks = new System.Windows.Forms.Button();
            this.mAbout = new System.Windows.Forms.Button();
            this.NavigationTransition = new System.Windows.Forms.Timer(this.components);
            this.mSettings = new System.Windows.Forms.Button();
            this.mMenu = new System.Windows.Forms.Button();
            this.mHome = new System.Windows.Forms.Button();
            this.mFavorites = new System.Windows.Forms.Button();
            this.mHistory = new System.Windows.Forms.Button();
            this.mDocViewer = new System.Windows.Forms.Button();
            this.ParentNavigationControl.SuspendLayout();
            this.BottomNavigationControl.SuspendLayout();
            this.NavigationControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParentNavigationControl
            // 
            this.ParentNavigationControl.Controls.Add(this.BottomNavigationControl);
            this.ParentNavigationControl.Controls.Add(this.NavigationControl);
            this.ParentNavigationControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParentNavigationControl.Location = new System.Drawing.Point(0, 0);
            this.ParentNavigationControl.Name = "ParentNavigationControl";
            this.ParentNavigationControl.Size = new System.Drawing.Size(200, 537);
            this.ParentNavigationControl.TabIndex = 4;
            // 
            // BottomNavigationControl
            // 
            this.BottomNavigationControl.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BottomNavigationControl.Controls.Add(this.mSettings);
            this.BottomNavigationControl.Controls.Add(this.mLogout);
            this.BottomNavigationControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomNavigationControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.BottomNavigationControl.Location = new System.Drawing.Point(0, 437);
            this.BottomNavigationControl.Name = "BottomNavigationControl";
            this.BottomNavigationControl.Size = new System.Drawing.Size(200, 100);
            this.BottomNavigationControl.TabIndex = 4;
            // 
            // mLogout
            // 
            this.mLogout.FlatAppearance.BorderSize = 0;
            this.mLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mLogout.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mLogout.Location = new System.Drawing.Point(3, 45);
            this.mLogout.Name = "mLogout";
            this.mLogout.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mLogout.Size = new System.Drawing.Size(194, 36);
            this.mLogout.TabIndex = 9;
            this.mLogout.Text = "           LogOut";
            this.mLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mLogout.UseVisualStyleBackColor = true;
            this.mLogout.Visible = false;
            // 
            // NavigationControl
            // 
            this.NavigationControl.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NavigationControl.Controls.Add(this.mMenu);
            this.NavigationControl.Controls.Add(this.mHome);
            this.NavigationControl.Controls.Add(this.mFavorites);
            this.NavigationControl.Controls.Add(this.mBookmarks);
            this.NavigationControl.Controls.Add(this.mHistory);
            this.NavigationControl.Controls.Add(this.mDocViewer);
            this.NavigationControl.Controls.Add(this.mAbout);
            this.NavigationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.NavigationControl.Location = new System.Drawing.Point(0, 0);
            this.NavigationControl.Name = "NavigationControl";
            this.NavigationControl.Size = new System.Drawing.Size(200, 537);
            this.NavigationControl.TabIndex = 3;
            // 
            // mBookmarks
            // 
            this.mBookmarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mBookmarks.FlatAppearance.BorderSize = 0;
            this.mBookmarks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mBookmarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mBookmarks.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mBookmarks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mBookmarks.Location = new System.Drawing.Point(3, 137);
            this.mBookmarks.Name = "mBookmarks";
            this.mBookmarks.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mBookmarks.Size = new System.Drawing.Size(194, 36);
            this.mBookmarks.TabIndex = 2;
            this.mBookmarks.Text = "           Bookmarks";
            this.mBookmarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mBookmarks.UseVisualStyleBackColor = true;
            this.mBookmarks.Visible = false;
            // 
            // mAbout
            // 
            this.mAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mAbout.FlatAppearance.BorderSize = 0;
            this.mAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mAbout.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mAbout.Location = new System.Drawing.Point(3, 263);
            this.mAbout.Name = "mAbout";
            this.mAbout.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mAbout.Size = new System.Drawing.Size(194, 36);
            this.mAbout.TabIndex = 4;
            this.mAbout.Text = "           About";
            this.mAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mAbout.UseVisualStyleBackColor = true;
            this.mAbout.Visible = false;
            // 
            // NavigationTransition
            // 
            this.NavigationTransition.Interval = 5;
            this.NavigationTransition.Tick += new System.EventHandler(this.NavigationTransitionTimer_Tick);
            // 
            // mSettings
            // 
            this.mSettings.FlatAppearance.BorderSize = 0;
            this.mSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mSettings.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mSettings.Image = global::MurliAnveshan.Properties.Resources.Settings;
            this.mSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mSettings.Location = new System.Drawing.Point(3, 3);
            this.mSettings.Name = "mSettings";
            this.mSettings.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mSettings.Size = new System.Drawing.Size(194, 36);
            this.mSettings.TabIndex = 8;
            this.mSettings.Text = "           Settings";
            this.mSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mSettings.UseVisualStyleBackColor = true;
            // 
            // mMenu
            // 
            this.mMenu.FlatAppearance.BorderSize = 0;
            this.mMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mMenu.Image = ((System.Drawing.Image)(resources.GetObject("mMenu.Image")));
            this.mMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mMenu.Location = new System.Drawing.Point(3, 8);
            this.mMenu.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.mMenu.Name = "mMenu";
            this.mMenu.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mMenu.Size = new System.Drawing.Size(194, 32);
            this.mMenu.TabIndex = 0;
            this.mMenu.UseVisualStyleBackColor = true;
            // 
            // mHome
            // 
            this.mHome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.mHome.FlatAppearance.BorderSize = 0;
            this.mHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mHome.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mHome.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mHome.Image = global::MurliAnveshan.Properties.Resources.House_041;
            this.mHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mHome.Location = new System.Drawing.Point(3, 53);
            this.mHome.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.mHome.Name = "mHome";
            this.mHome.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mHome.Size = new System.Drawing.Size(194, 36);
            this.mHome.TabIndex = 6;
            this.mHome.Text = "           Home";
            this.mHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mHome.UseVisualStyleBackColor = true;
            // 
            // mFavorites
            // 
            this.mFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mFavorites.FlatAppearance.BorderSize = 0;
            this.mFavorites.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mFavorites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mFavorites.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mFavorites.ForeColor = System.Drawing.Color.Black;
            this.mFavorites.Image = global::MurliAnveshan.Properties.Resources.Star1;
            this.mFavorites.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mFavorites.Location = new System.Drawing.Point(3, 95);
            this.mFavorites.Name = "mFavorites";
            this.mFavorites.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mFavorites.Size = new System.Drawing.Size(194, 36);
            this.mFavorites.TabIndex = 1;
            this.mFavorites.Text = "           Favorites";
            this.mFavorites.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mFavorites.UseVisualStyleBackColor = true;
            // 
            // mHistory
            // 
            this.mHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mHistory.FlatAppearance.BorderSize = 0;
            this.mHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mHistory.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mHistory.ForeColor = System.Drawing.Color.Black;
            this.mHistory.Image = global::MurliAnveshan.Properties.Resources.Tab_History;
            this.mHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mHistory.Location = new System.Drawing.Point(3, 179);
            this.mHistory.Name = "mHistory";
            this.mHistory.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mHistory.Size = new System.Drawing.Size(194, 36);
            this.mHistory.TabIndex = 7;
            this.mHistory.Text = "           History";
            this.mHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mHistory.UseVisualStyleBackColor = true;
            // 
            // mDocViewer
            // 
            this.mDocViewer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mDocViewer.FlatAppearance.BorderSize = 0;
            this.mDocViewer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mDocViewer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mDocViewer.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mDocViewer.Image = global::MurliAnveshan.Properties.Resources.File_Format_PDF;
            this.mDocViewer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mDocViewer.Location = new System.Drawing.Point(3, 221);
            this.mDocViewer.Name = "mDocViewer";
            this.mDocViewer.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.mDocViewer.Size = new System.Drawing.Size(194, 36);
            this.mDocViewer.TabIndex = 3;
            this.mDocViewer.Text = "           Doc Viewer";
            this.mDocViewer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mDocViewer.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 537);
            this.Controls.Add(this.ParentNavigationControl);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "Murli Anveshan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm2_Load);
            this.ParentNavigationControl.ResumeLayout(false);
            this.BottomNavigationControl.ResumeLayout(false);
            this.NavigationControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ParentNavigationControl;
        private System.Windows.Forms.FlowLayoutPanel BottomNavigationControl;
        private System.Windows.Forms.Button mSettings;
        private System.Windows.Forms.Button mLogout;
        private System.Windows.Forms.FlowLayoutPanel NavigationControl;
        private System.Windows.Forms.Button mMenu;
        private System.Windows.Forms.Button mHome;
        private System.Windows.Forms.Button mFavorites;
        private System.Windows.Forms.Button mBookmarks;
        private System.Windows.Forms.Button mHistory;
        private System.Windows.Forms.Button mDocViewer;
        private System.Windows.Forms.Button mAbout;
        private System.Windows.Forms.Timer NavigationTransition;
    }
}