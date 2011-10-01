Public MustInherit Class LuaValue
    Implements IEquatable(Of LuaValue)
    Public MustOverride ReadOnly Property Value() As Object

    Public MustOverride Function GetTypeCode() As String

    Public Overridable Function GetBooleanValue() As Boolean
        Return True
    End Function

    Public Overloads Function Equals(ByVal other As LuaValue) As Boolean Implements IEquatable(Of LuaScriptingPlugin.LuaValue).Equals
        If other Is Nothing Then
            Return False
        End If

        If TypeOf Me Is LuaNil Then
            Return TypeOf other Is LuaNil
        End If

        If TypeOf Me Is LuaTable AndAlso TypeOf other Is LuaTable Then
            Return Object.ReferenceEquals(Me, other)
        End If

        Return Me.Value.Equals(other.Value)
    End Function

    Public Overrides Function GetHashCode() As Integer
        If TypeOf Me Is LuaNumber OrElse TypeOf Me Is LuaString Then
            Return Me.Value.GetHashCode()
        End If

        Return MyBase.GetHashCode()
    End Function

    Public Shared Function GetKeyValue(ByVal baseValue As LuaValue, ByVal key As LuaValue) As LuaValue
        Dim table As LuaTable = TryCast(baseValue, LuaTable)

        If table IsNot Nothing Then
            Return table.GetValue(key)
        Else
            Dim userdata As LuaUserdata = TryCast(baseValue, LuaUserdata)
            If userdata IsNot Nothing Then
                If userdata.MetaTable IsNot Nothing Then
                    Dim index As LuaValue = userdata.MetaTable.GetValue("__index")
                    If index IsNot Nothing Then
                        Dim func As LuaFunction = TryCast(index, LuaFunction)
                        If func IsNot Nothing Then
                            Return func.Invoke(New LuaValue() {baseValue, key})
                        Else
                            Return GetKeyValue(index, key)
                        End If
                    End If
                End If
            End If

            Throw New Exception(String.Format("Access field '{0}' not from a table.", key.Value))
        End If
    End Function
End Class
