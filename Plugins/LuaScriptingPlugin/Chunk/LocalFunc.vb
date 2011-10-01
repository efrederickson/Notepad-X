Imports System.Collections.Generic
Imports System.Text

Public Partial Class LocalFunc
	Inherits Statement
	Public Overrides Function Execute(enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
		enviroment.SetNameValue(Me.Name, Me.Body.Evaluate(enviroment))
		isBreak = False
		Return Nothing
	End Function
End Class
