Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Library
    Public NotInheritable Class IOLib
        Private Sub New()
        End Sub
        Public Shared Sub RegisterModule(ByVal enviroment As LuaTable)
            Dim [module] As New LuaTable()
            RegisterFunctions([module])
            enviroment.SetNameValue("io", [module])
        End Sub

        Public Shared Sub RegisterFunctions(ByVal [module] As LuaTable)
            [module].Register("input", AddressOf input)
            [module].Register("output", AddressOf output)
            [module].Register("open", AddressOf open)
            [module].Register("read", AddressOf read)
            [module].Register("write", AddressOf write)
            [module].Register("flush", AddressOf flush)
            [module].Register("tmpfile", AddressOf tmpfile)
        End Sub

        Private Shared DefaultInput As TextReader = Console.In
        Private Shared DefaultOutput As TextWriter = Console.Out

        Public Shared Function input(ByVal values As LuaValue()) As LuaValue
            If values Is Nothing OrElse values.Length = 0 Then
                Return New LuaUserdata(DefaultInput)
            Else
                Dim file__1 As LuaString = TryCast(values(0), LuaString)
                If file__1 IsNot Nothing Then
                    DefaultInput = File.OpenText(file__1.Text)
                    Return Nothing
                End If

                Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
                If data IsNot Nothing AndAlso TypeOf data.Value Is TextReader Then
                    DefaultInput = TryCast(data.Value, TextReader)
                End If
                Return Nothing
            End If
        End Function

        Public Shared Function output(ByVal values As LuaValue()) As LuaValue
            If values Is Nothing OrElse values.Length = 0 Then
                Return New LuaUserdata(DefaultOutput)
            Else
                Dim file__1 As LuaString = TryCast(values(0), LuaString)
                If file__1 IsNot Nothing Then
                    DefaultOutput = File.CreateText(file__1.Text)
                    Return Nothing
                End If

                Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
                If data IsNot Nothing AndAlso TypeOf data.Value Is TextWriter Then
                    DefaultOutput = TryCast(data.Value, TextWriter)
                End If
                Return Nothing
            End If
        End Function

        Public Shared Function open(ByVal values As LuaValue()) As LuaValue
            Dim file__1 As LuaString = TryCast(values(0), LuaString)
            Dim modeStr As LuaString = If(values.Length > 1, TryCast(values(1), LuaString), Nothing)
            Dim mode As String = If(modeStr Is Nothing, "r", modeStr.Text)

            Select Case mode
                Case "r", "r+"
                    Dim reader As StreamReader = File.OpenText(file__1.Text)
                    Return New LuaUserdata(reader, FileLib.CreateMetaTable())
                Case "w", "w+"
                    Dim writer As StreamWriter = File.CreateText(file__1.Text)
                    Return New LuaUserdata(writer, FileLib.CreateMetaTable())
                Case "a", "a+"
                    Dim writer As StreamWriter = File.AppendText(file__1.Text)
                    Return New LuaUserdata(writer, FileLib.CreateMetaTable())
                Case Else
                    Throw New ArgumentException("Invalid file open mode " & mode)
            End Select
        End Function

        Public Shared Function read(ByVal values As LuaValue()) As LuaValue
            Dim args As New List(Of LuaValue)(values.Length + 1)
            args.Add(input(Nothing))
            args.AddRange(values)
            Return FileLib.read(args.ToArray())
        End Function

        Public Shared Function write(ByVal values As LuaValue()) As LuaValue
            Dim args As New List(Of LuaValue)(values.Length + 1)
            args.Add(output(Nothing))
            args.AddRange(values)
            Return FileLib.write(args.ToArray())
        End Function

        Public Shared Function flush(ByVal values As LuaValue()) As LuaValue
            Return FileLib.flush(New LuaValue() {output(Nothing)})
        End Function

        Public Shared Function tmpfile(ByVal values As LuaValue()) As LuaValue
            Dim writer As StreamWriter = File.CreateText(Path.GetTempFileName())
            Return New LuaUserdata(writer)
        End Function
    End Class
End Namespace
