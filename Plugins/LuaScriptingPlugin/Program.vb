Imports System.Windows.Forms

Public Class MainClass
    Implements NotepadX.IPlugin
    Dim _ti As ToolStripMenuItem
    Dim isShowing As Boolean = False
    Dim frm As mainForm

    'Shared Sub Main(ByVal args As String())
    '    Application.EnableVisualStyles()
    '    Application.SetCompatibleTextRenderingDefault(False)
    '    If args.Length > 0 Then
    '        Dim file__1 As String = args(0)
    '        If File.Exists(file__1) Then
    '            'try
    '            If True Then
    '                Dim [global] As LuaTable = LuaInterpreter.CreateGlobalEnviroment()
    '                [global].SetNameValue("_WORKDIR", New LuaString(Path.GetFullPath(Path.GetDirectoryName(file__1))))
    '                Try
    '                    Console.WriteLine("Loading File: " & file__1)
    '                    LuaInterpreter.RunFile(file__1, [global])
    '                Catch ex As Exception
    '                    Console.WriteLine(String.Format("OH NOES! An Error Occurred " & vbCrLf & "{0}", ex.ToString()))
    '                    MessageBox.Show(String.Format("{0}: {1}", ex.GetType().Name, ex.Message), "Error")
    '                End Try
    '            End If
    '        Else
    '            MessageBox.Show(file__1 & " not found.")
    '        End If
    '    End If
    '    Console.WriteLine(vbCrLf & "Press enter to exit...")
    '    While True
    '        Dim key As ConsoleKeyInfo = Console.ReadKey()
    '        If key.Key = ConsoleKey.Enter Then
    '            Exit Sub
    '        End If
    '    End While
    'End Sub

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
            Return "Lua Scripting Plugin for Notepad X"
        End Get
    End Property

    Public Sub Dispose() Implements NotepadX.IPlugin.Dispose
        _ti.Dispose()
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
            Return True
        End Get
    End Property

    Public ReadOnly Property HelpPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.HelpPage
        Get
            Return Nothing
        End Get
    End Property

    Public Sub Initialize() Implements NotepadX.IPlugin.Initialize
        If Not IO.Directory.Exists(NotepadX.Main.NotepadX_DocumentPath & "\Lua Scripts\") Then
            IO.Directory.CreateDirectory(NotepadX.Main.NotepadX_DocumentPath & "\Lua Scripts\")
        End If
        Try
            _ti = New ToolStripMenuItem("Start the Lua Scripting console", Nothing, New EventHandler(AddressOf ITEMCLICK))
            Dim files() As String = IO.Directory.GetFiles(NotepadX.Main.NotepadX_DocumentPath & "\Lua Scripts\", "*.nxl")
            Dim output As String = ""
            If files.Length > 0 Then ITEMCLICK()
            For Each s As String In files
                frm.TextBox1.Text = IO.File.ReadAllText(s)
                frm.Button1_Click(Nothing, EventArgs.Empty)
                output &= String.Format("--Loaded {0}...{1}", s, vbCrLf)
            Next
            If files.Length > 0 Then frm.TextBox1.Text = output
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
            Return "Lua Scripting Plugin"
        End Get
    End Property

    Public ReadOnly Property OptionsPage As System.Windows.Forms.TabPage Implements NotepadX.IPlugin.OptionsPage
        Get
            Dim t As New TabPage() With {
            .Text = "Lua Startup Scripts"
            }
            Dim lv As New ListView()
            For Each s As String In IO.Directory.GetFiles(NotepadX.Main.NotepadX_DocumentPath & "\Lua Scripts\")
                lv.Items.Add(New ListViewItem(IO.Path.GetFileName(s)) With {.Tag = s})
            Next
            lv.Parent = t
            lv.Location = New Drawing.Point(1, 1)
            lv.View = System.Windows.Forms.View.List
            lv.Size = New Drawing.Size(500, 100)
            lv.Dock = DockStyle.Fill
            Dim mi As New MenuStrip
            Dim add As New ToolStripMenuItem() With {
                .Text = "Add a Script"
            }
            AddHandler add.Click, Sub()
                                      Dim o As New OpenFileDialog
                                      o.Filter = "Notepad X Lua Script Files (*.nxl)|*.nxl"
                                      If o.ShowDialog = DialogResult.OK Then
                                          IO.File.Copy(o.FileName, String.Format("{0}\Lua Scripts\{1}", NotepadX.Main.NotepadX_DocumentPath, IO.Path.GetFileName(o.FileName)))
                                          lv.Items.Add(New ListViewItem(IO.Path.GetFileName(o.FileName)) With {.Tag = o.FileName})
                                      End If
                                  End Sub
            Dim r As New ToolStripMenuItem() With {
                .Text = "Remove Script"
            }
            AddHandler r.Click, Sub()
                                    Try
                                        Dim i As ListViewItem = lv.SelectedItems(0)
                                        IO.File.Delete(i.Tag.ToString)
                                        i.Remove()
                                    Catch ex As Exception
                                        MsgBox(ex.ToString)
                                    End Try
                                End Sub
            mi.Parent = t
            mi.Items.Add(add)
            mi.Items.Add(r)
            Return t
        End Get
    End Property

    Public ReadOnly Property OriginalFileName As String Implements NotepadX.IPlugin.OriginalFileName
        Get
            Return DownloadURL
        End Get
    End Property

    Public ReadOnly Property ToolStripItem As System.Windows.Forms.ToolStripMenuItem Implements NotepadX.IPlugin.ToolStripItem
        Get
            Return _ti
        End Get
    End Property

    Public ReadOnly Property Version As String Implements NotepadX.IPlugin.Version
        Get
            Return "1.0"
        End Get
    End Property

    Sub ITEMCLICK()
        If isShowing Then
            frm.Show()
        Else
            frm = New mainForm
            isShowing = True
            AddHandler frm.FormClosing, Sub()
                                            isShowing = False
                                        End Sub
            frm.Show(NotepadX.Main.MDIParent1.DockPanel1)
        End If
    End Sub
End Class
