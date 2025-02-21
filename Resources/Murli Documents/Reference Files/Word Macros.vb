'1 Removes starting Empty Lines or Empty Pages

Sub RemoveStartingEmptyLines()
    'Removes EmptyLines from every Page.
    'ShortCut Key: Alt+R
    '

    Dim i As Long
    Dim removedEmptyLines As Long
    Dim totalPages As Long
    Dim Msg, Style, Title, Response
    Dim p As Page

    Application.ScreenUpdating = False

    Selection.Collapse wdCollapseEnd
    'To Move cursor to the beggining of the Document
    Selection.HomeKey Unit:=wdStory

    totalPages = ActiveDocument.BuiltInDocumentProperties("Number of Pages")
    i = 1
    removedEmptyLines = 0
    Do Until i > totalPages

        Selection.GoTo wdGoToPage, wdGoToNext
        Selection.Expand wdLine
        Selection.Expand
        If (Len(Trim(Selection.Text)) = 1) Then
            removedEmptyLines = removedEmptyLines + 1
            Selection.TypeBackspace
        End If

        i = i + 1
    Loop

    Application.ScreenUpdating = True

    Msg = "Sucessfully Removed " & removedEmptyLines & " number of Empty lines."    ' Define message.
    Style = vbOKOnly Or vbInformation Or vbDefaultButton1    ' Define buttons.
    Title = "Removed Lines"

    'Ctxt = 1000    ' Define topic context.
    ' Display message.
    Response = MsgBox(Msg, Style, Title)

    Selection.Collapse wdCollapseEnd

End Sub

'2) Prepend 19 to the Year Part. 18-1-79 -> 18-1-1979


Sub Prepend19ToYear()
    Dim doc As Document
    Dim searchRange As Range
    Dim foundRange As Range
    Dim extractedDate As String
    Dim newDate As String
    Dim datePattern As String
    Dim match As Object
    Dim foundCount As Integer
    Dim fullText As String

    ' Initialize variables
    Set doc = ActiveDocument
    Set searchRange = doc.Content ' Start searching from the beginning of the document
    foundCount = 0

    ' Define the date pattern (DD-MM-YY or DD/MM/YY)
    datePattern = "\b([0-3]?\d[-\/][0-1]?\d[-\/](\d{2}))\b"

    ' Start the search for text with specific formatting
    With searchRange.Find
        .ClearFormatting
        .Font.Name = "Noto Sans"         ' Font name
        .Font.Size = 14                  ' Font size
        .Font.Color = wdColorRed         ' Font color: Red
        .Text = ""                       ' Find any text with the formatting
        .Forward = True
        .Wrap = wdFindStop               ' Stop at the end of the document

        Do While .Execute
            ' Increment found count
            foundCount = foundCount + 1

            ' Set the found range
            Set foundRange = searchRange.Duplicate

            ' Get the full text from the found range (which may include other content)
            fullText = foundRange.Text

            ' Debug output: Show the found text (for confirmation)
            Debug.Print "Found text with formatting: " & fullText

            ' Use Regular Expressions to find and extract the date in DD-MM-YY format
            With CreateObject("VBScript.RegExp")
                .Pattern = datePattern
                .Global = False ' Only find the first match

                ' Check if a date is found in the text
                If .Test(fullText) Then
                    Set match = .Execute(fullText)
                    extractedDate = match(0).Value ' Original date (DD-MM-YY)
                    
                    ' Extract day, month, and year components
                    Dim dayPart As String
                    Dim monthPart As String
                    Dim yearPart As String
                    dayPart = Left(extractedDate, 2)
                    monthPart = Mid(extractedDate, 4, 2)
                    yearPart = Right(extractedDate, 2)

                    ' Construct new date in the format "DD-MM-19YY"
                    newDate = dayPart & "-" & monthPart & "-19" & yearPart

                    ' Debug output: Print the extracted date and the new date
                    Debug.Print "Extracted date: " & extractedDate
                    Debug.Print "New date: " & newDate

                    ' Replace the date portion only
                    fullText = Replace(fullText, extractedDate, newDate)

                    ' Replace the found text with the updated text (which includes the replaced date)
                    foundRange.Text = fullText
                Else
                    Debug.Print "No date found in the formatted text."
                End If
            End With

            ' Continue the search to the next occurrence
            searchRange.Start = foundRange.End
            searchRange.End = doc.Content.End

        Loop
    End With

    ' Display a message with the total number of found items
    MsgBox "Total occurrences found and replaced with new dates: " & foundCount, vbInformation
End Sub



' Optionals
 
Sub UpdateDateStyleToAVDate()
    Dim doc As Document
    Dim searchRange As Range
    Dim updatedPages As String
    Dim pageNum As Long
    Dim found As Boolean
    Dim totalPages As Long

    Set doc = ActiveDocument
    updatedPages = ""
    totalPages = doc.ComputeStatistics(wdStatisticPages)
    pageNum = 1
    
    ' Initialize the search range
    Set searchRange = doc.Content
    searchRange.Find.ClearFormatting
    
    ' Set search criteria for Font Size and Color
    With searchRange.Find
        .Font.Size = 14
        .Font.Color = wdColorRed
        .Text = ""
        .Forward = True
        .Wrap = wdFindStop ' Stop after the last occurrence
        .Execute
    End With
    
    ' Loop to search the entire document
    Do While searchRange.Find.found
        ' Check if the current found text is on a new page
        pageNum = searchRange.Information(wdActiveEndPageNumber)
        
        ' Apply the "AV Date" style to the found range
        searchRange.Style = ActiveDocument.Styles("AV Date")
        
        ' Record the page number
        If InStr(updatedPages, CStr(pageNum)) = 0 Then
            updatedPages = updatedPages & pageNum & ", "
        End If
        
        ' Continue searching from the end of the found range
        searchRange.Collapse wdCollapseEnd
        searchRange.Find.Execute
    Loop
    
    ' Display the result at the end
    If Len(updatedPages) > 0 Then
        updatedPages = Left(updatedPages, Len(updatedPages) - 2) ' Remove the trailing comma and space
        MsgBox "Updated Date Style on pages: " & updatedPages
    Else
        MsgBox "No dates found with the specified format."
    End If
End Sub


'3) Add Page Numbers

'4) Create Index Table

Sub CreateIndex()
    Dim doc As Document
    Dim tbl As Table
    Dim dateText As String
    Dim titleText As String
    Dim pageNumber As Long
    Dim rowIndex As Long
    Dim searchRange As Range
    Dim titleFound As Boolean
    Dim dateFound As Boolean

    ' Initialize document and prepare for table creation
    Set doc = ActiveDocument
    Set searchRange = doc.Content
    rowIndex = 0
    
    ' Create a 3-column table at the end of the document
    Set tbl = CreateResultTable(doc)
    
    ' Search for Dates and Titles within the document
    Do
        ' Search for the next formatted date
        dateFound = FindFormattedDate(searchRange, dateText)

        If dateFound Then
            ' Expand search range to the next line to include the title after finding the date
            searchRange.MoveEnd Unit:=wdParagraph, Count:=1 'Expand range by 2 lines (including the title)


            ' Search for the title styled with "AV Murali Heading"
            titleFound = FindStyledTitle(searchRange, titleText)

            ' If both date and title are found, add them to the table
            If titleFound Then
                pageNumber = searchRange.Information(wdActiveEndAdjustedPageNumber)
                AddToTable tbl, rowIndex, dateText, titleText, pageNumber
                rowIndex = rowIndex + 1
            End If

            ' Move the search range forward to continue searching
            searchRange.Collapse Direction:=wdCollapseEnd
            searchRange.MoveStart wdCharacter, 1
        End If

    Loop While dateFound

    'SetRowHeight tbl, 0.5

    SetFontAndSizeForColumn tbl, 1, "Bookman Old Style", 10
    SetFontAndSizeForColumn tbl, 2, "Bookman Old Style", 10
    SetFontAndSizeForColumn tbl, 3, "Noto Sans", 11
    SetFontAndSizeForColumn tbl, 4, "Bookman Old Style", 10


    MsgBox "Extraction complete. Rows added to the table: " & rowIndex, vbInformation
End Sub

' Function to create the result table with specified properties and style
Function CreateResultTable(doc As Document) As Table
    Dim rng As Range
    Set rng = doc.Content
    rng.Collapse Direction:=wdCollapseEnd
    
    ' Create the table with 4 columns (Serial Number, Date, Title, Page Number)
    Set CreateResultTable = doc.Tables.Add(Range:=rng, NumRows:=1, NumColumns:=4)
    
    With CreateResultTable
        ' Set table width and alignment
        .PreferredWidthType = wdPreferredWidthPoints
        .PreferredWidth = CentimetersToPoints(17.46)
        .Rows.Alignment = wdAlignRowCenter
        .AllowPageBreaks = False

        ' Apply the table style (Grid Table 6 Colorful - Accent 3)
        .Style = "Grid Table 6 Colorful - Accent 3"

        ' Set column widths (adjusted to include the Serial Number column)
        .Columns(1).PreferredWidth = CentimetersToPoints(1.36)  ' Serial Number
        .Columns(2).PreferredWidth = CentimetersToPoints(2.44) ' Date
        .Columns(3).PreferredWidth = CentimetersToPoints(12.3)  ' Title
        .Columns(4).PreferredWidth = CentimetersToPoints(1.36) ' Page Number

        ' Set cell vertical alignment
        .Columns(1).Cells.VerticalAlignment = wdCellAlignVerticalCenter
        .Columns(2).Cells.VerticalAlignment = wdCellAlignVerticalCenter
        .Columns(3).Cells.VerticalAlignment = wdCellAlignVerticalCenter
        .Columns(4).Cells.VerticalAlignment = wdCellAlignVerticalCenter

        ' Set text alignment and paragraph formatting in cells
        Dim cell As cell

        ' 1st column (Serial Number) text alignment - Center
        For Each cell In .Columns(1).Cells
            cell.Range.ParagraphFormat.Alignment = wdAlignParagraphCenter
            cell.Range.ParagraphFormat.SpaceBefore = 2
            cell.Range.ParagraphFormat.SpaceAfter = 2
        Next cell

        ' 2nd column text alignment - Center
        For Each cell In .Columns(2).Cells
            cell.Range.ParagraphFormat.Alignment = wdAlignParagraphCenter
            cell.Range.ParagraphFormat.SpaceBefore = 2
            cell.Range.ParagraphFormat.SpaceAfter = 2
        Next cell

        ' 3rd column text alignment - Left (Title)
        For Each cell In .Columns(3).Cells
            cell.Range.ParagraphFormat.Alignment = wdAlignParagraphLeft
            cell.Range.ParagraphFormat.SpaceBefore = 2
            cell.Range.ParagraphFormat.SpaceAfter = 2
        Next cell

        ' 4th column text alignment - Center (Page Number)
        For Each cell In .Columns(4).Cells
            cell.Range.ParagraphFormat.Alignment = wdAlignParagraphCenter
            cell.Range.ParagraphFormat.SpaceBefore = 2
            cell.Range.ParagraphFormat.SpaceAfter = 2
        Next cell

        ' Set table borders (All sides, single solid line, Accent 3, 1pt width)
        .Borders.Enable = True
        .Borders.OutsideLineStyle = wdLineStyleSingle
        .Borders.OutsideColor = RGB(76, 172, 198) ' Accent 3 color
        .Borders.OutsideLineWidth = wdLineWidth075pt
        .Borders.InsideLineStyle = wdLineStyleSingle
        .Borders.InsideColor = RGB(76, 172, 198) ' Accent 3 color
        .Borders.InsideLineWidth = wdLineWidth075pt
    End With
End Function

' Function to find a date with specific formatting
Function FindFormattedDate(searchRange As Range, ByRef dateText As String) As Boolean
    With searchRange.Find
        .ClearFormatting
        .Font.Name = "Noto Sans"
        .Font.Size = 14
        .Font.Color = wdColorRed
        .Text = ""
        .Forward = True
        .Wrap = wdFindStop
        .Execute
    End With
    
    If searchRange.Find.found Then
        dateText = ExtractDate(searchRange)
        FindFormattedDate = True
    Else
        FindFormattedDate = False
    End If
End Function

' Function to find a title using the specified style "AV Murali Heading"
Function FindStyledTitle(searchRange As Range, ByRef titleText As String) As Boolean
    With searchRange.Find
        .ClearFormatting
        .Style = ActiveDocument.Styles("AV Murali Heading")
        .Text = ""
        .Forward = True
        .Wrap = wdFindStop
        .Execute
    End With
    
    If searchRange.Find.found Then
        titleText = searchRange.Text
        'titleText = Replace(titleText, """", "")
        FindStyledTitle = True
    Else
        FindStyledTitle = False
    End If
End Function

' Function to extract the date from the found range using regular expressions
Function ExtractDate(rng As Range) As String
    Dim regex As Object
    Dim matches As Object
    Dim datePattern As String
    
    datePattern = "\b([0-3]?\d[-\/][0-1]?\d[-\/](\d{4}))\b"
    Set regex = CreateObject("VBScript.RegExp")
    regex.Pattern = datePattern
    regex.Global = False
    
    If regex.Test(rng.Text) Then
        Set matches = regex.Execute(rng.Text)
        ExtractDate = matches(0).Value
    Else
        ExtractDate = ""
    End If
End Function

' Subroutine to add the extracted date, title, and page number to the table
Sub AddToTable(tbl As Table, row As Long, dateText As String, titleText As String, pageNum As Long)

    tbl.Rows.Add

    ' Add serial number in the 1st column
    tbl.cell(row + 1, 1).Range.Text = CStr(row + 1)

    ' Add date in the 2nd column
    tbl.cell(row + 1, 2).Range.Text = dateText

    ' Add title in the 3rd column
    tbl.cell(row + 1, 3).Range.Text = titleText

    ' Add page number in the 4th column
    tbl.cell(row + 1, 4).Range.Text = CStr(pageNum)
End Sub




Sub SetRowHeight(tbl As Table, rowHeight As Single)
    ' Set the row height for a specific row in points
    For Each row In tbl.Rows
        With row
            .Height = rowHeight                  ' Set the height (in points)
            .HeightRule = wdRowHeightExactly      ' Specify that the height should be exact
        End With
    Next row
End Sub

Sub SetFontAndSizeForColumn(tbl As Table, columnIndex As Long, fontName As String, fontSize As Long)
    Dim cell As cell

    ' Loop through each cell in the specified column
    For Each cell In tbl.Columns(columnIndex).Cells
        ' Set font name and size for each cell in the column
        With cell.Range.Font
            .Name = fontName    ' Set font name
            .Size = fontSize    ' Set font size
        End With
    Next cell
End Sub




'5) Manually Remove Paragraph mark in the Table.
 
 
'6) Move table to the Beginning

Sub MoveTableToTheBegining()
    Dim doc As Document
    Dim tbl As Table
    Dim rng As Range

    ' Set the current document
    Set doc = ActiveDocument

    ' Check if the document has tables
    If doc.Tables.Count = 0 Then
        MsgBox "No tables found in this document.", vbExclamation
        Exit Sub
    End If

    ' Set the range of the last table in the document (assuming it's the last one)
    Set tbl = doc.Tables(doc.Tables.Count)

    ' Cut the table from its current location
    tbl.Range.Cut

    ' Move the cursor to the beginning of the document
    Set rng = doc.Range(0, 0)

    ' Insert two blank lines before pasting the table
    rng.InsertBefore vbCrLf & vbCrLf

    ' Move the range to the first blank line to insert text and shading
    rng.Start = 0
    rng.End = 2

' Apply the "Normal" style to the blank lines before the table
    rng.Style = ActiveDocument.Styles(wdStyleNormal)

rng.Start = 0
    rng.End = 1
     
    ' Apply the shading color (RGB: 255, 235, 204)
    rng.Shading.BackgroundPatternColor = RGB(255, 235, 204)

    ' Insert the specified text on the first line
    'rng.Text = "????? ????"

    ' Apply the font settings (Noto Sans, Size 18)
    rng.Font.Name = "Noto Sans Devanagari"
    rng.Font.Size = 18

    ' Collapse the range to the end of the inserted text
    rng.Collapse Direction:=wdCollapseEnd

    ' Apply the "Normal" style to the blank line after the inserted text
    rng.InsertAfter vbCrLf ' Insert second blank line after the text
    

    rng.Start = 2

    ' Collapse to the end of the blank lines before the table
    rng.Collapse Direction:=wdCollapseEnd

    ' Paste the table after the blank lines
    rng.Paste

    rng.Font.Name = "Noto Sans Devanagari"
    rng.Font.Size = 12
    
    ' Move the range to the end of the newly pasted table
    rng.Collapse Direction:=wdCollapseEnd

    ' Insert three blank lines after the table
    rng.InsertAfter vbCrLf & vbCrLf & vbCrLf

    ' Set the range to the blank lines after the table
    rng.Start = rng.End - 3 ' Cover the inserted blank lines

    ' Apply the "Normal" style to the blank lines after the table
    rng.Style = ActiveDocument.Styles(wdStyleNormal)

    ' Collapse the range to the end of the blank lines
    rng.Collapse Direction:=wdCollapseEnd

    ' Insert a section break after the blank lines
    rng.InsertBreak Type:=wdSectionBreakNextPage

    MsgBox "Table moved, text added with shading, blank lines inserted, Normal style applied, and section break added.", vbInformation
End Sub

'7) Table Properties:


Sub ModifyIndexTableProperties()
    Dim tbl As Table
    Dim row As row
    Dim col As Column
    Dim lineCount As Integer
    
    ' Check if there are tables in the document
    If ActiveDocument.Tables.Count > 0 Then
        ' Get the first table
        Set tbl = ActiveDocument.Tables(1)
        
        ' Set column widths
        tbl.Columns(1).Width = CentimetersToPoints(1)
        tbl.Columns(2).Width = CentimetersToPoints(2.5)
        tbl.Columns(3).Width = CentimetersToPoints(12.4)
        tbl.Columns(4).Width = CentimetersToPoints(1.2)
        
        ' Set fonts for 1st, 2nd, and 4th columns
        SetFontForColumn tbl, 1, "Bookman Old Style", 10
        SetFontForColumn tbl, 2, "Bookman Old Style", 10
        SetFontForColumn tbl, 4, "Bookman Old Style", 10
        
        ' Set font for the 3rd column
        SetFontForColumn tbl, 3, "Noto Sans Devanagari", 11
        
        ' Set row height to 0.6 cm if the row has only one line
        For Each row In tbl.Rows
            ' Count the number of paragraphs (lines) in the 3rd column of the row
            lineCount = row.Cells(3).Range.Paragraphs.Count
            
            ' Set the row height based on the number of lines in the row
            If lineCount > 0 Then
                row.HeightRule = wdRowHeightExactly
                row.Height = CentimetersToPoints(lineCount * 0.6)
            End If
        Next row
        
    End If
	
	MsgBox "Index Table Properties have been Modified successfully.", vbInformation
End Sub

Sub SetFontForColumn(tbl As Table, colIndex As Integer, fontName As String, fontSize As Integer)
    Dim cell As Cell
    For Each row In tbl.Rows
        Set cell = row.Cells(colIndex)
        cell.Range.Font.Name = fontName
        cell.Range.Font.Size = fontSize
    Next row
End Sub


'8) Set Margin to Moderate:

Sub SetMarginsToModerateInCm()
    Dim doc As Document

    ' Reference the active document
    Set doc = ActiveDocument

    ' Set margins to Moderate in centimeters
    ' Top: 2.54 cm, Bottom: 2.54 cm, Left: 1.91 cm, Right: 1.91 cm
    With doc.PageSetup
        .TopMargin = CentimetersToPoints(2.54)
        .BottomMargin = CentimetersToPoints(2.54)
        .LeftMargin = CentimetersToPoints(1.91)
        .RightMargin = CentimetersToPoints(1.91)
    End With

    MsgBox "Margins set to Moderate successfully (in cm).", vbInformation
End Sub


'9) Page Borders

Sub AddPageBorderToDocument()
    Dim doc As Document
    Dim border As border

    ' Reference the active document
    Set doc = ActiveDocument

	for each Sec in doc.Sections
		With Sec.Borders
			' Enable all borders
			'.Enable = True
			
			' Set distance from the page edge
			.DistanceFrom = wdBorderDistanceFromPageEdge
			.DistanceFromTop = 24 ' Adjust in points
			.DistanceFromBottom = 24
			.DistanceFromLeft = 24
			.DistanceFromRight = 24
	
			With .Item(wdBorderTop)
				.LineStyle = wdLineStyleThinThickSmallGap
				.LineWidth = wdLineWidth300pt
				.Color = RGB(191, 191, 191)
			End With
			With .Item(wdBorderBottom)
				.LineStyle = wdLineStyleThinThickSmallGap
				.LineWidth = wdLineWidth300pt
				.Color = RGB(191, 191, 191)
			End With
			With .Item(wdBorderLeft)
				.LineStyle = wdLineStyleThinThickSmallGap
				.LineWidth = wdLineWidth300pt
				.Color = RGB(191, 191, 191)
			End With
			With .Item(wdBorderRight)
				.LineStyle = wdLineStyleThinThickSmallGap
				.LineWidth = wdLineWidth300pt
				.Color = RGB(191, 191, 191)
			End With
		End With
	Next Sec
    
	MsgBox "Page border added to the entire document.", vbInformation
End Sub


'10) Create Hyperlinks for easier Navigation to specific page.

Sub CreateHyperlinks()
    Dim doc As Document
    Dim tbl As Table
    Dim searchRange As Range
    Dim foundRange As Range
    Dim title As String
    Dim sanitizedTitle As String
    Dim i As Long
    Dim pageNum As Long
    Dim tblEnd As Range
    Dim uniqueID As Long ' To ensure unique bookmark names
    
    ' Set reference to the active document
    Set doc = ActiveDocument
    
    ' Assuming the table is the first one in the document
    Set tbl = doc.Tables(1)
    
    ' Set the search range to start after the table (to avoid self-linking)
    Set tblEnd = tbl.Range
    Set searchRange = doc.Range(Start:=tblEnd.End, End:=doc.Content.End) ' Start searching after the table

    ' Initialize the unique identifier
    uniqueID = 1

    ' Iterate over each row in the table (starting from the first row)
    For i = 1 To tbl.Rows.Count
        ' Get the title from the 2nd column of the table
        title = Trim(tbl.cell(i, 3).Range.Text)
        title = Replace(title, Chr(13) & Chr(7), "") ' Clean up cell text, removing end-of-cell markers

        If Len(title) > 0 Then
            ' Sanitize the title to create a valid bookmark name
            sanitizedTitle = "Title_" & uniqueID  ' Append the unique identifier
            
            ' Reset search range to exclude the table
            Set searchRange = doc.Range(Start:=tblEnd.End, End:=doc.Content.End)
            
            ' Find the title in the document, excluding the table
            With searchRange.Find
                .ClearFormatting
				.Style = ActiveDocument.Styles("AV Murali Heading")
                .Text = title
                .Forward = True
                .Wrap = wdFindStop
                .Execute
            End With

            ' If the title is found in the document
            If searchRange.Find.found Then
                ' Set found range for hyperlink creation
                Set foundRange = searchRange.Duplicate

                ' Add a bookmark around the found title for hyperlink
                doc.Bookmarks.Add Name:=sanitizedTitle, Range:=foundRange

                ' Create hyperlink for the title in the table
                tbl.cell(i, 3).Range.Hyperlinks.Add Anchor:=tbl.cell(i, 3).Range, Address:="", SubAddress:=sanitizedTitle, TextToDisplay:=title

                ' Get the page number of the found title based on its physical position
                pageNum = foundRange.Information(wdActiveEndAdjustedPageNumber)

                ' Update the page number in the 3rd column of the table
                tbl.cell(i, 4).Range.Text = pageNum
            Else
                ' If the title is not found, update the 3rd column with "Not Found"
                tbl.cell(i, 4).Range.Text = "Not Found"
            End If

            ' Increment the unique identifier for the next bookmark
            uniqueID = uniqueID + 1
        End If
    Next i

    MsgBox "Hyperlinks created and page numbers updated.", vbInformation
End Sub



'11)
Sub AddCustomProperty_InitialPageNumber()
    Dim doc As Document
    Dim tbl As Table
    Dim tblEndRange As Range
    Dim tblEndPageNumber As Long
    Dim docProp As DocumentProperty
    
    ' Reference the active document
    Set doc = ActiveDocument
    
    ' Check if there are any tables in the document
    If doc.Tables.Count = 0 Then
        MsgBox "No tables found in the document.", vbExclamation
        Exit Sub
    End If
    
    ' Get the first table in the document
    Set tbl = doc.Tables(1)
    
    ' Set a range object to the end of the table
    Set tblEndRange = tbl.Range.Duplicate
    tblEndRange.Collapse Direction:=wdCollapseEnd
    
    ' Determine the page number at which the table ends
    tblEndPageNumber = tblEndRange.Information(wdActiveEndAdjustedPageNumber)
    
    ' Check if the custom property already exists
    On Error Resume Next
    Set docProp = doc.CustomDocumentProperties("Initial Page Number")
    On Error GoTo 0
    
    If Not docProp Is Nothing Then
        ' Update the existing property
        doc.CustomDocumentProperties("Initial Page Number").Value = tblEndPageNumber
    Else
        ' Add a new custom property
        doc.CustomDocumentProperties.Add Name:="Initial Page Number", _
            LinkToContent:=False, Type:=msoPropertyTypeNumber, Value:=tblEndPageNumber
    End If
    
    MsgBox "Custom property 'Initial Page Number' set to: " & tblEndPageNumber, vbInformation
End Sub
