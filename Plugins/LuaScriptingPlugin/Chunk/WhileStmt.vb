Imports System.Collections.Generic
Imports System.Text

Public Partial Class WhileStmt
	Inherits Statement
	Public Overrides Function Execute(enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
		While True
			Dim condition As LuaValue = Me.Condition.Evaluate(enviroment)

			If condition.GetBooleanValue() = False Then
				Exit While
			End If

			Dim returnValue = Me.Body.Execute(enviroment, isBreak)
			If returnValue IsNot Nothing OrElse isBreak = True Then
				isBreak = False
				Return returnValue
			End If
		End While

		isBreak = False
		Return Nothing
	End Function
End Class
