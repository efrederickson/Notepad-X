Imports System.IO

Public Module Main
    'I know this isn't good practice, but it works,
    'and surprisingly well. Especially for the plugins...
    Public output As StreamWriter
    Public line As String
    Public ReadOnly StartupPath As String = IO.Path.GetDirectoryName(Reflection.Assembly.GetAssembly(GetType(NotepadX.MDIParent)).Location)
    Public ReadOnly NotepadX_DocumentPath As String = FileIO.SpecialDirectories.MyDocuments & "\Notepad X"
    Public PluginManager As PluginService ' Initialized in StartInfoScreen
    Public MDIParent1 As MDIParent ' used for Plugins, mainly
    Public encryptionCodes As New Hashtable
    'file / key
    Public decryptionCodes As New Hashtable
    'file / key
    Public ReadOnly LogPath As String = String.Format("C:\ProgramData\mlnlover11 Productions\Notepad X\{0}.log", Fix(My.User.Name))
    Public FilesToDelete As New List(Of String)
    Public PluginInterface As String = "NotepadX.IPlugin"
    'Public IconManager As New IconManager
    'Public LanguageManager As New LanguageManager
    Public DefaultSaveLocation As String = My.Settings.DefaultSaveLocation
    Public LSharpEnvironment As New LSharp.Environment()
    
    ''' <summary>
    ''' The Log for Notepad X
    ''' </summary>
    ''' <remarks></remarks>
    Class Log
        Shared Sub WriteLine(ByVal Text As String)
            Using _log As New IO.StreamWriter(LogPath, True)
                Dim Time As String = DateTime.Now.ToString & "> "
                _log.WriteLine(Time & Text)
                Debug.WriteLine(Time & Text)
#If VERBOSE Then
                console.writeline(time & text)
#End If
                _log.Close()
            End Using
        End Sub
    End Class

    Function Fix(ByVal str As String) As String
        Dim illegal() As Char = IO.Path.GetInvalidFileNameChars()
        For Each chara In illegal
            str = str.Replace(chara, ".")
        Next
        Return str
    End Function

    Sub InitializeLog()
        If Not Directory.Exists("C:\ProgramData\mlnlover11 Productions\Notepad X\") Then
            If Not Directory.Exists("C:\ProgramData\") Then Directory.CreateDirectory("C:\ProgramData")
            If Not IO.Directory.Exists("C:\ProgramData\mlnlover11 Productions") Then IO.Directory.CreateDirectory("C:\ProgramData\mlnlover11 Productions")
            If Not IO.Directory.Exists("C:\ProgramData\mlnlover11 Productions\Notepad X") Then IO.Directory.CreateDirectory("C:\ProgramData\mlnlover11 Productions\Notepad X")
        End If
        If Not IO.Directory.Exists("C:\ProgramData\mlnlover11 Productions\Notepad X\AutoSave") Then IO.Directory.CreateDirectory("C:\ProgramData\mlnlover11 Productions\Notepad X\AutoSave")
        Try
            Log.WriteLine("Started")
        Catch ex As Exception
            MsgBox("Error Creating/Opening Log: " & vbCrLf & ex.ToString)
            End
        End Try
    End Sub
    
    Sub LoadLSharp()
        LSharpEnvironment.AssignLocal(LSharp.Symbol.FromName("*NotepadX*"), MDIParent1)
        LSharpEnvironment.AssignLocal(Symbol.FromName("register-plugin"), New [Function](Function (args As Cons, env As LSharp.Environment)
                                                                                           NotepadXLSharp.NotepadXPluginGenerator.CreateClass(args.First.ToString(), args.Rest())
                                                                                       End Function))
        Runtime.EvalString("(using ""NotepadX"")", LSharpEnvironment)
    End Sub
End Module
