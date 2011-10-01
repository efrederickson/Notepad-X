Public Class CalculatorPlugin
    Implements NotepadX.IPlugin
    Dim _toolitem As System.Windows.Forms.ToolStripMenuItem
    Dim calc As CalculatorForm
    Dim createNew As Boolean = True

    Public ReadOnly Property Author As String Implements NotepadX.IPlugin.Author
        Get
            Return "Elijah Frederickson"
        End Get
    End Property

    Public ReadOnly Property Description As String Implements NotepadX.IPlugin.Description
        Get
            Return "A Calculator Plugin for Notepad X"
        End Get
    End Property

    Public Sub Dispose() Implements NotepadX.IPlugin.Dispose
        _toolitem.Dispose()
        calc.Dispose()
    End Sub

    Public ReadOnly Property DownloadURL As String Implements NotepadX.IPlugin.DownloadURL
        Get
            Return "http://elijah.awesome99.org/CalculatorPlugin.dll"
        End Get
    End Property

    Public Sub Initialize() Implements NotepadX.IPlugin.Initialize
        _toolitem = New System.Windows.Forms.ToolStripMenuItem("Calculator")
        calc = New CalculatorForm()
        AddHandler calc.FormClosed, Sub(sender, e)
                                        createNew = True
                                    End Sub
        AddHandler _toolitem.Click, AddressOf _item_Click
    End Sub

    Public ReadOnly Property Name As String Implements NotepadX.IPlugin.Name
        Get
            Return "Calculator Plugin"
        End Get
    End Property

    Public ReadOnly Property OriginalFileName As String Implements NotepadX.IPlugin.OriginalFileName
        Get
            Return DownloadURL
        End Get
    End Property

    Public ReadOnly Property ToolStripItem As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.IPlugin.ToolStripItem
        Get
            Return _toolitem
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.IPlugin.Version
        Get
            Return "1.0"
        End Get
    End Property

    Sub _item_Click(ByVal sender, ByVal e)
        If createNew Then
            calc = New CalculatorForm()
            AddHandler calc.FormClosed, Sub(_sender, _e)
                                            createNew = True
                                        End Sub
            createNew = False
            calc.Show(NotepadX.Main.MDIParent1.DockPanel1)
        Else
            calc.BringToFront()
            calc.Select()
        End If
    End Sub

    Public ReadOnly Property MenuItemPath As String Implements NotepadX.IPlugin.MenuItemPath
        Get
            Return "tools/"
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
