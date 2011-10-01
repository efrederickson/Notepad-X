Imports System.Collections.Generic
Imports System.Text

Public Partial Class NilLiteral
	Inherits Term
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Return LuaNil.Nil
	End Function
End Class
