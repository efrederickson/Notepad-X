Imports System.Collections.Generic
Imports System.Text

Public Partial Class GroupExpr
	Inherits BaseExpr
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Return Me.Expr.Evaluate(enviroment)
	End Function

	Public Overrides Function Simplify() As Term
		Return Me.Expr.Simplify()
	End Function
End Class
