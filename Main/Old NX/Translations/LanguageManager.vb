Public Class __LanguageManager

    Function Check(ByVal Form As MDIParent) As MDIParent
        On Error Resume Next
        For Each _item As ToolStripMenuItem In Form.MenuStrip.Items
            CheckLang(_item)
            Scan(_item)
        Next
        Return Form
    End Function

    Function Check(ByVal Form As Form1) As Form1
        On Error Resume Next
        For Each _item As ToolStripMenuItem In Form.MenuStrip1.Items
            CheckLang(_item)
            Scan(_item)
        Next
        Return Form
    End Function

    Function Check(ByVal form As EditForm) As EditForm
        On Error Resume Next
        For Each _item As ToolStripMenuItem In form.MenuStrip1.Items
            CheckLang(_item)
            Scan(_item)
        Next
        Return form
    End Function

    Function CheckLang(ByRef item As ToolStripMenuItem) As ToolStripMenuItem
        Dim ItemText As String = item.Text
        'ItemText = ItemText.Replace(" ", "")
        ItemText = ItemText.Replace("&", "")
        Log.WriteLine("LanguageManager: Converting " & ItemText & " to the language " & My.Settings.Language)
        item.Text = Translator.TranslateText(ItemText, Translator.FormatLanguages("English", My.Settings.Language))
        Return item
    End Function

    Sub Scan(ByVal item As ToolStripMenuItem)
        On Error Resume Next
        For Each _item In item.DropDownItems
            _item = CheckLang(_item)
            Scan(_item)
        Next
    End Sub
End Class
