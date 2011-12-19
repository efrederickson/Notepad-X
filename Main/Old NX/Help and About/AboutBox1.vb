Public NotInheritable Class AboutBox1
    'This was not created by me, it is a form that you can use, 
    'I only edited it some...

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'loadplugin abouts
        For Each plugin As AvailablePlugin In PluginManager.AvailablePlugins
            If plugin.Instance.HasAboutPage Then plugin.Instance.AboutPage.Parent = TabControl1
        Next
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        'right here:
        'Me.TextBoxDescription.Text = My.Application.Info.Description
        Me.TextBoxDescription.Text = "Notepad X is an application that is like Notepad, but somewhat more powerful." & vbCrLf & _
            "The Code editor is the Alsing.SyntaxBox Control. Copyright 2002-2003 Roger Alsing"
        Me.readMeTextBox.Text = IO.File.ReadAllText(IO.Path.GetDirectoryName(Reflection.Assembly.GetAssembly(GetType(NotepadX.MDIParent)).Location) & "\Documents\README.TXT")
        'if the readme file is not there, it will crash.
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Dim wb As New Web_Browser.Form1()
        wb.Show()
        wb.NavigateTo("http://sourceforge.net/projects/notepadx")
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Dim wb As New Web_Browser.Form1()
        wb.Show()
        wb.NavigateTo("http://elijah.awesome99.org/index.php/notepad-x")
    End Sub
End Class
