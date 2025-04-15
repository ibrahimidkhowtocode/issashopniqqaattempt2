Imports System.Drawing
Imports ZXing
Imports ZXing.Common
Imports ZXing.CoreCompat.System.Drawing

Public Class CameraCaptureForm
    Public Event BarcodeScanned As Action(Of String)
    Private bitmap As Bitmap

    Private Const ImageFileFilter As String = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
    Private Const DefaultWidth As Integer = 640
    Private Const DefaultHeight As Integer = 480

    Private Sub CameraCaptureForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Barcode Scanner"
        Me.ClientSize = New Size(DefaultWidth, DefaultHeight)
        Me.MinimumSize = New Size(DefaultWidth, DefaultHeight)
        Me.MaximizeBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
    End Sub

    Private Sub BtnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        Try
            Using ofd As New OpenFileDialog With {
                .Title = "Select Barcode Image",
                .Filter = ImageFileFilter,
                .Multiselect = False
            }
                If ofd.ShowDialog() = DialogResult.OK Then
                    Dim tempBitmap As Bitmap = Nothing
                    Try
                        tempBitmap = New Bitmap(ofd.FileName)
                        bitmap?.Dispose()
                        bitmap = New Bitmap(tempBitmap)
                        PictureBox1.Image = DirectCast(bitmap.Clone(), Image)
                        ScanBarcode()
                    Catch ex As ArgumentException
                        MessageBox.Show($"Invalid image file: {ex.Message}", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As OutOfMemoryException
                        MessageBox.Show("The image file is too large or corrupted", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        tempBitmap?.Dispose()
                    End Try
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Unexpected error: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ScanBarcode()
        If bitmap Is Nothing Then
            MessageBox.Show("Please load an image first", "Warning",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Create the reader with updated configuration
        Dim reader As New BarcodeReader With {
            .Options = New DecodingOptions With {
                .PossibleFormats = New List(Of BarcodeFormat) From {
                    BarcodeFormat.QR_CODE,
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.EAN_13,
                    BarcodeFormat.UPC_A
                },
                .TryHarder = True,
                .TryInverted = True  ' Moved TryInverted here as per new version
            },
            .AutoRotate = True
        }

        Try
            ' Simplified decoding - no need for luminance source in newer versions
            Dim result = reader.Decode(bitmap)

            If result IsNot Nothing Then
                RaiseEvent BarcodeScanned(result.Text)
                MessageBox.Show($"Success! Barcode Content:{Environment.NewLine}{result.Text}",
                              "Scan Successful",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                MessageBox.Show("No barcode detected in the image", "Scan Failed",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show($"Scanning error: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRetry_Click(sender As Object, e As EventArgs) Handles btnRetry.Click
        ScanBarcode()
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        ' Clean up resources
        If bitmap IsNot Nothing Then
            bitmap.Dispose()
            bitmap = Nothing
        End If
        PictureBox1.Image = Nothing
        MyBase.OnFormClosing(e)
    End Sub
End Class