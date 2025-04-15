Imports System.Drawing
Imports ZXing
Imports ZXing.QrCode
Imports ZXing.Windows.Compatibility

Public Class CameraCaptureForm
    Public Event BarcodeScanned As Action(Of String)
    Private bitmap As Bitmap

    Private Const ImageFileFilter As String = "Image Files|*.jpg;*.png"

    Private Sub CameraCaptureForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Scan Barcode"
        Me.ClientSize = New Size(640, 480)
    End Sub

    Private Sub BtnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        Try
            Using ofd As New OpenFileDialog
                ofd.Filter = ImageFileFilter
                If ofd.ShowDialog() = DialogResult.OK Then
                    Using tempBitmap As New Bitmap(ofd.FileName)
                        bitmap = New Bitmap(tempBitmap) ' Create a new bitmap to avoid locking the file
                        PictureBox1.Image = bitmap
                        ScanBarcode()
                    End Using
                End If
            End Using
        Catch ex As ArgumentException
            MessageBox.Show($"Invalid image format: {ex.Message}")
        Catch ex As Exception
            MessageBox.Show($"Scan error: {ex.Message}")
        End Try
    End Sub

    Private Sub ScanBarcode()
        If bitmap Is Nothing Then
            MessageBox.Show("No image to scan.")
            Return
        End If

        ' Correct instantiation of BarcodeReader with appropriate type argument
        Dim reader As New BarcodeReaderGeneric() ' Use BarcodeReaderGeneric for decoding
        Dim result = reader.Decode(bitmap)

        If result IsNot Nothing Then
            RaiseEvent BarcodeScanned(result.Text)
            MessageBox.Show("Barcode detected: " & result.Text) ' Provide feedback to the user
            Me.Close()
        Else
            MessageBox.Show("No barcode detected")
        End If
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If bitmap IsNot Nothing Then
            bitmap.Dispose() ' Dispose of the bitmap to free resources
        End If
        MyBase.OnFormClosing(e)
    End Sub
End Class