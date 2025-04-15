Public Class Form1
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        Try
            Dim username As String = TxtUsername.Text.Trim()
            Dim password As String = TxtPassword.Text.Trim()

            If Authenticate(username, password) Then
                DataModule.Initialize()
                Me.Hide()
                If username.Equals("admin", StringComparison.OrdinalIgnoreCase) Then
                    Dim adminForm As New AdminForm()
                    adminForm.Show()
                Else
                    Dim cashierForm As New CashierForm()
                    cashierForm.Show()
                End If
            Else
                MessageBox.Show("Invalid credentials", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            DataModule.LogError("Login Failed", ex)
            MessageBox.Show("Login error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Authenticate(username As String, password As String) As Boolean
        Return (username.Equals("admin", StringComparison.OrdinalIgnoreCase) AndAlso password = "admin123") OrElse
               (username.Equals("cashier", StringComparison.OrdinalIgnoreCase) AndAlso password = "cashier123")
    End Function
End Class