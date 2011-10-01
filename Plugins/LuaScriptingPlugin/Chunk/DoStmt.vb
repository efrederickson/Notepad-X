Partial Public Class DoStmt
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Return Me.Body.Execute(enviroment, isBreak)
    End Function
End Class
