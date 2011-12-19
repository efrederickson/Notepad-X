Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports NotepadX.Plugins

Public Class MDIParent
    Private m_ChildFormNumber As Integer
    Private ReadOnly NOTEPAD_ICO As New Icon(IO.Path.GetDirectoryName(Reflection.Assembly.GetAssembly(GetType(NotepadX.MDIParent)).Location) & "\NOTEPAD.ICO")

#Region " Event Handlers "

    Private Sub PrintPreviewToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewToolStripMenuItem1.Click
        Try
            Dim doc As Plugins.ITextEditorForm = CType(DockPanel1.ActiveDocument, Plugins.ITextEditorForm)
            doc.ShowPrintPreview()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click, NewWindowToolStripMenuItem.Click
        ' Create a new instance of the child form.
        Dim ChildForm As New TextEditor
        'ChildForm = Module1.IconManager.Check(ChildForm)
        'ChildForm = Module1.LanguageManager.Check(ChildForm)
        ' Make it a child of this MDI form before showing it.
        'ChildForm.MdiParent = Me
        m_ChildFormNumber += 1
        ChildForm.Text = "Notepad X Window " & m_ChildFormNumber

        ChildForm.Show(DockPanel1)
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub MDIParent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel.Text = "Documents: " & Microsoft.VisualBasic.CompilerServices.Conversions.ToString(Me.DockPanel1.DocumentsCount)
        If Me.DockPanel1.DocumentsCount = 0 Then
            ToolStripMenuItem1.Visible = False
            Me.Text = "Notepad X"
        Else
            ToolStripMenuItem1.Visible = True
            Try
                Me.Text = CType(Me.DockPanel1.ActiveContent, Form).Text
            Catch ex As Exception
                Log.WriteLine("MDIParent: Error: " & ex.ToString())
            End Try
        End If
        '''''''''SET ICONS''''''''''''''''''''''''''''
        For Each frm As Form In DockPanel1.Documents
            If frm.Icon Is Nothing Or frm.Icon IsNot NOTEPAD_ICO Then
                frm.Icon = NOTEPAD_ICO
            End If
        Next

        If Not Me.Text.ToLower().StartsWith("notepad x") Or Me.Text.ToLower().StartsWith("notepadx") Then
            Me.Text = "Notepad X " & Me.Text
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AboutBox1.ShowDialog()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        optionsForm.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
        optionsForm.Show(DockPanel1)
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        If Me.DockPanel1.DocumentsCount <> 0 Then
            Try
                Dim frm As ITextEditorForm = CType(DockPanel1.ActiveContent, ITextEditorForm)
                frm.Save()
            Catch ex As Exception
                'Dim frm As EditForm = CType(DockPanel1.ActiveContent, EditForm)
                'frm.SaveAs(frm.Doc.Path)
            End Try
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        If Me.DockPanel1.Documents.Count = 0 Then Return
        Try
            Dim frm As ITextEditorForm = CType(DockPanel1.ActiveContent, ITextEditorForm)
            frm.Print()
        Catch ex As Exception
            'Dim frm As EditForm = CType(DockPanel1.ActiveContent, EditForm)
            'frm.PrintToolStripMenuItem.PerformClick()
        End Try
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        If Me.DockPanel1.Documents.Count = 0 Then Return
        Try
            Dim frm As ITextEditorForm = CType(DockPanel1.ActiveContent, ITextEditorForm)
            frm.ShowPrintSetup()
        Catch ex As Exception
            'Dim frm As EditForm = CType(DockPanel1.ActiveContent, EditForm)
            'frm.PrintSetupToolStripMenuItem.PerformClick()

        End Try
    End Sub

    Private Sub OpenAWebPageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenAWebPageToolStripMenuItem.Click
        Dim N As New Web_Browser.Form1
        N.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document
        N.Show(DockPanel1)
        N.NavigateTo("http://www.google.com")
    End Sub

    Private Sub CloseCurrentFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseCurrentFormToolStripMenuItem.Click
        If Me.DockPanel1.Documents.Count <> 0 Then
            CType(Me.DockPanel1.ActiveContent, Form).Close()
        ElseIf Me.MdiChildren.Length = 0 Then
            Me.Close()
        End If
    End Sub

    Private Sub RecoverAFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoverAFileToolStripMenuItem.Click
        Dim OpenFileDialog1 As New OpenFileDialog
        Dim SaveFileDialog1 As New SaveFileDialog
        ' Create a new instance of the child form.
        Dim ChildForm As New TextEditor
        'ChildForm = Module1.IconManager.Check(ChildForm)
        'ChildForm = Module1.LanguageManager.Check(ChildForm)
        SaveFileDialog1 = ChildForm.SaveFileDialog1
        ' Make it a child of this MDI form before showing it.
        'ChildForm.MdiParent = Me
        m_ChildFormNumber += 1
        ChildForm.Text = "Notepad X Window " & m_ChildFormNumber

        OpenFileDialog1.Filter = "Notepad X Autosaves (*.save)|*.save"
        OpenFileDialog1.InitialDirectory = "C:\ProgramData\mlnlover11 Productions\Notepad X\AutoSave"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                ChildForm.open(OpenFileDialog1.FileName)
                ChildForm.save(ChildForm.TextBox1.Text)
                ChildForm.open(SaveFileDialog1.FileName)
                ChildForm.Show(DockPanel1)
            End If
        End If
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem1.Click
        helpForm.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub ViewUserGuideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewUserGuideToolStripMenuItem.Click
        Process.Start(IO.Path.GetDirectoryName(Reflection.Assembly.GetAssembly(GetType(NotepadX.MDIParent)).Location) & "\Documents\Notepad X.docx")
        Log.WriteLine("Viewed User Guide. Yay! ")
    End Sub

    Private Sub ViewMacroGuideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewMacroGuideToolStripMenuItem.Click
        Process.Start(IO.Path.GetDirectoryName(Reflection.Assembly.GetAssembly(GetType(NotepadX.MDIParent)).Location) & "\Documents\Notepad X Macros.docx")
        Log.WriteLine("Viewed Macro Guide. Yay! ")
    End Sub

    Private Sub SetDefaultItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetDefaultItemsToolStripMenuItem.Click
        Me.Visible = False
        RegEntries.ShowDialog()
        Me.Visible = True
    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        If My.Application.IsNetworkDeployed Then
            If My.Application.Deployment.CheckForUpdate(True) = True Then
                MsgBox("There are updates available!")
            Else
                MsgBox("You have the current version!")
            End If
        Else
            Install.ShowDialog()
        End If
    End Sub

    Private Sub InstallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallToolStripMenuItem.Click
        Install.ShowDialog()
    End Sub

    Private Sub FeatureRequestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FeatureRequestToolStripMenuItem.Click
        Dim wb As New Web_Browser.Form1()
        wb.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document
        wb.Show(DockPanel1)
        wb.NavigateTo("http://sourceforge.net/projects/notepadx/forums/forum/1832682")
    End Sub

    Private Sub EncryptionExamplesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptionExamplesToolStripMenuItem.Click
        Dim eui As New EncryptionLib.Encryption_UI() With {
            .ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document
        }
        eui.Show(DockPanel1)
        eui.BringToFront()
        eui.TopMost = True
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        OPEN()
    End Sub

    Private Sub PackageCreatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PackageCreatorToolStripMenuItem.Click
        Dim pkg As New PackageForm
        pkg.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
        'pkg.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
        pkg.Text = "Package Creator"
        pkg.Show(DockPanel1)
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        CType(DockPanel1.ActiveContent, Form).Close()
    End Sub

    Private Sub CloseAllDocumentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseAllDocumentsToolStripMenuItem.Click
        For Each frm As Form In DockPanel1.Documents
            frm.Close()
        Next
    End Sub

    Private Sub DownloadTheNotepadXSourceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadTheNotepadXSourceToolStripMenuItem.Click
        Dim wb As New Web_Browser.Form1()
        wb.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document
        wb.Show(DockPanel1)
        wb.NavigateTo("http://sourceforge.net/projects/notepadx/files/Notepad%20X.zip")
        'If dl.ShowDialog = DialogResult.Yes Then
        '    MsgBox("Successfully downloaded the Notepad X Source!")
        'End If
    End Sub
#End Region

#Region " Sub New"
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
#End Region

#Region " Single-Instance Application Calls"

    Public Delegate Sub ProcessParametersDelegate(ByVal sender As Object, ByVal args As String())

    Public Sub ProcessParameters(ByVal sender As Object, ByVal args As String())
        Me.BringToFront()
        OPEN(args(0))
        Me.BringToFront()
    End Sub

#End Region

    Sub OPEN(Optional ByVal ___filename As String = "")
        If ___filename = "" Then
            Dim OpenFileDialog As New OpenFileDialog()
            OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog.Filter = "Text File (*.txt)|*.txt|" & "Encrypted Text File (*.etxt)|*.etxt|" & "Rich Text File (*.rtf)|*.rtf|" & "Notepad X Macro (*.nxm)|*.nxm|" & "All Files (*.*)|*.*"
            
            If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                Dim FileName As String = OpenFileDialog.FileName

                Dim ChildForm As New TextEditor
                'ChildForm.MdiParent = Me
                'ChildForm = Module1.IconManager.Check(ChildForm)
                'ChildForm = Module1.LanguageManager.Check(ChildForm)
                m_ChildFormNumber += 1
                ChildForm.Text = "Notepad X Window " & m_ChildFormNumber
                ChildForm.Show(DockPanel1)
                ChildForm.open(FileName)
            End If
        Else
            Dim ChildForm As New TextEditor
            'ChildForm.MdiParent = Me
            'ChildForm = Module1.IconManager.Check(ChildForm)
            m_ChildFormNumber += 1
            ChildForm.Text = "Notepad X Window " & m_ChildFormNumber
            ChildForm.Show(DockPanel1)
            ChildForm.open(___filename)
        End If
    End Sub

    Sub ProcessCommandLineArguments(ByVal args As String())
        ' Create a new instance of the child form.
        Dim ChildForm As New TextEditor
        ' Make it a child of this MDI form before showing it.
        'ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        'ChildForm = Module1.IconManager.Check(ChildForm)
        'ChildForm = Module1.LanguageManager.Check(ChildForm)
        ChildForm.Text = "Notepad X Window " & m_ChildFormNumber
        ChildForm.Show(DockPanel1)

        If args.Length = 3 Then
            If args(2).ToLower() = "encrypt" Then
                Dim frm As New Encryptor(args(1))
                frm.ShowDialog()
                End
            ElseIf args(2).ToLower() = "decrypt" Then
                Dim frm As New Decryptor(args(1))
                frm.ShowDialog()
                End
            ElseIf args(2) = "-h" Then
                helpForm.ShowDialog()
                End
            ElseIf args(1).ToLower.EndsWith(".nxm") Then
                ChildForm.LoadMacro(args(1))
                'ChildForm.Show(DockPanel1)
            Else
                ChildForm.open(args(1))
                'ChildForm.Show(DockPanel1)
            End If
        ElseIf args.Length = 2 AndAlso args(1) = "-h" Then
            helpForm.ShowDialog()
            End
        ElseIf args.Length = 2 AndAlso args(1).ToLower.EndsWith(".nxm") Then
            ChildForm.LoadMacro(args(1))
            'ChildForm.Show(DockPanel1)
        ElseIf args.Length = 2 Then
            ChildForm.open(args(1))
            'ChildForm.Show(DockPanel1)
        End If
    End Sub

    Private LSharpInteractiveForm as LSharpForm
    Sub LToolStripMenuItem_Click(sender As Object, e As EventArgs)
    	If LSharpInteractiveform Is Nothing Then
    		LSharpInteractiveForm = New LSharpForm()
    		LSharpInteractiveForm.Show(DockPanel1)
    		LSharpInteractiveForm.SetSize()
    		LSharpInteractiveForm.Visible = True
    		return
    	End If
    	If lsharpinteractiveform.Visible = True Then
    		lsharpInteractiveForm.Visible = False
    	Else
    		lsharpInteractiveForm.Visible = True
    	End If
    End Sub
End Class
