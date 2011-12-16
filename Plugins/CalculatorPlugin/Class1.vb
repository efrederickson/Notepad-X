Public Class CalculatorPlugin
    Implements NotepadX.Plugins.IPlugin, NotepadX.Plugins.IMenuPlugin
    Dim _toolitem As System.Windows.Forms.ToolStripMenuItem
    Dim calc As CalculatorForm
    Dim createNew As Boolean = True

    Public ReadOnly Property Author As String Implements NotepadX.Plugins.IPlugin.Author
        Get
            Return "Elijah Frederickson"
        End Get
    End Property

    Public ReadOnly Property Description As String Implements NotepadX.Plugins.IPlugin.Description
        Get
            Return "A Calculator Plugin for Notepad X"
        End Get
    End Property

    Public Function Dispose() As Boolean Implements NotepadX.Plugins.IPlugin.Dispose, NotepadX.Plugins.IMenuPlugin.Dispose
        _toolitem.Dispose()
        calc.Dispose()
        Return True
    End Function

    Public ReadOnly Property DownloadURL As String Implements NotepadX.Plugins.IPlugin.UpdateUrl
        Get
            Return ""
        End Get
    End Property

    Public function Initialize() As Boolean Implements NotepadX.Plugins.IPlugin.Initialize, NotepadX.Plugins.IMenuPlugin.Initialize
        _toolitem = New System.Windows.Forms.ToolStripMenuItem("Calculator")
        calc = New CalculatorForm()
        AddHandler calc.FormClosed, Sub(sender, e)
                                        createNew = True
                                    End Sub
        AddHandler _toolitem.Click, AddressOf _item_Click
        Return True
    End Function

    Public ReadOnly Property Name As String Implements NotepadX.Plugins.IPlugin.Name
        Get
            Return "Calculator Plugin"
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.Plugins.IPlugin.Version
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
            NotepadX.MainForm.Instance.AddForm(CType(calc, WeifenLuo.WinformsUI.Docking.DockContent), Weifenluo.winformsui.docking.DockState.Float)
        Else
        End If
    End Sub

    Public ReadOnly Property Item() As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.Plugins.IMenuPlugin.Item
        Get
            Return _toolitem
        End Get
    End Property
    
    Public ReadOnly Property Path() As String Implements NotepadX.Plugins.IMenuPlugin.Path
        Get
            Return "tools/"
        End Get
    End Property
    
    Public ReadOnly Property Index() As Integer Implements NotepadX.Plugins.IMenuPlugin.Index
        Get
            Return 100
        End Get
    End Property
    
    Public ReadOnly Property AboutPage() As System.Windows.Forms.TabPage Implements NotepadX.Plugins.IPlugin.AboutPage
        Get
            Return Nothing
        End Get
    End Property
    
    Public ReadOnly Property OptionsPage() As System.Windows.Forms.TabPage Implements NotepadX.Plugins.IPlugin.OptionsPage
        Get
            Return Nothing
        End Get
    End Property
    
    Public ReadOnly Property HelpPage() As System.Windows.Forms.TabPage Implements NotepadX.Plugins.IPlugin.HelpPage
        Get
            Return Nothing
        End Get
    End Property

End Class
