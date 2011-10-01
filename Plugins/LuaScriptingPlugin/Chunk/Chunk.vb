Partial Public Class Chunk
    Public Enviroment As LuaTable

    Public Function Execute() As LuaValue
        Dim isBreak As Boolean
        Return Me.Execute(isBreak)
    End Function

    Public Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Me.Enviroment = New LuaTable(enviroment)
        Return Me.Execute(isBreak)
    End Function

    Public Function Execute(ByRef isBreak As Boolean) As LuaValue
        For Each statement As Statement In Statements
            Dim returnStmt As ReturnStmt = TryCast(statement, ReturnStmt)
            If returnStmt IsNot Nothing Then
                isBreak = False
                Return LuaMultiValue.WrapLuaValues(returnStmt.ExprList.ConvertAll(Function(expr) expr.Evaluate(Me.Enviroment)).ToArray())
            ElseIf TypeOf statement Is BreakStmt Then
                isBreak = True
                Return Nothing
            Else
                Dim returnValue = statement.Execute(Me.Enviroment, isBreak)
                If returnValue IsNot Nothing OrElse isBreak = True Then
                    Return returnValue
                End If
            End If
        Next

        isBreak = False
        Return Nothing
    End Function
End Class
