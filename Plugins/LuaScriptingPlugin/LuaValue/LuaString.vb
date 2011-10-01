Public Class LuaString
    Inherits LuaValue

    Public Sub New(ByVal text As String)
        Me.Text = text
    End Sub

    Public Shared ReadOnly Empty As New LuaString(String.Empty)

    Public Property Text() As String
        Get
            Return m_Text
        End Get
        Set(ByVal value As String)
            m_Text = Value
        End Set
    End Property
    Private m_Text As String

    Public Overrides ReadOnly Property Value() As Object
        Get
            Return Me.Text
        End Get
    End Property

    Public Overrides Function GetTypeCode() As String
        Return "string"
    End Function

    Public Overrides Function ToString() As String
        Return Me.Text
    End Function
End Class
