using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using MurliAnveshan.Classes;

using SelfControls.Controls;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

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

            //cmbLanguages.SelectedIndex = 0;
            //cmbLanguages.SelectedIndexChanged += CmbLanguages_SelectedIndexChanged;

            btnBuildIndex.DrawShadows = false;

            btnExport.Click += BtnExport_Click;

            ChangeLangToHindiInput();
        }

        #region Export Related Methods
        private void BtnExport_Click(object sender, EventArgs e)
        {
            //List<AvyaktMurliDetails> murliDetails = new List<AvyaktMurliDetails>();

            SearchLocation searchLocation = rdbAvyakthAll.Checked ? SearchLocation.All : SearchLocation.TitleOnly;

            List<MurliDetailsBase> details = engine.SearchAllIndex(searchTerm, searchLocation);

            //foreach (MurliCard2 card in flowLayoutPanel1.Controls)
            //{
            //    details = new AvyaktMurliDetails
            //    {
            //        MurliTitle = card.MurliTitle,
            //        MurliDate = card.MurliDate,
            //        MurliLines = new[] { card.MurliLines }.ToList<string>(),
            //        FileName = card.FileName,
            //        SearchTerm = card.SearchTerm
            //    };

            //    murliDetails.Add(details);
            //}

            ExportDocument(details, searchTerm);
        }

        private void ExportDocument(List<MurliDetailsBase> murliIndex, string searchTerm)
        {
            WordDocument doc = CreateAWordDocument(searchTerm);
            AddMurliDetailsToExportDoc(ref doc, murliIndex);

            // Step 7: Save the document
            string fileName = "Export_" + searchTerm + ".docx";
            doc.Save(fileName, FormatType.Docx);

            //SelfMessageBoxWrapper.ShowInfoMessage("Successfully Created Export File: " + fileName + ".", fileName);

            MessageBox.Show("Successfully Created the Export File: " + fileName + ".");

            OpenExportedFile(fileName);
        }

        private void OpenExportedFile(string fileName)
        {
            string filePath = @"C:\Program Files\MurliAnveshan\" + fileName;

            // Check if the file exists before opening
            if (System.IO.File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }

        private static WordDocument CreateAWordDocument(string searchTerm)
        {
            WordDocument document = new WordDocument();
            {
                // Step 2: Add a section to the document
                IWSection TitleSection = document.AddSection();

                SetMargin(TitleSection);

                AddSearchTermAsPrimaryTitle(searchTerm, TitleSection);
            }
            return document;
        }

        private static void AddSearchTermAsPrimaryTitle(string searchTerm, IWSection TitleSection)
        {
            // Step 3: Add a paragraph to the section
            IWParagraph paragraph = TitleSection.AddParagraph();
            IWTextRange fileTitleText = paragraph.AppendText(searchTerm); //.ApplyCharacterFormat(new WCharacterFormat()

            fileTitleText.CharacterFormat.FontName = "Noto Sans Devanagari";
            fileTitleText.CharacterFormat.FontSize = 18;
            fileTitleText.CharacterFormat.TextColor = Color.FromArgb(47, 85, 151);

            //{
            //    Bold = true,
            //    FontSize = 16
            //});

            // Add some spacing after the title
            paragraph.ParagraphFormat.BeforeSpacing = 12;
            paragraph.ParagraphFormat.AfterSpacing = 12;

            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

            paragraph.AppendText("\n");
        }

        private static void SetMargin(IWSection TitleSection)
        {
            //2.54 cm = 1 inch 
            //1 centimeter = 28.35 points.
            // Set the margins using centimeters (2 cm = 2 * 28.35 points).

            // Set the section's margins to "moderate" (1 inch = 72 points).
            TitleSection.PageSetup.Margins.Top = (float)(2.54 * 28.35);
            TitleSection.PageSetup.Margins.Bottom = (float)(2.54 * 28.35);
            TitleSection.PageSetup.Margins.Left = (float)(1.91 * 28.35);
            TitleSection.PageSetup.Margins.Right = (float)(1.91 * 28.35);
        }

        private static void AddMurliDetailsToExportDoc(ref WordDocument document, List<MurliDetailsBase> murliIndex)
        {
            IWSection section = document.Sections[0];

            for (int i = 0; i < murliIndex.Count; i++)
            {
                MurliDetailsBase item = murliIndex[i];

                AddMurliTitleToExportDocument(section, i, item);

                AddMurliDateToExportDocument(section, item);

                AddMurliLinesToExportDocument(section, item);
            }
        }

        private static void AddMurliLinesToExportDocument(IWSection section, MurliDetailsBase item)
        {
            //IWSection murliLinesSection = document.AddSection();
            IWParagraph murliLinesParagraph = section.AddParagraph();

            string combinedMurliLines = string.Join("।", item.MurliLines); // Join lines with newline

            string murliLines = string.Empty;

            if (combinedMurliLines.Length > 0)
            {
                //Removes starting "|"
                murliLines = combinedMurliLines.Remove(combinedMurliLines.IndexOf('।'), 1);
            }

            IWTextRange murliLinesText = murliLinesParagraph.AppendText(murliLines);
            FormatMurliLines(murliLinesParagraph, murliLinesText);

            if (murliLinesText.Text.Length > 0)
                murliLinesParagraph.AppendText("\n");
        }

        private static void FormatMurliLines(IWParagraph murliLinesParagraph, IWTextRange murliLinesText)
        {
            murliLinesText.CharacterFormat.FontName = "Noto Sans Devanagari";
            murliLinesText.CharacterFormat.FontSize = 11;

            //murliLinesParagraph.ParagraphFormat.LineSpacingRule = Syncfusion.DocIO.LineSpacingRule.Multiple;
            murliLinesParagraph.ParagraphFormat.LineSpacing = 15f;
            murliLinesParagraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Justify;

            murliLinesParagraph.ParagraphFormat.BeforeSpacing = 6;
            murliLinesParagraph.ParagraphFormat.AfterSpacing = 6;
        }

        private static void AddMurliDateToExportDocument(IWSection section, MurliDetailsBase item)
        {
            //IWSection murliDateSection = document.AddSection();
            IWParagraph murliDateParagraph = section.AddParagraph();

            IWTextRange murliDateText = murliDateParagraph.AppendText(item.MurliDate);
            murliDateText.CharacterFormat.FontName = "Bookman Old Style";
            murliDateText.CharacterFormat.FontSize = 10;
            murliDateText.CharacterFormat.TextColor = Color.FromArgb(91, 155, 213);

            //murliDateParagraph.ParagraphFormat.LineSpacingRule = Syncfusion.DocIO.LineSpacingRule.Exactly;
            murliDateParagraph.ParagraphFormat.LineSpacing = 12f;

            murliDateParagraph.AppendText("\n");
        }

        private static void AddMurliTitleToExportDocument(IWSection section, int i, MurliDetailsBase item)
        {
            IWParagraph murliTitleParagraph = section.AddParagraph();
            murliTitleParagraph.AppendText((i + 1).ToString() + ") ");
            IWTextRange murliTitleText = murliTitleParagraph.AppendText(item.MurliTitle);
            murliTitleText.CharacterFormat.FontName = "Noto Sans Devanagari";
            murliTitleText.CharacterFormat.FontSize = 13;
            murliTitleText.CharacterFormat.TextColor = Color.FromArgb(47, 85, 151);

            //murliTitleParagraph.ParagraphFormat.LineSpacingRule = Syncfusion.DocIO.LineSpacingRule.Exactly;
            murliTitleParagraph.ParagraphFormat.LineSpacing = 12f;
            murliTitleParagraph.ParagraphFormat.BeforeSpacing = 12;
            murliTitleParagraph.ParagraphFormat.AfterSpacing = 6;
        }

        #endregion Export Related Methods

        //private void CmbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbLanguages.SelectedIndex == 0)
        //    {
        //        txtSearch.Font = new Font("Bookman Old Style", 12);
        //        ToEnglishInput();
        //    }
        //    else if (cmbLanguages.SelectedIndex == 1)
        //    {
        //        txtSearch.ImeMode = ImeMode.On;

        //        //txtSearch.Font = new Font("Tiro Devanagari Hindi", 13);
        //        txtSearch.Font = new Font("Mangal", 13);
        //        ToHindiInput();
        //    }
        //    else
        //    {

        //    }
        //}

        ~FrmHome()
        {
            ChangeLangToEnglishInput();
        }

        public void ChangeLangToEnglishInput()
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                string CName = lang.Culture.EnglishName;
                if (CName.StartsWith("English"))
                {
                    InputLanguage.CurrentInputLanguage = lang;
                }
            }
        }

        public void ChangeLangToHindiInput()
        {
            //foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            //{
            //    string CName = lang.Culture.EnglishName;
            //    var l = lang.LayoutName;

            //    if (CName.StartsWith("Hindi"))
            //    {
            //        InputLanguage.CurrentInputLanguage = lang;
            //    }
            //}

            //var phoneticKeyboard = InputLanguage.InstalledInputLanguages
            //.Cast<InputLanguage>()
            //.FirstOrDefault(lang =>
            //    lang.Culture.KeyboardLayoutId == 1081 &&
            //lang.Culture.TwoLetterISOLanguageName == "hi"); 

            var phoneticKeyboard = InputLanguage.InstalledInputLanguages
            .Cast<InputLanguage>()
            .FirstOrDefault(lang =>
                lang.LayoutName == "Google Input Tools" && lang.Culture.Name == "hi-IN");

            if (phoneticKeyboard != null)
            {
                // Set Hindi Phonetic Keyboard as the current input language
                InputLanguage.CurrentInputLanguage = phoneticKeyboard;
                //MessageBox.Show($"Switched to: {phoneticKeyboard.LayoutName}");
            }
            else
            {
                MessageBox.Show("Hindi Phonetic Keyboard is not installed. Please add it in Windows settings.");
            }
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
                SelfMessageBoxWrapper.ShowInfoMessage("Successfully Created Index.");
                //MessageBox.Show("Successfully Created Index.");
            }
            else
            {
                SelfMessageBoxWrapper.ShowInfoMessage("Sorry!! Index Creation Failed.");

                //MessageBox.Show("Sorry!! Index Creation Failed.");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //Validate for NULL & Empty

            ExecuteSearch();

            CenterAlignPaginationControl();
        }

        private void CenterAlignPaginationControl()
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
                murliCard.ControlClicked += OnResultCardClick;

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

                //murliCard.HighlightSearchTerm(searchTerm);

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
