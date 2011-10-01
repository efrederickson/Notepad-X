Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Library
	Public NotInheritable Class TableLib
		Private Sub New()
		End Sub
		Public Shared Sub RegisterModule(enviroment As LuaTable)
			Dim [module] As New LuaTable()
			RegisterFunctions([module])
			enviroment.SetNameValue("table", [module])
		End Sub

		Public Shared Sub RegisterFunctions([module] As LuaTable)
			[module].Register("concat", AddressOf concat)
			[module].Register("insert", AddressOf insert)
			[module].Register("remove", AddressOf remove)
			[module].Register("removeitem", AddressOf removeitem)
			[module].Register("maxn", AddressOf maxn)
			[module].Register("sort", AddressOf sort)
		End Sub

		Public Shared Function concat(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim separator As LuaString = If(values.Length > 1, TryCast(values(1), LuaString), LuaString.Empty)
			Dim startNumber As LuaNumber = If(values.Length > 2, TryCast(values(2), LuaNumber), Nothing)
			Dim endNumber As LuaNumber = If(values.Length > 3, TryCast(values(3), LuaNumber), Nothing)

			Dim start As Integer = If(startNumber Is Nothing, 1, CInt(Math.Truncate(startNumber.Number)))
			Dim [end] As Integer = If(endNumber Is Nothing, table.Length, CInt(Math.Truncate(endNumber.Number)))

			If start > [end] Then
				Return LuaString.Empty
			Else
				Dim text As New StringBuilder()

				For index As Integer = start To [end] - 1
					text.Append(table.GetValue(index).ToString())
					text.Append(separator.Text)
				Next
				text.Append(table.GetValue([end]).ToString())

				Return New LuaString(text.ToString())
			End If
		End Function

		Public Shared Function insert(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			If values.Length = 2 Then
				Dim item As LuaValue = values(1)
				table.AddValue(item)
			ElseIf values.Length = 3 Then
				Dim number As LuaNumber = TryCast(values(1), LuaNumber)
				Dim item As LuaValue = values(2)
				Dim index As Integer = CInt(Math.Truncate(number.Number))
				table.InsertValue(index, item)
			End If
			Return Nothing
		End Function

		Public Shared Function remove(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim index As Integer = table.Length
			If values.Length = 2 Then
				Dim number As LuaNumber = TryCast(values(1), LuaNumber)
				index = CInt(Math.Truncate(number.Number))
			End If

			Dim item As LuaValue = table.GetValue(index)
			table.RemoveAt(index)
			Return item
		End Function

		Public Shared Function removeitem(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim item As LuaValue = values(1)

			Dim removed As Boolean = table.Remove(item)
			Return LuaBoolean.From(removed)
		End Function

		Public Shared Function maxn(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			Dim maxIndex As Double = Double.MinValue
            For Each key As LuaValue In table.Keys
                Dim number As LuaNumber = TryCast(key, LuaNumber)
                If number IsNot Nothing AndAlso number.Number > 0 Then
                    If number.Number > maxIndex Then
                        maxIndex = number.Number
                    End If
                End If
            Next
			Return New LuaNumber(maxIndex)
		End Function

		Public Shared Function sort(values As LuaValue()) As LuaValue
			Dim table As LuaTable = TryCast(values(0), LuaTable)
			If values.Length = 2 Then
				Dim comp As LuaFunction = TryCast(values(1), LuaFunction)
				table.Sort(comp)
			Else
				table.Sort()
			End If
			Return Nothing
		End Function
	End Class
End Namespace
