Public Class Encryption_UI
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

    Private Sub encryptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles encryptButton.Click
        Try
            Dim result As String = ""
            Dim result2 As String = ""
            tripleDesLib = New Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
            If asciiRadioButton.Checked Then
                result = asciiLib.Encrypt(TextBox1.Text, CInt(codeTextBox.Text))
                result2 = asciiLib.Decrypt(result, CInt(codeTextBox.Text))
            ElseIf rijndaelRadioButton.Checked Then
                Dim myEnc As System.Text.Encoding
                myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
                Dim t() As Byte = myEnc.GetBytes(TextBox1.Text.ToCharArray) 'convert string to bytes
                'Dim input() As Byte = m_utf8.GetBytes(TextBox1.Text.ToCharArray())
                rijndaelLib = New Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
                Dim R() As Byte = rijndaelLib.Encrypt(t)
                result = myEnc.GetString(R)
                result2 = myEnc.GetString(rijndaelLib.Decrypt(R))
            ElseIf desRadioButton.Checked Then
                desLib = New DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
                result = desLib.Encrypt(TextBox1.Text)
                result2 = desLib.Decrypt(result)
            ElseIf tripleDesRadioButton.Checked Then
                result = tripleDesLib.Encrypt(TextBox1.Text)
                result2 = tripleDesLib.Decrypt(result)
            ElseIf xorRadioButton.Checked Then
                result = xorLib.Encrypt(TextBox1.Text, CInt(codeTextBox.Text))
                result2 = xorLib.Decrypt(result, CInt(codeTextBox.Text))
            ElseIf aesRadioButton.Checked Then
                aesLib = New AES_Lib(objects.CreateAESKey, objects.CreateAESIV)
                result = aesLib.Encrypt(TextBox1.Text)
                result2 = aesLib.Decrypt(result)
            ElseIf rcTwoRadioButton.Checked Then
                rc2Lib = New RC2_Lib(objects.CreateRC2Key, objects.CreateRC2IV)
                result = rc2Lib.Encrypt(TextBox1.Text)
                result2 = rc2Lib.Decrypt(result)
            ElseIf rsaRadioButton.Checked Then
                rsaLib = New RSA_Lib()
                Try
                    result = rsaLib.Encrypt(TextBox1.Text)
                    result2 = rsaLib.Decrypt(result)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                    TextBox1.Text = ex.ToString
                End Try
            Else
                MsgBox("Invalid Option!!!")
                Return
            End If
            outputLabel.Text = String.Format("Encrypted Result: {0}  Decrypted Result: {1}", result, result2)
        Catch ex As Exception
            outputLabel.Text = String.Format("Error:{0}{1}", vbCrLf, ex)
        End Try
    End Sub
End Class