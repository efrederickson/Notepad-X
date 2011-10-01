Partial Public Class [Function]
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Dim table As LuaTable = enviroment

        If Me.Name.MethodName Is Nothing Then
            For i As Integer = 0 To Me.Name.FullName.Count - 2
                Dim obj As LuaValue = enviroment.GetValue(Me.Name.FullName(i))
                table = TryCast(obj, LuaTable)

                If table Is Nothing Then
                    Throw New Exception("Not a table: " & Me.Name.FullName(i))
                End If
            Next

            table.SetNameValue(Me.Name.FullName(Me.Name.FullName.Count - 1), Me.Body.Evaluate(enviroment))
        Else
            For i As Integer = 0 To Me.Name.FullName.Count - 1
                Dim obj As LuaValue = enviroment.GetValue(Me.Name.FullName(i))
                table = TryCast(obj, LuaTable)

                If table Is Nothing Then
                    Throw New Exception("Not a table " & Me.Name.FullName(i))
                End If
            Next

            Me.Body.ParamList.NameList.Insert(0, "self")

            table.SetNameValue(Me.Name.MethodName, Me.Body.Evaluate(enviroment))
        End If

        isBreak = False
        Return Nothing
    End Function
End Class
