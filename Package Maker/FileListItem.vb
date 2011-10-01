Public Class FileListItem
    Inherits ListViewItem

    Private _filename As String
    Public Property Filename As String
        Get
            Return _filename
        End Get
        Set(ByVal value As String)
            _filename = value
        End Set
    End Property


    Sub New(ByVal filename As String)
        Me.Filename = filename
    End Sub
End Class
