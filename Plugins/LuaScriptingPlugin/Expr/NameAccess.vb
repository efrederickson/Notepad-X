Imports System.Collections.Generic
Imports System.Text

Public Partial Class NameAccess
	Inherits Access
	Public Overrides Function Evaluate(baseValue As LuaValue, enviroment As LuaTable) As LuaValue
		Dim key As LuaValue = New LuaString(Me.Name)
		Return LuaValue.GetKeyValue(baseValue, key)
	End Function
End Class
