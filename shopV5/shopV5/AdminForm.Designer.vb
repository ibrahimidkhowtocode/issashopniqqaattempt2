<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminForm
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
        dgvLogs = New DataGridView()
        dgvSales = New DataGridView()
        dgvStock = New DataGridView()
        btnSave = New Button()
        CType(dgvLogs, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvSales, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvStock, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvLogs
        ' 
        dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvLogs.Location = New Point(612, 58)
        dgvLogs.Name = "dgvLogs"
        dgvLogs.RowHeadersWidth = 51
        dgvLogs.Size = New Size(143, 275)
        dgvLogs.TabIndex = 0
        ' 
        ' dgvSales
        ' 
        dgvSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSales.Location = New Point(339, 58)
        dgvSales.Name = "dgvSales"
        dgvSales.RowHeadersWidth = 51
        dgvSales.Size = New Size(141, 275)
        dgvSales.TabIndex = 1
        ' 
        ' dgvStock
        ' 
        dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvStock.Location = New Point(79, 58)
        dgvStock.Name = "dgvStock"
        dgvStock.RowHeadersWidth = 51
        dgvStock.Size = New Size(154, 275)
        dgvStock.TabIndex = 2
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(358, 355)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(94, 29)
        btnSave.TabIndex = 3
        btnSave.Text = "Button1"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' AdminForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnSave)
        Controls.Add(dgvStock)
        Controls.Add(dgvSales)
        Controls.Add(dgvLogs)
        Name = "AdminForm"
        Text = "AdminForm"
        CType(dgvLogs, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvSales, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvStock, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvLogs As DataGridView
    Friend WithEvents dgvSales As DataGridView
    Friend WithEvents dgvStock As DataGridView
    Friend WithEvents btnSave As Button
End Class
