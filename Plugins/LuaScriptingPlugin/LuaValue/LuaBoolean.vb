Public Class LuaBoolean
    Inherits LuaValue
    Public Shared ReadOnly [False] As New LuaBoolean() With { _
      .BoolValue = False _
    }

    Public Shared ReadOnly [True] As New LuaBoolean() With { _
      .BoolValue = True _
    }

    Public Property BoolValue() As Boolean
        Get
            Return m_BoolValue
        End Get
        Set(ByVal value As Boolean)
            m_BoolValue = Value
        End Set
    End Property
    Private m_BoolValue As Boolean

    Public Overrides ReadOnly Property Value() As Object
        Get
            Return Me.BoolValue
        End Get
    End Property

    Public Overrides Function GetTypeCode() As String
        Return "boolean"
    End Function

    Public Overrides Function GetBooleanValue() As Boolean
        Return Me.BoolValue
    End Function

    Public Overrides Function ToString() As String
        Return Me.BoolValue.ToString().ToLower()
    End Function

    Private Sub New()
    End Sub

    Public Shared Function From(ByVal value As Boolean) As LuaBoolean
        If value = True Then
            Return [True]
        Else
            Return [False]
        End If
    End Function
End Class
