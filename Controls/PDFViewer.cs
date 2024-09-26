using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using Syncfusion.Windows.Forms.PdfViewer;
using Syncfusion.Windows.PdfViewer;
using System.Windows.Controls.Ribbon;

namespace MurliAnveshan.Controls
{
    public partial class PDFViewer : System.Windows.Forms.UserControl
    {
        private string _loadedDocumentName;

        public PDFViewer()
        {
            InitializeComponent();

            ribbonControlAdv1.BackStageView = null;
            //ribbonControlAdv1.ShowRibbonDisplayOptionButton = false;

            //To Hide the BackStage Button (First Menu : similer to File Menu in Office 2013.)
            ribbonControlAdv1.MenuButtonVisible = false;

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            openToolStripButton.Click += OpenToolStripButton_Click;
            printToolStripButton.Click += PrintToolStripButton_Click;
            copyToolStripButton.Click += CopyToolStripButton_Click;

            //View Buttons
            ZoomInToolStripButton.Click += ZoomInToolStripButton_Click;
            ZoomOutToolStripButton.Click += ZoomOutToolStripButton_Click;
            ZoomLevelToolStripComboBox.Click += ZoomLevelToolStripComboBox_Click;

            FitToPageToolStripButton.Click += FitToPageToolStripButton_Click;
            FitToWidthToolStripButton.Click += FitToWidthToolStripButton_Click;
            FullScreenToolStripButton.Click += FullScreenToolStripButton_Click;

            KannadaAudioTsb.Click += KannadaAudioTsb_Click;
            KannadaVideoTsb.Click += KannadaVideoTsb_Click;
            KannadaPDFTsb.Click += KannadaPDFTsb_Click;
            KannadaHtmlTsb.Click += KannadaHtmlTsb_Click;

            HindiAudioTsb.Click += HindiAudioTsb_Click;
            HindiVideoTsb.Click += HindiVideoTsb_Click;
            HindiPDFTsb.Click += HindiPDFTsb_Click;
            HindiHtmlTsb.Click += HindiHtmlTsb_Click;

            EnglishAudioTsb.Click += EnglishAudioTsb_Click;
            EnglishVideoTsb.Click += EnglishVideoTsb_Click;
            EnglishPDFTsb.Click += EnglishPDFTsb_Click;
            EnglishHtmlTsb.Click += EnglishHtmlTsb_Click;

        }

        #region Home

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadPdf(openFileDialog.FileName, openFileDialog.SafeFileName);
            }
        }

        private void CopyToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            if (GetActivePdfViewer() != null)
            {
                ////GetActivePdfViewer().PrintDocument.Print();

                //PrintDialog printDialog = new PrintDialog();

                //// Create a PrintDocument to associate with the PrintDialog
                //PrintDocument printDocument = new PrintDocument();

                //// Set the document to print to the PrintDialog
                //printDialog.Document = printDocument;

                //// Show the PrintDialog
                //if (printDialog.ShowDialog() == DialogResult.OK)
                //{
                //    // If the user clicked OK, print the document
                //    printDocument.Print();
                //}
            }


            // Create and configure the PrintDialog
            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowSomePages = true;
            printDialog.AllowSelection = true;
            printDialog.UseEXDialog = true;


            // Show the PrintDialog and wait for the user's selection
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected printer name from the PrintDialog
                string selectedPrinter = printDialog.PrinterSettings.PrinterName;

                // Print the loaded PDF to the selected printer
                GetActivePdfViewer().Print(selectedPrinter);
            }
        }

        

        //private void PrintDocument(PrinterSettings settings)
        //{
        //    // Get the loaded PDF document
        //    PdfLoadedDocument loadedDoc = GetActivePdfViewer().LoadedDocument;

        //    if (loadedDoc != null)
        //    {
        //        // Create a new PrintDocument
        //        PrintDocument printDoc = new PrintDocument();
        //        printDoc.PrinterSettings = settings;
        //        printDoc.PrintPage += (sender, e) =>
        //        {
        //            PdfPageBase page = loadedDoc.Pages[e.PageSettings.PrinterSettings.ToPage - 1];
        //            // Render the PDF page to the graphics of the PrintPage event
        //            PdfGraphics graphics = page.Graphics;
        //            graphics.DrawImage(page.ExtractImages(), new RectangleF(new PointF(0, 0), e.PageBounds.Size));//, new PointF(0, 0), 1f);
        //            e.HasMorePages = e.PageSettings.PrinterSettings.ToPage < loadedDoc.Pages.Count;
        //        };

        //        // Print the document
        //        printDoc.Print();
        //    }
        //}

        //internal void Print()
        //{
        //    var m_activeView = GetActivePdfViewer();
        //    //if (!m_cancel)
        //    {
        //        PrintDialog printDialog = new PrintDialog();
        //        if (GetActivePdfViewer().PrinterSettings.PageOrientation == PdfViewerPrintOrientation.Landscape)
        //        {
        //            printDialog.PrinterSettings.DefaultPageSettings.Landscape = true;
        //        }

        //        int pageCount = m_activeView.LoadedDocument.PageCount;
        //        printDialog.PrinterSettings.Copies = (short)m_activeView.PrinterSettings.Copies;
        //        printDialog.AllowPrintToFile = true;
        //        printDialog.AllowSomePages = true;
        //        printDialog.PrinterSettings.FromPage = 1;
        //        printDialog.PrinterSettings.ToPage = pageCount;
        //        printDialog.PrinterSettings.MaximumPage = pageCount;
        //        printDialog.PrinterSettings.MinimumPage = 1;
        //        if (printDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            if (m_activeView.PrinterSettings.PageOrientation == PdfViewerPrintOrientation.Landscape && !printDialog.PrinterSettings.DefaultPageSettings.Landscape)
        //            {
        //                m_activeView.PrinterSettings.PageOrientation = PdfViewerPrintOrientation.Portrait;
        //            }

        //            if (printDialog.PrinterSettings.DefaultPageSettings.PaperSize.PaperName.ToLower().Contains("rotated"))
        //            {
        //                m_activeView.PrinterSettings.IsPageRotated = true;
        //            }
        //            else
        //            {
        //                m_activeView.PrinterSettings.IsPageRotated = false;
        //            }

        //            m_activeView.m_printFromPage = printDialog.PrinterSettings.FromPage;
        //            m_activeView.m_printToPage = printDialog.PrinterSettings.ToPage;
        //            m_activeView.SetCurrentPagePrint();
        //            PrinterSettings.PaperSizeCollection paperSizes = printDialog.PrinterSettings.PaperSizes;
        //            if (printDialog.PrinterSettings.PaperSources.Count > 0)
        //            {
        //                int rawKind = printDialog.PrinterSettings.PaperSources[0].RawKind;
        //                foreach (PaperSize item in paperSizes)
        //                {
        //                    if (item.RawKind == rawKind)
        //                    {
        //                        m_activeView.m_printHeight = item.Height;
        //                        m_activeView.m_printWidth = item.Width;
        //                    }
        //                }
        //            }

        //            PrintDocument printDocument = m_activeView.PrintDocument;
        //            printDocument.PrinterSettings = printDialog.PrinterSettings;
        //            m_activeView.m_currentPageOnPrint = printDialog.PrinterSettings.FromPage - 1;
        //            printDocument.Print();
        //            printDocument.Dispose();
        //        }
        //        else if (m_activeView != null)
        //        {
        //            PrintDocument printDocument2 = m_activeView.PrintDocument;
        //            if (printDocument2 != null)
        //            {
        //                printDocument2.PrinterSettings = printDialog.PrinterSettings;
        //                m_activeView.m_currentPageOnPrint = printDialog.PrinterSettings.FromPage - 1;
        //                m_activeView.m_printToPage = pageCount;
        //                printDocument2.Dispose();
        //            }
        //        }

        //        printDialog.Dispose();
        //    }

        //    m_cancel = false;
        //}



        #endregion Home

        #region View

        private void ZoomInToolStripButton_Click(object sender, EventArgs e)
        {
            if (GetActivePdfViewer() != null)
            {
                GetActivePdfViewer().ZoomTo(GetActivePdfViewer().ZoomPercentage + 10);
            }
        }

        private void ZoomOutToolStripButton_Click(object sender, EventArgs e)
        {
            if (GetActivePdfViewer() != null)
            {
                GetActivePdfViewer().ZoomTo(GetActivePdfViewer().ZoomPercentage - 10);
            }
        }

        private void ZoomLevelToolStripComboBox_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }


        private void FitToPageToolStripButton_Click(object sender, EventArgs e)
        {
            if (GetActivePdfViewer() != null)
            {
                GetActivePdfViewer().ZoomMode = ZoomMode.FitPage;
            }
        }


        private void FitToWidthToolStripButton_Click(object sender, EventArgs e)
        {
            if (GetActivePdfViewer() != null)
            {
                GetActivePdfViewer().ZoomMode = ZoomMode.FitWidth;
            }
        }


        private void FullScreenToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }




        #endregion View


        #region QRCodes

        private void KannadaAudioTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void KannadaVideoTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void KannadaPDFTsb_Click(object sender, EventArgs e)
        {
            DownloadPDFFile();
        }

        private void DownloadPDFFile()
        {
            string downloadsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads");

            GetActivePdfViewer().LoadedDocument.Save(Path.Combine(downloadsPath, this._loadedDocumentName));

            MessageBox.Show("The Selected Murli File: \"" + this._loadedDocumentName + "\" Downloaded to " + downloadsPath);
        }

        private void KannadaHtmlTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private void HindiAudioTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HindiVideoTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void HindiPDFTsb_Click(object sender, EventArgs e)
        {
            DownloadPDFFile();
        }
        private void HindiHtmlTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private void EnglishAudioTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EnglishVideoTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EnglishPDFTsb_Click(object sender, EventArgs e)
        {
            DownloadPDFFile();
        }

        private void EnglishHtmlTsb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }




        #endregion QRCodes

        public void LoadPdf(string filePath, string fileName)
        {
            // Create a new tab page
            TabPage tabPage = new TabPage();
            tabPage.Text = fileName;

            _loadedDocumentName = fileName;

            // Create PdfViewerControl and load the selected PDF
            PdfDocumentView pdfViewer = new PdfDocumentView();
            pdfViewer.TextSearchSettings.HighlightAllInstance = true;
            pdfViewer.Dock = DockStyle.Fill;
            pdfViewer.Load(filePath);

            // Add PdfViewerControl to the tab page
            tabPage.Controls.Add(pdfViewer);

            // Add the tab page to the tab control
            TabControl.TabPages.Add(tabPage);
        }


        public void LoadPdf(string filePath, string fileName, string murliDate)
        {
            // Create a new tab page
            TabPage tabPage = new TabPage();
            tabPage.Text = fileName;

            _loadedDocumentName = fileName;

            // Create PdfViewerControl and load the selected PDF
            PdfDocumentView pdfViewer = new PdfDocumentView();
            pdfViewer.TextSearchSettings.HighlightAllInstance = false;
            pdfViewer.Dock = DockStyle.Fill;
            pdfViewer.Load(filePath);

            //pdfViewer.SearchText(murliDate);
            pdfViewer.GoToPageAtIndex(74);

            // Add PdfViewerControl to the tab page
            tabPage.Controls.Add(pdfViewer);

            // Add the tab page to the tab control
            TabControl.TabPages.Add(tabPage);
        }

        private PdfDocumentView GetActivePdfViewer()
        {
            if (TabControl.SelectedTab != null && TabControl.SelectedTab.Controls.Count > 0)
            {
                return TabControl.SelectedTab.Controls[0] as PdfDocumentView;
            }
            return null;
        }

        private void PDFShowerControl_Load(object sender, EventArgs e)
        {

        }


    }
}
