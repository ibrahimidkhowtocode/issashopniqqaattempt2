Public Class AdminForm
    Private Sub AdminForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupDataGrids()
        RefreshData()
    End Sub

    Private Sub SetupDataGrids()
        dgvStock.DataSource = DataModule.StockTable
        dgvSales.DataSource = DataModule.SalesTable
        dgvLogs.DataSource = DataModule.LogTable
    End Sub

    Private Sub RefreshData()
        DataModule.LoadFromExcel()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            DataModule.SaveToExcel()
            DataModule.LogAction("Admin", "Data Saved", "Manual save triggered")
            MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            DataModule.LogError("Admin Save Failed", ex)
            MessageBox.Show($"Save failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class