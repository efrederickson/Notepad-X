Imports System.IO
Imports System.Windows.Forms

Public Class mainForm
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
    Public Shared ReadOnly Lua As New LuaInterpreter()
    ReadOnly _G As LuaTable = LuaInterpreter.CreateGlobalEnviroment()
    Dim out As New StringWriter
    Dim [in] As New StringReader("")

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Lua.Interpreter(TextBox1.Text, _G)
        Catch ex As Exception
            out.Write(ex.ToString)
        End Try
    End Sub

    Private Sub mainForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Console.OpenStandardError()
        Console.OpenStandardInput()
        Console.OpenStandardOutput()
    End Sub

    Private Sub mainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Console.SetOut(out)
        [in] = New StringReader(TextBox3.Text)
        Console.SetIn([in])
        Console.SetError(out)
        Console.WriteLine("Lua has Hijacked Console.In, Console.Out, and Console.Err")
    End Sub

    Private Sub update_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles update.Tick
        TextBox2.Text = out.ToString()
        [in] = New StringReader(TextBox3.Text)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        out = New StringWriter()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim o As New OpenFileDialog
        o.Filter = "Notepad X Lua Scripts (*.nxl)|*.nxl|All Files (*.*)|*.*"
        If o.ShowDialog = DialogResult.OK Then
            TextBox1.Text = File.ReadAllText(o.FileName)
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim s As New SaveFileDialog
        s.Filter = "Notepad X Lua Scripts (*.nxl)|*.nxl|All Files (*.*)|*.*"
        If s.ShowDialog = DialogResult.OK Then
            File.WriteAllText(s.FileName, TextBox1.Text)
        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        TextBox1.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        TextBox1.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        TextBox1.Paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        TextBox1.SelectAll()
    End Sub
End Class