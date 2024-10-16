using System;
using System.Linq;
using System.Windows.Forms;

using SelfControls.Controls;

using MurliAnveshan.Classes;

using static MurliAnveshan.Classes.Enums;

namespace MurliAnveshan
{
    public partial class FrmHome : Form
    {
        private readonly AvyaktMurliSearchEngine engine;

        private string searchTerm;

        readonly MainForm mainFormInstance;

        public FrmHome(MainForm mainFormInstance)
        {
            InitializeComponent();

            this.mainFormInstance = mainFormInstance;

            rdbAvyakthMurlis.CheckedChanged += MurliSelection_Changed;
            rdbSakarMurlis.CheckedChanged += MurliSelection_Changed;

            engine = new AvyaktMurliSearchEngine();

            ShowGrbAvyakthMurliCategory();


            paginationControl.PreviousClicked += PaginationControl_PreviousClicked;
            paginationControl.NextClicked += PaginationControl_NextClicked;
            paginationControl.PageClicked += PaginationControl_PageClicked;
            paginationControl.LastClicked += PaginationControl_LastClicked;

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


        int pageSize = 10;

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //Validate for NULL & Empty

            ExecuteSearch();
        }

        private void ExecuteSearch(int currentPage = 1)
        {
            SearchLocation searchLocation = rdbAvyakthAll.Checked ? SearchLocation.All : SearchLocation.TitleOnly;
            searchTerm = txtSearch.Text;
            var results = engine.SearchIndex(searchTerm, searchLocation, currentPage);

            this.paginationControl.PageSize = engine.pageSize;
            this.paginationControl.UpdatePagination(results.Results.Count());

            if (results.Results.Count() > 0)
            {
                //AddResultsToRichTextBox(results);

                AddResultsToCard(results.Results);

                //HighlightSearchTerm(richTextBox1, searchTerm);
            }

            else
            {
                MessageBox.Show("Sorry the Word:" + "\"" + searchTerm + "\"" + "Not Found in Murlis.");
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
            SelfControls.Controls.ResultCard4 resultCard;

            foreach (var item in results)
            {
                resultCard = new SelfControls.Controls.ResultCard4
                {
                    Width = flowLayoutPanel1.Width - 20,
                    Left = 10,
                    Title = item.MurliTitle,
                    ContentDate = item.MurliDate,
                    FileName = item.FileName
                };
                resultCard.Click += new System.EventHandler(OnResultCardClick);

                string combinedMurliLines = string.Join("।", item.MurliLines); // Join lines with newline

                if (combinedMurliLines.Length > 0)
                {
                    // Add the combined string to the RichTextBox

                    //Removes starting "|"
                    resultCard.Content = combinedMurliLines.Remove(combinedMurliLines.IndexOf('।'), 1);

                    //Removes Last ">"
                    //combinedMurliLines.Remove(combinedMurliLines.LastIndexOf(">"), 1);


                    //    var wordCount = combinedMurliLines
                    //.Split(new[] { ' ', '.', ',', ';', ':', '!', '?', '।' }, StringSplitOptions.RemoveEmptyEntries)
                    //.Count(w => w.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));

                    resultCard.ResultCardSize = ResultCardSizeEnum.Big;
                }
                else
                {
                    if (item.MurliTitle.Contains(searchTerm))
                    {
                        resultCard.ResultCardSize = ResultCardSizeEnum.Small;
                    }
                    else
                    {
                        continue;
                    }
                }

                //resultCard.HighlightSearchTerm(searchTerm);

                resultCard.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                resultCard.Width = this.flowLayoutPanel1.Width - 32;

                flowLayoutPanel1.Controls.Add(resultCard);
            }
        }

        private void OnResultCardClick(object sender, EventArgs e)
        {
            ResultDetails resultDetails = (sender as ResultCard4).FetchResultDetails();

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

        private void PaginationControl_LastClicked(object sender, int pageNumber)
        {
            flowLayoutPanel1.Controls.Clear();
            ExecuteSearch(pageNumber);
        }
    }
}

