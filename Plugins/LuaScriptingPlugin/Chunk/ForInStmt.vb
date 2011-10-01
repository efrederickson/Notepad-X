Partial Public Class ForInStmt
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Dim values As LuaValue() = Me.ExprList.ConvertAll(Function(expr) expr.Evaluate(enviroment)).ToArray()
        Dim neatValues As LuaValue() = LuaMultiValue.UnWrapLuaValues(values)

        Dim func As LuaFunction = TryCast(neatValues(0), LuaFunction)
        Dim state As LuaValue = neatValues(1)
        Dim loopVar As LuaValue = neatValues(2)

        Dim table = New LuaTable(enviroment)
        Me.Body.Enviroment = table

        While True
            Dim result As LuaValue = func.Invoke(New LuaValue() {state, loopVar})
            Dim multiValue As LuaMultiValue = TryCast(result, LuaMultiValue)

            If multiValue IsNot Nothing Then
                neatValues = LuaMultiValue.UnWrapLuaValues(multiValue.Values)
                loopVar = neatValues(0)

                For i As Integer = 0 To Math.Min(Me.NameList.Count, neatValues.Length) - 1
                    table.SetNameValue(Me.NameList(i), neatValues(i))
                Next
            Else
                loopVar = result
                table.SetNameValue(Me.NameList(0), result)
            End If

            If loopVar Is LuaNil.Nil Then
                Exit While
            End If

            Dim returnValue = Me.Body.Execute(isBreak)
            If returnValue IsNot Nothing OrElse isBreak = True Then
                isBreak = False
                Return returnValue
            End If
        End While

        isBreak = False
        Return Nothing
    End Function
End Class
