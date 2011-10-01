Partial Public Class IfStmt
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Dim condition As LuaValue = Me.Condition.Evaluate(enviroment)

        If condition.GetBooleanValue() = True Then
            Return Me.ThenBlock.Execute(enviroment, isBreak)
        Else
            For Each elseifBlock As ElseifBlock In Me.ElseifBlocks
                condition = elseifBlock.Condition.Evaluate(enviroment)

                If condition.GetBooleanValue() = True Then
                    Return elseifBlock.ThenBlock.Execute(enviroment, isBreak)
                End If
            Next

            If Me.ElseBlock IsNot Nothing Then
                Return Me.ElseBlock.Execute(enviroment, isBreak)
            End If
        End If

        isBreak = False
        Return Nothing
    End Function
End Class
