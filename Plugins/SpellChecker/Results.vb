Public Class Results
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    Public Shadows _text As String = ""
    Private Sub Results_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
        TextBox1.Text = _text
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class