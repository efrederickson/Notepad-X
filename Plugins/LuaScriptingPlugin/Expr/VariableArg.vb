Imports System.Collections.Generic
Imports System.Text

Public Partial Class VariableArg
	Inherits Term
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Return enviroment.GetValue(Me.Name)
	End Function
End Class
