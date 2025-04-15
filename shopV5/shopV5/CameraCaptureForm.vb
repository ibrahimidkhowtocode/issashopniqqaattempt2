Imports System.Drawing
Imports ZXing
Imports ZXing.Common
Imports ZXing.Windows.Compatibility

Public Class CameraCaptureForm
    Public Event BarcodeScanned As Action(Of String)
    Private bitmap As Bitmap
    Private Const ImageFileFilter As String = "Image Files|*.jpg;*.png;*.bmp"

    Private Sub BtnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        Try
            Using ofd As New OpenFileDialog
                ofd.Filter = ImageFileFilter
                If ofd.ShowDialog() = DialogResult.OK Then
                    ' Proper bitmap handling
                    Dim tempBitmap = New Bitmap(ofd.FileName)
                    bitmap = New Bitmap(tempBitmap) ' Create independent copy
                    tempBitmap.Dispose()

                    PictureBox1.Image = bitmap
                    ScanBarcode()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub

    Private Sub ScanBarcode()
        If bitmap Is Nothing Then Return

        ' Correct conversion using BitmapLuminanceSource
        Dim luminanceSource = New BitmapLuminanceSource(bitmap)
        Dim reader = New BarcodeReader()
        Dim result = reader.Decode(luminanceSource)

        If result IsNot Nothing Then
            RaiseEvent BarcodeScanned(result.Text)
            Me.Close()
        Else
            MessageBox.Show("No barcode detected")
        End If
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso bitmap IsNot Nothing Then
            bitmap.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
End Class