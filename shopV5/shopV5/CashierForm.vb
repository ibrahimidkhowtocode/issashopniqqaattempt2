Public Class CashierForm
    Private Sub CashierForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupUI()
        DataModule.InitializeTables()
    End Sub

    Private Sub SetupUI()
        ' Modern styling
        Me.BackColor = Color.FromArgb(240, 240, 240)
        Me.Font = New Font("Segoe UI", 12)

        ' Large buttons
        btnScan.BackColor = Color.SteelBlue
        btnScan.ForeColor = Color.White
        btnScan.Size = New Size(150, 50)

        ' DataGridView styling
        dgvCart.BackgroundColor = Color.White
        dgvCart.DefaultCellStyle.Font = New Font("Segoe UI", 10)
    End Sub

    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        Try
            Dim barcode = ScanBarcode()
            If Not String.IsNullOrEmpty(barcode) Then
                AddProductToCart(barcode)
            End If
        Catch ex As Exception
            ShowError("Scanning failed: " & ex.Message)
        End Try
    End Sub
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        Me.DoubleBuffered = True ' Smooth rendering
        ApplyModernTheme()
    End Sub
    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        Dim captureForm As New CameraCaptureForm()
        AddHandler captureForm.BarcodeScanned, Sub(barcode)
                                                   txtBarcode.Text = barcode
                                                   AddProductToCart(barcode)
                                               End Sub
        captureForm.ShowDialog()
    End Sub
    Private Sub ApplyModernTheme()
        ' Consistent styling across all forms
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is Button Then
                Dim btn = DirectCast(ctrl, Button)
                btn.FlatStyle = FlatStyle.Flat
                btn.FlatAppearance.BorderSize = 0
                btn.BackColor = Color.SteelBlue
                btn.ForeColor = Color.White
            End If
        Next
    End Sub
    Private Function ScanBarcode() As String
        ' Implement barcode scanning with ZXing
        Dim scanner As New ZXing.BarcodeReader
        Dim result = scanner.Decode(New Bitmap("barcode_image.png")) ' Replace with camera input
        Return If(result?.Text, "")
    End Function

    Private Sub AddProductToCart(barcode As String)
        ' Add product to cart logic

    End Sub
    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        Using cameraForm As New Form
            cameraForm.Size = New Size(800, 600)
            Dim pictureBox As New PictureBox With {.Dock = DockStyle.Fill}
            cameraForm.Controls.Add(pictureBox)

            ' Camera capture logic here (use AForge.NET for camera access)
            ' When image is captured:
            Dim scanner As New ZXing.BarcodeReader
            Dim result = scanner.Decode(DirectCast(pictureBox.Image, Bitmap))
            txtBarcode.Text = result?.Text
        End Using
    End Sub

End Class