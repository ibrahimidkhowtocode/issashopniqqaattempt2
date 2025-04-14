Public Class CashierForm
    Private Sub CashierForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupUI()
    End Sub

    Private Sub SetupUI()
        dgvCart.DataSource = DataModule.SalesTable
        dgvProducts.DataSource = DataModule.StockTable
    End Sub

    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        Try
            Dim barcode = InputBox("Enter barcode:", "Scan Product")
            If Not String.IsNullOrEmpty(barcode) Then
                ProcessBarcode(barcode)
            End If
        Catch ex As Exception
            DataModule.LogError("Scan Failed", ex)
            MessageBox.Show($"Scan error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ProcessBarcode(barcode As String)
        Dim product = DataModule.StockTable.Rows.Find(barcode)
        If product IsNot Nothing Then
            DataModule.SalesTable.Rows.Add(
                Guid.NewGuid(),
                DateTime.Now,
                product("Barcode"),
                1,
                product("Price")
            )
            DataModule.LogAction("Cashier", "Sale Recorded", $"Barcode: {barcode}")
        Else
            MessageBox.Show("Product not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class