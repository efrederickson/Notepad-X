﻿Public Class Class1
    Implements NotepadX.IPlugin
    Dim E As AlwaysOnTopEncryptionForm
    Dim _toolItem As System.Windows.Forms.ToolStripMenuItem
    Dim createNew As Boolean = True

    Public ReadOnly Property Author As String Implements NotepadX.IPlugin.Author
        Get
            Return "Elijah Frederickson"
        End Get
    End Property

    Public ReadOnly Property Description As String Implements NotepadX.IPlugin.Description
        Get
            Return "An Encryption plugin for Notepad X"
        End Get
    End Property

    Public Sub Dispose() Implements NotepadX.IPlugin.Dispose
        E.Dispose()
        _toolItem.Dispose()
    End Sub

    Public ReadOnly Property DownloadURL As String Implements NotepadX.IPlugin.DownloadURL
        Get
            Return "http://elijah.awesome99.org/EncryptionPlugin.dll"
        End Get
    End Property

    Public Sub Initialize() Implements NotepadX.IPlugin.Initialize
        E = New AlwaysOnTopEncryptionForm
        _toolItem = New System.Windows.Forms.ToolStripMenuItem("Encrypt Document")
        AddHandler E.FormClosed, Sub()
                                     createNew = True
                                 End Sub
        AddHandler _toolItem.Click, AddressOf _toolItem_CLick
    End Sub

    Public ReadOnly Property Name As String Implements NotepadX.IPlugin.Name
        Get
            Return "Encryption Plugin"
        End Get
    End Property

    Public ReadOnly Property OriginalFileName As String Implements NotepadX.IPlugin.OriginalFileName
        Get
            Return DownloadURL
        End Get
    End Property

    Public ReadOnly Property ToolStripItem As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.IPlugin.ToolStripItem
        Get
            Return _toolItem
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.IPlugin.Version
        Get
            Return "1.0"
        End Get
    End Property

    Sub _toolItem_CLick()
        If createNew Then
            E = New AlwaysOnTopEncryptionForm
            AddHandler E.FormClosed, Sub(_sender, _e)
                                         createNew = True
                                     End Sub
            createNew = False
            E.Show(NotepadX.Main.MDIParent1.DockPanel1)
        Else
            E.BringToFront()
            E.Select()
        End If
    End Sub

    Public ReadOnly Property MenuItemPath As String Implements NotepadX.IPlugin.MenuItemPath
        Get
            Return "new/Encryption"
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
