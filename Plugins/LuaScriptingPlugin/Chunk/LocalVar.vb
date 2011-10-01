Imports System.Collections.Generic
Imports System.Text

Public Partial Class LocalVar
	Inherits Statement
	Public Overrides Function Execute(enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
		Dim values As LuaValue() = Me.ExprList.ConvertAll(Function(expr) expr.Evaluate(enviroment)).ToArray()
		Dim neatValues As LuaValue() = LuaMultiValue.UnWrapLuaValues(values)

		For i As Integer = 0 To Math.Min(Me.NameList.Count, neatValues.Length) - 1
			enviroment.RawSetValue(Me.NameList(i), neatValues(i))
		Next

		If neatValues.Length < Me.NameList.Count Then
			For i As Integer = neatValues.Length To Me.NameList.Count - neatValues.Length - 1
				enviroment.RawSetValue(Me.NameList(i), LuaNil.Nil)
			Next
		End If

		isBreak = False
		Return Nothing
	End Function
End Class
