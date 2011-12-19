Public Class findReplaceForm
    Dim find As Boolean = False
    Dim textToFind As String = ""

    'this sets the form to find the text in.
    Dim theForm As TextEditor

    Sub New(ByVal daForm As TextEditor, ByVal findOrReplace As String)
        'create the form, and set the needed stuff.
        InitializeComponent()
        theForm = daForm
        If findOrReplace.ToLower = "find" Then
            find = True
            RadioButton1.Checked = True
        Else
            find = False
            RadioButton1.Checked = False
            RadioButton2.Checked = True
            RadioButton1.PerformClick()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim txt As String = theForm.TextBox1.Text
        textToFind = TextBox1.Text
        'if find, then select the text
        If find Then
            If txt.Contains(textToFind) Then
                theForm.TextBox1.Select(txt.IndexOf(textToFind), textToFind.Length)
                Me.Close()
            End If
        Else
            'other wise replace it with something else
            If txt.Contains(textToFind) Then
                Dim rep As String = TextBox2.Text
                theForm.TextBox1.Text = theForm.TextBox1.Text.Replace(textToFind, rep)
                MsgBox("Replaced Text")
            End If
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        'are you wanting to find, or replace
        find = CBool(RadioButton1.Checked)
        If find Then
            Button1.Text = "Find"
            Label2.Enabled = False
            TextBox2.Enabled = False
        Else
            Label2.Enabled = True
            TextBox2.Enabled = True
            Button1.Text = "Replace"
        End If
    End Sub
End Class