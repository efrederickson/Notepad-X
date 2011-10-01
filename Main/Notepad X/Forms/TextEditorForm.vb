Imports System.IO
Imports EncryptionLib
Imports System.ComponentModel
Imports System.Drawing.Printing

Public Class TextEditor
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
    Implements Plugins.ITextEditorForm

    Dim encryptAsc As New EncryptionLib.ASCII_Lib
    Dim encryptXor As New EncryptionLib.Xor_Lib
    Private objects As New EncryptionLib.CreateObjects
    Public Document As Document = New Document("", "", "")

    Private Sub timeBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles timeBackgroundWorker.DoWork
        While True
            'Update the time label
            Try
                timeBackgroundWorker.ReportProgress(1)
            Catch ex As Exception
            End Try
            'set the Notify Icon's visibility
            If Not My.Settings.ShowNotifyIcon Then
                NotifyIcon1.Visible = False
            Else
                NotifyIcon1.Visible = True
            End If
            'we can't have it break, can we?
            Threading.Thread.Sleep(10)
        End While
    End Sub

    Private Sub timeBackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles timeBackgroundWorker.ProgressChanged
        'if Me is disposing ,why update?
        If Me.Disposing Or Me.IsDisposed Then
            Exit Sub
        End If
        'set the status bar's visibilty
        If Not My.Settings.ShowStatusBar Then
            StatusStrip1.Visible = False
        Else
            StatusStrip1.Visible = True
        End If
        If My.Settings.AutoDetectURLS Then
            TextBox1.DetectUrls = True
        Else
            TextBox1.DetectUrls = False
        End If
        selectedCharactersCountStatusLabel.Text = "Selected Characters: " & TextBox1.SelectedText.Count
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'To keep the text box in perspective... 
        BackgroundWorker1.RunWorkerAsync()

        'this here is the color loading.
        Me.ForeColor = My.Settings.FormColor
        TextBox1.ForeColor = Me.ForeColor
        MenuStrip1.ForeColor = Me.ForeColor
        'and the font:
        TextBox1.Font = My.Settings.FormFont

        'start the time.
        timeBackgroundWorker.RunWorkerAsync()
        'check for command line args, or opening 
        'a file.
        TextBox1.Modified = False

        'show that Notepad X is running.
        If My.Settings.ShowNotifyIcon Then
            NotifyIcon1.ShowBalloonTip(10)
        End If
    End Sub

    Private Sub EncryptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem.Click
        Dim strCode As String = InputBox("ASCII Code: ")
        'an integer to encrypt the files.
        Dim code As Integer
        Try
            code = CInt(strCode)
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine("Error: " & vbCrLf & ex.ToString & vbCrLf)
            Return
        End Try
        'if the code passed CInt()ing, then
        'encrypt the file
        Dim encrypted As String = encryptAsc.Encrypt(TextBox1.Text, code)
        Document.Document = encrypted
        TextBox1.Text = encrypted
        Document.Save(False)
        Log.WriteLine("Encrypted " & Document.Title & " With ASCII. Key: " & code)
    End Sub

    Sub save(ByVal text As String)
        'SFN = Shortened File Name.
        'for later use.
        Dim SFN As String = ""
        Dim txtToWrite As String = text
        'if the Document.Path is nothing, then open a file to save.
        If Document.Path = "" Then
            SaveFileDialog1.Filter = "Text File (*.txt)|*.txt|" & _
                "Encrypted Text File (*.etxt)|*.etxt|" & _
                "Text File v2 (*.txtx)|*.txtx|" & _
                "Rich Text File (*.rtf)|*.rtf|" & _
                "Notepad X Macro (*.nxm)|*.nxm|" & _
                "All Files (*.*)|*.*"
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                Document.Path = SaveFileDialog1.FileName
            Else
                Exit Sub
            End If
        End If
        If Document.Path.ToLower.EndsWith(".rtf") Then
            TextBox1.SaveFile(Document.Path, RichTextBoxStreamType.RichText)
            Return
            'the file hasn't been changed yet!
            TextBox1.Modified = False
            'set the Current File Label's text to the shortened file name.
            currentFileStatusLabel.Text = "Current File: " & SFN
            Log.WriteLine("Saved File: " & text & "  ")
        End If
        'finally, save the file
        Try
            'if it uses "smart" encryption, find the code.
            If My.Settings.UseSmartEncryption Then
                'check if the code exists
                If encryptionCodes(Document.Path) IsNot Nothing Then
                    'if it does, encrypt & save it
                    txtToWrite = CStr(encryptAsc.Encrypt(txtToWrite, CInt(encryptionCodes(Document.Path))))
                    Document.Document = txtToWrite
                    Document.Save(False)
                Else
                    'otherwise, get the encryption code
                    Dim key = InputBox("Please Enter Encryption Code:")
                    Try
                        encryptionCodes(Document.Path) = CInt(key)
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                        Log.WriteLine("Error: " & vbCrLf & ex.ToString & vbCrLf)
                        Return
                    End Try
                    'then encrypt & save it
                    txtToWrite = encryptAsc.Encrypt(txtToWrite, CInt(decryptionCodes(Document.Path)))
                    Document.Document = txtToWrite
                    Document.Save(False)
                End If
            Else
                'if your not using "smart" encryption, check the other options.
                If My.Settings.AutoEncrypt Then
                    If My.Settings.AutoEncryptASCII Then
                        txtToWrite = encryptAsc.Encrypt(text, My.Settings.AutoEncryptCode)
                    Else
                        txtToWrite = encryptXor.Encrypt(text, My.Settings.AutoEncryptCode)
                    End If
                End If
                If Document.Path.ToLower.EndsWith("etxt") Then
                    If Not My.Settings.AutoEncrypt And My.Settings.AutoEncryptETXT Then
                        Document.Document = encryptAsc.Encrypt(txtToWrite, My.Settings.AutoEncryptETXTCode)
                        Document.Save(False)
                    Else
                        Document.Document = encryptAsc.Encrypt(txtToWrite, CInt(InputBox("ASCII Encryption Code:")))
                        Document.Save(False)
                    End If

                Else
                    Document.Document = txtToWrite
                    Document.Save(False)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine("Error: " & vbCrLf & ex.ToString & vbCrLf)
            Return
        End Try
        'set Me.Text to the Document.Path
        Me.Text = CStr("Notepad X - " & IO.Path.GetFileName(Document.Path))
        If My.Settings.ShowNotifyIcon Then
            If My.Settings.ShowFullPath Then
                If My.Settings.AutoEncrypt Then
                    NotifyIcon1.ShowBalloonTip(10, "Notepad X", Document.Path & " Saved Successfully With Encryption!", ToolTipIcon.Info)
                End If
                'if its an Encrypted Text File (etxt) then
                'get the encryption code
                If Document.Path.ToLower.EndsWith("etxt") Then
                    If Not My.Settings.AutoEncrypt Then
                        NotifyIcon1.ShowBalloonTip(10, "Notepad X", Document.Path & " Saved Successfully With Encryption!", ToolTipIcon.Info)
                    Else
                        NotifyIcon1.ShowBalloonTip(10, "Notepad X", Document.Path & " Saved Successfully!", ToolTipIcon.Info)
                    End If
                Else
                    NotifyIcon1.ShowBalloonTip(10, "Notepad X", Document.Path & " Saved Successfully!", ToolTipIcon.Info)
                End If

            Else
                'set the Notify Icon's Ballon Tip.
                If My.Settings.AutoEncrypt Then
                    NotifyIcon1.ShowBalloonTip(10, "Notepad X", SFN & " Saved Successfully With Encryption!", ToolTipIcon.Info)
                ElseIf SFN.ToLower.EndsWith("etxt") Then
                    If Not My.Settings.AutoEncrypt Then
                        NotifyIcon1.ShowBalloonTip(10, "Notepad X", SFN & " Saved Successfully With Encryption!", ToolTipIcon.Info)
                    Else
                        NotifyIcon1.ShowBalloonTip(10, "Notepad X", SFN & " Saved Successfully!", ToolTipIcon.Info)
                    End If
                Else
                    NotifyIcon1.ShowBalloonTip(10, "Notepad X", SFN & " Saved Successfully!", ToolTipIcon.Info)
                End If
            End If
        End If
        'the file hasn't been changed yet!
        TextBox1.Modified = False
        'set the Current File Label's text to the shortened file name.
        SFN = IO.Path.GetFileName(Document.Path)
        currentFileStatusLabel.Text = "Current File: " & SFN
        Log.WriteLine("Saved File: " & Document.Path)
    End Sub

    Private Sub DecryptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptToolStripMenuItem.Click
        'get the decryption code
        Dim strCode As String = InputBox("ASCII Code: ")
        Dim code As Integer
        Try
            code = CInt(strCode)
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
            Return
        End Try
        'if it passed, then decrypt the file.
        Dim decrypted As String = encryptAsc.Decrypt(TextBox1.Text, code)
        TextBox1.Text = decrypted
        TextBox1.Modified = False
        Log.WriteLine(String.Format("Decrypted {0} With ASCII. Key: {1}", Document.Path, code))
    End Sub

    Private Sub EncryptToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem1.Click
        'get the encryption code
        Dim strCode As String = InputBox("Xor Code: ")
        Dim code As Integer
        Try
            code = CInt(strCode)
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
            Return
        End Try
        'if it passed, then encrypt the file
        Dim encrypted As String = encryptXor.Encrypt(TextBox1.Text, code)
        save(encrypted)
        TextBox1.Text = encrypted
        Log.WriteLine(String.Format("Encrypted {0} With XOR. Key: {1}", Document.Path, code))
    End Sub

    Private Sub DecryptToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptToolStripMenuItem1.Click
        'get the decryption code
        Dim strCode As String = InputBox("Xor Code: ")
        Dim code As Integer
        Try
            code = CInt(strCode)
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
            Return
        End Try
        'if it passed CInt()ing, then decrypt the file
        Dim decrypted As String = encryptXor.Decrypt(TextBox1.Text, code)
        TextBox1.Text = decrypted
        Log.WriteLine(String.Format("Decrypted {0} With XOR. Key: {1}", Document.Path, code))
    End Sub

    'This is public for Macros Form
    Public Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        'exit the application.
        Me.Close()
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        'check if the Text Box's text has been changed
        If TextBox1.Modified = True Then
            If MsgBox("Save?", MsgBoxStyle.YesNo) = DialogResult.Yes Then
                save(TextBox1.Text)
            End If
        End If
        'set all the stuff...
        Document = New Document("", "", "")
        Me.Text = "Notepad X - Untitled"
        TextBox1.Text = Document.Document
        Log.WriteLine("Created a new Document")
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'the text has been changed! set the flag.
        TextBox1.Modified = True
        Dim Words As New List(Of String)
        Dim texts() As String = TextBox1.Text.Trim.Split(CChar(" "))
        For Each word In texts
            If word.Trim = "" Then
                Continue For
            Else
                Words.Add(word)
            End If
        Next
        totalCharactersStatusLabel.Text = "Characters: " & TextBox1.Text.Count & ". Words: " & Words.Count
        selectedCharactersCountStatusLabel.Text = "Selected Characters: " & TextBox1.SelectedText.Count
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        'open the file
        OpenFileDialog1.Filter = "Text File (*.txt)|*.txt|" & _
                "Encrypted Text File (*.etxt)|*.etxt|" & _
                "Rich Text File (*.rtf)|*.rtf|" & _
                "Notepad X Macro (*.nxm)|*.nxm|" & _
                "All Files (*.*)|*.*"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            open(OpenFileDialog1.FileName)
        End If
    End Sub

    Sub open(ByVal file As String)
        Dim txtToPut As String = ""
        Document.Path = file
        If IO.File.Exists(Document.Path) Then
            'read the file
            Try
                txtToPut = IO.File.ReadAllText(Document.Path)
            Catch ex As Exception
                MsgBox(ex.ToString)
                Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
                Return
            End Try
        Else
            'if it doen't exist 
            'create it?
            If MsgBox(Document.Path & " does not exist. Create?", MsgBoxStyle.YesNo) = DialogResult.Yes Then
                Dim QWER As IO.StreamWriter
                Try
                    QWER = New IO.StreamWriter(Document.Path)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                    Return
                End Try
                QWER.Close()
                'Open the new file, hopefully not stacking up calls and glitching.
                open(Document.Path)
            Else
                Return
            End If
        End If
        If file.ToLower.EndsWith("rtf") Then
            TextBox1.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText)
            Me.Text = CStr("Notepad X - " & IO.Path.GetFileName(Document.Path))
            If My.Settings.ShowNotifyIcon Then
                If My.Settings.ShowFullPath Then
                    NotifyIcon1.ShowBalloonTip(10, "Notepad X", Document.Path & " Opened Successfully!", ToolTipIcon.Info)
                Else

                    NotifyIcon1.ShowBalloonTip(10, "Notepad X", IO.Path.GetFileName(Document.Path) & " Opened Successfully!", ToolTipIcon.Info)
                End If
            End If
            'set the stuff...
            TextBox1.Modified = False
            currentFileStatusLabel.Text = "Current File: " & IO.Path.GetFileName(Document.Path)
            Log.WriteLine("Opened File: " & file & "  ")
            Return
        End If
        'check if its needing to to be decrypted.
        'if so, then decrypt it
        If My.Settings.UseSmartDecryption Then
            If decryptionCodes(Document.Path) IsNot Nothing Then
                txtToPut = encryptAsc.Decrypt(txtToPut, CInt(decryptionCodes(Document.Path)))
            Else
                Dim value As String = "-1"
                While CType(value, Integer) < 0
                    value = InputBox("Please Enter Decryption Code:")
                End While
                Try
                    decryptionCodes(Document.Path) = CInt(value)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                    Return
                End Try
                If CInt(decryptionCodes(Document.Path)) = 0 Then
                Else
                    txtToPut = encryptAsc.Decrypt(txtToPut, CInt(decryptionCodes(Document.Path)))
                End If

            End If
        Else
            If My.Settings.AutoDecrypt Then
                If My.Settings.AutoDecryptASCII Then
                    txtToPut = encryptAsc.Decrypt(txtToPut, My.Settings.AutoDecryptCode)
                Else
                    txtToPut = encryptXor.Decrypt(txtToPut, My.Settings.AutoDecryptCode)
                End If
            End If

            If file.ToLower.EndsWith("etxt") Then
                If Not My.Settings.AutoDecrypt Then
                    If Not My.Settings.AutoDecryptETXT Then
                        txtToPut = encryptAsc.Decrypt(txtToPut, CInt(InputBox("ASCII Encryption Code:")))
                    Else
                        txtToPut = encryptAsc.Decrypt(txtToPut, My.Settings.AutoDecryptETXTCode)
                    End If
                End If
            End If
        End If
        TextBox1.Text = txtToPut
        'show that it loaded
        Me.Text = CStr("Notepad X - " & IO.Path.GetFileName(Document.Path))
        If My.Settings.ShowNotifyIcon Then
            If My.Settings.ShowFullPath Then
                NotifyIcon1.ShowBalloonTip(10, "Notepad X", Document.Path & " Opened Successfully!", ToolTipIcon.Info)
            Else

                NotifyIcon1.ShowBalloonTip(10, "Notepad X", IO.Path.GetFileName(Document.Path) & " Opened Successfully!", ToolTipIcon.Info)
            End If
        End If
        'set the stuff...
        TextBox1.Modified = False
        currentFileStatusLabel.Text = "Current File: " & IO.Path.GetFileName(Document.Path)
        Log.WriteLine("Opened File: " & file & "  ")
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        'save the file
        save(TextBox1.Text)
    End Sub

    ' Public for Macros Form
    Public Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        'save as...
        Document.Path = ""
        'resets the Document.Path, making you need to resave it.
        'possibly under a different file name.
        save(TextBox1.Text)
    End Sub

    Private Sub PrintSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSetupToolStripMenuItem.Click
        'attempt printing setup
        PageSetupDialog1.Document = PrintDocument1
        PageSetupDialog1.ShowDialog(Me)
        Log.WriteLine("Print Setup: " & Document.Path)
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        'attempt actually printing the document.
        PrintDialog1.Document = PrintDocument1
        If PrintDialog1.ShowDialog() = DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub TimeDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimeDateToolStripMenuItem.Click
        'AKA the F5 key, insert the date
        TextBox1.Text = TextBox1.Text.Insert(0, DateTime.Now.ToString & " ")
    End Sub

    Sub Form1_Closing(ByVal sender As Object, ByVal e As CancelEventArgs) Handles Me.Closing
        'handles the application's close.
        'save if changed.
        If TextBox1.Modified Then
            Dim result As DialogResult = CType(MsgBox("Save?", MsgBoxStyle.YesNoCancel), DialogResult)
            If result = DialogResult.Yes Then
                save(TextBox1.Text)
            ElseIf result = DialogResult.Cancel Then
                e.Cancel = True
                GoTo _END
            End If
        End If
        'open the Encryption Codes.dat file.
        Try
            output = New StreamWriter(Application.LocalUserAppDataPath & "\Notepad X\Settings\Encryption Codes.dat")
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
            Return
        End Try
        'get the keys and values of the encryption codes,
        Dim keys As New List(Of String)
        Dim values As New List(Of String)
        For Each key In encryptionCodes.Keys
            keys.Add(CStr(key))
        Next
        For Each value In encryptionCodes.Values
            values.Add(CStr(value))
        Next
        'and write them to the file
        For i = 0 To keys.Count - 1
            output.WriteLine(keys(i) & "|" & values(i))
        Next
        output.Close()
        'open the Decryption Codes.dat file.
        Try
            output = New StreamWriter(Application.LocalUserAppDataPath & "\Notepad X\Settings\Decryption Codes.dat")
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
            Return
        End Try
        'clear the codes from the previous use,
        keys.Clear()
        values.Clear()
        'get the decryption codes stuff,
        For Each key In decryptionCodes.Keys
            keys.Add(CStr(key))
        Next
        For Each value In decryptionCodes.Values
            values.Add(CStr(value))
        Next
        'and write it to the file
        For i = 0 To keys.Count - 1
            output.WriteLine(keys(i) & "|" & values(i))
        Next
        output.Close()
        'save the font/color/other settings. hopefully.
        My.Settings.Save()
        Log.WriteLine("Closed")
_END:
    End Sub

    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        'Also Ctrl Z, undo's stuff
        TextBox1.Undo()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        'Also Ctrl X, cut's stuff
        TextBox1.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        'Also Ctrl C, copy's stuff
        TextBox1.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        'Also Ctrl V, paste's stuff
        TextBox1.Paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        'Also Ctrl A, select all the text in the text box.
        TextBox1.SelectAll()
    End Sub

    Private Sub FontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontToolStripMenuItem.Click
        'change the font of the form
        If FontDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Font = FontDialog1.Font
            My.Settings.FormFont = TextBox1.Font
        End If
    End Sub

    Private Sub ColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorToolStripMenuItem.Click
        'change the color of the form (not the text box :( hope to get that working soon.)
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            Me.ForeColor = ColorDialog1.Color
            MenuStrip1.ForeColor = Me.ForeColor
            TextBox1.ForeColor = Me.ForeColor
            My.Settings.FormColor = Me.ForeColor
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Also F2, shows the program info
        AboutBox1.ShowDialog()
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Also F1, shows the help
        helpForm.ShowDialog()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'df = optionsForm. idk why i chose df, because df is also Dragon Fable...
        'anyway, show the options form
        Dim df As New optionsForm
        df.ShowDialog()
        Log.WriteLine("Changed Options")
    End Sub

#Region " Notify Icon Context Menu "

    Private Sub BringToFrontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BringToFrontToolStripMenuItem.Click
        'bring me to the front
        Me.BringToFront()
    End Sub

    Private Sub SaveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem1.Click
        'save the file
        SaveToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PrintToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem1.Click
        'attempt printing...
        PrintToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem1.Click
        'End
        'that's quite self explanatory
        End
    End Sub
#End Region

    'back to the form

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'Ha! the printing. i hope this works...
        Dim numChars As Integer
        Dim numLines As Integer
        Dim stringForPage As String
        Dim strFormat As New StringFormat
        Dim StringToPrint As String = TextBox1.Text
        Dim PrintPageSettings As New PageSettings
        Dim PrintFont As Font = Me.TextBox1.SelectionFont
        'Based on page setup, define drawable rectangle on page
        Dim rectDraw As New RectangleF( _
        e.MarginBounds.Left, e.MarginBounds.Top, _
        e.MarginBounds.Width, e.MarginBounds.Height)
        'Define area to determine how much text can fit on a page
        'Make height one line shorter to ensure text doesn't clip
        Dim sizeMeasure As New SizeF(e.MarginBounds.Width, _
        e.MarginBounds.Height - PrintFont.GetHeight(e.Graphics))
        'When drawing long strings, break between words
        strFormat.Trimming = StringTrimming.Word
        'Compute how many chars and lines can fit based on sizeMeasure
        e.Graphics.MeasureString(StringToPrint, PrintFont, _
        sizeMeasure, strFormat, numChars, numLines)
        'Compute string that will fit on a page
        stringForPage = StringToPrint.Substring(0, numChars)
        'Print string on current page
        e.Graphics.DrawString(stringForPage, PrintFont, _
        Brushes.Black, rectDraw, strFormat)
        'If there is more text, indicate there are more pages
        If numChars < StringToPrint.Length Then
            'Subtract text from string that has been printed
            StringToPrint = StringToPrint.Substring(numChars)
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        'Change the Text Box's word wrap.
        If My.Settings.WordWrap Then
            My.Settings.WordWrap = False
            ToolStripMenuItem3.Text = "Word Wrap (Off)"
            TextBox1.WordWrap = False
        Else
            My.Settings.WordWrap = True
            ToolStripMenuItem3.Text = "Word Wrap (On)"
            TextBox1.WordWrap = True
        End If
    End Sub

    Private Sub UndoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem1.Click
        'redirect to undo
        UndoToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub SelectAllToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem1.Click
        'redirect to select all
        SelectAllToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub CopyToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem1.Click
        'redirect to copy
        CopyToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub CutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem1.Click
        CutToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PasteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem1.Click
        'redirect to redirect to paste
        PasteToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub OpenFileToReadBytecodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFileToReadBytecodeToolStripMenuItem.Click
        'this open files to read the bytecode
        'loading files like this can take a long time.
        OpenFileDialog1.Filter = ""
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim bytes() = IO.File.ReadAllBytes(OpenFileDialog1.FileName)
            Dim bytes2 As String = ""
            For Each Byte3 In bytes
                bytes2 &= Byte3 & " "
            Next
            TextBox1.Text = bytes2
            NotifyIcon1.ShowBalloonTip(5, "Notepad X", "Opened File Successfully!", Nothing)
            Me.Text = "Notepad X" & IO.Path.GetFileName(OpenFileDialog1.FileName)
            currentFileStatusLabel.Text = "Current File: " & IO.Path.GetFileName(OpenFileDialog1.FileName)
            TextBox1.Modified = False
        End If
    End Sub

    Private Sub FindToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem.Click
        'Also Ctrl F, find stuff in the text box.
        Dim a As New findReplaceForm(Me, "find")
        a.Show()
    End Sub

    Private Sub ReplaceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceToolStripMenuItem.Click
        'Also Ctrl R, replace text in the form. 
        Dim a As New findReplaceForm(Me, "replace")
        a.Show()
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'update the text box's size.
        'the equivalent of docking, i think... or anchor
        While True
            Try
                BackgroundWorker1.ReportProgress(10)
            Catch ex As Exception
            End Try
            Threading.Thread.Sleep(20)
        End While
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        'move it below the top stuff.
        TextBox1.Location = New Drawing.Point(0, 49)
        'then set the bottom bounds.
        'Unnecessary
        'If My.Settings.ShowStatusBar Then
        '    TextBox1.Size = New Size(Me.Size.Width - 15, Me.Size.Height - 110)
        'Else
        '    TextBox1.Size = New Size(Me.Size.Width - 15, Me.Size.Height - 90)
        'End If
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        'Redirect
        NewToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        'Redirect
        OpenToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        'Redirect
        SaveToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        'Redirect
        PrintToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click
        'Redirect
        CutToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click
        'Redirect
        CopyToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PasteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click
        'Redirect
        PasteToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click
        'Redirect
        HelpToolStripMenuItem1_Click(sender, e)
    End Sub

    Private Sub SaveFileAsBytecodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveFileAsBytecodeToolStripMenuItem.Click
        'this saves the file's bytecode.
        SaveFileDialog1.Filter = ""
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            'encapsulating it all...
            Try
                Dim bytes() = TextBox1.Text.Split(CChar(" "))
                Dim bytes2(bytes.Count) As Byte
                For i = 0 To bytes.Count - 1
                    bytes2(i) = CByte(bytes(i))
                Next
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, bytes2)
                NotifyIcon1.ShowBalloonTip(5, "Notepad X", "Saved File Successfully!", Nothing)
                Me.Text = "Notepad X" & IO.Path.GetFileName(OpenFileDialog1.FileName)
                TextBox1.Modified = False
            Catch ex As Exception
                MsgBox(ex.ToString)
                Log.WriteLine("Error: " & vbCrLf & ex.ToString & vbCrLf)
            End Try
        End If
    End Sub

    Private Sub EncryptToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem2.Click
        Dim myEnc As System.Text.Encoding
        myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
        'create encryption lib
        Dim rijndaelLib As New EncryptionLib.Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
        Dim encryptedStr As String
        encryptedStr = CStr(rijndaelLib.Encrypt(TextBox1.Text))
        save(encryptedStr)
        TextBox1.Text = encryptedStr
        Log.WriteLine("Encrypted " & Document.Path & " With Rindael.")
    End Sub

    Private Sub DecryptToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptToolStripMenuItem2.Click
        Dim myEnc As System.Text.Encoding
        myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
        'create Decryption lib
        Dim rijndaelLib As New EncryptionLib.Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
        Dim decryptedStr As String
        decryptedStr = CStr(rijndaelLib.Decrypt(TextBox1.Text))
        TextBox1.Text = decryptedStr
        Log.WriteLine("Decrypted " & Document.Path & " With Rijndael.")
    End Sub

    Private Sub EncryptToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem3.Click
        'create encryption lib
        Dim desLib As New EncryptionLib.DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
        Dim encryptedStr As String
        encryptedStr = CStr(desLib.Encrypt(TextBox1.Text))
        save(encryptedStr)
        TextBox1.Text = encryptedStr
        Log.WriteLine("Encrypted " & Document.Path & " With DES")
    End Sub

    Private Sub DecryptToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptToolStripMenuItem3.Click
        'create Decryption lib
        Dim desLib As New EncryptionLib.DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
        Dim decryptedStr As String
        decryptedStr = CStr(desLib.Decrypt(TextBox1.Text))
        TextBox1.Text = decryptedStr
        Log.WriteLine("Decrypted " & Document.Path & " With DES")
    End Sub

    Private Sub EncryptToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem4.Click
        'create encryption lib
        Dim desLib As New EncryptionLib.Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
        Dim encryptedStr As String
        encryptedStr = CStr(desLib.Encrypt(TextBox1.Text))
        save(encryptedStr)
        TextBox1.Text = encryptedStr
        Log.WriteLine("Encrypted " & Document.Path & " With Triple DES")
    End Sub

    Private Sub DecryptToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptToolStripMenuItem4.Click
        'create decryption lib
        Dim desLib As New EncryptionLib.Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
        Dim decryptedStr As String
        decryptedStr = CStr(desLib.Decrypt(TextBox1.Text))
        TextBox1.Text = decryptedStr
        Log.WriteLine("Decrypted " & Document.Path & " With Triple DES")
    End Sub

    Private Sub ViewEncryptionSamplesFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewEncryptionSamplesFormToolStripMenuItem.Click
        Dim NEUIF As New EncryptionLib.Encryption_UI
        NEUIF.ShowDialog()
    End Sub

    Private Sub TextFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextFileToolStripMenuItem.Click
        Dim FN2 As String = Document.Path & ".TXT"
        If Document.Path = "" Then
            MsgBox("No File Open!")
        Else
            Document.Save(False, FN2)
            MsgBox("Look for " & FN2)
            Log.WriteLine("Exported " & Document.Path & " To " & FN2)
        End If

    End Sub

    Private Sub EncryptedTextFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptedTextFileToolStripMenuItem.Click
        If Document.Path = "" Then
            MsgBox("No File Open!")
        Else
            Dim code As Integer
            Dim FN2 As String = Document.Path & ".ETXT"
            Dim strCode As String = InputBox("ASCII Code: ")
            'an integer to encrypt the files.
            Try
                code = CInt(strCode)
            Catch ex As Exception
                MsgBox(ex.ToString)
                Log.WriteLine("Error: " & vbCrLf & ex.ToString & vbCrLf)
                Return
            End Try
            'if the code passed CInt()ing, then
            'encrypt the file
            Dim encrypted As String = encryptAsc.Encrypt(TextBox1.Text, code)
            Document.Save(False, FN2)
            MsgBox("Look for " & FN2)
            Log.WriteLine("Exported " & Document.Path & " To " & FN2)
        End If
    End Sub

    Private Sub PDFFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDFFileToolStripMenuItem.Click
        If Document.Path = "" Then
            MsgBox("No File Open!")
        Else
            Dim text() As String = TextBox1.Text.Split(CChar(vbCrLf))
            Dim textToInsert As String = ""
            Dim line As String
            For Each line In text
                line = line.Trim()
                line = line.Replace(vbCrLf, "")
                textToInsert &= "(" & line & ") Tj" & vbCrLf & "T*" & vbCrLf
            Next
            Dim strToWrite = "%PDF-1.5" & vbCrLf & _
"1 0 obj<</Type/Catalog/Pages 2 0 R/PageMode/UseOutlines/PageLabels 5 0 R/Outlines 14 0 R>>endobj" & vbCrLf & _
"2 0 obj<</Type/Pages/Kids[12 0 R]/Count 1/Resources 3 0 R/MediaBox[0 0 595 842]>>endobj" & vbCrLf & _
"3 0 obj<</Font<</f6 6 0 R/f7 7 0 R/f11 11 0 R>>>>endobj" & vbCrLf & _
"4 0 obj<</Producer(Notepad X)/CreationDate(D:20110112042949-09'00')/Creator(" & Application.ExecutablePath & ")/Author(Notepad X)>>endobj" & vbCrLf & _
"5 0 obj<</Nums[0<</S/r>> 3<</S/D>>]>>endobj" & vbCrLf & _
"6 0 obj<</Type/Font/Subtype/Type1/BaseFont/Courier>>endobj" & vbCrLf & _
"7 0 obj<</Type/Font/Subtype/Type1/BaseFont/Helvetica>>endobj" & vbCrLf & _
"8 0 obj<</Registry(Adobe)/Ordering(GB1)/Supplement 4>>endobj" & vbCrLf & _
"9 0 obj<</Type/FontDescriptor/FontName/AdobeSongStd-Light-Acro/Flags 5/FontBBox[-134 -254 1001 905]/FD<</HRoman<</Type/FontDescriptor/FontBBox[-134 -254 434 905]>>>>>>endobj" & vbCrLf & _
"10 0 obj<</Type/Font/Subtype/CIDFontType0/BaseFont/AdobeSongStd-Light/CIDSystemInfo 8 0 R/FontDescriptor 9 0 R>>endobj" & vbCrLf & _
"11 0 obj<</Type/Font/Subtype/Type0/DescendantFonts[10 0 R]/BaseFont/AdobeSongStd-Light/Encoding/UniGB-UTF16-H>>endobj" & vbCrLf & _
"12 0 obj<</Type/Page/Parent 2 0 R/Contents 13 0 R>>endobj" & vbCrLf & _
"13 0 obj<</Length 60>>stream" & vbCrLf & _
"BT" & vbCrLf & _
"/f6 12 Tf" & vbCrLf & _
"0 Tr" & vbCrLf & _
"18 TL" & vbCrLf & _
"100 742 Td" & vbCrLf & _
"INSERT_TEXT_HERE" & _
"ET" & vbCrLf & _
"endstream" & vbCrLf & _
"endobj" & vbCrLf & _
"14 0 obj<</Type/Outlines/Last 15 0 R/First 15 0 R/Count 1>>endobj" & vbCrLf & _
"15 0 obj<</Title(Page 1)/Dest[12 0 R/Fit]/Parent 14 0 R>>endobj" & vbCrLf & _
"xref" & vbCrLf & _
"0 16" & vbCrLf & _
"0000000000 65535 f" & vbCrLf & _
"0000000010 00000 n" & vbCrLf & _
"0000000108 00000 n" & vbCrLf & _
"0000000197 00000 n" & vbCrLf & _
"0000000254 00000 n" & vbCrLf & _
"0000000483 00000 n" & vbCrLf & _
"0000000528 00000 n" & vbCrLf & _
"0000000588 00000 n" & vbCrLf & _
"0000000650 00000 n" & vbCrLf & _
"0000000712 00000 n" & vbCrLf & _
"0000000887 00000 n" & vbCrLf & _
"0000001007 00000 n" & vbCrLf & _
"0000001126 00000 n" & vbCrLf & _
"0000001185 00000 n" & vbCrLf & _
"0000001294 00000 n" & vbCrLf & _
"0000001361 00000 n" & vbCrLf & _
"trailer" & vbCrLf & _
"<</Root 1 0 R/Info 4 0 R/Size 16/ID[<D41D8CD98F00B204E9800998ECF8427E><D41D8CD98F00B204E9800998ECF8427E>]>>" & vbCrLf & _
"startxref" & vbCrLf & _
"1426" & vbCrLf & _
"%%EOF" & vbCrLf
            strToWrite = strToWrite.Replace("INSERT_TEXT_HERE", textToInsert)
            Dim file As String = Document.Path & ".PDF"
            Document.Save(False, file)
            MsgBox("Look for " & file)
            Log.WriteLine("Exported " & Document.Path & " To " & file)
        End If
    End Sub

    Private Sub EncryptToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem5.Click
        Dim aesLib As New AES_Lib(New CreateObjects().CreateAESKey, New CreateObjects().CreateAESIV)
        'create encryption lib
        Dim encryptedStr As String
        encryptedStr = CStr(aesLib.Encrypt(TextBox1.Text))
        save(encryptedStr)
        TextBox1.Text = encryptedStr
        Log.WriteLine("Encrypted " & Document.Path & " With AES")
    End Sub

    Private Sub DecryptToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptToolStripMenuItem5.Click
        Dim aesLib As New AES_Lib(New CreateObjects().CreateAESKey, New CreateObjects().CreateAESIV)
        'create encryption lib
        Dim encryptedStr As String
        encryptedStr = CStr(aesLib.Decrypt(TextBox1.Text))
        TextBox1.Text = encryptedStr
        Log.WriteLine("Decrypted " & Document.Path & " With AES")
    End Sub

    Private Sub EncryptToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem6.Click
        Dim rc2Lib As New RC2_Lib(New CreateObjects().CreateRC2Key, New CreateObjects().CreateRC2IV)
        'create encryption lib
        Dim encryptedStr As String
        encryptedStr = CStr(rc2Lib.Encrypt(TextBox1.Text))
        save(encryptedStr)
        TextBox1.Text = encryptedStr
        Log.WriteLine("Encrypted " & Document.Path & " With RC2")
    End Sub

    Private Sub DecryptToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptToolStripMenuItem6.Click
        Dim rc2Lib As New RC2_Lib(New CreateObjects().CreateRC2Key, New CreateObjects().CreateRC2IV)
        'create encryption lib
        Dim encryptedStr As String
        encryptedStr = CStr(rc2Lib.Decrypt(TextBox1.Text))
        save(encryptedStr)
        TextBox1.Text = encryptedStr
        Log.WriteLine("Encrypted " & Document.Path & " With RC2")
    End Sub

    Private Sub EncryptToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem7.Click
        Dim rsaLib As New RSA_Lib
        Dim encryptedStr As String
        Try
            encryptedStr = CStr(rsaLib.Encrypt(TextBox1.Text))
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine("Error: " & vbCrLf & ex.ToString & vbCrLf)
            encryptedStr = ex.ToString
        End Try
        save(encryptedStr)
        TextBox1.Text = encryptedStr
        Log.WriteLine("Encrypted " & Document.Path & " With RSA")
    End Sub

    Private Sub DecryptyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptyToolStripMenuItem.Click
        Dim rsaLib As New RSA_Lib
        Dim encryptedStr As String
        Try
            encryptedStr = CStr(rsaLib.Decrypt(TextBox1.Text))
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
            encryptedStr = ex.ToString
        End Try
        TextBox1.Text = encryptedStr
        Log.WriteLine(String.Format("Encrypted {0} With RSA", Document.Path))
    End Sub

    Private Sub ViewUserGuideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Process.Start(IO.Path.GetDirectoryName(Reflection.Assembly.GetAssembly(GetType(NotepadX.MDIParent)).Location) & "\Documents\Notepad X.docx")
        Log.WriteLine("Viewed User Guide. Yay! ")
    End Sub

    Private Sub TextBox1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles TextBox1.LinkClicked
        Dim N As New Web_Browser.Form1
        N.Show()
        N.NavigateTo(e.LinkText)
    End Sub
    Private Sub MacrosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MacrosToolStripMenuItem.Click
        Dim Macros As New Macros(Me)
        Macros.Show()
    End Sub

    Private Sub ViewMacroGuideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Process.Start(IO.Path.GetDirectoryName(Reflection.Assembly.GetAssembly(GetType(NotepadX.MDIParent)).Location) & "\Documents\Notepad X Macros.docx")
        Log.WriteLine("Viewed Macro Guide. Yay! ")
    End Sub

    Private Sub AutoSaveTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoSaveTimer.Tick
        If Document.Path = "" Then Return
        Try
            Dim str As String = currentFileStatusLabel.Text
            currentFileStatusLabel.Text = "Autosaving file..."
            Dim tmpPath As String = "C:\ProgramData\mlnlover11 Productions\Notepad X\AutoSave"
            Dim tmpName As String = IO.Path.GetFileNameWithoutExtension(Document.Path) & ".save"
            Dim fullname As String = String.Format("{0}\{1}", tmpPath, tmpName)
            TextBox1.SaveFile(fullname)
            currentFileStatusLabel.Text = str
        Catch ex As Exception
        End Try
    End Sub

    Sub LoadMacro(ByVal filePath As String)
        Dim Macros As New Macros(Me)
        Macros.macroPathTextBox.Text = filePath
        Macros.Show()
        Macros.runMacroButton.PerformClick()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If FontDialog1.ShowDialog() = DialogResult.OK Then
            TextBox1.SelectionFont = FontDialog1.Font
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.SelectionColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub TextBox1_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        TextBox1_TextChanged(sender, e)
    End Sub

    Public Sub InsertText(ByVal text As String) Implements Plugins.ITextEditorForm.InsertText
        TextBox1.Text &= text
    End Sub

    Public Sub OpenDocument(ByVal name As String) Implements Plugins.ITextEditorForm.OpenDocument
        open(name)
    End Sub

    Public Sub Print() Implements Plugins.ITextEditorForm.Print
        PrintToolStripButton_Click(Nothing, EventArgs.Empty)
    End Sub

    Public Sub Save1() Implements Plugins.ITextEditorForm.Save
        save(TextBox1.Text)
    End Sub

    Public Sub SaveAs() Implements Plugins.ITextEditorForm.SaveAs
        SaveAsToolStripMenuItem_Click(Nothing, EventArgs.Empty)
    End Sub

    Public Sub ShowPrintPreview() Implements Plugins.ITextEditorForm.ShowPrintPreview
        MsgBox("Not Enabled for this Form!")
    End Sub

    Public Sub ShowPrintSetup() Implements Plugins.ITextEditorForm.ShowPrintSetup
        PrintSetupToolStripMenuItem_Click(Nothing, EventArgs.Empty)
    End Sub
End Class
