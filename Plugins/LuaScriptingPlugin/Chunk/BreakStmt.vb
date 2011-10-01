Partial Public Class BreakStmt
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Throw New NotImplementedException()
    End Function
End Class
