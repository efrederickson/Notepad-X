Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Library
	Public NotInheritable Class StringLib
		Private Sub New()
		End Sub
		Public Shared Sub RegisterModule(enviroment As LuaTable)
			Dim [module] As New LuaTable()
			RegisterFunctions([module])
			enviroment.SetNameValue("string", [module])
		End Sub

		Public Shared Sub RegisterFunctions([module] As LuaTable)
			[module].Register("byte", AddressOf [byte])
			[module].Register("char", AddressOf [char])
			[module].Register("format", AddressOf format)
			[module].Register("len", AddressOf len)
			[module].Register("sub", AddressOf [sub])
			[module].Register("lower", AddressOf lower)
			[module].Register("upper", AddressOf upper)
			[module].Register("rep", AddressOf rep)
			[module].Register("reverse", AddressOf reverse)
		End Sub

		Public Shared Function [byte](values As LuaValue()) As LuaValue
			Dim str As LuaString = TryCast(values(0), LuaString)
			Dim startNumber As LuaNumber = If(values.Length > 1, TryCast(values(1), LuaNumber), Nothing)
			Dim endNumber As LuaNumber = If(values.Length > 2, TryCast(values(2), LuaNumber), Nothing)

			Dim start As Integer = If(startNumber Is Nothing, 1, CInt(Math.Truncate(startNumber.Number)))
			Dim [end] As Integer = If(endNumber Is Nothing, start, CInt(Math.Truncate(endNumber.Number)))

			Dim numbers As LuaValue() = New LuaValue([end] - start) {}
			For i As Integer = 0 To numbers.Length - 1
                numbers(i) = New LuaNumber(Char.ConvertToUtf32(str.Text, start - 1 + i))
			Next

			Return New LuaMultiValue(numbers)
		End Function

		Public Shared Function [char](values As LuaValue()) As LuaValue
			Dim chars As Char() = New Char(values.Length - 1) {}

			For i As Integer = 0 To chars.Length - 1
				Dim number As Integer = CInt(Math.Truncate(TryCast(values(i), LuaNumber).Number))
				chars(i) = ChrW(number)
			Next

			Return New LuaString(New String(chars))
		End Function

		Public Shared Function format(values As LuaValue()) As LuaValue
            Dim _format As LuaString = TryCast(values(0), LuaString)
			Dim args As Object() = New Object(values.Length - 2) {}
			For i As Integer = 0 To args.Length - 1
				args(i) = values(i + 1).Value
			Next
            Return New LuaString(String.Format(_format.Text, args))
		End Function

		Public Shared Function [sub](values As LuaValue()) As LuaValue
			Dim str As LuaString = TryCast(values(0), LuaString)
			Dim startNumber As LuaNumber = TryCast(values(1), LuaNumber)
			Dim endNumber As LuaNumber = If(values.Length > 2, TryCast(values(2), LuaNumber), Nothing)

			Dim start As Integer = CInt(Math.Truncate(startNumber.Number))
			Dim [end] As Integer = If(endNumber Is Nothing, -1, CInt(Math.Truncate(endNumber.Number)))

			If start < 0 Then
				start = str.Text.Length + start + 1
			End If
			If [end] < 0 Then
				[end] = str.Text.Length + [end] + 1
			End If

			Return New LuaString(str.Text.Substring(start - 1, [end] - start + 1))
		End Function

		Public Shared Function rep(values As LuaValue()) As LuaValue
			Dim str As LuaString = TryCast(values(0), LuaString)
			Dim number As LuaNumber = TryCast(values(1), LuaNumber)
			Dim text As New StringBuilder()
            For i As Integer = 0 To CInt(number.Number - 1)
                text.Append(str.Text)
            Next
			Return New LuaString(text.ToString())
		End Function

		Public Shared Function reverse(values As LuaValue()) As LuaValue
			Dim str As LuaString = TryCast(values(0), LuaString)
			Dim chars As Char() = str.Text.ToCharArray()
			Array.Reverse(chars)
			Return New LuaString(New String(chars))
		End Function

		Public Shared Function len(values As LuaValue()) As LuaValue
			Dim str As LuaString = TryCast(values(0), LuaString)
			Return New LuaNumber(str.Text.Length)
		End Function

		Public Shared Function lower(values As LuaValue()) As LuaValue
			Dim str As LuaString = TryCast(values(0), LuaString)
			Return New LuaString(str.Text.ToLower())
		End Function

		Public Shared Function upper(values As LuaValue()) As LuaValue
			Dim str As LuaString = TryCast(values(0), LuaString)
			Return New LuaString(str.Text.ToUpper())
		End Function
	End Class
End Namespace
