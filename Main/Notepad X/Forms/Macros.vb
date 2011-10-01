Imports Microsoft.VisualBasic.FileIO

Public Class Macros
    Shadows Owner As TextEditor

    Sub New(ByVal Owner As TextEditor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Owner = Owner
    End Sub

    Sub LoadFile(ByVal path As String)
        outputListBox.Items.Clear()
        Dim Lines() As String = IO.File.ReadAllLines(path)
        Dim line As String
        For Each line In Lines
            If line.StartsWith("--") Then
                Continue For
            End If
            line = line.Trim
            If line.ToLower = "save" Then
                Owner.save(Owner.TextBox1.Text)
                outputListBox.Items.Add("Saved File!")
            ElseIf line.ToLower = "exit" Then
                outputListBox.Items.Add("Closing...")
                Threading.Thread.Sleep(100)
                Owner.Form1_Closing(Nothing, New System.ComponentModel.CancelEventArgs())
                Owner.Close()
                Me.Close()
            ElseIf line.ToLower.StartsWith("open ") Then
                Dim line2 As String = line.Substring("open ".Length)
                Owner.open(line2)
                outputListBox.Items.Add("Opened File: " & IO.Path.GetFileName(line2))
            ElseIf line.ToLower = "saveas" Then
                Owner.Document.Path = ""
                Owner.SaveAsToolStripMenuItem_Click(Nothing, EventArgs.Empty)
            ElseIf line.StartsWith("settext ") Then
                Dim line2 As String = line.Substring("settext ".Length)
                Owner.TextBox1.Text &= line2
                outputListBox.Items.Add("Added Text to the TextBox")
            ElseIf line.ToLower = "resettext" Then
                Owner.TextBox1.Text = ""
                outputListBox.Items.Add("Resetted TextBox's Text")
            ElseIf line.ToLower.StartsWith("appendfile ") Then
                Dim line2 As String = line.Substring("appendfile ".Length)
                Owner.TextBox1.Text &= IO.File.ReadAllText(line2)
                outputListBox.Items.Add("Appended " & line2.ToUpper)
            ElseIf line.ToLower.StartsWith("end") Then
                outputListBox.Items.Add("Closing Notepad X...")
                Application.Exit()
            Else
                outputListBox.Items.Add("Command Not Found!")
            End If
        Next
        outputListBox.Items.Add("Successfully ran Macro!")
    End Sub

    Private Sub exitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitButton.Click
        Me.Close()
    End Sub

    Private Sub openFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles openFileButton.Click
        OpenFileDialog1.Filter = "Notepad X Macro File (*.nxm)|*.nxm"
        OpenFileDialog1.InitialDirectory = SpecialDirectories.MyDocuments & "\Notepad X\Macros"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            macroPathTextBox.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub runMacroButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles runMacroButton.Click
        If macroPathTextBox.Text = "" Then
            MsgBox("Please enter a file's Path!")
        Else
            LoadFile(macroPathTextBox.Text)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles runCommandButton.Click
        IO.File.WriteAllText(Application.LocalUserAppDataPath & "\Notepad X\TmpMacro.NXM", commandTextBox.Text)
        LoadFile(Application.LocalUserAppDataPath & "\Notepad X\TmpMacro.NXM")
        Try
            IO.File.Delete(Application.LocalUserAppDataPath & "\Notepad X\TmpMacro.NXM")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Macros_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim files As String() = IO.Directory.GetFiles(SpecialDirectories.MyDocuments & "\Notepad X\Macros")
        For Each file In files
            ListBox1.Items.Add(IO.Path.GetFileNameWithoutExtension(file) & " | " & file)
        Next
        If ListBox1.Items.Count > 0 Then ListBox1.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex <> -1 Then
            LoadFile(CStr(ListBox1.SelectedItem).Split(CChar("|"))(1).Trim)
        End If
    End Sub
End Class