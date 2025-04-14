Imports ZXing
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports ZXing.QrCode

Public Class CameraCaptureForm
    Public Event BarcodeScanned As Action(Of String)
    Private videoSource As VideoCaptureDevice

    Private Sub CameraCaptureForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim videoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)
        If videoDevices.Count > 0 Then
            videoSource = New VideoCaptureDevice(videoDevices(0).MonikerString)
            AddHandler videoSource.NewFrame, AddressOf VideoSource_NewFrame
            videoSource.Start()
        End If
    End Sub

    Private Sub VideoSource_NewFrame(sender As Object, eventArgs As NewFrameEventArgs)
        PictureBox1.Image = DirectCast(eventArgs.Frame.Clone(), Bitmap)
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        If PictureBox1.Image IsNot Nothing Then
            Dim reader As New BarcodeReader(Of Bitmap)()
            Dim result = reader.Decode(DirectCast(PictureBox1.Image, Bitmap))

            If result IsNot Nothing Then
                RaiseEvent BarcodeScanned(result.Text)
            End If
        End If
        Me.Close()
    End Sub
    Private WithEvents btnCapture As Button
    Private Sub CameraCaptureForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If videoSource IsNot Nothing AndAlso videoSource.IsRunning Then
            videoSource.SignalToStop()
        End If
    End Sub
End Class