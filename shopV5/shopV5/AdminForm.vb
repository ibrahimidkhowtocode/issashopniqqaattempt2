Public Class AdminForm
    Private Sub AdminForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshData()
    End Sub

    Private Sub RefreshData()
        DataModule.LoadFromExcel()
        dgvStock.DataSource = DataModule.StockTable
        dgvSales.DataSource = DataModule.SalesTable
        dgvLogs.DataSource = DataModule.LogTable
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            DataModule.SaveToExcel()
            MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            DataModule.LogError("Admin Save Failed", ex)
            MessageBox.Show("Save failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class