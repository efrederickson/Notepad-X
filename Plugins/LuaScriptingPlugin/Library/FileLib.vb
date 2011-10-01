Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Library
    Public NotInheritable Class FileLib
        Private Sub New()
        End Sub
        Public Shared Sub RegisterModule(ByVal enviroment As LuaTable)
            Dim [module] As New LuaTable()
            RegisterFunctions([module])
            enviroment.SetNameValue("file", [module])
        End Sub

        Public Shared Sub RegisterFunctions(ByVal [module] As LuaTable)
            [module].Register("close", AddressOf close)
            [module].Register("read", AddressOf read)
            [module].Register("write", AddressOf write)
            [module].Register("lines", AddressOf lines)
            [module].Register("flush", AddressOf flush)
            [module].Register("seek", AddressOf seek)
        End Sub

        Public Shared Function CreateMetaTable() As LuaTable
            Dim metatable As New LuaTable()
            RegisterFunctions(metatable)
            metatable.SetNameValue("__index", metatable)
            Return metatable
        End Function

        Public Shared Function close(ByVal values As LuaValue()) As LuaValue
            Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
            Dim reader As TextReader = TryCast(data.Value, TextReader)
            If reader IsNot Nothing Then
                reader.Close()
                Return Nothing
            End If

            Dim writer As TextWriter = TryCast(data.Value, TextWriter)
            If writer IsNot Nothing Then
                writer.Close()
            End If

            Return Nothing
        End Function

        Public Shared Function read(ByVal values As LuaValue()) As LuaValue
            Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
            Dim reader As TextReader = TryCast(data.Value, TextReader)

            Dim number As LuaNumber = If(values.Length > 1, TryCast(values(1), LuaNumber), Nothing)

            If number IsNot Nothing Then
                If number.Number = 0 Then
                    Return LuaString.Empty
                End If

                If reader.Peek() = -1 Then
                    Return LuaNil.Nil
                End If

                Dim block As Char() = New Char(CInt(Math.Truncate(number.Number)) - 1) {}
                Dim chars As Integer = reader.ReadBlock(block, 0, block.Length)
                Return New LuaString(New String(block, 0, chars))
            End If

            Dim param As LuaString = If(values.Length > 1, TryCast(values(1), LuaString), Nothing)
            Dim mode As String = If(param Is Nothing, "*l", param.Text)

            Select Case mode
                Case "*l"
                    If reader.Peek() = -1 Then
                        Return LuaNil.Nil
                    End If
                    Return New LuaString(reader.ReadLine())
                Case "*a"
                    Return New LuaString(reader.ReadToEnd())
                Case "*n"
                    Dim buffer As New List(Of Char)()
                    Dim ch As Integer = reader.Peek()
                    While ch >= Asc("0"c) AndAlso ch <= Asc("9"c)
                        buffer.Add(ChrW(reader.Read()))
                        ch = reader.Peek()
                    End While
                    Return New LuaNumber(Integer.Parse(New String(buffer.ToArray())))
            End Select

            Return Nothing
        End Function

        Public Shared Function lines(ByVal values As LuaValue()) As LuaValue
            Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
            Dim reader As TextReader = TryCast(data.Value, TextReader)

            Dim func As New LuaFunction(Function(args As LuaValue())
                                            Dim _data As LuaUserdata = TryCast(values(0), LuaUserdata)
                                            Dim _reader As TextReader = TryCast(data.Value, TextReader)

                                            Dim line As String = _reader.ReadLine()

                                            If line IsNot Nothing Then
                                                Return New LuaString(line)
                                            Else
                                                Return LuaNil.Nil
                                            End If

                                        End Function)

            Return New LuaMultiValue(New LuaValue() {func, data, LuaNil.Nil})
        End Function

        Public Shared Function seek(ByVal values As LuaValue()) As LuaValue
            Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
            Dim stream As Stream = Nothing

            Dim writer As StreamWriter = TryCast(data.Value, StreamWriter)
            If writer IsNot Nothing Then
                stream = writer.BaseStream
            Else
                Dim reader As StreamReader = TryCast(data.Value, StreamReader)
                If reader IsNot Nothing Then
                    stream = reader.BaseStream
                End If
            End If

            If stream IsNot Nothing Then
                Dim whenceStr As LuaString = If(values.Length > 1, TryCast(values(1), LuaString), Nothing)
                Dim whence As String = If(whenceStr Is Nothing, "cur", whenceStr.Text)

                Dim offsetNum As LuaNumber = If(values.Length > 1 AndAlso whenceStr Is Nothing, TryCast(values(1), LuaNumber), Nothing)
                offsetNum = If(values.Length > 2 AndAlso offsetNum Is Nothing, TryCast(values(2), LuaNumber), Nothing)
                Dim offset As Long = If(offsetNum Is Nothing, 0, CLng(Math.Truncate(offsetNum.Number)))

                stream.Seek(offset, GetSeekOrigin(whence))
            End If

            Return Nothing
        End Function

        Private Shared Function GetSeekOrigin(ByVal whence As String) As SeekOrigin
            Select Case whence
                Case "set"
                    Return SeekOrigin.Begin
                Case "end"
                    Return SeekOrigin.[End]
                Case "cur"
                    Return SeekOrigin.Current
                Case Else
                    Return SeekOrigin.Current
            End Select
        End Function

        Public Shared Function write(ByVal values As LuaValue()) As LuaValue
            Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
            Dim writer As TextWriter = TryCast(data.Value, TextWriter)

            For i As Integer = 1 To values.Length - 1
                writer.Write(values(i).Value)
            Next

            Return Nothing
        End Function

        Public Shared Function flush(ByVal values As LuaValue()) As LuaValue
            Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
            Dim writer As TextWriter = TryCast(data.Value, TextWriter)
            writer.Flush()
            Return Nothing
        End Function
    End Class
End Namespace
