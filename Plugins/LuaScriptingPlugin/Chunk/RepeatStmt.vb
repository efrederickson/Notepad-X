Partial Public Class RepeatStmt
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        While True
            Dim returnValue = Me.Body.Execute(enviroment, isBreak)
            If returnValue IsNot Nothing OrElse isBreak = True Then
                isBreak = False
                Return returnValue
            End If

            Dim condition As LuaValue = Me.Condition.Evaluate(enviroment)

            If condition.GetBooleanValue() = True Then
                Exit While
            End If
        End While

        Return Nothing
    End Function
End Class
