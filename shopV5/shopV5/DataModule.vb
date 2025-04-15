Imports System.Data
Imports System.IO
Imports OfficeOpenXml

Public Module DataModule
    Public ReadOnly Property StockTable As New DataTable
    Public ReadOnly Property SalesTable As New DataTable
    Public ReadOnly Property LogTable As New DataTable

    Public ReadOnly Property ExcelPath As String = Path.Combine(Application.StartupPath, "InventoryData.xlsx")

    Public Sub Initialize()
        SetupTables()
        LoadFromExcel()
    End Sub

    Private Sub SetupTables()
        ' Stock Table
        With StockTable
            .Columns.Add("Barcode", GetType(String))
            .Columns.Add("ProductName", GetType(String))
            .Columns.Add("Price", GetType(Decimal))
            .Columns.Add("Quantity", GetType(Integer))
            .PrimaryKey = { .Columns("Barcode")}
        End With

        ' Sales Table
        With SalesTable
            .Columns.Add("TransactionID", GetType(Guid))
            .Columns.Add("Timestamp", GetType(DateTime))
            .Columns.Add("Barcode", GetType(String))
            .Columns.Add("Quantity", GetType(Integer))
            .Columns.Add("TotalPrice", GetType(Decimal))
        End With

        ' Log Table
        With LogTable
            .Columns.Add("LogID", GetType(Integer))
            .Columns.Add("Timestamp", GetType(DateTime))
            .Columns.Add("User", GetType(String))
            .Columns.Add("Action", GetType(String))
            .Columns.Add("Details", GetType(String))
        End With
    End Sub

    Public Sub SaveToExcel()
        Try
            Using pkg As New ExcelPackage(New FileInfo(ExcelPath))
                CreateWorksheet(pkg, StockTable, "Stock")
                CreateWorksheet(pkg, SalesTable, "Sales")
                CreateWorksheet(pkg, LogTable, "Logs")
                pkg.Save()
            End Using
        Catch ex As Exception
            LogError("Excel Save Failed", ex)
        End Try
    End Sub

    Public Sub LoadFromExcel()
        If Not File.Exists(ExcelPath) Then Return

        Try
            Using pkg As New ExcelPackage(New FileInfo(ExcelPath))
                LoadWorksheet(pkg, StockTable, "Stock")
                LoadWorksheet(pkg, SalesTable, "Sales")
                LoadWorksheet(pkg, LogTable, "Logs")
            End Using
        Catch ex As Exception
            LogError("Excel Load Failed", ex)
        End Try
    End Sub

    Private Sub CreateWorksheet(pkg As ExcelPackage, dt As DataTable, name As String)
        Dim ws = pkg.Workbook.Worksheets.Add(name)
        ws.Cells(1, 1).LoadFromDataTable(dt, True)
        ws.Cells.AutoFitColumns()
    End Sub

    Private Sub LoadWorksheet(pkg As ExcelPackage, dt As DataTable, name As String)
        Dim ws = pkg.Workbook.Worksheets(name)
        If ws Is Nothing Then Return

        dt.Clear()
        For row As Integer = 2 To ws.Dimension.End.Row
            Dim newRow = dt.NewRow()
            For col As Integer = 1 To ws.Dimension.End.Column
                newRow(col - 1) = ws.Cells(row, col).Value
            Next
            dt.Rows.Add(newRow)
        Next
    End Sub

    Public Sub LogAction(user As String, action As String, details As String)
        LogTable.Rows.Add(LogTable.Rows.Count + 1, DateTime.Now, user, action, details)
        SaveToExcel()
    End Sub

    Public Sub LogError(context As String, ex As Exception)
        LogAction("SYSTEM", $"ERROR: {context}", $"{ex.Message} | {ex.StackTrace}")
    End Sub
End Module