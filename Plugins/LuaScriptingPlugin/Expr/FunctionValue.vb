Imports System.Collections.Generic
Imports System.Text

Public Partial Class FunctionValue
	Inherits Term
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Return Me.Body.Evaluate(enviroment)
	End Function
End Class
