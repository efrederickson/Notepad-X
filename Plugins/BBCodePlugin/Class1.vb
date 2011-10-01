Imports System.Windows.Forms
Imports System.Drawing

Public Class Class1
    'TODO: set the form size
    Implements NotepadX.IPlugin
    Dim _TI As ToolStripMenuItem
    Dim isShowing As Boolean = False
    Dim menu As BBCodeMenu
    Public ReadOnly Property AboutPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.AboutPage
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Author As String Implements NotepadX.IPlugin.Author
        Get
            Return "Elijah Frederickson"
        End Get
    End Property

    Public ReadOnly Property Description As String Implements NotepadX.IPlugin.Description
        Get
            Return "BBCode Plugin for Notepad X"
        End Get
    End Property

    Public Sub Dispose() Implements NotepadX.IPlugin.Dispose
        _TI.Dispose()
    End Sub

    Public ReadOnly Property DownloadURL As String Implements NotepadX.IPlugin.DownloadURL
        Get
            Return ""
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

    Public Sub Initialize() Implements NotepadX.IPlugin.Initialize
        Try
            menu = New BBCodeMenu()
            Dim frm As New WeifenLuo.WinFormsUI.Docking.DockContent() With {
                .Text = "BBCodes"
            }
            'frm.Size = New Size(frm.Size.Width, 15)
            'menu.Parent = frm
            'menu.Dock = DockStyle.Fill
            'frm.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop
            'frm.FormBorderStyle = FormBorderStyle.FixedToolWindow
            isShowing = False
            _TI = New ToolStripMenuItem("BBCodes", Nothing, Sub()
                                                                If isShowing Then
                                                                    isShowing = Not isShowing ' False
                                                                    frm.Close()
                                                                    _TI.Checked = False
                                                                Else ' its not showing
                                                                    menu = New BBCodeMenu
                                                                    isShowing = Not isShowing ' True
                                                                    frm = New WeifenLuo.WinFormsUI.Docking.DockContent() With {
                                                                    .Text = "BBCodes"
                                                                    }
                                                                    menu.Parent = frm
                                                                    menu.Dock = DockStyle.Fill
                                                                    frm.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop
                                                                    'frm.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockTopAutoHide
                                                                    frm.FormBorderStyle = FormBorderStyle.FixedToolWindow
                                                                    AddHandler frm.FormClosed, Sub()
                                                                                                   _TI.Checked = False
                                                                                                   isShowing = False
                                                                                               End Sub
                                                                    frm.Show(NotepadX.Main.MDIParent1.DockPanel1)
                                                                    frm.Size = New Size(15, 15)
                                                                    _TI.Checked = True
                                                                End If
                                                            End Sub) With {
            .Checked = False
        }
            _TI.PerformClick()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public ReadOnly Property MenuItemPath As String Implements NotepadX.IPlugin.MenuItemPath
        Get
            Return "tools/"
        End Get
    End Property

    Public ReadOnly Property Name As String Implements NotepadX.IPlugin.Name
        Get
            Return "BBCode Plugin"
        End Get
    End Property

    Public ReadOnly Property OptionsPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.OptionsPage
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property OriginalFileName As String Implements NotepadX.IPlugin.OriginalFileName
        Get
            Return DownloadURL
        End Get
    End Property

    Public ReadOnly Property ToolStripItem As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.IPlugin.ToolStripItem
        Get
            Return _TI
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.IPlugin.Version
        Get
            Return "1.0"
        End Get
    End Property
End Class
