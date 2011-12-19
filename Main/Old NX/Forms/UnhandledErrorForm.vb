Imports System.Windows.Forms

Public Class UnhandledErrorForm
    Public Err As Exception
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Sub New(ByVal err As Exception)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = err.Message
        Me.Err = err
        TextBox1.Text = "Unhandled Error! Please Report to elijah.frederickson@gmail.com!" & vbCrLf & _
            err.ToString
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim wb As New Web_Browser.Form1()
        wb.Show()
        wb.NavigateTo("mailto:elijah.frederickson@gmail.com")
    End Sub
End Class
