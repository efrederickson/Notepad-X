Public Class optionsForm

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub optionsForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.HomePage = homePageTextBox.Text
        My.Settings.SupressErrors = CheckBox1.Checked

        Form1.WebBrowser1.ScriptErrorsSuppressed = My.Settings.SupressErrors

        If RadioButton1.Checked Then
            My.Settings.DefaultSearcher = "google.com"
        Else
            My.Settings.DefaultSearcher = "bing.com"
        End If
        My.Settings.TopMost = CheckBox2.Checked
    End Sub

    Private Sub optionsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckBox2.Checked = My.Settings.TopMost
        homePageTextBox.Text = My.Settings.HomePage
        CheckBox1.Checked = My.Settings.SupressErrors
        If My.Settings.DefaultSearcher = "google.com" Then
            RadioButton1.Checked = True
            RadioButton2.Checked = False
        Else
            RadioButton1.Checked = False
            RadioButton2.Checked = True
        End If
        With extensionsListBox.Items
            .Add(".html")
            .Add(".htm")
            .Add(".mht")
            .Add(".mhtml")
        End With
    End Sub

    Function GetExtensionInfo(ByVal ext As String) As String
        ext = ext.ToLower
        Select Case ext
            Case ".css"
                Return "Cascading Style Sheets File"
            Case ".deploy"
                Return "Deployment File"
            Case ".dic"
                Return "Dictionary File"
            Case ".encrypt"
                Return "Rijndael Encrypted File"
            Case ".esf"
                Return "Exported Settings File"
            Case ".etxt"
                Return "Encrypted Text File"
            Case ".log"
                Return "Log File"
            Case ".pcdf"
                Return "Pokemon Card Database"
            Case ".tmp"
                Return "Temporary File"
            Case ".txt"
                Return "Text File"
            Case ".vbm"
                Return "Visual Basic for MiniOS"
            Case ".inf"
                Return "Setup Information File"
            Case ".ini"
                Return "Information File"
            Case ".xml"
                Return "XML Data"
            Case ".html"
                Return "Web Page"
            Case ".exe"
                Return "Executable File"
            Case ".dll"
                Return "Application Extension"
            Case ".c"
                Return "C Source Code"
            Case ".cs"
                Return "C# Source Code"
            Case ".cpp"
                Return "C++ Source Code"
            Case ".vb"
                Return "Visual Basic Source Code"
            Case ".lua"
                Return "Lua Script"
            Case ".js"
                Return "Java Script "
            Case ".vbs"
                Return "Visual Basic Script"
            Case ".doc", ".docx"
                Return "Word Document"
            Case ".rtf"
                Return "Rich Text Document"
            Case ".nxm"
                Return "Notepad X Macro"
            Case Else
                Return ext.ToUpper & " File"
        End Select
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If NewExtensionForm.ShowDialog = DialogResult.OK Then
            extensionsListBox.Items.Add(NewExtensionForm.Extension)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For Each item In extensionsListBox.Items
            ExtensionMods._Set(item, GetExtensionInfo(item), Application.ExecutablePath)
        Next
        MsgBox("Sucessfully Set Programs!")
    End Sub
End Class