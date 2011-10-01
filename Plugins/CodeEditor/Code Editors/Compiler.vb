Imports System.Reflection
Imports System.IO
Imports Alsing.SourceCode

Public Class Compiler
    Enum ApplicationType
        CSharp
        VisualBasic
        FSharp
        IL
        Boo
        Lua
    End Enum

    Private FSharpCompilers As String() = {"Fsc.exe", "FSharp.Build.dll", "FSharp.Compiler.dll", "FSharp.Compiler.Interactive.Settings.dll",
                                           "Fsi.exe", "Fsi.exe.config", "Microsoft.FSharp.targets"}
    Private CSharpCompilers As String() = {"csc.exe", "csc.exe.config", "csc.rsp"}
    Private MSBuild As String() = {"MSBuild.exe", "msbuild.exe.config", "msbuild.rsp"}
    Private VBCompilers As String() = {"vbc.exe", "vbc.exe.config", "vbc.rsp"}
    Private ILCompilers As String() = {"ilasm.exe", "ilasm.exe.config"}
    Private BooCompilers As String() = {"Boo.Lang.CodeDom.dll", "Boo.Lang.Compiler.dll", "Boo.Lang.dll", "Boo.Lang.Extensions.dll", "Boo.Lang.Interpreter.dll", "Boo.Lang.Parser.dll",
"Boo.Lang.PatternMatching.dll", "Boo.Lang.Useful.dll", "Boo.Microsoft.Build.targets", "Boo.NAnt.Tasks.dll", "booc.exe", "booc.exe.config", "booc.rsp", "booi.exe", "booi.exe.config",
"booish.exe", "booish.exe.config"}
    Private LuaCompilers As String() = {"lua.exe", "lua51.dll", "lua5.1.dll"}

    Public AppType As ApplicationType
    Public files() As String
    Private Extracted As Boolean = False

    Private Function GetEmbeddedStream(ByVal Name As String) As System.IO.Stream
        ' The name of the embedded resource often uses the project name as a prefix.
        ' This is set in the project properties page in VS2008. 
        Dim embeddedName As String = String.Format("Notepad_X.{0}", Name)
        Dim myself = Assembly.GetExecutingAssembly()
        Return myself.GetManifestResourceStream(embeddedName)
    End Function

    Sub New(ByVal Compile As ApplicationType, ByVal files() As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.AppType = Compile
        Me.files = files
    End Sub

    Sub ExtractResources()
        IO.Directory.CreateDirectory("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\")
        Select Case AppType
            Case ApplicationType.Boo
                For Each file In BooCompilers
                    Dim Stream As Stream = GetEmbeddedStream(file)
                    Dim filestream As New FileStream("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\" & file, FileMode.Create)
                    Stream.Position = 0
                    Stream.CopyTo(filestream)
                    Stream.Close()
                    filestream.Close()
                Next
            Case ApplicationType.CSharp
                For Each File In CSharpCompilers
                    Dim Stream As Stream = GetEmbeddedStream(File)
                    Dim filestream As New FileStream("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\" & File, FileMode.Create)
                    Stream.Position = 0
                    Stream.CopyTo(filestream)
                    Stream.Close()
                    filestream.Close()
                Next
            Case ApplicationType.FSharp
                For Each File In FSharpCompilers
                    Dim Stream As Stream = GetEmbeddedStream(File)
                    Dim filestream As New FileStream("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\" & File, FileMode.Create)
                    Stream.Position = 0
                    Stream.CopyTo(filestream)
                    Stream.Close()
                    filestream.Close()
                Next
            Case ApplicationType.IL
                For Each File In ILCompilers
                    Dim Stream As Stream = GetEmbeddedStream(File)
                    Dim filestream As New FileStream("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\" & File, FileMode.Create)
                    Stream.Position = 0
                    Stream.CopyTo(filestream)
                    Stream.Close()
                    filestream.Close()
                Next
            Case ApplicationType.Lua
                For Each File In LuaCompilers
                    Dim Stream As Stream = GetEmbeddedStream(File)
                    Dim filestream As New FileStream("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\" & File, FileMode.Create)
                    Stream.Position = 0
                    Stream.CopyTo(filestream)
                    Stream.Close()
                    filestream.Close()
                Next
            Case ApplicationType.VisualBasic
                For Each File In VBCompilers
                    Dim Stream As Stream = GetEmbeddedStream(File)
                    Dim filestream As New FileStream("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\" & File, FileMode.Create)
                    Stream.Position = 0
                    Stream.CopyTo(filestream)
                    Stream.Close()
                    filestream.Close()
                Next
            Case Else
                Throw New Exception("Invalid ApplicationType Selected!")
                Return
        End Select
        Extracted = True
    End Sub

    Sub Compile()
        If Not Extracted Then
            Throw New Exception("Please extract resources first!")
        End If
        Dim process As Process
        Select Case AppType
            Case ApplicationType.Boo
                ProgressBar1.Value = 0
                ProgressBar1.Max = files.Count
                For Each File In files
                    process = New Process()
                    process.Start("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\booc.exe", File)
                    ProgressBar1.Value += 1
                    ProgressBar1.TextValue = "Compiling Documents... {0}% Done"
                Next
            Case ApplicationType.CSharp
                ProgressBar1.Value = 0
                ProgressBar1.Max = files.Count
                For Each File In files
                    process = New Process()
                    process.Start("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\csc.exe", File)
                    ProgressBar1.Value += 1
                    ProgressBar1.TextValue = "Compiling Documents... {0}% Done"
                Next
            Case ApplicationType.FSharp
                ProgressBar1.Value = 0
                ProgressBar1.Max = files.Count
                For Each File In files
                    process = New Process()
                    process.Start("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\fsc.exe", File)
                    ProgressBar1.Value += 1
                    ProgressBar1.TextValue = "Compiling Documents... {0}% Done"
                Next
            Case ApplicationType.IL
                ProgressBar1.Value = 0
                ProgressBar1.Max = files.Count
                For Each File In files
                    process = New Process()
                    process.Start("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\ilasm.exe", File)
                    ProgressBar1.Value += 1
                    ProgressBar1.TextValue = "Compiling Documents... {0}% Done"
                Next
            Case ApplicationType.Lua
                ProgressBar1.Value = 0
                ProgressBar1.Max = files.Count
                For Each File In files
                    process = New Process()
                    process.Start("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\lua.exe", File)
                    ProgressBar1.Value += 1
                    ProgressBar1.TextValue = "Compiling Documents... {0}% Done"
                Next
            Case ApplicationType.VisualBasic
                ProgressBar1.Value = 0
                ProgressBar1.Max = files.Count
                For Each File In files
                    process = New Process()
                    process.Start("C:\ProgramData\mlnlover11 Productions\Notepad X\Compilers\vbc.exe", File)
                    ProgressBar1.Value += 1
                    ProgressBar1.TextValue = "Compiling Documents... {0}% Done"
                Next
        End Select
    End Sub
End Class
