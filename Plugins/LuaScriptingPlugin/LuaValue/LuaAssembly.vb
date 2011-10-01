Imports System.Reflection

Public Class LuaAssembly
    Inherits LuaValue

    Dim m_assembly As Assembly

    Public Overrides ReadOnly Property Value() As Object
        Get
            If m_assembly IsNot Nothing Then
                Return Me.m_assembly
            Else
                Return Assembly.LoadWithPartialName("mscorlib")
            End If
        End Get
    End Property

    Public Overrides Function GetTypeCode() As String
        Return "Assembly"
    End Function

    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function

    Private Sub New(ByVal assembly As Assembly)
        Me.m_assembly = assembly
    End Sub

    Public Shared Function From(ByVal value As Assembly) As LuaAssembly
        Return New LuaAssembly(value)
    End Function
End Class
