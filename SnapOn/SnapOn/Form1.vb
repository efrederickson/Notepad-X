Public Class Form1
    Public snaponfile As String = Application.LocalUserAppDataPath & "\SnapOnData.txt"
    Public Delegate Sub ProcessParametersDelegate(ByVal sender As Object, ByVal args As String())

    Public Sub ProcessParameters(ByVal sender As Object, ByVal args As String())
        Me.BringToFront()
        If args.Count = 0 Then Return
        OPEN(args(0))
        Me.BringToFront()
    End Sub

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        If Not IO.Directory.Exists(Application.StartupPath & "\SnapOns") Then
            IO.Directory.CreateDirectory(Application.StartupPath & "\SnapOns")
        End If
        [Set](".snapon", "SnapOn Definition File", Application.ExecutablePath)
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Using strm As New IO.StreamWriter(snaponfile)
            For Each file In Files
                strm.WriteLine(file)
            Next
            strm.Close()
        End Using
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim args() As String = Environment.GetCommandLineArgs
        If args.Count > 2 Then
            If IO.File.Exists(args(1)) Then
                LoadSnapOnFile(IO.File.ReadAllText(args(1)), args(1))
            End If
        End If
        If Not IO.File.Exists(snaponfile) Then
            IO.File.WriteAllText(snaponfile, "")
        End If
        For Each line As String In IO.File.ReadAllLines(snaponfile)
            Dim hash As String() = PluginManager.GetPluginInfo(line, PluginInterface)
            If hash IsNot Nothing Then
                ListView1.Items.Add(New ListViewItem(hash(0)) With {.Tag = line})
                Files.Add(line)
            Else
                MsgBox(String.Format("The file '{0}' isn't a valid Snap-On application!", line))
            End If
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Using openfiledialog As New OpenFileDialog() With {.Filter = "SnapOn Definition Files (*.snapon)|*.snapon|.NET Assembly File (*.dll; *.exe)|*.dll; *.exe"}
            If openfiledialog.ShowDialog = DialogResult.OK Then
                OPEN(openfiledialog.FileName)
            End If
        End Using
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            For Each item As ListViewItem In ListView1.SelectedItems
                ListView1.Items.Remove(item)
                Files.Remove(CStr(item.Tag))
                IO.File.Delete(CStr(item.Tag))
            Next
        Catch ex As Exception
            MsgBox(String.Format("An Error Occurred: {0}{1}", vbCrLf, ex.ToString))
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            PluginManager.AddPlugin(CStr(ListView1.SelectedItems(0).Tag))
            Dim hash() As String = PluginManager.GetPluginInfo(CStr(ListView1.SelectedItems(0).Tag), PluginInterface)
            For Each plugin As AvailablePlugin In PluginManager.AvailablePlugins
                If plugin.Instance.Name = hash(0) Then
                    plugin.Instance.Form.Show()
                End If
            Next
        Catch ex As Exception
            MsgBox(String.Format("An Error Occurred: {0}{1}", vbCrLf, ex.ToString))
        End Try
    End Sub

    Public Sub LoadSnapOnFile(ByVal text As String, ByVal filename As String)
        Dim lines() As String = text.Split(CChar(vbCrLf))
        For Each line In lines
            If Not IO.File.Exists(String.Format("{0}\{1}", IO.Path.GetDirectoryName(filename), line)) Then
                Continue For
            End If
            Packages.Unpack(String.Format("{0}\{1}", IO.Path.GetDirectoryName(filename), line), Application.StartupPath & "\SnapOns")
        Next
        For Each file In IO.Directory.GetFiles(Application.StartupPath & "\SnapOns")
            Dim hash() As String = PluginManager.GetPluginInfo(file, PluginInterface)
            If hash IsNot Nothing Then
                Files.Add(file)
                ListView1.Items.Add(New ListViewItem(hash(0)) With { _
                    .Tag = String.Format(file)})
            End If
        Next
    End Sub

    Shared Sub [Set](ByVal extension As String, ByVal extInfo As String, ByVal myName As String)
        With My.Computer.Registry
            .SetValue(String.Format("HKEY_CURRENT_USER\Software\Classes\NotepadX{0}.File", extension), "", "")
            .SetValue(String.Format("HKEY_CURRENT_USER\software\Classes\NotepadX{0}.File\", extension), "", extInfo)
            .SetValue(String.Format("HKEY_CURRENT_USER\software\Classes\NotepadX{0}.File\shell", extension), "", "")
            .SetValue(String.Format("HKEY_CURRENT_USER\software\Classes\NotepadX{0}.File\shell\open", extension), "", "")
            .SetValue(String.Format("HKEY_CURRENT_USER\software\Classes\NotepadX{0}.File\shell\open\command", extension), "", String.Format("{0}{1}{0} {0}%1{0}", ControlChars.Quote, myName))
            .SetValue("HKEY_CURRENT_USER\Software\Classes\" & extension, "", String.Format("NotepadX{0}.File", extension))
        End With
    End Sub

    Sub OPEN(ByVal filenaem As String)
        If filenaem.ToLower.EndsWith(".snapon") Then
            LoadSnapOnFile(IO.File.ReadAllText(filenaem), filenaem)
        Else
            Dim hash() As String = PluginManager.GetPluginInfo(filenaem, PluginInterface)
            If hash IsNot Nothing Then
                Files.Add(filenaem)
                ListView1.Items.Add(New ListViewItem(hash(0)) With {.Tag = filenaem})
            Else
                MsgBox("Invalid Snap-On File!")
            End If
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim hash() As String = PluginManager.GetPluginInfo(CStr(ListView1.SelectedItems(0).Tag), PluginInterface)
            Dim version As String = hash(1)
            Dim downloadURL As String = hash(2)
            Using DownLoad As New Download(downloadURL, Application.StartupPath & "\snapon.snapon", SnapOn.Download.DownloadTypes.Autoclose)
                DownLoad.ShowDialog()
            End Using
            LoadSnapOnFile(IO.File.ReadAllText(Application.StartupPath & "\snapon.snapon"), Application.StartupPath & "\snapon.snapon")
            IO.File.Delete(Application.StartupPath & "\snapon.snapon")
        Catch ex As Exception
        End Try
    End Sub
End Class
