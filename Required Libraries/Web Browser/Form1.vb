Public Class Form1
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    Dim GoogleSearchURL As String = "http://www.google.com/#sclient=psy&hl=en&site=&source=hp&q=<<STRING>>&aq=f&aqi=&aql=&oq=&pbx=1&bav=on.2,or.r_gc.r_pw.&fp=d3cf963e1097ce14"
    Dim BingSearchURL As String = "http://www.bing.com/search?q=<<STRING>>&form=QBLH&qs=n&sk=&sc=8-4"
    'Replace <<STRING>> with the search%20terms

    Private Sub goButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles goButton.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.Navigate(GetURL(URLComboBox.Text))
        URLComboBox.Text = GetURL(URLComboBox.Text)
        URLComboBox.Items.Add(GetURL(URLComboBox.Text))

    End Sub

    Function GetURL(ByVal URI As String) As String
        If URI.Trim = "" Then Return My.Settings.HomePage
        If URI.StartsWith("http://") Then Return URI
        If URI.StartsWith("ftp://") Then Return URI
        If URI.StartsWith("file://") Then Return URI

        If URI.StartsWith("\\") Then
            Return "file:" & URI.Replace("\", "/")
            ' will return file://server/share
        ElseIf URI.Substring(1, 2) = ":\" Then
            Return "file://" & URI.Replace("\", "/")
            ' will return file://drive/folder ?/file.ext
        End If
        'it could be a www.* or something else... ... ...
        Return "http://" & URI
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Dim aaaa As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
            Me.Text = "Web Browser - " & aaaa.DocumentTitle
            If aaaa.DocumentTitle <> TabControl1.TabPages(TabControl1.SelectedIndex).Text Then
                Dim aaaaaaaaaa As String = aaaa.DocumentTitle
                If aaaaaaaaaa.Length > 50 Then
                    aaaaaaaaaa = aaaaaaaaaa.Substring(0, 50) & "..."
                End If
                TabControl1.TabPages(TabControl1.SelectedIndex).Text = aaaaaaaaaa
            End If
        Catch ex As Exception
            Dim aaaa As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
            Me.Text = "Web Browser"
            Dim Str As String = GetURL("about:tabs")
            If Str.Length > 15 Then Str = Str.Substring(0, 15) & "..."
            TabControl1.TabPages(TabControl1.SelectedIndex).Text = GetURL(Str)
        End Try
        Dim aaa As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        If aaa.IsBusy = False Then
            If Me.Text.EndsWith("Navigation Canceled") Or Me.Text.EndsWith("Internet Explorer cannot display the webpage") Then
                Dim Url As String = URLComboBox.Text
                Url = Url.Substring("http://".Length)
                Url = Url.Replace(" ", "%20")
                If Url.EndsWith("/") Then Url = Url.Substring(0, Url.Length - 1)
                If My.Settings.DefaultSearcher = "google.com" Then
                    Url = GoogleSearchURL.Replace("<<STRING>>", Url)
                Else
                    Url = BingSearchURL.Replace("<<STRING>>", Url)
                End If
                Dim aa As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
                aa.Navigate(Url)
                Me.Text = "Web Browser - Redirecting..."
            End If
        End If
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        If a.CanGoBack Then gobackButton.Enabled = True Else gobackButton.Enabled = False
        If a.CanGoForward Then goforwardButton.Enabled = True Else goforwardButton.Enabled = False
    End Sub

    Private Sub exitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitButton.Click
        Me.Close()
    End Sub

    Private Sub stopButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles stopButton.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.Stop()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim FolderBrowser As New OpenFileDialog
        FolderBrowser.Filter = "All Files (*.*)|*.*"
        If FolderBrowser.ShowDialog = DialogResult.OK Then
            Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
            a.Navigate(GetURL(FolderBrowser.FileName))
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.ShowSaveAsDialog()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        optionsForm.ShowDialog()
        If My.Settings.TopMost Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.ShowPrintDialog()
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.ShowPrintPreviewDialog()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.Refresh()
        URLComboBox.Text = WebBrowser1.Url.ToString
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gobackButton.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.GoBack()
        URLComboBox.Text = WebBrowser1.Url.ToString
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles goforwardButton.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.GoForward()
        URLComboBox.Text = WebBrowser1.Url.ToString
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim args() As String = Environment.GetCommandLineArgs
        If args.Count >= 2 Then
            WebBrowser1.Navigate(GetURL(args(1)))
            WebBrowser1.Url = New Uri(args(1))
            URLComboBox.Text = args(1)
        Else
            WebBrowser1.Navigate(My.Settings.HomePage)
            WebBrowser1.Url = New Uri(My.Settings.HomePage)
        End If
        WebBrowser1.ScriptErrorsSuppressed = My.Settings.SupressErrors
        URLComboBox.Text = My.Settings.HomePage
        URLComboBox.Items.Add(My.Settings.HomePage)
        If My.Settings.TopMost Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub

    Private Sub gohomeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gohomeButton.Click
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.Navigate(My.Settings.HomePage)
        URLComboBox.Text = WebBrowser1.Url.ToString
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem1.Click
        HelpForm.ShowDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub NewTabToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewTabToolStripMenuItem.Click
        Dim Tab As New TabPage
        Dim Wb As New WebBrowser
        Wb.Name = "wb"
        Wb.ScriptErrorsSuppressed = My.Settings.SupressErrors
        Wb.Navigate("about:tabs")
        Wb.Dock = DockStyle.Fill
        AddHandler Wb.DocumentCompleted, AddressOf WebBrowser1_DocumentCompleted
        Tab.Controls.Add(Wb)
        TabControl1.Controls.Add(Tab)
        TabControl1.SelectTab(TabControl1.TabCount - 1)
        TabControl1_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Try
            Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
            URLComboBox.Text = GetURL(a.Url.ToString)
        Catch ex As Exception
            URLComboBox.Text = GetURL("about:tabs")
        End Try
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        Dim Str As String = GetURL(a.Url.ToString)
        If Str.Length > 15 Then Str = Str.Substring(0, 15) & "..."
        TabControl1.TabPages(TabControl1.SelectedIndex).Text = GetURL(Str)
    End Sub

    Private Sub CloseTabToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseTabToolStripMenuItem.Click
        TabControl1.TabPages.RemoveAt(TabControl1.SelectedIndex)
        If TabControl1.TabCount = 0 Then
            Me.Close()
        End If
    End Sub

    Public Sub NavigateTo(ByVal URL As String)
        Dim a As WebBrowser = TabControl1.TabPages(TabControl1.SelectedIndex).Controls(0)
        a.Navigate(GetURL(URL))
        URLComboBox.Text = GetURL(URL)
        URLComboBox.Items.Add(GetURL(URL))
    End Sub
End Class
