Public Class DebtRecord
    Public Property Id As Integer
    Public Property CustomerName As String
    Public Property Amount As Decimal
    Public Property AmountPaid As Decimal
    Public Property DateIncurred As DateTime
    Public Property IsFullyPaid As Boolean
    Public Property Notes As String

    Public ReadOnly Property Balance As Decimal
        Get
            Return Amount - AmountPaid
        End Get
    End Property
End Class