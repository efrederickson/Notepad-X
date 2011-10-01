Imports System.Collections.Generic
Imports System.Text

Public Partial Class TableConstructor
	Inherits Term
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Dim table As New LuaTable()

		For Each field As Field In Me.FieldList
			Dim nameValue As NameValue = TryCast(field, NameValue)
			If nameValue IsNot Nothing Then
				table.SetNameValue(nameValue.Name, nameValue.Value.Evaluate(enviroment))
				Continue For
			End If

			Dim keyValue As KeyValue = TryCast(field, KeyValue)
			If keyValue IsNot Nothing Then
				table.SetKeyValue(keyValue.Key.Evaluate(enviroment), keyValue.Value.Evaluate(enviroment))
				Continue For
			End If

			Dim itemValue As ItemValue = TryCast(field, ItemValue)
			If itemValue IsNot Nothing Then
				table.AddValue(itemValue.Value.Evaluate(enviroment))
				Continue For
			End If
		Next

		Return table
	End Function
End Class
