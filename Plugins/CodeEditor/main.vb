Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports System.Reflection

Public Class Main
    Implements NotepadX.Plugins.IPlugin
    Dim _ti As ToolStripMenuItem
    
    Public ReadOnly Property Author As String Implements NotepadX.Plugins.IPlugin.Author
        Get
            Return "Elijah Frederickson"
        End Get
    End Property
    
    Public ReadOnly Property Description As String Implements NotepadX.Plugins.IPlugin.Description
        Get
            Return "The Code Editing Utility for Notepad X"
        End Get
    End Property
    
    Public Function Dispose() As Boolean Implements NotepadX.Plugins.IPlugin.Dispose
        Return true
    End Function
    
    Public ReadOnly Property UpdateUrl As String Implements NotepadX.Plugins.IPlugin.UpdateUrl
        Get
            Return ""
        End Get
    End Property
    
    Public Function Initialize() As Boolean Implements NotepadX.Plugins.IPlugin.Initialize
        Dim list As New CodeEditor.SyntaxDefinitionList
        If Not System.IO.File.Exists(Application.LocalUserAppDataPath & "\hasExtractedCodeSyntax") Then
            Dim strms As List(Of Stream) = New LanguageList().LanguageList
            Directory.CreateDirectory(Application.LocalUserAppDataPath & "\SyntaxDefinitions\")
            For i = 0 To strms.Count - 1
                Dim langStream As Stream = strms(i)
                Dim path As String = String.Format("{0}\SyntaxDefinitions\{1}.syn", Application.LocalUserAppDataPath, IO.Path.GetFileName(IO.Path.GetTempFileName))
                Dim file As New FileStream(path, FileMode.Create)
                If langStream Is Nothing Then
                    MsgBox(String.Format("Stream {0} is empty. Please restart Notepad X!", i))
                Else
                    langStream.CopyTo(file)
                    langStream.Close()
                End If
                file.Close()
                If langStream IsNot Nothing Then
                    list.GetLanguageFromFile(path)
                End If
            Next
            System.IO.File.Create(Application.LocalUserAppDataPath  & "\hasExtractedCodeSyntax")
        Else
            Dim files234() As String = IO.Directory.GetFiles(Application.LocalUserAppDataPath & "\SyntaxDefinitions\")
            If files234.Count > 0 Then
                For i = 0 To files234.Count - 1
                    list.GetLanguageFromFile(files234(i))
                Next
            Else
                System.IO.File.Delete(Application.LocalUserAppDataPath  & "\hasExtractedCodeSyntax")
                MessageBox.Show("Error: no syntax files! Please restart Notepad X")
            End If
        End If
        Dim files() As String = IO.Directory.GetFiles(SpecialDirectories.MyDocuments & "\Notepad X\SyntaxFiles\")
        If files.Count > 0 Then
            For i = 0 To files.Count - 1
                list.GetLanguageFromFile(files(i))
            Next
        End If
        Dim lang As New LanguageForm(list)
        lang.ShowDialog()
        'If lang.DialogResult = DialogResult.OK Then
            'lang.EditForm.MdiParent = Me
            'lang.EditForm = Module1.IconManager.Check(lang.EditForm)
            'lang.EditForm = Module1.LanguageManager.Check(lang.EditForm)
            'lang.EditForm.Show(NotepadX.Main.MDIParent1.DockPanel1)
        'End If
        Return True
    End Function
    
    Public ReadOnly Property Name As String Implements NotepadX.Plugins.IPlugin.Name
        Get
            Return "Allows many new code file types to be opened"
        End Get
    End Property
    
    Public ReadOnly Property Version As String Implements NotepadX.Plugins.IPlugin.Version
        Get
            Return "1.0"
        End Get
    End Property
    
    Public ReadOnly Property AboutPage As System.Windows.Forms.TabPage Implements NotepadX.Plugins.IPlugin.AboutPage
        Get
            Return Nothing
        End Get
    End Property
    
    Public ReadOnly Property HelpPage As TabPage Implements NotepadX.Plugins.IPlugin.HelpPage
        Get
            Return Nothing
        End Get
    End Property
    
    Public ReadOnly Property OptionsPage As System.Windows.Forms.TabPage Implements NotepadX.Plugins.IPlugin.OptionsPage
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
