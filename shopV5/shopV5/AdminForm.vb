Imports System.IO
Imports Newtonsoft.Json

Public Class AdminForm
    Private stockList As New List(Of Product)
    Private transactionHistory As New List(Of TransactionHistory)
    Private debtRecords As New List(Of DebtRecord)
    Private Const StockFile As String = "stock.dat"
    Private Const HistoryFile As String = "history.dat"
    Private Const DebtFile As String = "debt.dat"

    Private WithEvents TabControl1 As New TabControl With {
        .Dock = DockStyle.Fill
    }

    Private Sub AdminForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Controls.Add(TabControl1)
        LoadData()
        SetupTabs()
    End Sub

    Private Sub SetupTabs()
        TabControl1.TabPages.Clear()

        ' Stock Management Tab
        Dim stockTab As New TabPage("Stock Management")
        SetupStockTab(stockTab)
        TabControl1.TabPages.Add(stockTab)

        ' History Tab
        Dim historyTab As New TabPage("History")
        SetupHistoryTab(historyTab)
        TabControl1.TabPages.Add(historyTab)

        ' Debt Management Tab
        Dim debtTab As New TabPage("Debt Management")
        SetupDebtTab(debtTab)
        TabControl1.TabPages.Add(debtTab)
    End Sub

    Private Sub SetupStockTab(tab As TabPage)
        Dim panel As New Panel With {.Dock = DockStyle.Fill}
        Dim dgvStock As New DataGridView With {
            .Dock = DockStyle.Top,
            .Height = 300,
            .ReadOnly = True,
            .AllowUserToAddRows = False
        }

        dgvStock.Columns.Add("Id", "ID")
        dgvStock.Columns.Add("Name", "Product Name")
        dgvStock.Columns.Add("Price", "Price")
        dgvStock.Columns.Add("Quantity", "Quantity")

        Dim btnAdd As New Button With {.Text = "Add Stock", .Top = 310, .Left = 20}
        Dim btnEdit As New Button With {.Text = "Edit Selected", .Top = 310, .Left = 120}

        AddHandler btnAdd.Click, Sub(s, ev) AddStock(dgvStock)
        AddHandler btnEdit.Click, Sub(s, ev) EditStock(dgvStock)

        panel.Controls.AddRange({dgvStock, btnAdd, btnEdit})
        tab.Controls.Add(panel)
        RefreshStockGrid(dgvStock)
    End Sub

    Private Sub SetupHistoryTab(tab As TabPage)
        Dim dgvHistory As New DataGridView With {
            .Dock = DockStyle.Fill,
            .ReadOnly = True,
            .AllowUserToAddRows = False
        }

        dgvHistory.Columns.Add("Timestamp", "Timestamp")
        dgvHistory.Columns.Add("Action", "Action")
        dgvHistory.Columns.Add("Details", "Details")
        dgvHistory.Columns.Add("User", "User")

        RefreshHistoryGrid(dgvHistory)
        tab.Controls.Add(dgvHistory)
    End Sub

    Private Sub SetupDebtTab(tab As TabPage)
        Dim panel As New Panel With {.Dock = DockStyle.Fill}
        Dim dgvDebt As New DataGridView With {
            .Dock = DockStyle.Top,
            .Height = 300,
            .ReadOnly = True
        }

        dgvDebt.Columns.Add("Id", "ID")
        dgvDebt.Columns.Add("Customer", "Customer")
        dgvDebt.Columns.Add("Amount", "Amount")
        dgvDebt.Columns.Add("Paid", "Paid")
        dgvDebt.Columns.Add("Balance", "Balance")
        dgvDebt.Columns.Add("Date", "Date")
        dgvDebt.Columns.Add("Status", "Status")

        Dim btnAddDebt As New Button With {.Text = "Add Debt", .Top = 310, .Left = 20}
        Dim btnAddPayment As New Button With {.Text = "Record Payment", .Top = 310, .Left = 120}

        AddHandler btnAddDebt.Click, Sub(s, ev) AddDebt(dgvDebt)
        AddHandler btnAddPayment.Click, Sub(s, ev) AddPayment(dgvDebt)

        panel.Controls.AddRange({dgvDebt, btnAddDebt, btnAddPayment})
        tab.Controls.Add(panel)
        RefreshDebtGrid(dgvDebt)
    End Sub

    Private Sub RefreshStockGrid(dgv As DataGridView)
        dgv.Rows.Clear()
        For Each product In stockList
            dgv.Rows.Add(product.Id, product.Name, product.Price, product.Quantity)
        Next
    End Sub

    Private Sub RefreshHistoryGrid(dgv As DataGridView)
        dgv.Rows.Clear()
        For Each entry In transactionHistory
            dgv.Rows.Add(entry.Timestamp, entry.ActionType, entry.Details, entry.User)
        Next
    End Sub

    Private Sub RefreshDebtGrid(dgv As DataGridView)
        dgv.Rows.Clear()
        For Each debt In debtRecords
            dgv.Rows.Add(debt.Id, debt.CustomerName, debt.Amount,
                         debt.AmountPaid, debt.Balance, debt.DateIncurred,
                         If(debt.IsFullyPaid, "Paid", "Pending"))
        Next
    End Sub

    Private Sub AddStock(dgv As DataGridView)
        Dim frm As New Form With {.Text = "Add New Product", .Size = New Size(300, 250)}

        ' Add input controls
        Dim lblId As New Label With {.Text = "Product ID:", .Top = 20, .Left = 20}
        Dim txtId As New TextBox With {.Top = 40, .Left = 20, .Width = 200}

        Dim lblName As New Label With {.Text = "Product Name:", .Top = 70, .Left = 20}
        Dim txtName As New TextBox With {.Top = 90, .Left = 20, .Width = 200}

        Dim lblPrice As New Label With {.Text = "Price:", .Top = 120, .Left = 20}
        Dim txtPrice As New TextBox With {.Top = 140, .Left = 20, .Width = 200}

        Dim lblQuantity As New Label With {.Text = "Quantity:", .Top = 170, .Left = 20}
        Dim txtQuantity As New TextBox With {.Top = 190, .Left = 20, .Width = 200}

        Dim btnOK As New Button With {.Text = "OK", .Top = 220, .Left = 20, .DialogResult = DialogResult.OK}
        Dim btnCancel As New Button With {.Text = "Cancel", .Top = 220, .Left = 120, .DialogResult = DialogResult.Cancel}

        frm.Controls.AddRange({lblId, txtId, lblName, txtName, lblPrice, txtPrice, lblQuantity, txtQuantity, btnOK, btnCancel})

        If frm.ShowDialog() = DialogResult.OK Then
            ' Validate inputs
            Dim newId As Integer
            Dim newPrice As Decimal
            Dim newQuantity As Integer

            If Integer.TryParse(txtId.Text, newId) AndAlso
               Decimal.TryParse(txtPrice.Text, newPrice) AndAlso
               Integer.TryParse(txtQuantity.Text, newQuantity) Then

                ' Check for duplicate ID
                If stockList.Any(Function(p) p.Id = newId) Then
                    MessageBox.Show("Product ID already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                ' Create new product
                Dim newProduct As New Product(newId, txtName.Text, newPrice, newQuantity)
                stockList.Add(newProduct)
                RefreshStockGrid(dgv)
                SaveData()
                AddTransaction("STOCK_ADD", $"Added product ID: {newProduct.Id}")
            Else
                MessageBox.Show("Invalid input values!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub EditStock(dgv As DataGridView)
        If dgv.SelectedRows.Count = 0 Then Return

        Dim selectedId = CInt(dgv.SelectedRows(0).Cells("Id").Value)
        Dim product = stockList.FirstOrDefault(Function(p) p.Id = selectedId)

        If product IsNot Nothing Then
            Dim frm As New Form With {.Text = "Edit Product", .Size = New Size(300, 250)}

            ' Add input controls with current values
            Dim lblName As New Label With {.Text = "Product Name:", .Top = 20, .Left = 20}
            Dim txtName As New TextBox With {.Top = 40, .Left = 20, .Width = 200, .Text = product.Name}

            Dim lblPrice As New Label With {.Text = "Price:", .Top = 70, .Left = 20}
            Dim txtPrice As New TextBox With {.Top = 90, .Left = 20, .Width = 200, .Text = product.Price.ToString()}

            Dim lblQuantity As New Label With {.Text = "Quantity:", .Top = 120, .Left = 20}
            Dim txtQuantity As New TextBox With {.Top = 140, .Left = 20, .Width = 200, .Text = product.Quantity.ToString()}

            Dim btnOK As New Button With {.Text = "OK", .Top = 180, .Left = 20, .DialogResult = DialogResult.OK}
            Dim btnCancel As New Button With {.Text = "Cancel", .Top = 180, .Left = 120, .DialogResult = DialogResult.Cancel}

            frm.Controls.AddRange({lblName, txtName, lblPrice, txtPrice, lblQuantity, txtQuantity, btnOK, btnCancel})

            If frm.ShowDialog() = DialogResult.OK Then
                ' Update product
                Dim newPrice As Decimal
                Dim newQuantity As Integer

                If Decimal.TryParse(txtPrice.Text, newPrice) AndAlso
                   Integer.TryParse(txtQuantity.Text, newQuantity) Then

                    product.Name = txtName.Text
                    product.Price = newPrice
                    product.Quantity = newQuantity

                    RefreshStockGrid(dgv)
                    SaveData()
                    AddTransaction("STOCK_EDIT", $"Edited product ID: {product.Id}")
                Else
                    MessageBox.Show("Invalid input values!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End If
    End Sub

    Private Sub AddDebt(dgv As DataGridView)
        Dim frm As New Form With {.Text = "Add New Debt", .Size = New Size(300, 200)}

        ' Add input controls
        Dim lblCustomer As New Label With {.Text = "Customer Name:", .Top = 20, .Left = 20}
        Dim txtCustomer As New TextBox With {.Top = 40, .Left = 20, .Width = 200}

        Dim lblAmount As New Label With {.Text = "Amount:", .Top = 70, .Left = 20}
        Dim txtAmount As New TextBox With {.Top = 90, .Left = 20, .Width = 200}

        Dim btnOK As New Button With {.Text = "OK", .Top = 130, .Left = 20, .DialogResult = DialogResult.OK}
        Dim btnCancel As New Button With {.Text = "Cancel", .Top = 130, .Left = 120, .DialogResult = DialogResult.Cancel}

        frm.Controls.AddRange({lblCustomer, txtCustomer, lblAmount, txtAmount, btnOK, btnCancel})

        If frm.ShowDialog() = DialogResult.OK Then
            Dim amount As Decimal
            If Decimal.TryParse(txtAmount.Text, amount) AndAlso Not String.IsNullOrEmpty(txtCustomer.Text) Then
                Dim newDebt As New DebtRecord With {
                    .Id = If(debtRecords.Count > 0, debtRecords.Max(Function(d) d.Id) + 1, 1),
                    .CustomerName = txtCustomer.Text,
                    .Amount = amount,
                    .AmountPaid = 0,
                    .DateIncurred = DateTime.Now,
                    .IsFullyPaid = False
                }

                debtRecords.Add(newDebt)
                RefreshDebtGrid(dgv)
                SaveData()
                AddTransaction("DEBT_ADD", $"Added debt for {newDebt.CustomerName}")
            Else
                MessageBox.Show("Invalid input values!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub AddPayment(dgv As DataGridView)
        If dgv.SelectedRows.Count = 0 Then Return

        Dim selectedId = CInt(dgv.SelectedRows(0).Cells("Id").Value)
        Dim debt = debtRecords.FirstOrDefault(Function(d) d.Id = selectedId)

        If debt IsNot Nothing Then
            Dim frm As New Form With {.Text = "Record Payment", .Size = New Size(300, 150)}

            Dim lblPayment As New Label With {.Text = $"Payment Amount (Balance: {debt.Balance}):", .Top = 20, .Left = 20}
            Dim txtPayment As New TextBox With {.Top = 40, .Left = 20, .Width = 200}

            Dim btnOK As New Button With {.Text = "OK", .Top = 80, .Left = 20, .DialogResult = DialogResult.OK}
            Dim btnCancel As New Button With {.Text = "Cancel", .Top = 80, .Left = 120, .DialogResult = DialogResult.Cancel}

            frm.Controls.AddRange({lblPayment, txtPayment, btnOK, btnCancel})

            If frm.ShowDialog() = DialogResult.OK Then
                Dim payment As Decimal
                If Decimal.TryParse(txtPayment.Text, payment) Then
                    If payment > debt.Balance Then
                        MessageBox.Show("Payment cannot exceed balance!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    debt.AmountPaid += payment
                    debt.IsFullyPaid = (debt.Balance <= 0)

                    RefreshDebtGrid(dgv)
                    SaveData()
                    AddTransaction("DEBT_PAYMENT", $"Recorded payment of {payment} for {debt.CustomerName}")
                Else
                    MessageBox.Show("Invalid payment amount!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End If
    End Sub

    Private Sub LoadData()
        Try
            If File.Exists(StockFile) Then
                stockList = JsonConvert.DeserializeObject(Of List(Of Product))(File.ReadAllText(StockFile))
            End If

            If File.Exists(HistoryFile) Then
                transactionHistory = JsonConvert.DeserializeObject(Of List(Of TransactionHistory))(File.ReadAllText(HistoryFile))
            End If

            If File.Exists(DebtFile) Then
                debtRecords = JsonConvert.DeserializeObject(Of List(Of DebtRecord))(File.ReadAllText(DebtFile))
            End If
        Catch ex As Exception
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveData()
        Try
            File.WriteAllText(StockFile, JsonConvert.SerializeObject(stockList))
            File.WriteAllText(HistoryFile, JsonConvert.SerializeObject(transactionHistory))
            File.WriteAllText(DebtFile, JsonConvert.SerializeObject(debtRecords))
        Catch ex As Exception
            MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddTransaction(action As String, details As String)
        transactionHistory.Add(New TransactionHistory(action, details, Environment.UserName))
        SaveData()
    End Sub
End Class