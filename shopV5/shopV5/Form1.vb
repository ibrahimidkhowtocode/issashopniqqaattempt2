Public Class Form1
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            If Authenticate(txtUsername.Text, txtPassword.Text) Then
                DataModule.Initialize()
                Me.Hide()
                If txtUsername.Text = "admin" Then
                    New AdminForm().Show()
                Else
                    New CashierForm().Show()
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
        Return (username = "admin" AndAlso password = "admin123") OrElse
               (username = "cashier" AndAlso password = "cashier123")
    End Function
End Class