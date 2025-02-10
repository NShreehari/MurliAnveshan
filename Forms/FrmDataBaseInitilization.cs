using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Anotar.NLog;

using Microsoft.WindowsAPICodePack.Dialogs;

using MurliSearch.Classes;

using Syncfusion.DocIO.DLS;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MurliAnveshan.Forms
{
    public partial class FrmDataBaseInitilization : Form
    {
        #region Private Fields

        private string folderPath;

        #endregion Private Fields

        #region Public Constructors

        public FrmDataBaseInitilization()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        // Function to get the visible text, excluding hyperlink fields metadata
        static string GetVisibleMurliTitle(WTableCell cell)
        {
            string result = string.Empty;

            // Loop through each paragraph in the cell
            foreach (WParagraph paragraph in cell.Paragraphs)
            {
                // Loop through each text body item (run/field) in the paragraph
                foreach (Entity entity in paragraph.ChildEntities)
                {
                    if (entity is WTextRange textRange)
                    {
                        // Append the visible text to the result
                        result = textRange.Text;
                        break;
                    }
                }
            }

            return result.Trim();
        }

        private void RemoveQuotationMarks()
        {
            const byte murliTitleCellNumber = 3;
            char[] charsToTrim = { '"', '“', '”' };

            if (!Directory.Exists(folderPath)) return;

            var dirFile = new DirectoryInfo(folderPath);

            foreach (var file in dirFile.GetFiles("*.doc*"))
            {
                try
                {
                    using (WordDocument document = new WordDocument(file.FullName))
                    {
                        string fileName = file.Name;
                        string murliDate = string.Empty;
                        string murliTitle = string.Empty;

                        foreach (IWSection section in document.Sections)
                        {
                            foreach (WTable table in section.Tables)
                            {
                                foreach (WTableRow row in table.Rows)
                                {
                                    byte cellNumber = 0;

                                    foreach (WTableCell cell in row.Cells)
                                    {
                                        cellNumber++;

                                        switch (cellNumber)
                                        {
                                            case murliTitleCellNumber:

                                                if (IsHyperlinked(cell))
                                                {
                                                    murliTitle = GetVisibleMurliTitle(cell);
                                                }
                                                else
                                                {
                                                    murliTitle = cell.Paragraphs[0].Text;
                                                }

                                                string trimmedMurliTitle = murliTitle.Trim(charsToTrim);

                                                // Get formatting of the first text entity
                                                IWParagraph originalParagraph = cell.Paragraphs[0];
                                                WTextRange originalTextRange = originalParagraph.ChildEntities.OfType<WTextRange>().FirstOrDefault();

                                                // Backup original formatting
                                                string fontName = originalTextRange?.CharacterFormat.FontName ?? "Calibri"; // Default to Calibri
                                                float fontSize = originalTextRange?.CharacterFormat.FontSize ?? 11f;       // Default to 11
                                                Color fontColor = originalTextRange?.CharacterFormat.TextColor ?? Color.Black;

                                                // Clear the existing content of the cell
                                                cell.ChildEntities.Clear();

                                                // Add the new content (trimmedMurliTitle) to the cell
                                                IWParagraph newParagraph = cell.AddParagraph();
                                                WTextRange newTextRange = newParagraph.AppendText(trimmedMurliTitle) as WTextRange;

                                                // Apply original formatting to the new text
                                                newTextRange.CharacterFormat.FontName = fontName;
                                                newTextRange.CharacterFormat.FontSize = fontSize;
                                                newTextRange.CharacterFormat.TextColor = fontColor;

                                                break;
                                            default:
                                                continue;
                                        }
                                    }
                                }
                            }
                        }

                        string fileNameWithoutExtenstion = fileName.Split('.')[0];
                        string modifiedFileName = fileNameWithoutExtenstion + " removedQt" + file.Extension;

                        string outputFilePath = Path.Combine("C:\\Users\\Shreehari\\Desktop\\AV Murlis\\Removed\\", modifiedFileName); // Set your output directory
                        document.Save(outputFilePath);
                    }
                }
                catch (Exception ex)
                {
                    LogTo.Error(ex.Message, ex);
                }
            }
        }

        private static bool IsHyperlinked(WTableCell cell)
        {
            return cell.Paragraphs[0].ChildEntities[0] is WField field && field.FieldType == Syncfusion.DocIO.FieldType.FieldHyperlink;
        }

        List<string> murliIndex = new List<string>();

        private void BtnExtract_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(folderPath)) return;

            //DataTable murliTitlesTable, filePagesTable;
            InitializeDataTables(out DataTable fileTable, out DataTable murliTitlesTable, out DataTable filePagesTable);

            var dirFile = new DirectoryInfo(folderPath);

            foreach (var file in dirFile.GetFiles("*.doc*"))
            {
                try
                {
                    ProcessFile(file, fileTable, murliTitlesTable, filePagesTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {file.Name}: {ex.Message}");
                }
            }

            //UnComment CreateIndexedDoc() to Create a Index Word Doc.
            //CreateIndexedDoc(murliIndex);

            InsertDataToDatabase(fileTable, murliTitlesTable, filePagesTable);

            Messages.InfoMessage("Successfully Added Details to DataBase.");
        }

        private void ProcessFile(FileInfo file, DataTable fileTable, DataTable murliTitlesTable, DataTable filePagesTable)
        {
            using (WordDocument document = new WordDocument(file.FullName))
            {
                int initialPageNumber = GetInitialPageNumber(document);

                string fileName = file.Name;
                string murliDate = string.Empty;
                string murliTitle = string.Empty;
                int pageNumber = 0;

                foreach (IWSection section in document.Sections)
                {
                    foreach (WTable table in section.Tables)
                    {
                        foreach (WTableRow row in table.Rows)
                        {
                            ProcessRow(row, ref murliDate, ref murliTitle, ref pageNumber);

                            //UnComment AddDetailsToIndexList() to Create a Index Word Doc.
                            AddDetailsToIndexList(murliIndex, murliDate, murliTitle, pageNumber);

                            AddDateAndTitlestoDataTable(murliTitlesTable, murliDate, murliTitle);
                            AddPageNumberToDataTable(filePagesTable, fileName, murliTitle, murliDate, initialPageNumber + pageNumber);
                        }
                    }
                }

                AddFileNameToDataTable(fileTable, fileName);
            }
        }

        private void ProcessRow(WTableRow row, ref string murliDate, ref string murliTitle, ref int pageNumber)
        {
            const byte murliDateCellNumber = 2;
            const byte murliTitleCellNumber = 3;
            const byte pageNumberCellNumber = 4;

            byte cellNumber = 0;

            foreach (WTableCell cell in row.Cells)
            {
                cellNumber++;

                switch (cellNumber)
                {
                    case murliDateCellNumber:
                        murliDate = cell.Paragraphs[0].Text;
                        break;
                    case murliTitleCellNumber:
                        murliTitle = GetVisibleMurliTitle(cell);
                        break;
                    case pageNumberCellNumber:
                        pageNumber = Convert.ToInt32(cell.Paragraphs[0].Text);
                        break;
                    default:
                        continue;
                }
            }
        }


        //IndexTable Document Creation Methods

        private static void AddDetailsToIndexList(List<string> murliIndex, string murliDate, string murliTitle, int pageNumber)
        {
            var indexRow = string.Format(murliDate + "\t" + murliTitle + "\t" + pageNumber);

            murliIndex.Add(indexRow);

            indexRow = string.Empty;
        }

        private void CreateIndexedDoc(List<string> murliIndex)
        {
            WordDocument doc = CreateAWordDocument();

            IWTable table = CreateTable(doc, murliIndex);

            AddDataToTable(table, murliIndex);

            // Step 7: Save the document
            string fileName = "IndexTable.docx";
            doc.Save(fileName);

            Console.WriteLine($"Document saved successfully as {fileName}");
        }

        private static WordDocument CreateAWordDocument()
        {
            WordDocument document;

            // Step 1: Create a new Word document
            using (document = new WordDocument())
            {
                // Step 2: Add a section to the document


                // Step 3: Add a paragraph to the section
                //IWParagraph paragraph = section.AddParagraph();
                //paragraph.AppendText("3-Column Table Example").ApplyCharacterFormat(new WCharacterFormat()
                //{
                //    Bold = true,
                //    FontSize = 16
                //});

                // Add some spacing after the title
                //paragraph.ParagraphFormat.AfterSpacing = 10;



            }

            return document;
        }

        private static IWTable CreateTable(WordDocument document, List<string> murliIndex)
        {
            IWSection section = document.AddSection();
            // Step 4: Add a table to the section
            IWTable table = section.AddTable();

            // Step 5: Initialize the table with rows and columns
            table.ResetCells(murliIndex.Count + 2, 4);

            // Optional: Format the table (e.g., add borders)
            table.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            table.TableFormat.Borders.LineWidth = 1.0F;

            return table;
        }

        private static void AddDataToTable(IWTable table, List<string> murliIndex)
        {
            // Step 6: Add data to the table cells
            for (int row = 0; row < murliIndex.Count; row++)
            {
                string[] currentRow = murliIndex[row].Split('\t');

                // Add serial number to the first column
                IWParagraph serialNumberParagraph = table.Rows[row].Cells[0].AddParagraph();
                serialNumberParagraph.AppendText((row + 1).ToString()); // Serial number starts from 1

                // Optional: Center align the text in the cell
                serialNumberParagraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                for (int col = 1; col < 4; col++)
                {
                    IWParagraph cellParagraph = table.Rows[row].Cells[col].AddParagraph();
                    cellParagraph.AppendText(currentRow[col - 1]);

                    // Optional: Center align the text in the cell
                    cellParagraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                }
            }
        }
        //IndexTable Document Creation Methods

        private int GetInitialPageNumber(WordDocument document)
        {
            CustomDocumentProperties customProperties = document.CustomDocumentProperties;

            string propertyName = "Initial Page Number";
            int propertyValue = 0;

            for (int i = 0; i < customProperties.Count; i++)
            {
                if (customProperties[i].Name == propertyName)
                {
                    propertyValue = (int)customProperties[i].Value;
                    break;
                }
            }

            return propertyValue;
        }


        //    //using (WordDocument document = new WordDocument("Sample.doc"))
        //    //{
        //    //    // Initialize the DocIORenderer to get page numbers
        //    //    using (DocIORenderer renderer = new DocIORenderer())
        //    //    {
        //    //        // Render the document
        //    //        using (Syncfusion.Pdf.PdfDocument pdfDocument = renderer.ConvertToPDF(document))
        //    //        {
        //    //            // Iterate through the sections
        //    //            foreach (WSection section in document.Sections)
        //    //            {
        //    //                // Iterate through paragraphs in the section
        //    //                foreach (WParagraph paragraph in section.Body.Paragraphs)
        //    //                {
        //    //                    // Check for PageBreak in paragraph's child entities
        //    //                    foreach (EntityBase entity in paragraph.ChildEntities)
        //    //                    {
        //    //                        if (entity is Break pageBreak && pageBreak.BreakType == BreakType.PageBreak)
        //    //                        {
        //    //                            // Get the page number of the paragraph containing the page break
        //    //                            int pageNumber = renderer.GetPageNumber(paragraph);
        //    //                            Console.WriteLine($"First Page Break found at Page Number: {pageNumber}");
        //    //                            return; // Exit after finding the first page break
        //    //                        }
        //    //                    }
        //    //                }
        //    //            }

        //    //            Console.WriteLine("No Page Break found in the document.");
        //    //        }
        //    //    }
        //    //}

        //}

        private static void AddFileNameToDataTable(DataTable fileTable, string fileName)
        {
            // Add fileName to the TblFiles table
            DataRow fileRow = fileTable.NewRow();
            fileRow["FileName"] = fileName;
            fileTable.Rows.Add(fileRow);
        }

        private static void AddPageNumberToDataTable(DataTable filePagesTable, string fileName, string murliTitle, string murliDate, int pageNumber)
        {
            // Leave the FileID and MurliTitleID for now
            DataRow filePagesRow = filePagesTable.NewRow();
            filePagesRow["PageNo"] = pageNumber;
            filePagesRow["fileName"] = fileName;
            filePagesRow["murliTitle"] = murliTitle;
            filePagesRow["murliDate"] = murliDate;
            filePagesTable.Rows.Add(filePagesRow);
        }

        private static void AddDateAndTitlestoDataTable(DataTable murliTitlesTable, string murliDate, string murliTitle)
        {
            // Add MurliDate and MurliTitle to TblMurliTitles table
            DataRow murliRow = murliTitlesTable.NewRow();
            murliRow["MurliDate"] = murliDate;
            murliRow["MurliTitle"] = murliTitle;
            murliTitlesTable.Rows.Add(murliRow);
        }

        private static void InitializeDataTables(out DataTable fileTable, out DataTable murliTitlesTable, out DataTable filePagesTable)
        {
            fileTable = CreateFileTable();
            murliTitlesTable = CreateMurliTitlesTable();
            filePagesTable = CreateFilePagesTable();
        }

        private static DataTable CreateFileTable()
        {
            var fileTable = new DataTable();
            fileTable.Columns.Add("FileName", typeof(string));
            return fileTable;
        }

        private static DataTable CreateMurliTitlesTable()
        {
            var murliTitlesTable = new DataTable();
            murliTitlesTable.Columns.Add("MurliDate", typeof(string));
            murliTitlesTable.Columns.Add("MurliTitle", typeof(string));
            return murliTitlesTable;
        }

        private static DataTable CreateFilePagesTable()
        {
            var filePagesTable = new DataTable();
            filePagesTable.Columns.Add("FileID", typeof(int));
            filePagesTable.Columns.Add("MurliTitleID", typeof(int));
            filePagesTable.Columns.Add("PageNo", typeof(int));
            filePagesTable.Columns.Add("fileName", typeof(string));
            filePagesTable.Columns.Add("murliTitle", typeof(string));
            filePagesTable.Columns.Add("murliDate", typeof(string));
            return filePagesTable;
        }

        private static void InsertDataToDatabase(DataTable fileTable, DataTable murliTitlesTable, DataTable filePagesTable)
        {
            using (var connection = DBOperations.GetSqlConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    // 1. Create a dictionary to store FileName -> FileID mappings
                    Dictionary<string, int> fileIDMap = InsertFiles(fileTable, connection, transaction);

                    // 3. Insert into TblMurliTitles and store results for use in TblFilePages
                    DataTable murliTitlesMappingTable = InsertMurliTitles(murliTitlesTable, connection, transaction);

                    // 4. Update filePagesTable with correct FileID and MurliTitleID
                    UpdateFilePagesTable(filePagesTable, fileIDMap, murliTitlesMappingTable);

                    // 5. Bulk insert into TblFilePages
                    BulkInsertFilePages(filePagesTable, connection, transaction);

                    transaction.Commit();

                    Messages.InfoMessage("Successfully Inserted Data to DB.");
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of error
                    transaction.Rollback();
                    Console.WriteLine("Transaction rolled back due to error: " + ex.Message);
                    throw;
                }
            }
        }

        private static DataTable InsertMurliTitles(DataTable murliTitlesTable, SqlConnection connection, SqlTransaction transaction)
        {
            DataTable murliTitlesMappingTable = new DataTable();
            murliTitlesMappingTable.Columns.Add("MurliTitle", typeof(string));
            murliTitlesMappingTable.Columns.Add("MurliDate", typeof(string)); // Keep date as string in dd-MM-yyyy format
            murliTitlesMappingTable.Columns.Add("MurliTitleID", typeof(int));

            foreach (DataRow murliRow in murliTitlesTable.Rows)
            {
                const string insertMurliSql = "INSERT INTO TblMurliTitles (MurliDate, MurliTitle) OUTPUT INSERTED.MurliTitleID VALUES (@MurliDate, @MurliTitle)";
                using (SqlCommand cmd = new SqlCommand(insertMurliSql, connection, transaction))
                {
                    DateTime murliDate;
                    bool isValidDate = DateTime.TryParseExact(murliRow["MurliDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out murliDate);

                    if (isValidDate)
                    {
                        cmd.Parameters.AddWithValue("@MurliDate", murliDate);
                        Console.WriteLine("@MurliDate", murliDate);
                    }
                    else
                    {
                        // TODO: Log invalid date and skip this row if necessary
                        Console.WriteLine("Invalid date format: " + murliRow["MurliDate"]);
                        continue; // Skip to next row if date is invalid
                    }
                    cmd.Parameters.AddWithValue("@MurliTitle", murliRow["MurliTitle"]);
                    int murliTitleID = (int)cmd.ExecuteScalar();

                    // Update the mapping table with the inserted ID
                    murliTitlesMappingTable.Rows.Add(murliRow["MurliTitle"], murliDate, murliTitleID);
                }
            }

            return murliTitlesMappingTable;
        }

        private static Dictionary<string, int> InsertFiles(DataTable fileTable, SqlConnection connection, SqlTransaction transaction)
        {
            var fileIDMap = new Dictionary<string, int>();

            foreach (DataRow fileRow in fileTable.Rows)
            {
                const string insertFileSql = "INSERT INTO TblFiles (FileName) OUTPUT INSERTED.FileID VALUES (@FileName)";
                using (var cmd = new SqlCommand(insertFileSql, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@FileName", fileRow["FileName"]);
                    int fileID = (int)cmd.ExecuteScalar();

                    // Store FileID in the dictionary
                    fileIDMap.Add(fileRow["FileName"].ToString(), fileID);
                }
            }

            return fileIDMap;
        }

        private static void BulkInsertFilePages(DataTable filePagesTable, SqlConnection connection, SqlTransaction transaction)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
            {
                bulkCopy.DestinationTableName = "TblFilePages";
                bulkCopy.ColumnMappings.Add("FileID", "FileID");
                bulkCopy.ColumnMappings.Add("MurliTitleID", "MurliTitleID");
                bulkCopy.ColumnMappings.Add("PageNo", "PageNo");
                bulkCopy.WriteToServer(filePagesTable);
            }
        }

        private static void UpdateFilePagesTable(DataTable filePagesTable, Dictionary<string, int> fileIDMap, DataTable murliTitlesMappingTable)
        {
            foreach (DataRow pageRow in filePagesTable.Rows)
            {
                string fileName = pageRow["FileName"].ToString();
                string murliTitle = pageRow["MurliTitle"].ToString();
                string murliDate = pageRow["MurliDate"].ToString(); // Use string for date comparison

                // Lookup FileID
                pageRow["FileID"] = fileIDMap[fileName];

                // Lookup MurliTitleID from mapping table
                DataRow[] matchingRows = murliTitlesMappingTable.Select($"MurliTitle = '{murliTitle.Replace("'", "''")}' AND MurliDate LIKE '{murliDate}*'");

                if (matchingRows.Length > 0)
                {
                    pageRow["MurliTitleID"] = matchingRows[0]["MurliTitleID"];
                }
            }
        }


        private void BtnSelect_Click(object sender, EventArgs e)
        {
            SelectFolder();
        }

        private void SelectFolder()
        {
            CommonOpenFileDialog folderBrowserDialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,

                EnsurePathExists = true,
                Title = "Pick Murli Folder to Load."
            };

            CommonFileDialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == CommonFileDialogResult.Ok)
            {
                txtFile.Text = folderBrowserDialog.FileName;
                folderPath = folderBrowserDialog.FileName;
            }
        }

        #endregion Private Methods

        private void BtnRemoveQutotations_Click(object sender, EventArgs e)
        {
            RemoveQuotationMarks();
        }
    }
}
