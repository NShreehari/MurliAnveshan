using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using MurliAnveshan.Classes;

using static MurliAnveshan.Classes.Enums;

namespace MurliAnveshan
{
    public partial class MainForm : Form
    {
        AvyaktMurliSearchEngine engine;


        public MainForm()
        {
            InitializeComponent();

            rdbAvyakthMurlis.CheckedChanged += MurliSelection_Changed;
            rdbSakarMurlis.CheckedChanged += MurliSelection_Changed;

            engine = new AvyaktMurliSearchEngine();

            ShowGrbAvyakthMurliCategory();

            txtSearch.Focus();
        }



        private void btnBuildIndex_Click(object sender, EventArgs e)
        {
            if(engine.BuildIndex())
            {
                MessageBox.Show("Successfully Created Index.");
            }
            else
            {
                MessageBox.Show("Sorry!! Index Creation Failed.");
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Validate for NULL & Empty

            SearchLocation searchLocation = rdbAvyakthAll.Checked ? SearchLocation.All : SearchLocation.TitleOnly;
            var results = engine.SearchIndex(txtSearch.Text, searchLocation);

            if (results.Count() > 0)
            {
                AddResultsToRichTextBox(results);

                HighlightSearchTerm(richTextBox1, txtSearch.Text);
            }

            else
            {
                MessageBox.Show("Sorry the Word:" + "\"" + txtSearch.Text + "\"" + "Not Found in Murlis.");
            }
        }

        private void AddResultsToRichTextBox(System.Collections.Generic.IEnumerable<MurliDetailsBase> results)
        {
            foreach (var item in results)
            {
                richTextBox1.AppendText(item.MurliDate);
                richTextBox1.AppendText("\n");
                richTextBox1.AppendText(item.MurliTitle);
                richTextBox1.AppendText("\n");

                string combinedMurliLines = string.Join("।", item.MurliLines); // Join lines with newline

                // Add the combined string to the RichTextBox
                richTextBox1.AppendText(combinedMurliLines);

                //richTextBox1.AppendText(item.MurliLines.ToString());

                richTextBox1.AppendText("\n");
                richTextBox1.AppendText("\n");
            }
        }

        public void HighlightSearchTerm(RichTextBox richTextBox, string searchTerm)
        {
            // Reset any previous highlighting
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = richTextBox.BackColor;

            // Start searching from the beginning of the text
            int startIndex = 0;

            while (startIndex < richTextBox.TextLength)
            {
                // Find the search term in the text
                int searchTermIndex = richTextBox.Text.IndexOf(searchTerm, startIndex, StringComparison.OrdinalIgnoreCase);

                // If the search term is found
                if (searchTermIndex != -1)
                {
                    // Select the search term in the RichTextBox
                    richTextBox.Select(searchTermIndex, searchTerm.Length);

                    // Set the background color of the selected text to yellow (highlight)
                    richTextBox.SelectionBackColor = Color.Yellow;

                    // Move the start index past the current search term
                    startIndex = searchTermIndex + searchTerm.Length;
                }
                else
                {
                    // If the search term is not found, break the loop
                    break;
                }
            }

            // Deselect any text (optional)
            richTextBox.DeselectAll();
        }        

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
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
    }
}

