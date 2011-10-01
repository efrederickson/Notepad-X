Imports System.Windows.Forms

Public Class Class1
    Implements NotepadX.IPlugin
    Dim item As ToolStripMenuItem

    Public ReadOnly Property Author As String Implements NotepadX.IPlugin.Author
        Get
            Return "Elijah Frederickson"
        End Get
    End Property

    Public ReadOnly Property Description As String Implements NotepadX.IPlugin.Description
        Get
            Return "A Sample Notepad X Plugin"
        End Get
    End Property

    Public ReadOnly Property DownloadURL As String Implements NotepadX.IPlugin.DownloadURL
        Get
            Return "http://elijah.awesome99.org/NotepadXSamplePlugin.dll"
        End Get
    End Property

    Public Sub Dispose() Implements NotepadX.IPlugin.Dispose
        item.Visible = False
        item.Dispose()
    End Sub

    Public Sub Initialize() Implements NotepadX.IPlugin.Initialize
        item = New ToolStripMenuItem("Sample Plugin Item")
        AddHandler item.Click, AddressOf MenuItemClick
    End Sub

    Public ReadOnly Property OriginalFileName As String Implements NotepadX.IPlugin.OriginalFileName
        Get
            Return DownloadURL
        End Get
    End Property

    Public ReadOnly Property Name As String Implements NotepadX.IPlugin.Name
        Get
            Return "Sample Plugin"
        End Get
    End Property

    Public ReadOnly Property ToolStripItem As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.IPlugin.ToolStripItem
        Get
            Return item
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.IPlugin.Version
        Get
            Return "1.1"
        End Get
    End Property

    Sub MenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim frm As New WeifenLuo.WinFormsUI.Docking.DockContent()
        frm.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
        frm.Text = "NotepadXSamplePlugin"
        Dim btn As New Button()
        btn.Text = "Close"
        AddHandler btn.Click, Sub(s, _e)
                                  frm.Close()
                              End Sub
        btn.Parent = frm
        btn.Location = New System.Drawing.Point(10, 10)
        frm.Show(NotepadX.Main.MDIParent1.DockPanel1)
    End Sub

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
