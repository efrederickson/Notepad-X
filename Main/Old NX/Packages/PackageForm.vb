Public Class PackageForm
    Inherits DockContent

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim d As New OpenFileDialog
        d.Multiselect = True
        If d.ShowDialog = DialogResult.OK Then
            For Each filename In d.FileNames
                ListView1.Items.Add(New FileListItem(filename, False) With {
                                    .Text = IO.Path.GetFileName(filename),
                .Name = IO.Path.GetFileNameWithoutExtension(filename & IO.Path.GetFileNameWithoutExtension(IO.Path.GetRandomFileName))
                                })
            Next
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListView1.SelectedItems.Count > 0 Then
            ListView1.Items.Remove(ListView1.SelectedItems(0))
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim save As New SaveFileDialog()
        save.Filter = "Package files (*.pack)|*.pack"
        If save.ShowDialog(Me) = DialogResult.OK Then
            Dim pack As New PackFileInfoCollection()
            For Each file As FileListItem In ListView1.Items
                Dim p As String = file.Filename
                If file.isDir Then
                    pack.AddDirectory(p, file.Dir)
                Else
                    pack.AddFile(p, "")
                End If
            Next
            Packages.Pack(pack, save.FileName)
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim open As New OpenFileDialog()
        open.Filter = "Package files (*.pack)|*.pack"
        If open.ShowDialog = DialogResult.OK Then
            Dim fb As New FolderBrowserDialog()
            If fb.ShowDialog = DialogResult.OK Then
                Packages.Unpack(open.FileName, fb.SelectedPath)
            End If
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim d As New FolderBrowserDialog
        If d.ShowDialog = DialogResult.OK Then
            ListView1.Items.Add(New FileListItem(d.SelectedPath, True) With {
                                .Text = IO.Path.GetFileName(d.SelectedPath),
            .Name = IO.Path.GetFileNameWithoutExtension(d.SelectedPath & IO.Path.GetFileNameWithoutExtension(IO.Path.GetRandomFileName))
                            })
        End If
    End Sub
End Class
