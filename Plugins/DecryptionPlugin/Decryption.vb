Imports EncryptionLib
Public Class Decryption
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
    Private asciiLib As New ASCII_Lib
    Private rijndaelLib As Rijndael_Lib
    Private xorLib As New Xor_Lib
    Private desLib As DES_Lib
    Private tripleDesLib As Triple_DES_Lib
    Private objects As New CreateObjects

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub encryptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles encryptButton.Click
        Dim Owner As NotepadX.TextEditor
        Try
            Owner = CType(NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument, NotepadX.TextEditor)
        Catch ex As Exception
            MsgBox("Please select a Notepad X form!")
            Return
        End Try
        Try
            Dim result2 As String
            Dim inputFileText As String = Owner.TextBox1.Text
            tripleDesLib = New Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
            If asciiRadioButton.Checked Then
                result2 = CStr(asciiLib.Decrypt(inputFileText, CInt(codeTextBox.Text)))
            ElseIf rijndaelRadioButton.Checked Then
                Dim myEnc As System.Text.Encoding
                myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
                Dim t() As Byte = myEnc.GetBytes(inputFileText.ToCharArray) 'convert string to bytes
                'Dim input() As Byte = m_utf8.GetBytes(TextBox1.Text.ToCharArray())
                rijndaelLib = New Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
                Dim R() As Byte
                R = myEnc.GetBytes(inputFileText)
                result2 = myEnc.GetString((rijndaelLib.Decrypt(R)))
            ElseIf desRadioButton.Checked Then
                desLib = New DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
                result2 = desLib.Decrypt(inputFileText)
            ElseIf tripleDesRadioButton.Checked Then
                result2 = tripleDesLib.Decrypt(inputFileText)
            ElseIf xorRadioButton.Checked Then
                result2 = CStr(xorLib.Decrypt(inputFileText, CInt(codeTextBox.Text)))
            ElseIf aesRadioButton.Checked Then
                result2 = CStr(New AES_Lib(objects.CreateAESKey, objects.CreateAESIV).Decrypt(inputFileText))
            ElseIf rcTwoRadioButton.Checked Then
                result2 = CStr(New RC2_Lib(objects.CreateRC2Key, objects.CreateRC2IV).Decrypt(inputFileText))
            ElseIf rsaRadioButton.Checked Then
                Try
                    result2 = CStr(xorLib.Decrypt(inputFileText, CInt(codeTextBox.Text)))
                Catch ex2 As Exception
                    MsgBox(ex2.ToString)
                    result2 = ex2.ToString
                End Try
            Else
                MsgBox("Invalid Option!!!")
                Return
            End If
            Owner.TextBox1.Text = result2
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AlwaysOnTopDecryptionForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

End Class