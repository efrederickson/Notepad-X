Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports EncryptionLib

Public Class Decryptor
    Dim args() As String = System.Environment.GetCommandLineArgs

    Private asciiLib As New ASCII_Lib
    Private rijndaelLib As Rijndael_Lib
    Private xorLib As New Xor_Lib
    Private desLib As DES_Lib
    Private tripleDesLib As Triple_DES_Lib
    Private objects As New CreateObjects
    Dim EncryptionType As String

    Private Sub encryptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles encryptButton.Click
        Dim result2 As String
        Dim inputFileText As String = IO.File.ReadAllText(args(1))
        tripleDesLib = New Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
        If asciiRadioButton.Checked Then
            result2 = CStr(asciiLib.Decrypt(inputFileText, CInt(codeTextBox.Text)))
            EncryptionType = "ASCII"
        ElseIf rijndaelRadioButton.Checked Then
            EncryptionType = "Rijndael"
            Dim myEnc As System.Text.Encoding
            myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
            Dim t() As Byte = myEnc.GetBytes(inputFileText.ToCharArray) 'convert string to bytes
            'Dim input() As Byte = m_utf8.GetBytes(TextBox1.Text.ToCharArray())
            rijndaelLib = New Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
            Dim R() As Byte
            R = myEnc.GetBytes(inputFileText)
            result2 = myEnc.GetString((rijndaelLib.Decrypt(R)))
        ElseIf desRadioButton.Checked Then
            EncryptionType = "DES"
            desLib = New DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
            result2 = desLib.Decrypt(inputFileText)
        ElseIf tripleDesRadioButton.Checked Then
            EncryptionType = "Triple DES"
            result2 = tripleDesLib.Decrypt(inputFileText)
        ElseIf xorRadioButton.Checked Then
            EncryptionType = "XOR"
            result2 = CStr(xorLib.Decrypt(inputFileText, CInt(codeTextBox.Text)))
        ElseIf aesRadioButton.Checked Then
            EncryptionType = "AES"
            result2 = CStr(New AES_Lib(objects.CreateAESKey, objects.CreateAESIV).Decrypt(inputFileText))
        ElseIf rcTwoRadioButton.Checked Then
            EncryptionType = "RC2"
            result2 = CStr(New RC2_Lib(objects.CreateRC2Key, objects.CreateRC2IV).Decrypt(inputFileText))
        ElseIf rsaRadioButton.Checked Then
            Try
                result2 = CStr(xorLib.Decrypt(inputFileText, CInt(codeTextBox.Text)))
                EncryptionType = "RSA"
            Catch ex As Exception
                MsgBox(ex.ToString)
                result2 = ex.ToString
            End Try
        Else
            MsgBox("Invalid Option!!!")
            Return
        End If
        IO.File.WriteAllText(inputFileTextBox.Text, result2)
        MsgBox("Decrypted File!")
        log.WriteLine("Decrypted " & args(1) & " With " & EncryptionType & ". Key: " & codeTextBox.Text)
        Log.WriteLine("Decryption Time: ")
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        args = System.Environment.GetCommandLineArgs
        If args.Length = 2 Then
            inputFileTextBox.Text = args(1)
        End If
    End Sub

    Sub New(Optional ByVal file As String = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If file = Nothing Then
        Else
            inputFileTextBox.Text = file
        End If
    End Sub
End Class