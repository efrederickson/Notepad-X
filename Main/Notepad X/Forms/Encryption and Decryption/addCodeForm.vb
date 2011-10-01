Public Class addCodeForm
    Public Code As Integer
    Public File As String

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.Filter = ""
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Code = CInt(TextBox2.Text)
        Catch ex As Exception
            MessageBox.Show("Please enter an integer.", "Notepad X", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        File = TextBox1.Text
        Me.Close()
    End Sub
End Class