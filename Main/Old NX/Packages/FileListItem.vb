Public Class FileListItem
    Inherits ListViewItem
    Public isDir As Boolean
    Private _filename As String
    Public Dir As String
    Public Property Filename As String
        Get
            Return _filename
        End Get
        Set(ByVal value As String)
            _filename = value
        End Set
    End Property

    Sub New(ByVal filename As String, ByVal dir As Boolean)
        Me.Filename = filename
        Me.isDir = dir
        If Me.isDir Then
            Dim nfn As String = IO.Path.GetFileName(filename) & "\"
            Me.Dir = nfn
        Else
            Me.Dir = ""
        End If
    End Sub
End Class
