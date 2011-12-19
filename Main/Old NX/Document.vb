Public Class Document
    Public Document As String
    Public Path As String
    Public Title As String

    Sub Save(ByVal overrideFileNAme As Boolean, Optional ByVal newFileNAme As String = "")
        If Not overrideFileNAme Then
            IO.File.WriteAllText(Path, Document)
        Else
            IO.File.WriteAllText(newFileNAme, Document)
        End If
    End Sub

    Sub New(ByVal title As String, ByVal path As String, ByVal document As String)
        Me.Document = document
        Me.Path = path
        Me.Title = title
    End Sub
End Class
