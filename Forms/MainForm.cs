using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Anotar.NLog;

using MurliAnveshan.Classes;

using SelfControls.Controls;

namespace MurliAnveshan
{
    public partial class MainForm : BaseForm2
    {
        #region Private Fields

        public const int _navigationControlMaxWidth = 200;
        public const int _navigationControlMinimumWidth = 60;

        private bool _isNavigationControlExpanded;

        private System.Windows.Forms.Button activeMenu;

        readonly MainForm mainFormInstance;

        FrmHome frmHome;

        FrmFavorites frmFavorites;

        readonly FrmBookmarks frmBookmarks;

        readonly FrmHistry frmHistry;

        FrmDocViewer frmDocViewer;

        readonly FrmSettings frmSettings;

        readonly FrmAbout frmAbout;

        private System.Windows.Forms.Button previouslyActiveMenu;

        #endregion Private Fields

        #region Public Properties

        public bool IsNavigationControlExpanded
        {
            get { return _isNavigationControlExpanded; }
            set { _isNavigationControlExpanded = value; }
        }

        public Button ActiveMenu { get => activeMenu; set => activeMenu = value; }
        public Button PreviouslyActiveMenu { get => previouslyActiveMenu; set => previouslyActiveMenu = value; }
        //public Button PreviouslyActiveForm { get => previouslyActiveMenu; set => previouslyActiveMenu = value; }

        #endregion Public Properties

        [LogToErrorOnException]

        //[LogToWarnOnException]
        //[LogToDebugOnException]
        //[LogToTraceOnException]

        public MainForm()
        {
            InitializeComponent();

            mHome.Click += OnHomeMenu_Click;
            mMenu.Click += OnMenu_Click;
            mFavorites.Click += OnFavorites_Click;
            mBookmarks.Click += OnBookmarks_Click;
            mHistory.Click += OnHistory_Click;
            MDocViewer.Click += OnDocViewer_Click;
            mAbout.Click += OnAbout_Click;
            mSettings.Click += OnSettings_Click;

            _isNavigationControlExpanded = true;

            MdiProp();

            mainFormInstance = this;

            SizeChanged += OnSizeChanged;
            Resize += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            ParentNavigationControl.Height = this.Height - this.TitleBarSize.Height - 2;
            NavigationControl.Height = ParentNavigationControl.Height - BottomNavigationControl.Height;
        }

        private void OnMenu_Click(object sender, EventArgs e)
        {
            NavigationTransition.Start();
            AdjustChildForms();

            LogTo.Debug("Menu Clicked");
        }

        private void AdjustChildForms()
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is FrmHome customChild)
                {
                    if (IsNavigationControlExpanded) //Shrinking
                        customChild.AdjustBounds(_navigationControlMinimumWidth);
                    else //Expanding
                    {
                        customChild.AdjustBounds(_navigationControlMaxWidth);

                    }
                }
            }
        }


        private void OnHomeMenu_Click(object sender, EventArgs e)
        {
            SelectHome();
        }

        private void OnFavorites_Click(object sender, EventArgs e)
        {
            LoadFaviorites();
            ToggleMenuHighilightion();
        }

        private void OnBookmarks_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private void OnDocViewer_Click(object sender, EventArgs e)
        {
            LoadDocViewer(mainFormInstance);

            ToggleMenuHighilightion();
        }


        public void LoadDocViewer(MainForm mainFormInstance)
        {
            PreviouslyActiveMenu = ActiveMenu;
            ActiveMenu = MDocViewer;

            if (frmDocViewer == null)
            {
                frmDocViewer = new FrmDocViewer(mainFormInstance)
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill
                };

                frmDocViewer.Show();
            }
            else
            {
                frmDocViewer.Activate();
            }
        }

        public void LoadDocViewer(MainForm mainFormInstance, ResultDetails resultDetails)
        {
            PreviouslyActiveMenu = ActiveMenu;
            ActiveMenu = MDocViewer;

            if (frmDocViewer == null)
            {
                frmDocViewer = new FrmDocViewer(mainFormInstance, resultDetails)
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill
                };

                frmDocViewer.Show();
            }
            else
            {
                //TODO: Identify the Clicked Card. If New One Load otherwise Activate
                frmDocViewer.LoadThePDF(resultDetails);
                frmDocViewer.Activate();
            }
        }

        private void LoadFaviorites()
        {
            PreviouslyActiveMenu = ActiveMenu;
            ActiveMenu = mFavorites;

            if (frmFavorites == null)
            {
                frmFavorites = new FrmFavorites
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill
                };

                frmFavorites.Show();
            }
            else
            {
                frmFavorites.Activate();
            }
        }


        private void OnHistory_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnSettings_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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
                MDocViewer.Enabled = false;
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
                MDocViewer.Enabled = true;
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
            MDocViewer.Width = NavigationControl.Width;
            mAbout.Width = NavigationControl.Width;
            mSettings.Width = NavigationControl.Width;
            mLogout.Width = NavigationControl.Width;
        }

        private void SelectHome()
        {
            PreviouslyActiveMenu = ActiveMenu;
            ActiveMenu = mHome;

            if (frmHome == null)
            {
                frmHome = new FrmHome(mainFormInstance)
                {
                    //Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                    MdiParent = this,
                    StartPosition = FormStartPosition.Manual,
                    //Width = this.Width - this.ParentNavigationControl.Width,
                    //Height = this.Height // Adjust as needed
                    //Dock = DockStyle.Fill
                };

                frmHome.Show();

                if (IsNavigationControlExpanded) //Shrinking
                {
                    if (frmHome.IsAccessible)
                    {
                        frmHome.AdjustBounds(_navigationControlMinimumWidth);
                    }
                    else
                    {
                        frmHome.AdjustBounds(_navigationControlMaxWidth);
                    }
                }
                else //Expanding
                {
                    frmHome.AdjustBounds(_navigationControlMaxWidth);
                }
            }
            else
            {
                frmHome.Activate();
            }

            ToggleMenuHighilightion();
        }

        private void ToggleMenuHighilightion()
        {
            ActiveMenu.BackColor = Color.RoyalBlue;
            ActiveMenu.ForeColor = Color.White;

            if (PreviouslyActiveMenu != null)
            {
                PreviouslyActiveMenu.BackColor = Color.FromKnownColor(KnownColor.ControlLight);
                PreviouslyActiveMenu.ForeColor = Color.Black;
            }
        }

        private void MainForm2_Load(object sender, EventArgs e)
        {
            SelectHome();
            ParentNavigationControl.Height = this.Height - this.TitleBarSize.Height - 2;
            ParentNavigationControl.Top = TitleBarSize.Height + 1;
            ParentNavigationControl.Left = 2;
        }

        private void MdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.White;
        }
    }
}
