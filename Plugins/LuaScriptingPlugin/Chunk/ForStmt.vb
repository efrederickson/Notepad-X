Partial Public Class ForStmt
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Dim start As LuaNumber = TryCast(Me.Start.Evaluate(enviroment), LuaNumber)
        Dim [end] As LuaNumber = TryCast(Me.[End].Evaluate(enviroment), LuaNumber)

        Dim [step] As Double = 1
        If Me.[Step] IsNot Nothing Then
            [step] = TryCast(Me.[Step].Evaluate(enviroment), LuaNumber).Number
        End If

        Dim table = New LuaTable(enviroment)
        table.SetNameValue(Me.VarName, start)
        Me.Body.Enviroment = table

        While [step] > 0 AndAlso start.Number <= [end].Number OrElse [step] <= 0 AndAlso start.Number >= [end].Number
            Dim returnValue = Me.Body.Execute(isBreak)
            If returnValue IsNot Nothing OrElse isBreak = True Then
                isBreak = False
                Return returnValue
            End If
            start.Number += [step]
        End While

        isBreak = False
        Return Nothing
    End Function
End Class
