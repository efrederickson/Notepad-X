Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports System.Reflection

Public Class Main
    Implements NotepadX.IPlugin
    Dim _ti As ToolStripMenuItem

    Public ReadOnly Property Author As String Implements NotepadX.IPlugin.Author
        Get
            Return "Elijah Frederickson"
        End Get
    End Property

    Public ReadOnly Property Description As String Implements NotepadX.IPlugin.Description
        Get
            Return "The Code Editing Utility for Notepad X"
        End Get
    End Property

    Public Sub Dispose() Implements NotepadX.IPlugin.Dispose
        _ti.Dispose()
    End Sub

    Public ReadOnly Property DownloadURL As String Implements NotepadX.IPlugin.DownloadURL
        Get
            Return ""
        End Get
    End Property

    Public Sub Initialize() Implements NotepadX.IPlugin.Initialize
        _ti = New ToolStripMenuItem("New Code Editor", Nothing, AddressOf NewCodeEditorToolStripMenuItem_Click) With {
            .ShortcutKeys = Keys.Control + Keys.Shift + Keys.N
        }
    End Sub

    Public ReadOnly Property MenuItemPath As String Implements NotepadX.IPlugin.MenuItemPath
        Get
            Return "file/"
        End Get
    End Property

    Public ReadOnly Property Name As String Implements NotepadX.IPlugin.Name
        Get
            Return "Code Editor"
        End Get
    End Property

    Public ReadOnly Property OriginalFileName As String Implements NotepadX.IPlugin.OriginalFileName
        Get
            Return DownloadURL
        End Get
    End Property

    Public ReadOnly Property ToolStripItem As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.IPlugin.ToolStripItem
        Get
            Return _ti
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.IPlugin.Version
        Get
            Return "1.0"
        End Get
    End Property


    Private Sub NewCodeEditorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim pb As New Library.Controls.ProgressBar
        pb.TextValue = "Loading Syntax Documents..."
        pb.TextShow = Library.Controls.ProgressBar.eTextShow.TextOnly
        pb.Parent = NotepadX.Main.MDIParent1
        pb.Location = New Point(10, 10)
        pb.Size = New Size(10, 10)
        pb.Dock = DockStyle.Fill
        pb.BarColorSolid = Color.Green
        pb.BackColor = Color.Orange
        pb.Maximum = 100
        pb.Value = 0
        pb.Minimum = 0
        pb.TextPlacement = Library.Controls.ProgressBar.eTextPlacement.OverBar
        Dim strms As List(Of Stream) = New LanguageList().LanguageList
        Dim list As New CodeEditor.SyntaxDefinitionList
        Directory.CreateDirectory(Application.LocalUserAppDataPath & "\SyntaxDefinitions\")
        pb.Maximum = strms.Count
        For i = 0 To strms.Count - 1
            pb.TextValue = String.Format("Loading Syntax Documents... {0}% Completed ({1} out of {2})", pb.ValuePercent, pb.Value, pb.Maximum)
            Dim langStream As Stream = strms(i)
            Dim path As String = String.Format("{0}\SyntaxDefinitions\{1}", Application.LocalUserAppDataPath, IO.Path.GetFileName(IO.Path.GetTempFileName))
            Dim file As New FileStream(path, FileMode.Create)
            If langStream Is Nothing Then
                MsgBox(String.Format("Stream {0} Is empty. Please restart Notepad X!", i))
            Else
                langStream.CopyTo(file)
                langStream.Close()
            End If
            file.Close()
            If langStream IsNot Nothing Then
                list.GetLanguageFromFile(path)
            End If
            pb.Value += 1
        Next
        Dim files() As String = IO.Directory.GetFiles(SpecialDirectories.MyDocuments & "\Notepad X\SyntaxFiles\")
        pb.Value = 0
        pb.Maximum = files.Count
        If files.Count > 0 Then
            For i = 0 To files.Count - 1
                pb.TextValue = String.Format("Loading User Syntax Files... {0}% Completed ({1} out of {2})", pb.ValuePercent, pb.Value, pb.Maximum)
                list.GetLanguageFromFile(files(i))
                pb.Value += 1
            Next
        ElseIf files.Count = 0 Then
            pb.TextValue = "No User Syntax Files to load!"
        Else
            MsgBox("ERROR")
        End If
        pb.Dispose()
        Dim lang As New LanguageForm(list)
        lang.ShowDialog()
        If lang.DialogResult = DialogResult.OK Then
            'lang.EditForm.MdiParent = Me
            'lang.EditForm = Module1.IconManager.Check(lang.EditForm)
            'lang.EditForm = Module1.LanguageManager.Check(lang.EditForm)
            lang.EditForm.Show(NotepadX.Main.MDIParent1.DockPanel1)
        End If
    End Sub

    Public ReadOnly Property AboutPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.AboutPage
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property HasAboutPage As Boolean Implements NotepadX.IPlugin.HasAboutPage
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property HasHelpPage As Boolean Implements NotepadX.IPlugin.HasHelpPage
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property HasOptionsPage As Boolean Implements NotepadX.IPlugin.HasOptionsPage
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property HelpPage As TabPage Implements NotepadX.IPlugin.HelpPage
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property OptionsPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.OptionsPage
        Get
            Return Nothing
        End Get
    End Property

    Sub New()
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, New ResolveEventHandler(AddressOf Resolver)
    End Sub

    Shared Function Resolver(ByVal sender As Object, ByVal args As ResolveEventArgs) As System.Reflection.Assembly
        Dim a1 As Assembly = Assembly.GetExecutingAssembly()
        Dim s As Stream = a1.GetManifestResourceStream("CodeEditor.SyntaxBox.dll")
        If s Is Nothing Then
            Throw New Exception("Cannot Load the SyntaxBox!")
        End If
        Dim block As Byte() = New Byte(s.Length){}
        s.Read(block, 0, block.Length)
        Dim a2 As Assembly = Assembly.Load(block)
        Return a2
    End Function

End Class
