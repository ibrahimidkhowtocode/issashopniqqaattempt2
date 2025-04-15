<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CameraCaptureForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        PictureBox1 = New PictureBox()
        btnCapture = New Button()
        btnRetry = New Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(295, 133)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(125, 62)
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' btnCapture
        ' 
        btnCapture.Location = New Point(532, 83)
        btnCapture.Name = "btnCapture"
        btnCapture.Size = New Size(94, 29)
        btnCapture.TabIndex = 1
        btnCapture.Text = "Button1"
        btnCapture.UseVisualStyleBackColor = True
        ' 
        ' btnRetry
        ' 
        btnRetry.Location = New Point(358, 78)
        btnRetry.Name = "btnRetry"
        btnRetry.Size = New Size(94, 29)
        btnRetry.TabIndex = 2
        btnRetry.Text = "Button2"
        btnRetry.UseVisualStyleBackColor = True
        ' 
        ' CameraCaptureForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnRetry)
        Controls.Add(btnCapture)
        Controls.Add(PictureBox1)
        Name = "CameraCaptureForm"
        Text = "CameraCaptureForm"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnCapture As Button
    Friend WithEvents btnRetry As Button
End Class
