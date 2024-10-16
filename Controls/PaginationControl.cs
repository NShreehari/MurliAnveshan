using System;
using System.ComponentModel;
using System.Windows.Forms;

using MaterialSkin.Controls;

public class PaginationControl : Control
{
    #region Private Fields

    // Property to allow users to select the FlowLayoutPanel for displaying the results
    //private Control _targetPanel;

    private MaterialButton btnLast;
    private MaterialButton btnNext;
    private MaterialButton btnPrevious;
    private int currentPage = 1;
    private FlowLayoutPanel flowLayoutPanel;
    private FlowLayoutPanel panelPages;

    #endregion Private Fields

    #region Public Constructors

    public PaginationControl()
    {
        InitializeComponents();
        UpdatePagination();
    }

    #endregion Public Constructors

    #region Public Events

    // Exposed Events for Previous, Next, Page Clicks, and Last

    public event EventHandler<int> LastClicked;

    public event EventHandler<int> NextClicked;

    public event EventHandler<int> PageClicked;

    public event EventHandler<int> PreviousClicked;

    #endregion Public Events

    #region Public Properties

    public int CurrentPage { get => currentPage; set => currentPage = value; }

    public int PageSize { get; set; }
    
    //[Browsable(true)]
    //[Category("Custom Properties")]
    //[Description("Control to display paginated content")]
    //public Control TargetPanel
    //{
    //    get { return _targetPanel; }
    //    set { _targetPanel = value; }
    //}

    public int TotalPages { get; set; }

    #endregion Public Properties

    //public PaginationControl(int totalPages = 5, int pageSize = 10) : this()
    //{
    //    this.totalPages = totalPages;
    //    this.lastPageNumber = totalPages;

    //    this.pageSize = pageSize;
    //    UpdatePagination();  // Setup default pagination
    //}

    //public PaginationControl(int totalPages, int pageSize)
    //{
    //    InitializeComponents();

    //    this.totalPages = totalPages;
    //    this.lastPageNumber = totalPages;

    //    this.pageSize = pageSize;
    //    UpdatePagination();  // Setup default pagination
    //}

    #region Public Methods

    // Method to update pagination buttons dynamically
    public void UpdatePagination(int totalItems = 0)
    {
        if (totalItems > 0)
        {
            TotalPages = (int)Math.Ceiling((double)totalItems / PageSize);
        }

        panelPages.Controls.Clear();
        for (int i = 1; i <= TotalPages; i++)  // Always show at least 5 buttons or as per total pages
        {
            MaterialButton pageButton = new MaterialButton { AutoSize = false, Text = i.ToString(), Width = 32, Height = 28, Margin = new Padding(10, 5, 10, 0) };
            pageButton.Click += (s, e) =>
            {
                CurrentPage = int.Parse(((Button)s).Text);

                // Raise PageClicked event
                PageClicked?.Invoke(this, CurrentPage);
            };
            panelPages.Controls.Add(pageButton);
        }

        panelPages.Width = (panelPages.Controls.Count * 54);

    }

    #endregion Public Methods

    #region Private Methods

    private void BtnLast_Click(object sender, EventArgs e)
    {
        CurrentPage = TotalPages;

        // Raise LastClicked event
        LastClicked?.Invoke(this, CurrentPage);
    }

    private void BtnNext_Click(object sender, EventArgs e)
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage++;

            // Raise NextClicked event
            NextClicked?.Invoke(this, CurrentPage);
        }
    }

    private void BtnPrevious_Click(object sender, EventArgs e)
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;

            // Raise PreviousClicked event
            PreviousClicked?.Invoke(this, CurrentPage);
        }
    }

    private void InitializeComponents()
    {
        // Initialize controls
        flowLayoutPanel = new FlowLayoutPanel();

        btnPrevious = new MaterialButton
        {
            AutoSize = false,
            CharacterCasing = MaterialButton.CharacterCasingEnum.Title,
            Margin = new System.Windows.Forms.Padding(10, 8, 10, 0),
            Text = "<",
            Width = 32,
            Height = 28
        };

        btnNext = new MaterialButton
        {
            AutoSize = false,
            CharacterCasing = MaterialButton.CharacterCasingEnum.Title,
            Margin = new System.Windows.Forms.Padding(0, 8, 10, 0),
            Text = ">",
            Width = 32,
            Height = 28
        };

        btnLast = new MaterialButton
        {
            AutoSize = false,
            CharacterCasing = MaterialButton.CharacterCasingEnum.Title,
            Margin = new System.Windows.Forms.Padding(10, 8, 10, 0),
            Text = ">>",
            Width = 32,
            Height = 28
        };


        panelPages = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight};
        //panelPages.BackColor = Color.Ivory;

        btnPrevious.Click += BtnPrevious_Click;
        btnNext.Click += BtnNext_Click;
        btnLast.Click += BtnLast_Click;

         
        //flowLayoutPanel.BackColor = Color.Red;
        this.Controls.Add(flowLayoutPanel);
        flowLayoutPanel.Controls.Add(btnPrevious);
        flowLayoutPanel.Controls.Add(panelPages);
        flowLayoutPanel.Controls.Add(btnNext);
        flowLayoutPanel.Controls.Add(btnLast);

        //flowLayoutPanel.Height = 45;

        this.Height = 45;
        this.Width = 430;
        
        panelPages.Height = this.Height-10;
        this.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);

        flowLayoutPanel.Dock = DockStyle.Fill;
    }

    #endregion Private Methods



}
