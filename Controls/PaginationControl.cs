using System;
using System.Windows.Forms;

using MaterialSkin.Controls;

public class PaginationControl : Control
{
    #region Private Fields

    // Property to allow users to select the FlowLayoutPanel for displaying the results
    //private Control _targetPanel;

    private MaterialButton btnNext;
    private MaterialButton btnPrevious;
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

    public event EventHandler<int> NextClicked;

    public event EventHandler<int> PageClicked;

    public event EventHandler<int> PreviousClicked;

    #endregion Public Events

    #region Public Properties

    public int CurrentPage { get; set; } = 1;

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

        if (TotalPages <= 1)
        {
            AddSinglePageButton();
            AdjustWidth();
            return;
        }

        const int maxVisibleButtons = 3;
        CalculatePageRange(maxVisibleButtons, out int startPage, out int endPage);

        AddFirstPageButton();
        AddPreviousEllipsisButton(startPage, maxVisibleButtons);
        AddVisiblePageButtons(startPage, endPage);
        AddNextEllipsisButton(endPage);
        AddLastPageButton();

        AdjustWidth();

        //EnablePreviousButton();
        //EnableNextButton();
    }

    private void AddSinglePageButton()
    {
        MaterialButton singlePageButton = new MaterialButton
        {
            AutoSize = false,
            Text = "1",
            Width = 32,
            Height = 28,
            Margin = new Padding(10, 5, 10, 0),
            Type = MaterialButton.MaterialButtonType.Contained
        };
        singlePageButton.Click += (s, e) =>
        {
            CurrentPage = 1;
            PageClicked?.Invoke(this, CurrentPage);
        };
        panelPages.Controls.Add(singlePageButton);

        TogglePreviousButtonEnableState();
        ToggleNextButtonEnableState();
    }

    private void CalculatePageRange(int maxVisibleButtons, out int startPage, out int endPage)
    {
        startPage = Math.Max(2, CurrentPage - (maxVisibleButtons / 2));
        endPage = Math.Min(TotalPages - 1, startPage + maxVisibleButtons - 1);

        if (endPage - startPage + 1 < maxVisibleButtons && TotalPages > maxVisibleButtons)
        {
            if (startPage == 2)
                endPage = Math.Min(TotalPages - 1, maxVisibleButtons);
            else
                startPage = Math.Max(2, endPage - maxVisibleButtons + 1);
        }
    }

    private void AddFirstPageButton()
    {
        MaterialButton firstButton = new MaterialButton
        {
            AutoSize = false,
            Text = "1",
            Width = 32,
            Height = 28,
            Margin = new Padding(10, 5, 10, 0),
            Type = CurrentPage == 1 ? MaterialButton.MaterialButtonType.Contained : MaterialButton.MaterialButtonType.Outlined
        };
        firstButton.Click += (s, e) =>
        {
            CurrentPage = 1;
            PageClicked?.Invoke(this, CurrentPage);
            TogglePreviousButtonEnableState();
            UpdatePagination();
        };
        panelPages.Controls.Add(firstButton);
    }

    private void AddPreviousEllipsisButton(int startPage, int maxVisibleButtons)
    {
        if (startPage > 2)
        {
            MaterialButton prevEllipsisButton = new MaterialButton
            {
                AutoSize = false,
                Text = "...",
                Width = 32,
                Height = 28,
                Margin = new Padding(10, 5, 10, 0),
                Type = MaterialButton.MaterialButtonType.Outlined
            };
            prevEllipsisButton.Click += (s, e) =>
            {
                CurrentPage = Math.Max(1, startPage - maxVisibleButtons);
                UpdatePagination();
            };
            panelPages.Controls.Add(prevEllipsisButton);
        }
    }

    private void AddVisiblePageButtons(int startPage, int endPage)
    {
        for (int i = startPage; i <= endPage; i++)
        {
            MaterialButton pageButton = new MaterialButton
            {
                AutoSize = false,
                Text = i.ToString(),
                Width = 32,
                Height = 28,
                Margin = new Padding(10, 5, 10, 0),
                Type = i == CurrentPage ? MaterialButton.MaterialButtonType.Contained : MaterialButton.MaterialButtonType.Outlined
            };
            pageButton.Click += (s, e) =>
            {
                CurrentPage = int.Parse(((Button)s).Text);
                PageClicked?.Invoke(this, CurrentPage);
                UpdatePagination();
            };
            panelPages.Controls.Add(pageButton);
        }

        TogglePreviousButtonEnableState();
        ToggleNextButtonEnableState();
    }

    private void AddNextEllipsisButton(int endPage)
    {
        if (endPage < TotalPages - 1)
        {
            MaterialButton nextEllipsisButton = new MaterialButton
            {
                AutoSize = false,
                Text = "...",
                Width = 32,
                Height = 28,
                Margin = new Padding(10, 5, 10, 0),
                Type = MaterialButton.MaterialButtonType.Outlined
            };
            nextEllipsisButton.Click += (s, e) =>
            {
                CurrentPage = Math.Min(TotalPages, endPage + 1);
                UpdatePagination();
            };
            panelPages.Controls.Add(nextEllipsisButton);
        }
    }

    private void AddLastPageButton()
    {
        MaterialButton lastButton = new MaterialButton
        {
            AutoSize = false,
            Text = TotalPages.ToString(),
            Width = 32,
            Height = 28,
            Margin = new Padding(10, 5, 10, 0),
            Type = CurrentPage == TotalPages ? MaterialButton.MaterialButtonType.Contained : MaterialButton.MaterialButtonType.Outlined
        };
        lastButton.Click += (s, e) =>
        {
            CurrentPage = TotalPages;
            PageClicked?.Invoke(this, CurrentPage);
            UpdatePagination();
            ToggleNextButtonEnableState();
        };
        panelPages.Controls.Add(lastButton);
    }

    private void AdjustWidth()
    {
        // Adjust panel width dynamically
        panelPages.Width = panelPages.Controls.Count * 54;
        this.Width = panelPages.Width + (3 * 54) + 20;
    }

    #endregion Public Methods

    #region Private Methods

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

            TogglePreviousButtonEnableState();
        }
    }

    private void TogglePreviousButtonEnableState()
    {
        btnPrevious.Enabled = CurrentPage != 1;
    }

    private void ToggleNextButtonEnableState()
    {
        btnNext.Enabled = CurrentPage != TotalPages;
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
            Height = 28,
            Type = MaterialButton.MaterialButtonType.Outlined
        };

        btnNext = new MaterialButton
        {
            AutoSize = false,
            CharacterCasing = MaterialButton.CharacterCasingEnum.Title,
            Margin = new System.Windows.Forms.Padding(0, 8, 10, 0),
            Text = ">",
            Width = 32,
            Height = 28,
            Type = MaterialButton.MaterialButtonType.Outlined
        };

        panelPages = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight };
        //panelPages.BackColor = Color.Ivory;

        btnPrevious.Click += BtnPrevious_Click;
        btnNext.Click += BtnNext_Click;

        //flowLayoutPanel.BackColor = Color.Red;
        this.Controls.Add(flowLayoutPanel);
        flowLayoutPanel.Controls.Add(btnPrevious);
        flowLayoutPanel.Controls.Add(panelPages);
        flowLayoutPanel.Controls.Add(btnNext);

        //flowLayoutPanel.Height = 45;

        this.Height = 45;
        this.Width = 430;

        panelPages.Height = this.Height - 10;
        this.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);

        flowLayoutPanel.Dock = DockStyle.Fill;
    }

    #endregion Private Methods

}
