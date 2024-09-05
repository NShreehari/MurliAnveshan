using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MurliAnveshan.Classes;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace MurliAnveshan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            engine = new MurliSearchEngine();
            //engine.indexPath

            if (!System.IO.Directory.Exists(engine.indexPath) || System.IO.Directory.GetFiles(engine.indexPath).Count() < 5)
            {
                BuildIndex();
            }

            txtSearch.Focus();
        }


        MurliSearchEngine engine;

        private void btnBuildIndex_Click(object sender, EventArgs e)
        {
            BuildIndex();
        }

        private void BuildIndex()
        {
            List<MurliDetails> murliDetailsList = ExtractDetailsFromDocx("C:\\Users\\Shreehari\\source\\repos\\2025\\MurliAnveshan\\AV Murlis\\Jan 1969 Only 25 Pages.docx");

            engine.AddMurliDetailsToIndex(murliDetailsList);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Validate for NULL & Empty

            MurliSearchEngine.SearchLocation searchLocation = rdbAll.Checked ? MurliSearchEngine.SearchLocation.All : MurliSearchEngine.SearchLocation.TitleOnly;
            var results = engine.SearchIndex(txtSearch.Text, searchLocation);

            if (results.Count() > 0)
            {
                foreach (var item in results)
                {
                    richTextBox1.AppendText(item.MurliDate);
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText(item.MurliTitle);
                    richTextBox1.AppendText("\n");

                    string combinedMurliLines = string.Join(Environment.NewLine, item.MurliLines); // Join lines with newline

                    // Add the combined string to the RichTextBox
                    richTextBox1.AppendText(combinedMurliLines);

                    //richTextBox1.AppendText(item.MurliLines.ToString());

                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText("\n");

                }

                HighlightSearchTerm(richTextBox1, txtSearch.Text);
            }

            else
            {
                MessageBox.Show("Sorry the Word:" + "\"" + txtSearch.Text + "\"" + "Not Found in Murlis.");
            }
        }

        public List<MurliDetails> ExtractDetailsFromDocx(string docxPath)
        {
            List<MurliDetails> murliDetails = new List<MurliDetails>();
            MurliDetails currentChapter = null;
            //StringBuilder currentTitleBuilder = new StringBuilder();
            StringBuilder fullParagraphText = new StringBuilder();
            string fullParagraph;

            using (WordDocument document = new WordDocument(docxPath, FormatType.Docx))
            {
                foreach (IWSection section in document.Sections)
                {
                    foreach (IWParagraph paragraph in section.Body.Paragraphs)
                    {
                        fullParagraphText.Clear();// To accumulate the full line

                        int appendCount = 0;
                        foreach (WTextRange textRange in paragraph.ChildEntities)
                        {
                            fullParagraphText.Append(textRange.Text); // Concatenate text from all runs

                            appendCount++;

                            if (appendCount != paragraph.ChildEntities.Count)
                                continue;

                            //if (string.IsNullOrEmpty(fullParagraphText)) continue;


                            var format = textRange.CharacterFormat;
                            fullParagraph = fullParagraphText.ToString();

                            // Check for Date (Noto Sans, Size 14, Red color)
                            if (IsDate(format))
                            {
                                if (currentChapter != null)
                                    murliDetails.Add(currentChapter);

                                currentChapter = new MurliDetails { MurliDate = fullParagraph.Split(' ')[0] };
                            }

                            // Check for Title (Noto Sans, Size 16, Double quoted)
                            else if (IsTitle(format, fullParagraph))
                            {
                                if (currentChapter != null)
                                {
                                    currentChapter.MurliTitle = fullParagraph;
                                }
                            }

                            // Body (Tiro Devanagari Hindi, Size 13)
                            else if (IsBodyText(format))
                            {
                                currentChapter?.MurliLines.Add(fullParagraph);
                            }
                        }
                    }

                }


                if (currentChapter != null) murliDetails.Add(currentChapter); // Add last chapter

            }

            return murliDetails;

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


        private bool IsDate(WCharacterFormat format)
        {
            return format.FontName == "Noto Sans" && format.FontSize == 14 && format.Bold && format.TextColor == Color.FromArgb(255, 255, 0, 0);
        }

        private bool IsTitle(WCharacterFormat format, string text)
        {
            return format.FontName == "Noto Sans" && format.FontSize == 16 && format.Bold && text.StartsWith("\"") && text.EndsWith("\"");
        }

        private bool IsBodyText(WCharacterFormat format)
        {
            return format.FontName == "Tiro Devanagari Hindi" || format.FontName == "Noto Sans" && format.FontSize == 13;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}

