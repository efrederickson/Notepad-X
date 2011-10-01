Imports EncryptionLib
Public Class AlwaysOnTopEncryptionForm
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    Private asciiLib As New ASCII_Lib
    Private rijndaelLib As Rijndael_Lib
    Private xorLib As New Xor_Lib
    Private desLib As DES_Lib
    Private tripleDesLib As Triple_DES_Lib
    Private objects As New CreateObjects
    Private aesLib As AES_Lib
    Private rc2Lib As RC2_Lib
    Private rsaLib As RSA_Lib

    Private Sub AlwaysOnTopEncryptionForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub encryptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles encryptButton.Click
        Dim Owner As NotepadX.TextEditor
        Try
            Owner = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.TextEditor)
        Catch ex As Exception
            MsgBox("Please select a Notepad X form!")
            Return
        End Try
        Dim result As String = ""
        tripleDesLib = New Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
        If asciiRadioButton.Checked Then
            result = asciiLib.Encrypt(Owner.TextBox1.Text, CInt(codeTextBox.Text))
        ElseIf rijndaelRadioButton.Checked Then
            Dim myEnc As System.Text.Encoding
            myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
            Dim t() As Byte = myEnc.GetBytes(Owner.TextBox1.Text.ToCharArray) 'convert string to bytes
            'Dim input() As Byte = m_utf8.GetBytes(Owner.TextBox1.Text.ToCharArray())
            rijndaelLib = New Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
            Dim R() As Byte = rijndaelLib.Encrypt(t)
            result = myEnc.GetString(R)
        ElseIf desRadioButton.Checked Then
            desLib = New DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
            result = desLib.Encrypt(Owner.TextBox1.Text)
        ElseIf tripleDesRadioButton.Checked Then
            result = tripleDesLib.Encrypt(Owner.TextBox1.Text)
        ElseIf xorRadioButton.Checked Then
            result = xorLib.Encrypt(Owner.TextBox1.Text, CInt(codeTextBox.Text))
        ElseIf aesRadioButton.Checked Then
            aesLib = New AES_Lib(objects.CreateAESKey, objects.CreateAESIV)
            result = aesLib.Encrypt(Owner.TextBox1.Text)
        ElseIf rcTwoRadioButton.Checked Then
            rc2Lib = New RC2_Lib(objects.CreateRC2Key, objects.CreateRC2IV)
            result = rc2Lib.Encrypt(Owner.TextBox1.Text)
        ElseIf rsaRadioButton.Checked Then
            rsaLib = New RSA_Lib()
            Try
                result = rsaLib.Encrypt(Owner.TextBox1.Text)
            Catch ex As Exception
                MsgBox(ex.ToString)
                Owner.TextBox1.Text = ex.ToString
            End Try
        Else
            MsgBox("Invalid Option!!!")
            Return
        End If
        Owner.TextBox1.Text = result
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class