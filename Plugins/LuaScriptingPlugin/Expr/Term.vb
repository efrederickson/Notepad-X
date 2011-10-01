Partial Public Class Term
    Inherits Expr
    Public Overrides Function Evaluate(ByVal enviroment As LuaTable) As LuaValue
        Throw New NotImplementedException()
    End Function

    Public Overrides Function Simplify() As Term
        Return Me
    End Function
End Class
