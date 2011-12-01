Public Class SnapOnClass
    Implements SnapOn.IPlugin

    Dim frm As StartInfoScreen

    Public Sub Dispose() Implements SnapOn.IPlugin.Dispose
        frm.Close()
        frm = Nothing
    End Sub

    Public ReadOnly Property DownloadURL As String Implements SnapOn.IPlugin.DownloadURL
        Get
            Return "http://elijah.awesome99.org/NotepadX/Notepad%20X.pack"
        End Get
    End Property

    Public ReadOnly Property Form As System.Windows.Forms.Form Implements SnapOn.IPlugin.Form
        Get
            Return Main.MDIParent1
        End Get
    End Property

    Public Sub Initialize() Implements SnapOn.IPlugin.Initialize
        frm = New StartInfoScreen()
        frm.Show()
    End Sub

    Public ReadOnly Property Name As String Implements SnapOn.IPlugin.Name
        Get
            Return "Notepad X"
        End Get
    End Property

    Public ReadOnly Property Version As String Implements SnapOn.IPlugin.Version
        Get
            Return "1.1"
        End Get
    End Property
End Class
