<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CashierForm
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
        btnScan = New Button()
        dgvProducts = New DataGridView()
        dgvCart = New DataGridView()
        CType(dgvProducts, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvCart, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnScan
        ' 
        btnScan.Location = New Point(323, 310)
        btnScan.Name = "btnScan"
        btnScan.Size = New Size(94, 29)
        btnScan.TabIndex = 0
        btnScan.Text = "Button1"
        btnScan.UseVisualStyleBackColor = True
        ' 
        ' dgvProducts
        ' 
        dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProducts.Location = New Point(375, 103)
        dgvProducts.Name = "dgvProducts"
        dgvProducts.RowHeadersWidth = 51
        dgvProducts.Size = New Size(113, 188)
        dgvProducts.TabIndex = 1
        ' 
        ' dgvCart
        ' 
        dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCart.Location = New Point(220, 103)
        dgvCart.Name = "dgvCart"
        dgvCart.RowHeadersWidth = 51
        dgvCart.Size = New Size(129, 188)
        dgvCart.TabIndex = 2
        ' 
        ' CashierForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(dgvCart)
        Controls.Add(dgvProducts)
        Controls.Add(btnScan)
        Name = "CashierForm"
        Text = "CashierForm"
        CType(dgvProducts, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvCart, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents btnScan As Button
    Friend WithEvents dgvProducts As DataGridView
    Friend WithEvents dgvCart As DataGridView
End Class
