Imports System.Collections.Generic
Imports System.Text

Public MustInherit Partial Class Expr
	Public MustOverride Function Evaluate(enviroment As LuaTable) As LuaValue

	Public MustOverride Function Simplify() As Term
End Class
