Imports System.IO
Imports Newtonsoft.Json
Imports System.Linq

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

    ' ============== FORM LOAD AND INITIALIZATION ==============
    Private Sub LoadData1()
        ' Initialize files if they don't exist
        If Not File.Exists(StockFile) Then
            stockList = New List(Of Product) From {
            New Product(1, "Sample Product 1", 9.99, 10, "123456"),
            New Product(2, "Sample Product 2", 14.99, 5, "789012")
        }
            File.WriteAllText(StockFile, JsonConvert.SerializeObject(stockList))
        Else
            stockList = JsonConvert.DeserializeObject(Of List(Of Product))(File.ReadAllText(StockFile))
        End If

        If Not File.Exists(HistoryFile) Then
            transactionHistory = New List(Of TransactionHistory) From {
            New TransactionHistory("SYSTEM_INIT", "Application started", Environment.UserName)
        }
            File.WriteAllText(HistoryFile, JsonConvert.SerializeObject(transactionHistory))
        Else
            transactionHistory = JsonConvert.DeserializeObject(Of List(Of TransactionHistory))(File.ReadAllText(HistoryFile))
        End If

        If Not File.Exists(DebtFile) Then
            debtRecords = New List(Of DebtRecord) From {
            New DebtRecord With {
                .Id = 1,
                .CustomerName = "Sample Customer",
                .Amount = 100.0,
                .DateIncurred = DateTime.Now
            }
        }
            File.WriteAllText(DebtFile, JsonConvert.SerializeObject(debtRecords))
        Else
            debtRecords = JsonConvert.DeserializeObject(Of List(Of DebtRecord))(File.ReadAllText(DebtFile))
        End If

        ' Verify creation
        If Not File.Exists(StockFile) OrElse Not File.Exists(HistoryFile) OrElse Not File.Exists(DebtFile) Then
            MessageBox.Show("CRITICAL: Failed to create data files!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub LoadData()
        Try
            If Not File.Exists(StockFile) Then File.WriteAllText(StockFile, "[]")
            If Not File.Exists(HistoryFile) Then File.WriteAllText(HistoryFile, "[]")
            If Not File.Exists(DebtFile) Then File.WriteAllText(DebtFile, "[]")

            stockList = JsonConvert.DeserializeObject(Of List(Of Product))(File.ReadAllText(StockFile))
            transactionHistory = JsonConvert.DeserializeObject(Of List(Of TransactionHistory))(File.ReadAllText(HistoryFile))
            debtRecords = JsonConvert.DeserializeObject(Of List(Of DebtRecord))(File.ReadAllText(DebtFile))

            If stockList Is Nothing Then stockList = New List(Of Product)
            If transactionHistory Is Nothing Then transactionHistory = New List(Of TransactionHistory)
            If debtRecords Is Nothing Then debtRecords = New List(Of DebtRecord)
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

    ' ============== TAB MANAGEMENT ==============
    Private Sub SetupTabs()
        TabControl1.TabPages.Clear()

        ' Stock Management Tab
        Dim stockTab As New TabPage("Stock Management")
        SetupStockTab(stockTab)
        TabControl1.TabPages.Add(stockTab)

        ' History Tab
        Dim historyTab As New TabPage("Transaction History")
        SetupHistoryTab(historyTab)
        TabControl1.TabPages.Add(historyTab)

        ' Debt Management Tab
        Dim debtTab As New TabPage("Debt Management")
        SetupDebtTab(debtTab)
        TabControl1.TabPages.Add(debtTab)
    End Sub

    ' ============== STOCK TAB ==============
    Private Sub SetupStockTab(tab As TabPage)
        ' ... [Previous SetupStockTab implementation] ...
    End Sub

    Private Sub RefreshStockGrid(dgv As DataGridView)
        ' ... [Previous RefreshStockGrid implementation] ...
    End Sub

    Private Sub AddStock(dgv As DataGridView)
        Dim addForm As New Form With {
            .Text = "Add New Product",
            .Width = 400,
            .Height = 300,
            .StartPosition = FormStartPosition.CenterParent
        }

        ' Add controls to form
        Dim lblId As New Label With {.Text = "Product ID:", .Top = 20, .Left = 20}
        Dim txtId As New TextBox With {.Top = 40, .Left = 20, .Width = 200}

        Dim lblName As New Label With {.Text = "Product Name:", .Top = 70, .Left = 20}
        Dim txtName As New TextBox With {.Top = 90, .Left = 20, .Width = 200}

        Dim lblPrice As New Label With {.Text = "Price:", .Top = 120, .Left = 20}
        Dim txtPrice As New TextBox With {.Top = 140, .Left = 20, .Width = 200}

        Dim lblQuantity As New Label With {.Text = "Quantity:", .Top = 170, .Left = 20}
        Dim txtQuantity As New TextBox With {.Top = 190, .Left = 20, .Width = 200}

        Dim btnAdd As New Button With {
            .Text = "Add Product",
            .DialogResult = DialogResult.OK,
            .Top = 220,
            .Left = 20,
            .Width = 100
        }

        addForm.Controls.AddRange({lblId, txtId, lblName, txtName, lblPrice, txtPrice, lblQuantity, txtQuantity, btnAdd})

        If addForm.ShowDialog() = DialogResult.OK Then
            Try
                Dim newProduct As New Product(
                    Integer.Parse(txtId.Text),
                    txtName.Text,
                    Decimal.Parse(txtPrice.Text),
                    Integer.Parse(txtQuantity.Text),)

                If stockList.Any(Function(p) p.Id = newProduct.Id) Then
                    MessageBox.Show("Product ID already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                stockList.Add(newProduct)
                RefreshStockGrid(dgv)
                AddTransaction("STOCK_ADD", $"Added product: {newProduct.Name} (ID: {newProduct.Id})")
                SaveData()
            Catch ex As Exception
                MessageBox.Show($"Invalid input: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub EditStock(dgv As DataGridView)
        If dgv.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a product to edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim productId = CInt(dgv.SelectedRows(0).Cells("Id").Value)
        Dim product = stockList.FirstOrDefault(Function(p) p.Id = productId)

        If product Is Nothing Then Return

        Dim editForm As New Form With {
            .Text = "Edit Product",
            .Width = 400,
            .Height = 300,
            .StartPosition = FormStartPosition.CenterParent
        }

        ' Add controls to form
        Dim lblName As New Label With {.Text = "Product Name:", .Top = 20, .Left = 20}
        Dim txtName As New TextBox With {.Top = 40, .Left = 20, .Width = 200, .Text = product.Name}

        Dim lblPrice As New Label With {.Text = "Price:", .Top = 70, .Left = 20}
        Dim txtPrice As New TextBox With {.Top = 90, .Left = 20, .Width = 200, .Text = product.Price.ToString()}

        Dim lblQuantity As New Label With {.Text = "Quantity:", .Top = 120, .Left = 20}
        Dim txtQuantity As New TextBox With {.Top = 140, .Left = 20, .Width = 200, .Text = product.Quantity.ToString()}

        Dim btnSave As New Button With {
            .Text = "Save Changes",
            .DialogResult = DialogResult.OK,
            .Top = 220,
            .Left = 20,
            .Width = 100
        }

        editForm.Controls.AddRange({lblName, txtName, lblPrice, txtPrice, lblQuantity, txtQuantity, btnSave})

        If editForm.ShowDialog() = DialogResult.OK Then
            Try
                product.Name = txtName.Text
                product.Price = Decimal.Parse(txtPrice.Text)
                product.Quantity = Integer.Parse(txtQuantity.Text)

                RefreshStockGrid(dgv)
                AddTransaction("STOCK_EDIT", $"Edited product: {product.Name} (ID: {product.Id})")
                SaveData()
            Catch ex As Exception
                MessageBox.Show($"Invalid input: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' ============== HISTORY TAB ==============
    Private Sub SetupHistoryTab(tab As TabPage)
        ' ... [Previous SetupHistoryTab implementation] ...
    End Sub

    Private Sub RefreshHistoryGrid(dgv As DataGridView)
        dgv.Rows.Clear()
        For Each entry In transactionHistory.OrderByDescending(Function(t) t.Timestamp)
            dgv.Rows.Add(entry.Timestamp.ToString("g"), entry.ActionType, entry.Details, entry.User)
        Next
    End Sub

    ' ============== DEBT TAB ==============
    Private Sub SetupDebtTab(tab As TabPage)
        ' ... [Previous SetupDebtTab implementation] ...
    End Sub

    Private Sub RefreshDebtGrid(dgv As DataGridView)
        dgv.Rows.Clear()
        For Each debt In debtRecords
            dgv.Rows.Add(
                debt.Id,
                debt.CustomerName,
                debt.Amount.ToString("C"),
                debt.AmountPaid.ToString("C"),
                debt.Balance.ToString("C"),
                debt.DateIncurred.ToString("d"),
                If(debt.IsFullyPaid, "Paid", "Pending"))
        Next
    End Sub

    Private Sub AddDebt(dgv As DataGridView)
        Dim addForm As New Form With {
            .Text = "Add New Debt",
            .Width = 400,
            .Height = 250,
            .StartPosition = FormStartPosition.CenterParent
        }

        ' Add controls to form
        Dim lblCustomer As New Label With {.Text = "Customer Name:", .Top = 20, .Left = 20}
        Dim txtCustomer As New TextBox With {.Top = 40, .Left = 20, .Width = 200}

        Dim lblAmount As New Label With {.Text = "Amount:", .Top = 70, .Left = 20}
        Dim txtAmount As New TextBox With {.Top = 90, .Left = 20, .Width = 200}

        Dim lblNotes As New Label With {.Text = "Notes:", .Top = 120, .Left = 20}
        Dim txtNotes As New TextBox With {.Top = 140, .Left = 20, .Width = 200, .Height = 60, .Multiline = True}

        Dim btnAdd As New Button With {
            .Text = "Add Debt",
            .DialogResult = DialogResult.OK,
            .Top = 170,
            .Left = 20,
            .Width = 100
        }

        addForm.Controls.AddRange({lblCustomer, txtCustomer, lblAmount, txtAmount, lblNotes, txtNotes, btnAdd})

        If addForm.ShowDialog() = DialogResult.OK Then
            Try
                Dim newDebt As New DebtRecord With {
                    .Id = If(debtRecords.Count > 0, debtRecords.Max(Function(d) d.Id) + 1, 1),
                    .CustomerName = txtCustomer.Text,
                    .Amount = Decimal.Parse(txtAmount.Text),
                    .AmountPaid = 0,
                    .DateIncurred = DateTime.Now,
                    .IsFullyPaid = False,
                    .Notes = txtNotes.Text
                }

                debtRecords.Add(newDebt)
                RefreshDebtGrid(dgv)
                AddTransaction("DEBT_ADD", $"Added debt for {newDebt.CustomerName}: {newDebt.Amount.ToString("C")}")
                SaveData()
            Catch ex As Exception
                MessageBox.Show($"Invalid input: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub RecordPayment(dgv As DataGridView)
        If dgv.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a debt record first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim debtId = CInt(dgv.SelectedRows(0).Cells("Id").Value)
        Dim debt = debtRecords.FirstOrDefault(Function(d) d.Id = debtId)

        If debt Is Nothing Then Return

        Dim paymentForm As New Form With {
            .Text = "Record Payment",
            .Width = 400,
            .Height = 200,
            .StartPosition = FormStartPosition.CenterParent
        }

        ' Add controls to form
        Dim lblInfo As New Label With {
            .Text = $"Recording payment for {debt.CustomerName}",
            .Top = 20,
            .Left = 20,
            .AutoSize = True
        }

        Dim lblBalance As New Label With {
            .Text = $"Current Balance: {debt.Balance.ToString("C")}",
            .Top = 50,
            .Left = 20,
            .AutoSize = True
        }

        Dim lblAmount As New Label With {.Text = "Payment Amount:", .Top = 80, .Left = 20}
        Dim txtAmount As New TextBox With {.Top = 100, .Left = 20, .Width = 200}

        Dim btnRecord As New Button With {
            .Text = "Record Payment",
            .DialogResult = DialogResult.OK,
            .Top = 130,
            .Left = 20,
            .Width = 120
        }

        paymentForm.Controls.AddRange({lblInfo, lblBalance, lblAmount, txtAmount, btnRecord})

        If paymentForm.ShowDialog() = DialogResult.OK Then
            Try
                Dim paymentAmount = Decimal.Parse(txtAmount.Text)

                If paymentAmount <= 0 Then
                    MessageBox.Show("Payment amount must be positive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                If paymentAmount > debt.Balance Then
                    MessageBox.Show("Payment cannot exceed balance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                debt.AmountPaid += paymentAmount
                debt.IsFullyPaid = (debt.Balance <= 0)

                RefreshDebtGrid(dgv)
                AddTransaction("DEBT_PAYMENT", $"Recorded payment of {paymentAmount.ToString("C")} for {debt.CustomerName}")
                SaveData()

                MessageBox.Show("Payment recorded successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Invalid input: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

End Class