Public Class LuaNotepadXMDIParent
    Inherits LuaValue

    Dim m_form As NotepadX.MDIParent

    Public Overrides ReadOnly Property Value() As Object
        Get
            Return m_form
        End Get
    End Property

    Public Overrides Function GetTypeCode() As String
        Return "NotepadX.MDIParent"
    End Function

    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function

    Private Sub New(ByVal form As NotepadX.MDIParent)
        Me.m_form = form
    End Sub

    Public Shared Function From(ByVal value As NotepadX.MDIParent) As LuaNotepadXMDIParent
        Return New LuaNotepadXMDIParent(value)
    End Function
End Class
