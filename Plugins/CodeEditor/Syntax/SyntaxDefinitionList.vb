Imports Alsing.SourceCode
Imports System.IO

Public Class SyntaxDefinitionList
    Private ReadOnly languages As New List(Of SyntaxDefinition)

    Public Function GetLanguageFromFile(ByVal path As String) As SyntaxDefinition
        Dim s As SyntaxDefinition
        s = SyntaxDefinition.FromSyntaxFile(path)
        languages.Add(s)
        Return s
    End Function

    Public Function GetSyntaxDefinitions() As List(Of SyntaxDefinition)
        Return languages
    End Function
End Class