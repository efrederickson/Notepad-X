Public Class BBCodeMenu
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[b][/b]")
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[i][/i]")
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[u][/u]")
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[URL=www.google.com]Google[/URL]")
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[IMG]image URL[/IMG]")
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[quote=""who quoted""]the quoted text[/quote]")
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[code][/code]")
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton8.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[color=color_name_or_hex][/color]")
    End Sub

    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        Dim form As NotepadX.Plugins.ITextEditorForm
        Try
            form = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.Plugins.ITextEditorForm)
        Catch ex As Exception
            MsgBox("Please Select a Notepad X Form")
            Return
        End Try
        form.InsertText("[list][*]item[*]item2[/list]")
    End Sub

End Class
