Public Class CashierForm
    Private Sub CashierForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvProducts.DataSource = DataModule.StockTable
        dgvCart.DataSource = DataModule.SalesTable
    End Sub

    Private Sub BtnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        Using captureForm As New CameraCaptureForm()
            AddHandler captureForm.BarcodeScanned, Sub(barcode)
                                                       Me.Invoke(Sub() ProcessBarcode(barcode))
                                                   End Sub
            captureForm.ShowDialog()
        End Using
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