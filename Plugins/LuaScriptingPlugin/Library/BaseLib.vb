Imports System.Linq

Namespace Library
	Public Class BaseLib
		Public Shared Sub RegisterFunctions([module] As LuaTable)
			[module].Register("print", AddressOf print)
			[module].Register("type", AddressOf type)
			[module].Register("getmetatable", AddressOf getmetatable)
			[module].Register("setmetatable", AddressOf setmetatable)
			[module].Register("tostring", AddressOf tostring)
			[module].Register("tonumber", AddressOf tonumber)
			[module].Register("ipairs", AddressOf ipairs)
			[module].Register("pairs", AddressOf pairs)
			[module].Register("next", AddressOf [next])
			[module].Register("assert", AddressOf assert)
			[module].Register("error", AddressOf [error])
			[module].Register("rawget", AddressOf rawget)
			[module].Register("rawset", AddressOf rawset)
			[module].Register("select", AddressOf [select])
			[module].Register("dofile", AddressOf dofile)
			[module].Register("loadstring", AddressOf loadstring)
			[module].Register("unpack", AddressOf unpack)
            [module].Register("pcall", AddressOf pcall)
            [module].Register("getdir", New LuaFunc(Function(values() As LuaValue) As LuaValue
                                                        Dim V As LuaString = New LuaString(values(0).Value.ToString)
                                                        Return New LuaString(System.IO.Path.GetDirectoryName(V.Text))
                                                    End Function))
		End Sub

        Public Shared Function print(ByVal values As LuaValue()) As LuaValue
            Dim _v As String() = New String(values.Count) {}
            For i As Integer = 0 To values.Count - 1
                _v(i) = values(i).Value
            Next
            Dim _str As String = String.Join("    ", _v)
            Console.WriteLine(_str)
            Return Nothing
        End Function

		Public Shared Function type(values As LuaValue()) As LuaValue
			If values.Length > 0 Then
				Return New LuaString(values(0).GetTypeCode())
			Else
				Throw New Exception("bad argument #1 to 'type' (value expected)")
			End If
		End Function

		Public Shared Overloads Function tostring(values As LuaValue()) As LuaValue
			Return New LuaString(values(0).ToString())
		End Function

		Public Shared Function tonumber(values As LuaValue()) As LuaValue
			Dim text As LuaString = TryCast(values(0), LuaString)
			If text IsNot Nothing Then
				Return New LuaNumber(Double.Parse(text.Text))
			End If

			Dim number As LuaString = TryCast(values(0), LuaString)
			If number IsNot Nothing Then
				Return number
			End If

			Return LuaNil.Nil
		End Function

		Public Shared Function setmetatable(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim metatable As LuaTable = TryCast(values(1), LuaTable)
			table.MetaTable = metatable
			Return Nothing
		End Function

		Public Shared Function getmetatable(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Return table.MetaTable
		End Function

		Public Shared Function rawget(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim index As LuaValue = values(1)
			Return table.RawGetValue(index)
		End Function

		Public Shared Function rawset(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim index As LuaValue = values(1)
			Dim value As LuaValue = values(2)
			table.SetKeyValue(index, value)
			Return Nothing
		End Function

		Public Shared Function ipairs(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim func As New LuaFunction(Function(args As LuaValue()) 
			Dim tbl As LuaTable = TryCast(args(0), LuaTable)
			Dim index As Integer = CInt(Math.Truncate(TryCast(args(1), LuaNumber).Number))
			Dim nextIndex As Integer = index + 1

			If nextIndex <= tbl.Length Then
				Return New LuaMultiValue(New LuaValue() {New LuaNumber(nextIndex), tbl.GetValue(nextIndex)})
			Else
				Return LuaNil.Nil
			End If

End Function)

			Return New LuaMultiValue(New LuaValue() {func, table, New LuaNumber(0)})
		End Function

		Public Shared Function pairs(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim func As New LuaFunction(AddressOf [next])
			Return New LuaMultiValue(New LuaValue() {func, table, LuaNil.Nil})
		End Function

		Public Shared Function [next](values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim index As LuaValue = If(values.Length > 1, values(1), LuaNil.Nil)

			Dim prevKey As LuaValue = LuaNil.Nil
			Dim nextIndex As LuaValue = LuaNil.Nil
            For Each key As LuaValue In table.Keys
                If prevKey.Equals(index) Then
                    nextIndex = key
                    Exit For
                End If
                prevKey = key
            Next

			Return New LuaMultiValue(New LuaValue() {nextIndex, table.GetValue(nextIndex)})
		End Function

		Public Shared Function assert(values As LuaValue()) As LuaValue
			Dim condition As Boolean = values(0).GetBooleanValue()
			Dim message As LuaString = If(values.Length > 1, TryCast(values(1), LuaString), Nothing)
			If message IsNot Nothing Then
				Throw New LuaError(message.Text)
			Else
				Throw New LuaError("assertion failed!")
			End If
			' return new LuaMultiValue { Values = values };
		End Function

		Public Shared Function [error](values As LuaValue()) As LuaValue
			Dim message As LuaString = TryCast(values(0), LuaString)
			If message IsNot Nothing Then
				Throw New LuaError(message.Text)
			Else
				Throw New LuaError("error raised!")
			End If
		End Function

		Public Shared Function [select](values As LuaValue()) As LuaValue
			Dim number As LuaNumber = TryCast(values(0), LuaNumber)
			If number IsNot Nothing Then
				Dim index As Integer = CInt(Math.Truncate(number.Number))
				Dim args As LuaValue() = New LuaValue(values.Length - index - 1) {}
				For i As Integer = 0 To args.Length - 1
					args(i) = values(index + i)
				Next
				Return New LuaMultiValue(args)
			End If

			Dim text As LuaString = TryCast(values(0), LuaString)
			If text.Text = "#" Then
				Return New LuaNumber(values.Length - 1)
			End If

			Return LuaNil.Nil
		End Function

		Public Shared Function dofile(values As LuaValue()) As LuaValue
			Dim file As LuaString = TryCast(values(0), LuaString)
			Dim enviroment As LuaTable = TryCast(values(1), LuaTable)
            Return LuaInterpreter.RunFile(file.Text, enviroment)
        End Function

        Public Shared Function loadstring(ByVal values As LuaValue()) As LuaValue
            Dim code As LuaString = TryCast(values(0), LuaString)
            Dim enviroment As LuaTable = TryCast(values(1), LuaTable)
            Dim chunk As Chunk = LuaInterpreter.Parse(code.Text)

            Dim func As New LuaFunction(Function(args As LuaValue())
                                            chunk.Enviroment = enviroment
                                            Return chunk.Execute()

                                        End Function)

            Return func
        End Function

		Public Shared Function unpack(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim startNumber As LuaNumber = If(values.Length > 1, TryCast(values(1), LuaNumber), Nothing)
			Dim lengthNumber As LuaNumber = If(values.Length > 2, TryCast(values(2), LuaNumber), Nothing)

			Dim start As Integer = If(startNumber Is Nothing, 1, CInt(Math.Truncate(startNumber.Number)))
			Dim length As Integer = If(lengthNumber Is Nothing, values.Length, CInt(Math.Truncate(lengthNumber.Number)))

			Dim section As LuaValue() = New LuaValue(length - 1) {}
			For i As Integer = 0 To length - 1
				section(i) = table.GetValue(start + i)
			Next
			Return New LuaMultiValue(section)
		End Function

		Public Shared Function pcall(values As LuaValue()) As LuaValue
			Dim func As LuaFunction = TryCast(values(0), LuaFunction)
			Try
				Dim args As LuaValue() = New LuaValue(values.Length - 2) {}
				For i As Integer = 0 To args.Length - 1
					args(i) = values(i + 1)
				Next
				Dim result As LuaValue = func.Invoke(args)
				Return New LuaMultiValue(LuaMultiValue.UnWrapLuaValues(New LuaValue() {LuaBoolean.[True], result}))
			Catch [error] As Exception
				Return New LuaMultiValue(New LuaValue() {LuaBoolean.[False], New LuaString([error].Message)})
			End Try
		End Function
	End Class
End Namespace
