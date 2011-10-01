Imports System.Collections.Generic
Imports System.Text

Public Partial Class KeyAccess
	Inherits Access
	Public Overrides Function Evaluate(baseValue As LuaValue, enviroment As LuaTable) As LuaValue
		Dim key As LuaValue = Me.Key.Evaluate(enviroment)
		Return LuaValue.GetKeyValue(baseValue, key)
	End Function
End Class
