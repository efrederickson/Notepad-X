Public Class RegEntries
    Dim filetypes() As String = {".css", ".deploy", ".dic", ".encrypt",
".esf", ".etxt", ".log", ".lua", ".nxm", ".pcdf", ".psd1", ".psm1",
".scp", ".tmp", ".txt", ".vbm", ".vol", ".adr", ".inf", ".oqy", ".ini"}
    Dim myName As String = Application.ExecutablePath

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CheckedListBox1.Enabled = False
        For i = 0 To CheckedListBox1.CheckedItems.Count - 1 'filetypes.Count - 1
            Dim daft As String = CStr(CheckedListBox1.CheckedItems(i))
            Dim extinfo As String = GetExtensionInfo(daft)
            ExtensionMods._Set(daft, extinfo, myName)
        Next
        CheckedListBox1.Enabled = True
        MsgBox("Successfully set programs!")
        Log.WriteLine("Modified Registry: ")
        Me.Close()
    End Sub

    Private Sub RegEntries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i = 0 To filetypes.Length - 1
            CheckedListBox1.Items.Add(filetypes(i))
            CheckedListBox1.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim newExt As String = InputBox("File Extension: (please include the '.' before it)", "New Extension")
        CheckedListBox1.Items.Add(newExt)
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
            Case ".save"
                Return "Notepad X AutoSave"
            Case ".h"
                Return "C/C++ Header File"
            Case ".xls", ".xlsx"
                Return "Excel Worksheet"
            Case Else
                Return ext.ToUpper & " File"
        End Select
    End Function

End Class