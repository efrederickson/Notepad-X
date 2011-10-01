Public Class Xor_Lib

    Function Encrypt(ByVal line As String, ByVal xcode As Integer) As String
        Dim letter As Char
        Dim strCode As String = xcode
        Dim i, charsInFile, Code As Short
        Dim encrypted As String = ""


        'save text with encryption scheme
        Code = CShort(strCode)
        charsInFile = line.Length
        For i = 0 To charsInFile - 1
            letter = line.Substring(i, 1)
            'convert to number w/ Asc, then use Xor to encrypt
            encrypted &= (Asc(letter) Xor Code) 'and save in file
            'separate numbers with a space
            encrypted &= " "
        Next
        Return encrypted
    End Function

    Function Decrypt(ByVal line As String, ByVal xcode As Integer) As String
        Dim AllText As String
        Dim i As Short
        Dim ch As Char
        Dim strCode As String = xcode
        Dim Code, Number As Short
        Dim Numbers() As String
        Dim Decrypted As String = ""

        Code = CShort(strCode)
        'read encrypted numbers
        AllText = line
        AllText = AllText.Trim
        'split numbers in to an array based on space
        Numbers = AllText.Split(" ")
        'loop through array
        For i = 0 To Numbers.Length - 1
            If Numbers(i) = "" Then Continue For
            If Numbers(i) = ControlChars.Quote Then Continue For
            Number = CShort(Numbers(i)) 'convert string to number
            Try
                ch = Chr(Number Xor Code) 'convert with Xor
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Decrypted = Decrypted & ch 'and build string
        Next
        Return Decrypted
    End Function
End Class

