Imports System.Collections.Generic
Imports System.Text

Public Partial Class VarName
	Inherits BaseExpr
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Return enviroment.GetValue(Me.Name)
	End Function

	Public Overrides Function Simplify() As Term
		Return Me
	End Function
End Class
