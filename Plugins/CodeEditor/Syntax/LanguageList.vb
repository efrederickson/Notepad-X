Imports System.IO
Imports System.Reflection
Public Class LanguageList
    Dim _LangList As New List(Of Stream)

    ''' <summary>
    ''' DO NOT INCLUDE THE .syn Extension!!
    ''' </summary>
    ''' <param name="lang">The Language e.g. C# or VB.NET or Cobol</param>
    ''' <returns></returns>
    Private Function GetSyntaxFileName(ByVal lang As SyntaxLanguage) As String
        Dim file = [Enum].GetName(GetType(SyntaxLanguage), lang)
        file &= ".syn"
        Return file
    End Function

    Private Function GetEmbeddedStream(ByVal name As String) As System.IO.Stream

        ' The name of the embedded resource often uses the project name as a prefix.
        ' This is set in the project properties page in VS2008. 
        Dim embeddedName As String = String.Format("CodeEditor.{0}", name)
        Dim myself = Assembly.GetExecutingAssembly()
        Return myself.GetManifestResourceStream(embeddedName)
    End Function

    ''' <summary>
    ''' Gets the List of supported Language Syntax files
    ''' </summary>
    Public ReadOnly Property LanguageList As List(Of Stream)
        Get
            _LangList.Clear()
            Dim languages As SyntaxLanguage() = CType([Enum].GetValues(GetType(CodeEditor.LanguageList.SyntaxLanguage)), SyntaxLanguage())
            For Each current In languages
                Dim strm As Stream = GetEmbeddedStream(GetSyntaxFileName(CType(current, SyntaxLanguage)))
                _LangList.Add(strm)
            Next
            Return _LangList
        End Get
    End Property

    Public Enum SyntaxLanguage
        ASP
        AutoIt
        Cobol
        CPP
        'CS
        CSharp
        CSS
        DataFlex
        Delphi
        DOSBatch
        Fortran90
        FoxPro
        Gemix
        HTML
        Java
        JavaScript
        JSP
        Lang6502
        LotusScript
        Lua
        MSIL
        MySQL_SQL
        Nemerle
        NotepadXMacro
        npath
        Oracle_SQL
        Perl
        PHP
        Povray
        Python
        rtf
        SmallTalk
        SqlServer2K
        SqlServer2K5
        SQLServer2K_SQL
        SqlServer7
        SQLServer7_SQL
        SystemPolicies
        Template
        Text
        TurboPascal
        VB
        VBNET
        VBM
        VBScript
        VRML97
        XML

    End Enum
End Class
