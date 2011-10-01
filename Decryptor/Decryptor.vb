Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports EncryptionLib

Public Class Decryptor
    Dim args() As String = Environment.GetCommandLineArgs

    Private asciiLib As New ASCII_Lib
    Private rijndaelLib As Rijndael_Lib
    Private xorLib As New Xor_Lib
    Private desLib As DES_Lib
    Private tripleDesLib As Triple_DES_Lib
    Private objects As New CreateObjects

    Private Sub encryptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles encryptButton.Click
        Dim result2 As String
        Dim inputFileText As String = IO.File.ReadAllText(args(1))
        tripleDesLib = New Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
        If asciiRadioButton.Checked Then
            inputFileText = asciiLib.Encrypt(inputFileText, CInt(codeTextBox.Text))
            result2 = asciiLib.Decrypt(inputFileText, CInt(codeTextBox.Text))
        ElseIf rijndaelRadioButton.Checked Then
            Dim myEnc As System.Text.Encoding
            myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
            Dim t() As Byte = myEnc.GetBytes(inputFileText.ToCharArray) 'convert string to bytes
            'Dim input() As Byte = Convert.FromBase64String(TextBox1.Text.ToCharArray())
            rijndaelLib = New Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
            Dim R() As Byte
            R = myEnc.GetBytes(inputFileText)
            result2 = myEnc.GetString(rijndaelLib.Decrypt(R))
        ElseIf desRadioButton.Checked Then
            desLib = New DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
            result2 = desLib.Decrypt(inputFileText)
        ElseIf tripleDesRadioButton.Checked Then
            result2 = tripleDesLib.Decrypt(inputFileText)
        ElseIf xorRadioButton.Checked Then
            result2 = xorLib.Decrypt(inputFileText, CInt(codeTextBox.Text))
        Else
            MsgBox("Invalid Option!!!")
            Return
        End If
        IO.File.WriteAllText(inputFileTextBox.Text, result2)
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        args = Environment.GetCommandLineArgs
        If args.Count = 2 Then
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