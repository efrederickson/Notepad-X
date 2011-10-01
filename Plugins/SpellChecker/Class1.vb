Imports System.Windows.Forms

Public Class Class1
    Implements NotepadX.IPlugin
    Private searcher As New Searcher
    Public _toolitem As ToolStripMenuItem

    Public ReadOnly Property Author As String Implements NotepadX.IPlugin.Author
        Get
            Return "Elijah Frederickson"
        End Get
    End Property

    Public ReadOnly Property Description As String Implements NotepadX.IPlugin.Description
        Get
            Return "A Spell Checker Plugin"
        End Get
    End Property

    Public Sub Dispose() Implements NotepadX.IPlugin.Dispose
        _toolitem.Visible = False
        _toolitem.Dispose()
        searcher = Nothing
    End Sub

    Public ReadOnly Property OriginalFileName As String Implements NotepadX.IPlugin.OriginalFileName
        Get
            Return DownloadURL
        End Get
    End Property

    Public Sub Initialize() Implements NotepadX.IPlugin.Initialize
        _toolitem = New ToolStripMenuItem("Spell Check Document")
        AddHandler _toolitem.Click, AddressOf CheckDocument
    End Sub

    Public ReadOnly Property Name As String Implements NotepadX.IPlugin.Name
        Get
            Return "Spell Checker"
        End Get
    End Property

    Public ReadOnly Property ToolStripItem As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.IPlugin.ToolStripItem
        Get
            Return _toolitem
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.IPlugin.Version
        Get
            Return "1.1"
        End Get
    End Property

    Sub CheckDocument(ByVal sender, ByVal e)
        Dim resultText As String = ""
        Dim doc As String
        Try
            doc = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveContent, NotepadX.TextEditor).TextBox1.Text
        Catch ex As Exception
            MsgBox("Please select a Notepad X Form")
            Return
        End Try
        Dim text() As String = doc.Split((New Char() {" ", ".", ",", "?", "!", _
                                   ControlChars.Quote, "'", ":", ";", vbNewLine}))
        For Each word In text
            If Not searcher.IsWord(word) Then
                resultText &= String.Format("Word Not Found: ""{0}{1}{2}", word, ControlChars.Quote, vbCrLf)
            End If
        Next
        If resultText = "" Then
            resultText = "Document passed spell check!"
        End If
        resultText = String.Format("Spell Checker for Notepad X v{0}{1}{2}", Me.Version, vbCrLf, resultText)
        Dim results As New Results()
        results._text = resultText
        results.Text = "Spell Checker"
        results.Show(NotepadX.Main.MDIParent1.DockPanel1)
    End Sub

    Public ReadOnly Property DownloadURL As String Implements NotepadX.IPlugin.DownloadURL
        Get
            Return "http://elijah.awesome99.org/SpellChecker.dll"
        End Get
    End Property

    Public ReadOnly Property MenuItemPath As String Implements NotepadX.IPlugin.MenuItemPath
        Get
            Return "tools"
        End Get
    End Property

    Public ReadOnly Property AboutPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.AboutPage
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property HasAboutPage As Boolean Implements NotepadX.IPlugin.HasAboutPage
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property HasHelpPage As Boolean Implements NotepadX.IPlugin.HasHelpPage
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property HasOptionsPage As Boolean Implements NotepadX.IPlugin.HasOptionsPage
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property HelpPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.HelpPage
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property OptionsPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.OptionsPage
        Get
            Return Nothing
        End Get
    End Property
End Class
