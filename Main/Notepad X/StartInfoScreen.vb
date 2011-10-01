Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Public NotInheritable Class StartInfoScreen

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Version.Text = String.Format("Version {0}.{1}.{2} Build {3}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
        Copyright.Text = My.Application.Info.Copyright
        'totalProgressBar.SendToBack()
        'totalProgressBar.Dock = DockStyle.Fill
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim wb As New Web_Browser.Form1()
        wb.Show()
        wb.NavigateTo("http://sourceforge.com/projects/notepadx")
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim wb As New Web_Browser.Form1()
        wb.Show()
        wb.NavigateTo("http://elijah.awesome99.org/index.php/notepad-x")
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        BackgroundWorker1.ReportProgress(-5, 10)
        BackgroundWorker1.ReportProgress(-1, "Initializing Log...")
        InitializeLog()
        BackgroundWorker1.ReportProgress(-3, 1)
        'Create the Documents\Notepad X folders
        BackgroundWorker1.ReportProgress(-1, "Checking Folders...")
        Dim baseFolder As String = SpecialDirectories.MyDocuments
        If Not IO.Directory.Exists(baseFolder & "\Notepad X") Then
            IO.Directory.CreateDirectory(baseFolder & "\Notepad X")
        End If
        If Not IO.Directory.Exists(baseFolder & "\Notepad X\Macros") Then
            IO.Directory.CreateDirectory(baseFolder & "\Notepad X\Macros")
        End If
        If Not IO.Directory.Exists(baseFolder & "\Notepad X\SyntaxFiles") Then
            IO.Directory.CreateDirectory(baseFolder & "\Notepad X\SyntaxFiles")
        End If
        If Not IO.Directory.Exists(NotepadX_DocumentPath & "\Plugins") Then
            Directory.CreateDirectory(NotepadX_DocumentPath & "\Plugins")
        End If
        If Not IO.Directory.Exists(Application.LocalUserAppDataPath & "\SyntaxDefinitions\") Then
            Directory.CreateDirectory(Application.LocalUserAppDataPath & "\SyntaxDefinitions\")
        End If
        If Not IO.Directory.Exists(NotepadX_DocumentPath & "\Icons") Then
            IO.Directory.CreateDirectory(NotepadX_DocumentPath & "\Icons")
        End If
        'check for the local AppData's Notepad X directory.
        'if its not there, then create it.
        If Not IO.Directory.Exists(Application.LocalUserAppDataPath & "\Notepad X\") Then
            If Not IO.Directory.Exists(Application.LocalUserAppDataPath & "\Notepad X") Then
                IO.Directory.CreateDirectory(Application.LocalUserAppDataPath & "\Notepad X\")
            End If

            If Not IO.Directory.Exists(Application.LocalUserAppDataPath & "\Notepad X\Settings\") Then
                IO.Directory.CreateDirectory(Application.LocalUserAppDataPath & "\Notepad X\Settings\")
            End If
            Threading.Thread.Sleep(1)
            Dim notif As New NotifyIcon
            notif.ShowBalloonTip(5, "Notepad X", "Created Settings!", ToolTipIcon.Info)
            Threading.Thread.Sleep(1000)
            notif.Dispose()
        End If
        If Not IO.Directory.Exists(Application.LocalUserAppDataPath & "\Notepad X\Settings") Then
            IO.Directory.CreateDirectory(Application.LocalUserAppDataPath & "\Notepad X\Settings")
        End If
        'create the "smart" encryption and decryption code files
        If Not IO.File.Exists(Application.LocalUserAppDataPath & "\Notepad X\Settings\Encryption Codes.dat") Then
            Dim j = New StreamWriter(Application.LocalUserAppDataPath & "\Notepad X\Settings\Encryption Codes.dat")
            j.Close()
        End If
        If Not IO.File.Exists(Application.LocalUserAppDataPath & "\Notepad X\Settings\Decryption Codes.dat") Then
            Dim j As New StreamWriter(Application.LocalUserAppDataPath & "\Notepad X\Settings\Decryption Codes.dat")
            j.Close()
        End If
        BackgroundWorker1.ReportProgress(-3, 1)
        BackgroundWorker1.ReportProgress(-1, "Checking Default Items...")
        If Not CBool(My.Computer.Registry.CurrentUser.GetValue(String.Format("{0}.{1}.{2}.{3}.Used", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision))) Then
            My.Computer.Registry.CurrentUser.SetValue(String.Format("{0}.{1}.{2}.{3}.Used", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision), "1")
            If MsgBox("Would You like to set Notepad X as the default program for text files?", MsgBoxStyle.YesNo) = DialogResult.Yes Then
                RegEntries.ShowDialog()
                My.Settings.DefaultSaveLocation = SpecialDirectories.MyDocuments & "\Notepad X"
            End If
        Else
            My.Computer.Registry.CurrentUser.SetValue(String.Format("{0}.{1}.{2}.{3}.Used", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision), "1")
        End If
        BackgroundWorker1.ReportProgress(-3, 1)
        BackgroundWorker1.ReportProgress(-1, "Loading Encryption Codes...")
        Dim input As IO.StreamReader
        Try
            input = New StreamReader(Application.LocalUserAppDataPath & "\Notepad X\Settings\Encryption Codes.dat")
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
            Return
        End Try
        While input.Peek <> -1
            line = input.ReadLine
            Dim line2() As String = line.Split(CChar("|"))
            encryptionCodes.Add(line2(0), line2(1))
        End While
        input.Close()
        'decryption codes. for "smart" encryption and decryption
        Try
            input = New StreamReader(Application.LocalUserAppDataPath & "\Notepad X\Settings\Decryption Codes.dat")
        Catch ex As Exception
            MsgBox(ex.ToString)
            Log.WriteLine(String.Format("Error: {0}{1}{0}", vbCrLf, ex))
            Return
        End Try
        While input.Peek <> -1
            line = input.ReadLine
            Dim line2() As String = line.Split(CChar("|"))
            decryptionCodes.Add(line2(0), line2(1))
        End While
        input.Close()
        BackgroundWorker1.ReportProgress(-3, 1)
        BackgroundWorker1.ReportProgress(-1, "Setting Basic Shell Commands...")
        'Set basic REG commands
        Dim myName As String = Application.ExecutablePath
        ExtensionMods.SetBasicCommands(myName)
        BackgroundWorker1.ReportProgress(-1, "Initializing UI/Plugins...")
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        If e.ProgressPercentage = -1 Then
            progressLabel.Text = CStr(e.UserState)
        ElseIf e.ProgressPercentage = -3 Then
            totalProgressBar.Value += CInt(e.UserState)
        ElseIf e.ProgressPercentage = -5 Then
            totalProgressBar.Maximum = CInt(e.UserState) + 1
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
    	MDIParent1 = New MDIParent()
    	Dim args() As String = System.Environment.GetCommandLineArgs
        MDIParent1.ProcessCommandLineArguments(args)
        'MDIParent1 = Module1.IconManager.Check(MDIParent1)
        'MDIParent1 = Module1.LanguageManager.Check(MDIParent1)
        PluginManager = New PluginService(MDIParent1, NotepadX_DocumentPath & "\Plugins\Plugins.ini")
        progressLabel.Text = "Loading Plugins..."
        PluginManager.FindPlugins()
        PluginManager.FindPlugins(NotepadX_DocumentPath & "\Plugins")
        If Not My.Application.IsNetworkDeployed Then
            'Encourage them to install!
            MDIParent1.InstallToolStripMenuItem.Visible = True
        End If
        AddHandler MDIParent1.FormClosed, Sub()
                                              My.Settings.Save()
                                              Application.Exit()
        End Sub
        progressLabel.Text = "Loading L#..."
        Main.LoadLSharp()
        MDIParent1.Show()
        Visible = False
    End Sub

    Private Sub StartInfoScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        StartLoading()
    End Sub

    Function AssemblyLoaded(ByVal sender As Object, ByVal e As AssemblyLoadEventArgs) As Object
        Log.WriteLine(String.Format("Loading '{0}'", e.LoadedAssembly.ManifestModule.Name))
        progressLabel.Text = String.Format("Loading '{0}'", e.LoadedAssembly.ManifestModule.Name)
        Return Nothing
    End Function

    Sub StartLoading()
        If String.IsNullOrWhiteSpace(My.Settings.DefaultSaveLocation) Or String.IsNullOrEmpty(My.Settings.DefaultSaveLocation) Then
            My.Settings.DefaultSaveLocation = NotepadX_DocumentPath & "\"
        End If
        'Module1.IconManager.LoadIcons(My.Settings.IconFile)
        BackgroundWorker1.RunWorkerAsync()
    End Sub
End Class
