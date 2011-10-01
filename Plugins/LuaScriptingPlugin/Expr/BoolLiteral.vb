Imports System.Collections.Generic
Imports System.Text

Public Partial Class BoolLiteral
	Inherits Term
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Return LuaBoolean.From(Boolean.Parse(Me.Text))
	End Function
End Class
