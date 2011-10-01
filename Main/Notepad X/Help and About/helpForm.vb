Public Class helpForm
    'there is only one button that needs code.
    'otherwise it is all made by the designer,
    'which is easy for me.

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Process.Start(IO.Path.GetDirectoryName(Reflection.Assembly.GetAssembly(GetType(NotepadX.MDIParent)).Location) & "\Documents\Notepad X.docx")
    End Sub

    Private Sub helpForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label2.Text = "The Default Save Location is: " & My.Settings.DefaultSaveLocation
        For Each plugin As AvailablePlugin In PluginManager.AvailablePlugins
            If plugin.Instance.HasHelpPage Then plugin.Instance.HelpPage.Parent = TabControl1
        Next
    End Sub
End Class