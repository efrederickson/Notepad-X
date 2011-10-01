Public Class LuaUserdata
    Inherits LuaValue
    Private [Object] As Object

    Public Sub New(ByVal obj As Object)
        Me.[Object] = obj
    End Sub

    Public Sub New(ByVal obj As Object, ByVal metatable As LuaTable)
        Me.[Object] = obj
        Me.MetaTable = metatable
    End Sub

    Public Overrides ReadOnly Property Value() As Object
        Get
            Return Me.[Object]
        End Get
    End Property

    Public Property MetaTable() As LuaTable

    Public Overrides Function GetTypeCode() As String
        Return "userdata"
    End Function

    Public Overrides Function ToString() As String
        Return "userdata"
    End Function
End Class
