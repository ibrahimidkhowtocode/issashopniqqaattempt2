Public Class TransactionHistory
    Public Property Id As Integer
    Public Property ActionType As String ' "STOCK_ADD", "STOCK_EDIT", "SALE", "DEBT_ADD", "DEBT_PAYMENT"
    Public Property Details As String
    Public Property User As String
    Public Property Timestamp As DateTime

    Public Sub New(action As String, details As String, user As String)
        ActionType = action
        details = details
        user = user
        Timestamp = DateTime.Now
    End Sub
End Class