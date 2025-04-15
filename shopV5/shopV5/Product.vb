<Serializable()>
Public Class Product
    Public Property Id As Integer
    Public Property Name As String
    Public Property Price As Decimal
    Public Property Quantity As Integer
    Public Property Barcode As String

    Public Sub New(id As Integer, name As String, price As Decimal, quantity As Integer, Optional barcode As String = "")
        Me.Id = id
        Me.Name = name
        Me.Price = price
        Me.Quantity = quantity
        Me.Barcode = barcode
    End Sub
End Class