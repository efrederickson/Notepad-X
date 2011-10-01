Imports System.Collections.Generic
Imports System.Text

Public Partial Class StringLiteral
	Inherits Term
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Return New LuaString(Me.Text)
	End Function
End Class
