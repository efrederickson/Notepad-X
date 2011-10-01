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
    Dim createbackup As Boolean
    Dim EncryptionType As String = "None"

    Private Sub encryptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles encryptButton.Click
        Dim result As String
        Dim result2 As String
        Dim inputFileText As String = IO.File.ReadAllText(args(1))
        tripleDesLib = New Triple_DES_Lib(objects.CreateTripleDESKey, objects.CreateTripleDESIV)
        If asciiRadioButton.Checked Then
            EncryptionType = "ASCII"
            result = CStr(asciiLib.Encrypt(inputFileText, CInt(codeTextBox.Text)))
            result2 = CStr(asciiLib.Decrypt(result, CInt(codeTextBox.Text)))
        ElseIf rijndaelRadioButton.Checked Then
            EncryptionType = "Rijndael"
            Dim myEnc As System.Text.Encoding
            myEnc = System.Text.Encoding.GetEncoding("Windows-1252")
            Dim t() As Byte = myEnc.GetBytes(inputFileText.ToCharArray) 'convert string to bytes
            'Dim input() As Byte = m_utf8.GetBytes(TextBox1.Text.ToCharArray())
            rijndaelLib = New Rijndael_Lib(objects.CreateRijndaelKeyWithSHA512(InputBox("Key: ")), objects.CreateRijndaelIVWithSHA512(InputBox("IV: ")))
            Dim R() As Byte = rijndaelLib.Encrypt(t)
            result = myEnc.GetString(R)
        ElseIf desRadioButton.Checked Then
            EncryptionType = "DES"
            desLib = New DES_Lib(objects.CreateDESKey, objects.CreateDESIV)
            result = desLib.Encrypt(inputFileText)
            result2 = desLib.Decrypt(result)
        ElseIf tripleDesRadioButton.Checked Then
            EncryptionType = "Triple DES"
            result = tripleDesLib.Encrypt(inputFileText)
            result2 = tripleDesLib.Decrypt(result)
        ElseIf xorRadioButton.Checked Then
            EncryptionType = "XOR"
            result = CStr(xorLib.Encrypt(inputFileText, CInt(codeTextBox.Text)))
            result2 = CStr(xorLib.Decrypt(result, CInt(codeTextBox.Text)))
        ElseIf aesRadioButton.Checked Then
            EncryptionType = "AES"
            result = CStr(New AES_Lib(objects.CreateAESKey, objects.CreateAESIV).Encrypt(inputFileText))
        ElseIf rcTwoRadioButton.Checked Then
            EncryptionType = "RC2"
            result = CStr(New RC2_Lib(objects.CreateRC2Key, objects.CreateRC2IV).Encrypt(inputFileText))
        ElseIf rsaRadioButton.Checked Then
            Try
                result = CStr(New RSA_Lib().Encrypt(inputFileText))
                EncryptionType = "RSA"
            Catch ex As Exception
                MsgBox(ex.ToString)
                result = ex.ToString
            End Try
        Else
            MsgBox("Invalid Option!!!")
            Return
        End If
        If createbackup Then
            Try
                IO.File.Copy(TextBox1.Text, Modify(TextBox1.Text))
            Catch ex As Exception
                MsgBox(ex.ToString)
                Log.WriteLine("Error: " & vbCrLf & ex.ToString & vbCrLf)
            End Try
            IO.File.WriteAllText(TextBox1.Text, result)
            MsgBox("Encrypted File!")
        Else
            IO.File.WriteAllText(TextBox1.Text, result)
            MsgBox("Encrypted File!")
        End If
        log.WriteLine("Encrypted " & args(1) & " With " & EncryptionType & ". Key: " & codeTextBox.Text)
        Log.WriteLine("Encryption Time: ")
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        args = System.Environment.GetCommandLineArgs
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

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        createbackup = CBool(CheckBox1.CheckState)
    End Sub

    Function Modify(ByVal path As String) As String
        Dim tmp As String = ""
        For i = 0 To path.Length - 1
            If path.Substring(i, 1) = "\" Then
                tmp = path.Substring(0, i)
            End If
        Next
        Dim filename As String = IO.Path.GetFileNameWithoutExtension(path)
        Dim ext As String = IO.Path.GetExtension(path)
        Return tmp & "\" & filename & ".backup" & ext
    End Function
End Class
