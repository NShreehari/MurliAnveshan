using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MurliAnveshan.Classes;

namespace MurliAnveshan
{
    public partial class MainForm : Form
    {
        #region Private Fields

        private const int _navigationControlMaxWidth = 200;
        private const int _navigationControlMinimumWidth = 60;

        private bool _isNavigationControlExpanded;

        System.Windows.Forms.Button activeMenu;

        FrmHome frmHome;

        FrmFavorites frmFavorites;

        FrmBookmarks frmBookmarks;

        FrmHistry frmHistry;

        FrmDocViewer frmDocViewer;

        FrmSettings frmSettings;

        FrmAbout frmAbout;

        System.Windows.Forms.Button previouslyActiveMenu;

        #endregion Private Fields

        #region Public Properties

        public bool IsNavigationControlExpanded
        {
            get { return _isNavigationControlExpanded; }
            set { _isNavigationControlExpanded = value; }
        }

        #endregion Public Properties


        public MainForm()
        {
            InitializeComponent();

            mHome.Click += OnHomeMenu_Click;
            mMenu.Click += OnMenu_Click;
            mFavorites.Click += OnFavorites_Click;
            mBookmarks.Click += OnBookmarks_Click;
            mHistory.Click += OnHistory_Click;
            mDocViewer.Click += OnDocViewer_Click;
            mAbout.Click += OnAbout_Click;
            mSettings.Click += OnSettings_Click;

            _isNavigationControlExpanded = true;

            mdiProp();
        }


        private void OnMenu_Click(object sender, EventArgs e)
        {
            NavigationTransition.Start();
        }

        private void OnHomeMenu_Click(object sender, EventArgs e)
        {
            SelectHome();
        }

        private void OnFavorites_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnBookmarks_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private void OnDocViewer_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnHistory_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnSettings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnAbout_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ToggleAllMenuState()
        {
            if (mHome.Enabled)
            {
                mHome.Enabled = false;
                mFavorites.Enabled = false;
                mBookmarks.Enabled = false;
                mHistory.Enabled = false;
                mDocViewer.Enabled = false;
                mAbout.Enabled = false;
                mSettings.Enabled = false;
                mLogout.Enabled = false;
            }
            else
            {
                mHome.Enabled = true;
                mFavorites.Enabled = true;
                mBookmarks.Enabled = true;
                mHistory.Enabled = true;
                mDocViewer.Enabled = true;
                mAbout.Enabled = true;
                //mSettings.Enabled = true;
                mLogout.Enabled = true;
            }
        }



        private void NavigationTransitionTimer_Tick(object sender, EventArgs e)
        {
            if (IsNavigationControlExpanded)
            {
                ParentNavigationControl.Width -= 10;
                NavigationControl.Width -= 10;
                BottomNavigationControl.Width -= 10;

                if (NavigationControl.Width <= _navigationControlMinimumWidth)
                {
                    SetMenusWidth();
                }
            }
            else
            {
                ParentNavigationControl.Width += 10;
                NavigationControl.Width += 10;
                BottomNavigationControl.Width += 10;

                if (NavigationControl.Width >= _navigationControlMaxWidth)
                {
                    SetMenusWidth();
                }
            }
        }


        private void SetMenusWidth()
        {
            NavigationTransition.Stop();
            _isNavigationControlExpanded = !_isNavigationControlExpanded;

            //mMenu.Width = NavigationControl.Width;
            mFavorites.Width = NavigationControl.Width;
            mBookmarks.Width = NavigationControl.Width;
            mDocViewer.Width = NavigationControl.Width;
            mAbout.Width = NavigationControl.Width;
            mSettings.Width = NavigationControl.Width;
            mLogout.Width = NavigationControl.Width;
        }

        private void SelectHome()
        {
            previouslyActiveMenu = activeMenu;
            activeMenu = mHome;

            if (frmHome == null)
            {
                frmHome = new FrmHome();
                frmHome.MdiParent = this;
                frmHome.Dock = DockStyle.Fill;

                frmHome.Show();
            }
            else
            {
                frmHome.Activate();
            }

            ToggleMenuHighilightion();
        }

        private void ToggleMenuHighilightion()
        {
            activeMenu.BackColor = Color.RoyalBlue;
            activeMenu.ForeColor = Color.White;

            if (previouslyActiveMenu != null)
            {
                previouslyActiveMenu.BackColor = Color.FromKnownColor(KnownColor.ControlLight);
                previouslyActiveMenu.ForeColor = Color.Black;
            }
        }

        private void MainForm2_Load(object sender, EventArgs e)
        {
            SelectHome();
        }

        private void mdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.White;
        }

    }
}
