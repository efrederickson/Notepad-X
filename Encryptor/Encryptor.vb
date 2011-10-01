Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports EncryptionLib

Public Class Encryptor
    Dim args() As String

    Private asciiLib As New EncryptionLib.ASCII_Lib
    Private rijndaelLib As EncryptionLib.Rijndael_Lib
    Private xorLib As New EncryptionLib.Xor_Lib
    Private desLib As DES_Lib
    Private tripleDesLib As Triple_DES_Lib
    Private objects As New CreateObjects

    Private Sub encryptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles encryptButton.Click
        Dim result As String
        Dim result2 As String
        Dim inputFileText As String = IO.File.ReadAllText(args(1))
        tripleDesLib = New Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
        If asciiRadioButton.Checked Then
            result = asciiLib.Encrypt(inputFileText, CInt(codeTextBox.Text))
            result2 = asciiLib.Decrypt(result, CInt(codeTextBox.Text))
        ElseIf rijndaelRadioButton.Checked Then
            Dim myEnc As System.Text.Encoding
            myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
            Dim t() As Byte = myEnc.GetBytes(inputFileText.ToCharArray) 'convert string to bytes
            'Dim input() As Byte = Convert.FromBase64String(TextBox1.Text.ToCharArray())
            rijndaelLib = New Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
            Dim R() As Byte = rijndaelLib.Encrypt(t)
            result = myEnc.GetString(R)
            result2 = myEnc.GetString(rijndaelLib.Decrypt(R))
        ElseIf desRadioButton.Checked Then
            desLib = New DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
            result = desLib.Encrypt(inputFileText)
            result2 = desLib.Decrypt(result)
        ElseIf tripleDesRadioButton.Checked Then
            result = tripleDesLib.Encrypt(inputFileText)
            result2 = tripleDesLib.Decrypt(result)
        ElseIf xorRadioButton.Checked Then
            result = xorLib.Encrypt(inputFileText, CInt(codeTextBox.Text))
            result2 = xorLib.Decrypt(result, CInt(codeTextBox.Text))
        Else
            MsgBox("Invalid Option!!!")
            Return
        End If
        IO.File.WriteAllText(TextBox1.Text, result)
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        args = Environment.GetCommandLineArgs
        If args.Count = 2 Then
            TextBox1.Text = args(1)
        End If
    End Sub

    Sub New(Optional ByVal file As String = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If file = Nothing Then
        Else
            TextBox1.Text = file
        End If
    End Sub
End Class
