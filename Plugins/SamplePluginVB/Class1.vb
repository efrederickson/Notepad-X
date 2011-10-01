Public Class Class1
    Implements NotepadX.IPlugin, IDisposable
    'the actual ToolStripItem for Notepad X usage
    Private _toolStripItem As System.Windows.Forms.ToolStripMenuItem

    Public ReadOnly Property Author As String Implements NotepadX.IPlugin.Author
        Get
            'Your name. Or your companies name.
            Return "Your name"
        End Get
    End Property

    Public ReadOnly Property Description As String Implements NotepadX.IPlugin.Description
        Get
            ' The Plugin Description
            Return "Plugin Description"
        End Get
    End Property

    Public Sub Dispose() Implements NotepadX.IPlugin.Dispose, IDisposable.Dispose
        'Clean up and remove the plugin from existence
        _toolStripItem.Dispose()
    End Sub

    Public ReadOnly Property DownloadURL As String Implements NotepadX.IPlugin.DownloadURL
        Get
            'The URL of the actual .DLL file
            Return ""
        End Get
    End Property

    Public Sub Initialize() Implements NotepadX.IPlugin.Initialize
        'Create the ToolStripItem and other initialization
        _toolStripItem = New System.Windows.Forms.ToolStripMenuItem("SamplePluginVB")
    End Sub

    Public ReadOnly Property Name As String Implements NotepadX.IPlugin.Name
        Get
            'The Plugins name
            Return My.Application.Info.ProductName
        End Get
    End Property

    Public ReadOnly Property OriginalFileName As String Implements NotepadX.IPlugin.OriginalFileName
        Get
            'hmmm... easiest to set it to this.
            Return DownloadURL
        End Get
    End Property

    Public ReadOnly Property ToolStripItem As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.IPlugin.ToolStripItem
        Get
            'the ToolStripMenuItem for under the "Tools" menu
            Return _toolStripItem
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.IPlugin.Version
        Get
            'the Plugin Version
            Return My.Application.Info.Version.ToString
        End Get
    End Property

    Public ReadOnly Property MenuItemPath As String Implements NotepadX.IPlugin.MenuItemPath
        Get
            Return "new/Sample Plugins"
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
