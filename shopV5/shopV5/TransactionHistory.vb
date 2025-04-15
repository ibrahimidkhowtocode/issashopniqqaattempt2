<Serializable()>
Public Class TransactionHistory
    Public Property ActionType As String
    Public Property Details As String
    Public Property User As String
    Public Property Timestamp As DateTime

    Public Sub New(action As String, details As String, user As String)
        Me.ActionType = action
        Me.Details = details
        Me.User = user
        Me.Timestamp = DateTime.Now
    End Sub
End Class