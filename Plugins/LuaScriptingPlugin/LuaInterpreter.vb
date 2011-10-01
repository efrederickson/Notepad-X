Imports System.IO
Imports LuaScriptingPlugin.Library

Public Class LuaInterpreter
    Public Shared Function RunFile(ByVal luaFile As String) As LuaValue
        Return Interpreter(File.ReadAllText(luaFile))
    End Function

    Public Shared Function RunFile(ByVal luaFile As String, ByVal enviroment As LuaTable) As LuaValue
        Return Interpreter(File.ReadAllText(luaFile), enviroment)
    End Function

    Public Shared Function Interpreter(ByVal luaCode As String) As LuaValue
        Return Interpreter(luaCode, CreateGlobalEnviroment())
    End Function

    Public Shared Function Interpreter(ByVal luaCode As String, ByVal enviroment As LuaTable) As LuaValue
        Dim chunk As Chunk = Parse(luaCode)
        chunk.Enviroment = enviroment
        Return chunk.Execute()
    End Function

    Shared parser As New Parser()

    Public Shared Function Parse(ByVal luaCode As String) As Chunk
        Dim success As Boolean
        Dim chunk As Chunk = parser.ParseChunk(New TextInput(luaCode), success)
        If success Then
            Return chunk
        Else
            Throw New ArgumentException(String.Format("Code has syntax errors:{0}{1}{2}", vbCr, vbLf, parser.GetEorrorMessages()))
        End If
    End Function

    Public Shared Function CreateGlobalEnviroment() As LuaTable
        Dim [global] As New LuaTable()
        WinFormsLib.RegisterModule([global])
        script.RegisterModule([global])
        SystemLib.RegisterModule([global])
        ConsoleLib.RegisterModule([global])
        BaseLib.RegisterFunctions([global])
        StringLib.RegisterModule([global])
        TableLib.RegisterModule([global])
        IOLib.RegisterModule([global])
        FileLib.RegisterModule([global])
        MathLib.RegisterModule([global])
        OSLib.RegisterModule([global])
        NotepadXLib.RegisterModule([global])
        PackagesLib.RegisterModule([global])

        [global].SetNameValue("_G", [global])
        Return [global]
    End Function
End Class
