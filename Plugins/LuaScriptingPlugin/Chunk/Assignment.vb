Partial Public Class Assignment
    Inherits Statement
    Public Overrides Function Execute(ByVal enviroment As LuaTable, ByRef isBreak As Boolean) As LuaValue
        Dim values As LuaValue() = Me.ExprList.ConvertAll(Function(expr) expr.Evaluate(enviroment)).ToArray()
        Dim neatValues As LuaValue() = LuaMultiValue.UnWrapLuaValues(values)

        For i As Integer = 0 To Math.Min(Me.VarList.Count, neatValues.Length) - 1
            Dim var As Var = Me.VarList(i)

            If var.Accesses.Count = 0 Then
                Dim varName As VarName = TryCast(var.Base, VarName)

                If varName IsNot Nothing Then
                    SetKeyValue(enviroment, New LuaString(varName.Name), values(i))
                    Continue For
                End If
            Else
                Dim baseValue As LuaValue = var.Base.Evaluate(enviroment)

                For j As Integer = 0 To var.Accesses.Count - 2
                    Dim access As Access = var.Accesses(j)

                    baseValue = access.Evaluate(baseValue, enviroment)
                Next

                Dim lastAccess As Access = var.Accesses(var.Accesses.Count - 1)

                Dim nameAccess As NameAccess = TryCast(lastAccess, NameAccess)
                If nameAccess IsNot Nothing Then
                    SetKeyValue(baseValue, New LuaString(nameAccess.Name), values(i))
                    Continue For
                End If

                Dim keyAccess As KeyAccess = TryCast(lastAccess, KeyAccess)
                If lastAccess IsNot Nothing Then
                    SetKeyValue(baseValue, keyAccess.Key.Evaluate(enviroment), values(i))
                End If
            End If
        Next

        isBreak = False
        Return Nothing
    End Function

    Private Shared Sub SetKeyValue(ByVal baseValue As LuaValue, ByVal key As LuaValue, ByVal value As LuaValue)
        Dim newIndex As LuaValue = LuaNil.Nil
        Dim table As LuaTable = TryCast(baseValue, LuaTable)
        If table IsNot Nothing Then
            If table.ContainsKey(key) Then
                table.SetKeyValue(key, value)
                Return
            Else
                If table.MetaTable IsNot Nothing Then
                    newIndex = table.MetaTable.GetValue("__newindex")
                End If

                If newIndex Is LuaNil.Nil Then
                    table.SetKeyValue(key, value)
                    Return
                End If
            End If
        Else
            Dim userdata As LuaUserdata = TryCast(baseValue, LuaUserdata)
            If userdata IsNot Nothing Then
                If userdata.MetaTable IsNot Nothing Then
                    newIndex = userdata.MetaTable.GetValue("__newindex")
                End If

                If newIndex Is LuaNil.Nil Then
                    Throw New Exception("Assign field of userdata without __newindex defined.")
                End If
            End If
        End If

        Dim func As LuaFunction = TryCast(newIndex, LuaFunction)
        If func IsNot Nothing Then
            func.Invoke(New LuaValue() {baseValue, key, value})
        Else
            SetKeyValue(newIndex, key, value)
        End If
    End Sub
End Class
