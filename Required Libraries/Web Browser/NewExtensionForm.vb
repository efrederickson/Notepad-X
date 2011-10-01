Public Class NewExtensionForm
    Public Extension As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As String = TextBox1.Text
        If a = "" Then MsgBox("Please enter an extension!") : Return
        If Not a.StartsWith(".") Then a = "." & a
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub
End Class