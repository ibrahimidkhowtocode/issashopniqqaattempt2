Public Class CashierForm
    Private WithEvents TabControl1 As New TabControl With {.Dock = DockStyle.Fill}
    Private shoppingCart As New List(Of Product)
    Private debtRecords As New List(Of DebtRecord)

    Private Sub CashierForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Controls.Add(TabControl1)
        SetupTabs()
    End Sub

    Private Sub SetupTabs()
        TabControl1.TabPages.Clear()

        ' Sales Tab
        Dim salesTab As New TabPage("Sales")
        SetupSalesTab(salesTab)
        TabControl1.TabPages.Add(salesTab)

        ' Debt Tab
        Dim debtTab As New TabPage("Debt")
        SetupDebtTab(debtTab)
        TabControl1.TabPages.Add(debtTab)
    End Sub

    Private Sub SetupSalesTab(tab As TabPage)
        ' ... (existing sales implementation)
    End Sub

    Private Sub SetupDebtTab(tab As TabPage)
        Dim dgvDebt As New DataGridView With {.Dock = DockStyle.Fill}
        ' ... (add columns)

        Dim btnAddPayment As New Button With {.Text = "Record Payment", .Top = 310}
        AddHandler btnAddPayment.Click, Sub(s, ev) AddPayment(dgvDebt)

        tab.Controls.AddRange({dgvDebt, btnAddPayment})
        RefreshDebtGrid(dgvDebt)
    End Sub

    Private Sub RefreshDebtGrid(dgv As DataGridView)
        dgv.Rows.Clear()
        For Each debt In debtRecords
            dgv.Rows.Add(debt.Id, debt.CustomerName, debt.Amount,
                         debt.AmountPaid, debt.Balance, debt.DateIncurred,
                         If(debt.IsFullyPaid, "Paid", "Pending"))
        Next
    End Sub

    Private Sub AddPayment(dgv As DataGridView)
        If dgv.SelectedRows.Count = 0 Then Return

        Dim selectedId = CInt(dgv.SelectedRows(0).Cells("Id").Value)
        Dim debt = debtRecords.FirstOrDefault(Function(d) d.Id = selectedId)

        If debt IsNot Nothing Then
            ' Show payment form
            ' ... (implementation)
            RefreshDebtGrid(dgv)
        End If
    End Sub
End Class