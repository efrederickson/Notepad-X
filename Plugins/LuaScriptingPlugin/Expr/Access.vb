Imports System.Collections.Generic
Imports System.Text

Public MustInherit Partial Class Access
	Public MustOverride Function Evaluate(baseValue As LuaValue, enviroment As LuaTable) As LuaValue
End Class
