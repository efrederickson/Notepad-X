Public Class Form1

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim o As New OpenFileDialog
        o.Filter = "Package (*.pack)|*.pack"
        If o.ShowDialog = DialogResult.OK Then
            ListView1.Items.Add(New ListViewItem(IO.Path.GetFileName(o.FileName)) With {.Tag = o.FileName})
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            ListView1.SelectedItems(0).Remove()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim o As New OpenFileDialog
        o.Filter = "SnapOn Definition Files (*.snapon)|*.snapon"
        If o.ShowDialog = DialogResult.OK Then
            Dim t As String = IO.File.ReadAllText(o.FileName)
            For Each line As String In t.Split(vbCrLf)
                If IO.File.Exists(String.Format("{0}\{1}", IO.Path.GetDirectoryName(o.FileName), line)) Then
                    ListView1.Items.Add(New ListViewItem(IO.Path.GetFileName(line)) With {.Tag = line})
                End If
            Next
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim s As New SaveFileDialog
        s.Filter = "SnapOn Definition Files (*.snapon)|*.snapon"
        If s.ShowDialog = DialogResult.OK Then
            Dim v As String = InputBox("Version: ")
            Using file As New IO.StreamWriter(s.FileName)
                file.WriteLine(v)
                For Each item As ListViewItem In ListView1.Items
                    file.WriteLine(item.Text)
                Next
                file.Close()
            End Using
        End If
    End Sub
End Class
