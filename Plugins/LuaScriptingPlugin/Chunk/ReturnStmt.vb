Imports System.Collections.Generic
Imports System.Text

Public Partial Class ReturnStmt
	Inherits Statement
	Public Overrides Function Execute(enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
		Throw New NotImplementedException()
	End Function
End Class
