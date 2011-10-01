Imports System.IO

Public Class optionsForm
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
    'this is the OPTIONS FORM!!!
    'mostly, this code just sets the variables.

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        My.Settings.ShowNotifyIcon = CBool(CheckBox1.CheckState)
        If Not My.Settings.ShowNotifyIcon Then
            CheckBox2.Enabled = False
            Label2.Enabled = False
        Else
            CheckBox2.Enabled = True
            Label2.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        My.Settings.ShowFullPath = CBool(CheckBox2.CheckState)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub optionsForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.DefaultSaveLocation = TextBox5.Text
        Main.DefaultSaveLocation = My.Settings.DefaultSaveLocation
    End Sub

    Private Sub optionsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'load plugin option pages
        For Each plugin As AvailablePlugin In PluginManager.AvailablePlugins
            If plugin.Instance.HasOptionsPage Then plugin.Instance.OptionsPage.Parent = TabControl1
        Next
        'set the controls
        useSmartDecryptionCheckbox.Checked = My.Settings.UseSmartDecryption
        If My.Settings.UseSmartDecryption Then
            GroupBox3.Enabled = False
            CheckBox6.Enabled = False
            Label5.Enabled = False
            TextBox3.Enabled = False
        Else
            GroupBox3.Enabled = True
            CheckBox6.Enabled = True
            Label5.Enabled = True
            TextBox3.Enabled = True
        End If
        smartEncryptionCheckBox.Checked = My.Settings.UseSmartEncryption
        If My.Settings.UseSmartEncryption Then
            GroupBox2.Enabled = False
            CheckBox7.Enabled = False
            Label1.Enabled = False
            TextBox4.Enabled = False
        Else
            GroupBox2.Enabled = True
            CheckBox7.Enabled = True
            Label1.Enabled = True
            TextBox4.Enabled = True
        End If
        CheckBox5_CheckedChanged(sender, e)
        CheckBox4_CheckedChanged(sender, e)
        CheckBox1.Checked = My.Settings.ShowNotifyIcon
        CheckBox2.Checked = My.Settings.ShowFullPath
        CheckBox3.Checked = My.Settings.ShowStatusBar
        CheckBox4.Checked = My.Settings.AutoEncrypt
        TextBox1.Text = CStr(My.Settings.AutoEncryptCode)
        CheckBox6.Checked = My.Settings.AutoDecryptETXT
        TextBox3.Text = CStr(My.Settings.AutoDecryptETXTCode)
        If My.Settings.AutoEncryptASCII Then
            RadioButton1.Checked = True
            RadioButton2.Checked = False
        Else
            RadioButton1.Checked = False
            RadioButton2.Checked = True
        End If
        CheckBox5.Checked = My.Settings.AutoDecrypt
        TextBox2.Text = CStr(My.Settings.AutoDecryptCode)
        If My.Settings.AutoDecryptASCII Then
            RadioButton3.Checked = True
            RadioButton4.Checked = False
        Else
            RadioButton3.Checked = False
            RadioButton4.Checked = True
        End If
        If My.Settings.AutoDecryptETXT Then
            Label5.Enabled = True
            TextBox3.Enabled = True
        Else
            Label5.Enabled = False
            TextBox3.Enabled = False
        End If
        CheckBox7.CheckState = CType(My.Settings.AutoEncryptETXT, CheckState)
        TextBox4.Text = CStr(My.Settings.AutoEncryptETXTCode)
        If My.Settings.AutoEncryptETXT Then
            Label1.Enabled = True
            TextBox4.Enabled = True
        Else
            Label1.Enabled = False
            TextBox4.Enabled = False
        End If
        autoDetectURLsCheckBox.Checked = My.Settings.AutoDetectURLS
        TextBox5.Text = My.Settings.DefaultSaveLocation

        ' plugins
        pluginsListView.Items.Clear()
        For Each file As System.IO.FileInfo In (New System.IO.DirectoryInfo(NotepadX_DocumentPath & "\Plugins").GetFiles("*.dll"))
            Dim hash As String() = PluginManager.GetPluginInfo(file.FullName, PluginInterface)

            If hash IsNot Nothing Then
                Dim newItem As New ListViewItem()
                newItem.Name = hash(0)
                'name
                newItem.Text = hash(0)
                'name
                newItem.SubItems.Add(file.FullName) 'Path
                newItem.SubItems.Add(hash(1))
                'author
                newItem.SubItems.Add(hash(2))
                'version
                newItem.SubItems.Add(hash(3))
                'description
                newItem.SubItems.Add(hash(4)) 'DownloadURL
                pluginsListView.Items.Add(newItem)
                If PluginManager.AvailablePlugins.Exist(file.FullName) = True Then
                    newItem.Checked = True
                End If
            End If
        Next
        'icons
        'For Each File In IO.Directory.GetFiles(NotepadX_DocumentPath & "\Icons", "*.ini")
        '    Module1.IconManager.LoadIcons(File)
        '    Dim item As New ListViewItem(Module1.IconManager.Info("Name").ToString) With {
        '    .Tag = File
        '               }
        '    If File = My.Settings.IconFile Then
        '        item.Checked = True
        '    End If
        '    ListView1.Items.Add(item)
        'Next
        'Module1.IconManager.LoadIcons(My.Settings.IconFile)
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        My.Settings.ShowStatusBar = CBool(CheckBox3.CheckState)
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        My.Settings.AutoEncrypt = CBool(CheckBox4.CheckState)
        If My.Settings.AutoEncrypt Then
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
            TextBox1.Enabled = True
            Label3.Enabled = True
        Else
            RadioButton1.Enabled = False
            RadioButton2.Enabled = False
            TextBox1.Enabled = False
            Label3.Enabled = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        My.Settings.AutoEncryptASCII = RadioButton1.Checked
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'verify the text is an int
        Try
            If CInt(TextBox1.Text) > 0 Then
                My.Settings.AutoEncryptCode = CInt(TextBox1.Text)
            Else
                MessageBox.Show("Please enter a non-negative integer", "Notepad X", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch
            MessageBox.Show("Please enter an integer", "Notepad X", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        My.Settings.AutoDecrypt = CBool(CheckBox5.CheckState)
        If My.Settings.AutoDecrypt Then
            Label4.Enabled = True
            TextBox2.Enabled = True
            RadioButton3.Enabled = True
            RadioButton4.Enabled = True
        Else
            Label4.Enabled = False
            TextBox2.Enabled = False
            RadioButton3.Enabled = False
            RadioButton4.Enabled = False
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        My.Settings.AutoDecryptASCII = RadioButton3.Checked
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        'verify the text is an int
        Try
            If CInt(TextBox2.Text) > 0 Then
                My.Settings.AutoDecryptCode = CInt(TextBox1.Text)
            Else
                MessageBox.Show("Please enter a non-negative integer", "Notepad X", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch
            MessageBox.Show("Please enter an integer", "Notepad X", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        My.Settings.AutoDecryptETXT = CBool(CheckBox6.CheckState)
        If My.Settings.AutoDecryptETXT Then
            Label5.Enabled = True
            TextBox3.Enabled = True
        Else
            Label5.Enabled = False
            TextBox3.Enabled = False
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        My.Settings.AutoEncryptETXT = CBool(CheckBox7.CheckState)
        If My.Settings.AutoEncryptETXT Then
            Label1.Enabled = True
            TextBox4.Enabled = True
        Else
            Label1.Enabled = False
            TextBox4.Enabled = False
        End If
    End Sub

    Private Sub smartEncryptionCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smartEncryptionCheckBox.CheckedChanged
        My.Settings.UseSmartEncryption = smartEncryptionCheckBox.Checked
        If My.Settings.UseSmartEncryption Then
            GroupBox2.Enabled = False
            CheckBox7.Enabled = False
            Label1.Enabled = False
            TextBox4.Enabled = False
        Else
            GroupBox2.Enabled = True
            CheckBox7.Enabled = True
            Label1.Enabled = True
            TextBox4.Enabled = True
        End If
    End Sub

    Private Sub useSmartDecryptionCheckbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles useSmartDecryptionCheckbox.CheckedChanged
        My.Settings.UseSmartDecryption = useSmartDecryptionCheckbox.Checked
        If My.Settings.UseSmartDecryption Then
            GroupBox3.Enabled = False
            CheckBox6.Enabled = False
            Label5.Enabled = False
            TextBox3.Enabled = False
        Else
            GroupBox3.Enabled = True
            CheckBox6.Enabled = True
            Label5.Enabled = True
            TextBox3.Enabled = True
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim newAddCodeForm As New addCodeForm()
        newAddCodeForm.ShowDialog()
        Try
            encryptionCodes.Add(newAddCodeForm.File, newAddCodeForm.Code)
        Catch ex As Exception
            MsgBox("The File already has an encryption code.")
        End Try
        optionsForm_Load(sender, e)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim newAddCodeForm As New addCodeForm()
        newAddCodeForm.ShowDialog()
        Try
            decryptionCodes.Add(newAddCodeForm.File, newAddCodeForm.Code)
        Catch ex As Exception
            MsgBox("The File already has an encryption code.")
        End Try
        optionsForm_Load(sender, e)
    End Sub

    Private Sub encryptionRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles encryptionRadioButton.CheckedChanged
        DADADA.Items.Clear()
        savedCodesListBox.Items.Clear()
        If encryptionRadioButton.Checked Then
            Dim keys, values As New List(Of String)
            For Each key In encryptionCodes.Keys
                keys.Add(CStr(key))
            Next
            For Each value In encryptionCodes.Values
                values.Add(CStr(value))
            Next
            For i = 0 To keys.Count - 1
                savedCodesListBox.Items.Add("File: " & keys(i) & " | Code: " & values(i))
                DADADA.Items.Add(keys(i))
            Next
        Else
            Dim keys, values As New List(Of String)
            For Each key In decryptionCodes.Keys
                keys.Add(CStr(key))
            Next
            For Each value In decryptionCodes.Values
                values.Add(CStr(value))
            Next
            For i = 0 To keys.Count - 1
                savedCodesListBox.Items.Add("File: " & keys(i) & " | Code: " & values(i))
                DADADA.Items.Add(keys(i))
            Next
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim index As Integer = -1
        Dim item As String = CStr(-1)
        Try
            index = savedCodesListBox.SelectedIndex
            item = CStr(DADADA.Items(index))
        Catch ex As Exception
            MessageBox.Show("Please make sure you have selected an item.", "Notepad X", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If encryptionRadioButton.Checked Then
            encryptionCodes.Remove(item)
        Else
            decryptionCodes.Remove(item)
        End If
        encryptionRadioButton_CheckedChanged(sender, e)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim str As String = "Encryption Codes: "
        Dim keys As New List(Of String)
        Dim values As New List(Of String)
        For Each key In encryptionCodes.Keys
            keys.Add(CStr(key))
        Next
        For Each value In encryptionCodes.Values
            values.Add(CStr(value))
        Next
        For i = 0 To keys.Count - 1
            str &= keys(i) & " " & values(i) & vbCrLf
        Next
        values.Clear()
        keys.Clear()
        For Each key In decryptionCodes.Keys
            keys.Add(CStr(key))
        Next
        For Each value In decryptionCodes.Values
            values.Add(CStr(value))
        Next
        str &= "Decryption Codes: "
        For i = 0 To keys.Count - 1
            str &= keys(i) & " " & values(i) & vbCrLf
        Next
        MsgBox(str)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If MsgBox("Are you sure? This action cannot be undone", MsgBoxStyle.YesNo) = DialogResult.Yes Then
            encryptionCodes.Clear()
            decryptionCodes.Clear()
            Log.WriteLine("Erased All Encryption/Decryption Codes ")
        End If
    End Sub

    Private Sub autoDetectURLsCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles autoDetectURLsCheckBox.CheckedChanged
        My.Settings.AutoDetectURLS = autoDetectURLsCheckBox.Checked
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim pb As New Library.Controls.ProgressBar
        pb.Parent = Me
        pb.Location = New Point(10, 10)
        Dim files() As String = Directory.GetFiles(Application.LocalUserAppDataPath & "\SyntaxDefinitions\")
        pb.Dock = DockStyle.Fill
        pb.Maximum = files.Count
        pb.Value = 0
        pb.TextShow = Library.Controls.ProgressBar.eTextShow.TextOnly
        For Each File In files
            IO.File.Delete(File)
            pb.Value += 1
            pb.TextValue = "Deleting Files... " & pb.ValuePercent & "% Done (" & pb.Value & " out of " & pb.Maximum & ")"
        Next
        pb.Dispose()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim fb As New FolderBrowserDialog
        If fb.ShowDialog = DialogResult.OK Then
            TextBox5.Text = fb.SelectedPath
        End If

    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        My.Settings.DefaultSaveLocation = TextBox5.Text
    End Sub

    Private Sub pi_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pi_add.Click
        Try
            Dim dialog As New OpenFileDialog()
            dialog.Filter = "Notepad X Plugins (*.dll)|*.dll"
            dialog.Title = "Add Plugins"
            dialog.CheckFileExists = True
            If dialog.ShowDialog() = DialogResult.OK Then
                For Each file As String In dialog.FileNames
                    Dim hash As String() = PluginManager.GetPluginInfo(file, PluginInterface)
                    If hash IsNot Nothing Then
                        Dim path As String = String.Format("{0}\Plugins\{1}", NotepadX_DocumentPath, System.IO.Path.GetFileName(file))
                        If path.ToLower() <> file.ToLower() Then
                            System.IO.File.Copy(file, path, True)
                        End If
                        If listContains(pluginsListView, hash(0)) <> True Then
                            'name
                            Dim newItem As New ListViewItem() With {
                                .Text = hash(0)
                            }
                            newItem.SubItems.Add(file) ' Path
                            newItem.SubItems.Add(hash(1)) 'author
                            newItem.SubItems.Add(hash(2)) 'version
                            newItem.SubItems.Add(hash(3)) 'description
                            newItem.SubItems.Add(hash(4)) ' Download URL
                            pluginsListView.Items.Add(newItem)
                            If PluginManager.PluginINI.GetSection(path) IsNot Nothing Then
                            Else
                                Dim ret As String() = PluginManager.GetPluginInfo(path, PluginInterface)
                                Dim plugin As INIFile.INISection = PluginManager.PluginINI.AddSection(path)
                                Dim nameKey = plugin.AddKey("Name")
                                nameKey.SetValue(ret(0))
                                Dim authorKey = plugin.AddKey("Author")
                                authorKey.SetValue(ret(1))
                                Dim versionKey = plugin.AddKey("Version")
                                versionKey.SetValue(ret(2))
                                Dim descriptionKey = plugin.AddKey("Description")
                                descriptionKey.SetValue(ret(3))
                                Dim downloadUrlKey = plugin.AddKey("DownloadURL")
                                downloadUrlKey.SetValue(ret(4))
                                Dim origFileName = plugin.AddKey("OriginalFilename")
                                origFileName.SetValue(dialog.FileName)
                            End If
                            MsgBox("Please restart Notepad X to use this plugin")
                        End If
                    Else ' hash is Nothing
                        MessageBox.Show("Invalid file")
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("Error Getting Plugin: " & ex.ToString)
        End Try
    End Sub

    Private Function listContains(ByVal list As ListView, ByVal text As String) As Boolean
        text = text.ToLower()
        For Each item As ListViewItem In list.Items
            If item.Text.ToLower() = text Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub pi_a_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pi_a.Click
        For Each item As ListViewItem In pluginsListView.SelectedItems
            item.Checked = True
        Next
        On Error Resume Next
        For Each item As ListViewItem In pluginsListView.SelectedItems
            If PluginManager.AvailablePlugins.Exist(item.SubItems(0).Text) <> True Then
                PluginManager.AddPlugin(item.SubItems(0).Text)
                item.Checked = True
            End If
        Next
    End Sub

    Private Sub pi_d_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pi_d.Click
        On Error Resume Next
        For Each item As ListViewItem In pluginsListView.SelectedItems
            If PluginManager.AvailablePlugins.Exist(item.SubItems(0).Text) = True Then
                PluginManager.ClosePlugin(item.SubItems(0).Text)
                item.Checked = False
            End If
        Next
    End Sub

    Private Sub pi_r_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pi_r.Click
        For Each item As ListViewItem In pluginsListView.SelectedItems
            item.Checked = False
            If PluginManager.AvailablePlugins.Exist(item.SubItems(0).Text) = True Then
                PluginManager.ClosePlugin(item.SubItems(0).Text)
            End If
            IO.File.Delete(item.SubItems(0).Text)
            FilesToDelete.Add(item.SubItems(0).Text)
            item.Remove()
        Next
    End Sub

    Private Sub pluginsListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim enabled As Boolean = False
        If pluginsListView.SelectedItems.Count > 0 Then
            If Not pluginsListView.SelectedItems(0).Text.StartsWith("[Uninstalled] ") Then
                enabled = True
                Dim selectedItem As ListViewItem = pluginsListView.SelectedItems(0)
                pluginNameLabel.Text = "Plugin Name: " & selectedItem.Text
                pluginDescriptionLabel.Text = "Description: " & selectedItem.SubItems(4).Text
                pluginVersionLabel.Text = "Version: " & selectedItem.SubItems(3).Text
                pluginAuthorLabel.Text = "Author: " & selectedItem.SubItems(2).Text
                downloadUrlLabel.Text = "Download/Update URL: " & selectedItem.SubItems(5).Text
            End If
        Else
            pluginAuthorLabel.Text = ""
            pluginDescriptionLabel.Text = ""
            pluginVersionLabel.Text = ""
            pluginNameLabel.Text = ""
            downloadUrlLabel.Text = ""
        End If
        pi_a.Enabled = enabled
        pi_d.Enabled = enabled
        pi_r.Enabled = enabled
        'pi_i.Visible = enabled
        If pluginsListView.SelectedItems.Count > 0 AndAlso pluginsListView.SelectedItems(0).Text.StartsWith("[Uninstalled] ") Then
            pi_dll.Visible = True
        Else
            pi_dll.Visible = False
        End If
        If (enabled = True) AndAlso (pluginsListView.SelectedItems(0).Checked = True) Then
            pi_a.Enabled = False
        End If
        If (enabled = True) AndAlso (pluginsListView.SelectedItems(0).Checked = False) Then
            pi_d.Enabled = False
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'update selected
        Dim hash As String()
        For Each item As ListViewItem In pluginsListView.SelectedItems
            If item.Text.StartsWith("[Uninstalled] ") Then
                MsgBox("Error: Plugin is not yet installed.")
                Return
            End If
            Dim path As String = item.SubItems(1).Text
            hash = PluginManager.GetPluginInfo(path, PluginInterface)
            If hash Is Nothing Then MsgBox("Cannot check file """ & path & ControlChars.Quote) : Continue For
            If hash(4) = "" Then
                If MsgBox("Download URL is empty. Cannot download plugin." & vbCrLf & "Attempt download from original file path?", MsgBoxStyle.YesNo) = DialogResult.Yes Then
                    'get filename
                    Dim iH As String() = PluginManager.GetINIPluginInfo(path)
                    Dim fn As String = iH(5)
                    If IO.File.Exists(fn) Then
                        Dim nFnH() As String = PluginManager.GetPluginInfo(fn, PluginInterface)
                        If nFnH IsNot Nothing Then
                            IO.File.Copy(fn, path)
                        End If
                    End If
                Else

                End If
                Continue For
            End If
            Try
                Dim DLL As New Download(hash(4), path, Download.DownloadTypes.Plugin)
                DLL.ShowDialog()
                If DLL.DialogResult = DialogResult.Yes Then
                    MsgBox("Updated " & hash(0))
                End If
            Catch ex As Exception
                MsgBox("Error Getting Plugin: " & ex.ToString)
            End Try
        Next
    End Sub

    Private Sub pluginsListView_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pluginsListView.SelectedIndexChanged
        pluginsListView_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub pi_dll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pi_dll.Click
        For Each item As ListViewItem In pluginsListView.SelectedItems
            Dim path As String = item.Text.Substring("[Uninstalled] ".Length)
            Try
                Dim DLL As New Download(path, NotepadX_DocumentPath & "\Plugins\" & path.Substring(path.LastIndexOf("/")), Download.DownloadTypes.Plugin)
                If DLL.ShowDialog() = DialogResult.Yes Then
                    MsgBox("Downloaded " & path)
                    Dim KTD As INIFile.INISection.INIKey = Nothing
                    Dim area As INIFile.INISection = PluginManager.PluginINI.GetSection("Uninstalled")
                    For Each key As INIFile.INISection.INIKey In area.Keys
                        If key.Value = path Then
                            KTD = key
                        End If
                    Next
                    Try
                        PluginManager.PluginINI.RemoveKey("Uninstalled", KTD.Name)
                    Catch ex As Exception
                        Log.WriteLine(ex.ToString)
                    End Try
                End If
            Catch ex As Exception
                MsgBox("Error Getting Plugin: " & ex.ToString)
            End Try
        Next
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim files(0 To 1) As String
            Dim dialog As New OpenFileDialog()
            dialog.Filter = "Package File (*.pack)|*.pack"
            dialog.Title = "Add Plugins"
            dialog.CheckFileExists = True
            If dialog.ShowDialog() = DialogResult.OK Then
                Dim PACKfile As String = dialog.FileName
                Packages.Unpack(PACKfile, StartupPath & "\tmp\")
                Dim files2() As String = Directory.GetFiles(StartupPath & "\tmp\", "*.dll")
                For Each _file In files2
                    Dim _hash() As String = PluginManager.GetPluginInfo(_file, PluginInterface)
                    If _hash IsNot Nothing Then
                        ReDim Preserve files(files.Count) ' Adds 1 to end of the array
                        files(files.Count) = _file
                        'exit, we found a possible plugin
                        Exit For
                    End If
                Next
                For Each File In files
                    Dim hash As String() = PluginManager.GetPluginInfo(File, PluginInterface)
                    If hash IsNot Nothing Then
                        Dim path As String = NotepadX_DocumentPath + "\Plugins\" & System.IO.Path.GetFileName(File)
                        If path.ToLower() <> File.ToLower() Then
                            System.IO.File.Copy(File, path, True)
                            IO.Directory.Delete(StartupPath & "\tmp\", True)
                        End If
                        If listContains(pluginsListView, hash(0)) <> True Then
                            Dim newItem As New ListViewItem()

                            newItem.Text = hash(0) 'name
                            newItem.SubItems.Add(File) ' Path
                            newItem.SubItems.Add(hash(1)) 'author
                            newItem.SubItems.Add(hash(2)) 'version
                            newItem.SubItems.Add(hash(3)) 'description
                            newItem.SubItems.Add(hash(4)) ' Download URL
                            pluginsListView.Items.Add(newItem)
                            If PluginManager.PluginINI.GetSection(path) IsNot Nothing Then
                            Else
                                Dim ret As String() = PluginManager.GetPluginInfo(path, PluginInterface)
                                Dim plugin As INIFile.INISection = PluginManager.PluginINI.AddSection(path)
                                Dim nameKey = plugin.AddKey("Name")
                                nameKey.SetValue(ret(0))
                                Dim authorKey = plugin.AddKey("Author")
                                authorKey.SetValue(ret(1))
                                Dim versionKey = plugin.AddKey("Version")
                                versionKey.SetValue(ret(2))
                                Dim descriptionKey = plugin.AddKey("Description")
                                descriptionKey.SetValue(ret(3))
                                Dim downloadUrlKey = plugin.AddKey("DownloadURL")
                                downloadUrlKey.SetValue(ret(4))
                                Dim origFileName = plugin.AddKey("OriginalFilename")
                                origFileName.SetValue(dialog.FileName)
                            End If
                        End If
                    Else ' hash is Nothing
                        MessageBox.Show("Invalid file")
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("Error Getting Plugin: " & ex.ToString)
        End Try
    End Sub
End Class