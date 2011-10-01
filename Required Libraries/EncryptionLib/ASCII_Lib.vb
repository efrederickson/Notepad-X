Public Class ASCII_Lib

    Function Encrypt(ByVal line As String, ByVal acode As Integer) As String
        Dim Encrypted As String = ""
        Dim letter As Char
        Dim i, charsInFile As Short
        'save text with encryption scheme (ASCII code + acode)
        charsInFile = line.Length
        For i = 0 To charsInFile - 1
            letter = line.Substring(i, 1)
            'determine ASCII code and add acode to it
            Encrypted = Encrypted & Chr(Asc(letter) + CInt(acode))
        Next
        'return encrypted text

        Return Encrypted
    End Function

    Function Decrypt(ByVal line As String, ByVal acode As Integer) As String
        Dim AllText As String
        Dim i, charsInFile As Short
        Dim letter As Char
        Dim Decrypted As String = ""

        AllText = line
        'now, decrypt string by subtracting acode from ASCII code
        charsInFile = AllText.Length 'get length of string
        For i = 0 To charsInFile - 1 'loop once for each char
            letter = AllText.Substring(i, 1) 'get character
            Decrypted = Decrypted & Chr(Asc(letter) - CInt(acode)) 'subtract acode
        Next i 'and build new string
        Return Decrypted
    End Function
End Class
