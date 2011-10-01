Partial Public Class ExprStmt
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Me.Expr.Evaluate(enviroment)
        isBreak = False
        Return Nothing
    End Function
End Class
