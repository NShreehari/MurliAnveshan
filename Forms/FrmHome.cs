using System;
using System.Linq;
using System.Windows.Forms;

using SelfControls.Controls;

using MurliAnveshan.Classes;

using static MurliAnveshan.Classes.Enums;
using Panel = System.Windows.Forms.Panel;

namespace MurliAnveshan
{
    public partial class FrmHome : Form
    {
        private readonly AvyaktMurliSearchEngine engine;

        private string searchTerm;

        readonly MainForm mainFormInstance;

        private readonly Panel pnlToShowMurliCardsInFullExpansion;

        public FrmHome()
        {
            InitializeComponent();

            flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;
            pnlToShowMurliCardsInFullExpansion = new Panel
            {
                Size = flowLayoutPanel1.Size,
                Location = flowLayoutPanel1.Location
            };

            this.Controls.Add(pnlToShowMurliCardsInFullExpansion);
        }

        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            pnlToShowMurliCardsInFullExpansion.Size = flowLayoutPanel1.Size;
            pnlToShowMurliCardsInFullExpansion.Location = flowLayoutPanel1.Location;
        }

        public FrmHome(MainForm mainFormInstance) : this()
        {
            this.mainFormInstance = mainFormInstance;

            rdbAvyakthMurlis.CheckedChanged += MurliSelection_Changed;
            rdbSakarMurlis.CheckedChanged += MurliSelection_Changed;

            engine = new AvyaktMurliSearchEngine();

            ShowGrbAvyakthMurliCategory();

            paginationControl.PreviousClicked += PaginationControl_PreviousClicked;
            paginationControl.NextClicked += PaginationControl_NextClicked;
            paginationControl.PageClicked += PaginationControl_PageClicked;

            // Optionally set the TargetPanel for pagination
            //paginationControl.TargetPanel = flowLayoutPanel1;

            txtSearch.Focus();
        }

        private void BtnBuildIndex_Click(object sender, EventArgs e)
        {
            if (engine.BuildIndex())
            {
                MessageBox.Show("Successfully Created Index.");
            }
            else
            {
                MessageBox.Show("Sorry!! Index Creation Failed.");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //Validate for NULL & Empty

            ExecuteSearch();

            CenterAlighPaginationControl();
        }

        private void CenterAlighPaginationControl()
        {
            this.paginationControl.Left = (this.ClientSize.Width - paginationControl.Width) / 2;
        }

        private void ExecuteSearch(int currentPage = 1)
        {
            SearchLocation searchLocation = rdbAvyakthAll.Checked ? SearchLocation.All : SearchLocation.TitleOnly;
            searchTerm = txtSearch.Text;
            var results = engine.SearchIndex(searchTerm, searchLocation, currentPage);

            this.paginationControl.PageSize = engine.pageSize;
            this.paginationControl.UpdatePagination(results.TotalHits);

            if (results.Results.Any())
            {
                //AddResultsToRichTextBox(results);

                AddResultsToCard(results.Results);

                //HighlightSearchTerm(richTextBox1, searchTerm);
            }
            else
            {
                //MessageBox.Show("Sorry the Word:  \"" + searchTerm + "\"  Not Found in Murlis.");
                //Messages.ErrorMessage("Sorry the Word:  \"" + searchTerm + "\"  Not Found in Murlis.");
                SelfMessageBoxWrapper.ShowErrorMessage("Sorry, the Word:  \"" + searchTerm + "\"  Not Found in Murlis.", searchTerm);
            }
        }

        //private void AddResultsToRichTextBox(System.Collections.Generic.IEnumerable<MurliDetailsBase> results)
        //{
        //    foreach (var item in results)
        //    {
        //        richTextBox1.AppendText(item.MurliDate);
        //        richTextBox1.AppendText("\n");
        //        richTextBox1.AppendText(item.MurliTitle);
        //        richTextBox1.AppendText("\n");

        //        string combinedMurliLines = string.Join("।", item.MurliLines); // Join lines with newline

        //        // Add the combined string to the RichTextBox
        //        richTextBox1.AppendText(combinedMurliLines);

        //        //richTextBox1.AppendText(item.MurliLines.ToString());

        //        richTextBox1.AppendText("\n");
        //        richTextBox1.AppendText("\n");
        //    }
        //}

        private void AddResultsToCard(System.Collections.Generic.IEnumerable<MurliDetailsBase> results)
        {
            SelfControls.Controls.MurliCard2 murliCard;

            foreach (var item in results)
            {
                murliCard = new SelfControls.Controls.MurliCard2
                {
                    //Width = flowLayoutPanel1.Width - 20,
                    Left = 10,
                    MurliTitle = item.MurliTitle,
                    MurliDate = item.MurliDate,
                    FileName = item.FileName,
                    TitleAlignment = System.Drawing.ContentAlignment.MiddleLeft,
                    SearchTerm = this.searchTerm
                };

                murliCard.FullExpansionStateChanged += MurliCard_FullExpansionStateChanged;
                murliCard.Click += OnResultCardClick;

                string combinedMurliLines = string.Join("।", item.MurliLines); // Join lines with newline

                if (combinedMurliLines.Length > 0)
                {
                    // Add the combined string to the RichTextBox

                    //Removes starting "|"
                    murliCard.MurliLines = combinedMurliLines.Remove(combinedMurliLines.IndexOf('।'), 1);

                    //Removes Last ">"
                    //combinedMurliLines.Remove(combinedMurliLines.LastIndexOf(">"), 1);

                    //    var wordCount = combinedMurliLines
                    //.Split(new[] { ' ', '.', ',', ';', ':', '!', '?', '।' }, StringSplitOptions.RemoveEmptyEntries)
                    //.Count(w => w.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));

                    //resultCard.ResultCardSize = ResultCardSizeEnum.Big;
                }
                else
                {
                    //if (item.MurliTitle.Contains(searchTerm))
                    {
                        //resultCard.ResultCardSize = ResultCardSizeEnum.Small;

                        murliCard.IsTitleOnlySearch = true;
                    }
                    //else
                    //{
                    //    continue;
                    //}
                }

                //resultCard.HighlightSearchTerm(searchTerm);

                //resultCard.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //resultCard.Width = this.flowLayoutPanel1.Width - 32;

                flowLayoutPanel1.Controls.Add(murliCard);
            }
        }

        private void MurliCard_FullExpansionStateChanged(object sender, bool e)
        {
            MurliCard2 cardToHandle = (sender as MurliCard2);

            if (e) //Expanded
            {
                cardToHandle.Dock = DockStyle.Fill;

                pnlToShowMurliCardsInFullExpansion.Controls.Add(cardToHandle);

                flowLayoutPanel1.Hide();
                pnlToShowMurliCardsInFullExpansion.Show();
                //pnlToShowMurliCardsInFullExpansion.BringToFront();
            }
            else //Collapsed
            {
                pnlToShowMurliCardsInFullExpansion.Controls.Remove(cardToHandle);
                flowLayoutPanel1.Controls.Add(cardToHandle);

                cardToHandle.Dock = DockStyle.None;

                pnlToShowMurliCardsInFullExpansion.Hide();
                flowLayoutPanel1.Show();
            }
        }

        private void OnResultCardClick(object sender, EventArgs e)
        {
            ResultDetails resultDetails = (sender as MurliCard2).FetchResultDetails();

            mainFormInstance.LoadDocViewer(mainFormInstance, resultDetails);
        }

        //public void HighlightSearchTerm(RichTextBox richTextBox, string searchTerm)
        //{
        //    // Reset any previous highlighting
        //    richTextBox.SelectAll();
        //    richTextBox.SelectionBackColor = richTextBox.BackColor;

        //    // Start searching from the beginning of the text
        //    int startIndex = 0;

        //    while (startIndex < richTextBox.TextLength)
        //    {
        //        // Find the search term in the text
        //        int searchTermIndex = richTextBox.Text.IndexOf(searchTerm, startIndex, StringComparison.OrdinalIgnoreCase);

        //        // If the search term is found
        //        if (searchTermIndex != -1)
        //        {
        //            // Select the search term in the RichTextBox
        //            richTextBox.Select(searchTermIndex, searchTerm.Length);

        //            // Set the background color of the selected text to yellow (highlight)
        //            richTextBox.SelectionBackColor = Color.Yellow;

        //            // Move the start index past the current search term
        //            startIndex = searchTermIndex + searchTerm.Length;
        //        }
        //        else
        //        {
        //            // If the search term is not found, break the loop
        //            break;
        //        }
        //    }

        //    // Deselect any text (optional)
        //    richTextBox.DeselectAll();
        //}

        private void BtnClear_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }

        private void MurliSelection_Changed(object sender, EventArgs e)
        {
            if (rdbAvyakthMurlis.Checked)
            {
                ShowGrbAvyakthMurliCategory();
            }
            else
            {
                ShowGrbSakarMurliCategory();
            }
        }

        private void ShowGrbSakarMurliCategory()
        {
            grbAvyakthMurliCategory.Visible = false;

            grbSakarMurliCategory.Visible = true;
        }

        private void ShowGrbAvyakthMurliCategory()
        {
            grbAvyakthMurliCategory.Visible = true;

            grbSakarMurliCategory.Visible = false;
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void PaginationControl_PreviousClicked(object sender, int pageNumber)
        {
            flowLayoutPanel1.Controls.Clear();

            ExecuteSearch(pageNumber);
        }

        private void PaginationControl_NextClicked(object sender, int pageNumber)
        {
            flowLayoutPanel1.Controls.Clear();

            ExecuteSearch(pageNumber);
        }

        private void PaginationControl_PageClicked(object sender, int pageNumber)
        {
            flowLayoutPanel1.Controls.Clear();

            ExecuteSearch(pageNumber);
        }

    }
}
