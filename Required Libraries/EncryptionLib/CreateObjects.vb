Imports System.Security.Cryptography

Public Class CreateObjects

    '*************************
    '** Create A Key
    '*************************

    Public Function CreateRijndaelKeyWithSHA512(ByVal strPassword As String) As Byte()
        'Convert strPassword to an array and store in chrData.
        Dim chrData() As Char = strPassword.ToCharArray
        'Use intLength to get strPassword size.
        Dim intLength As Integer = chrData.GetUpperBound(0)
        'Declare bytDataToHash and make it the same size as chrData.
        Dim bytDataToHash(intLength) As Byte

        'Use For Next to convert and store chrData into bytDataToHash.
        For i As Integer = 0 To chrData.GetUpperBound(0)
            bytDataToHash(i) = CByte(Asc(chrData(i)))
        Next

        'Declare what hash to use.
        Dim SHA512 As New System.Security.Cryptography.SHA512Managed
        'Declare bytResult, Hash bytDataToHash and store it in bytResult.
        Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
        'Declare bytKey(31).  It will hold 256 bits.
        Dim bytKey(31) As Byte

        'Use For / Next to put a specific size (256 bits) of 
        'bytResult into bytKey. The 0 To 31 will put the first 256 bits
        'of 512 bits into bytKey.
        For i As Integer = 0 To 31
            bytKey(i) = bytResult(i)
        Next

        Return bytKey 'Return the key.
    End Function

    'This gets a key without SHA512 hashing
    Public Function CreateRijndaelKeyWithoutSHA512(ByVal strPassword As String) As Byte()
        Dim bytKey As Byte()
        Dim bytSalt As Byte() = System.Text.Encoding.ASCII.GetBytes("saltsalt")
        Dim pdb As New PasswordDeriveBytes(strPassword, bytSalt)
        Dim pdb2 As New Rfc2898DeriveBytes(strPassword, bytSalt)
        bytKey = pdb2.GetBytes(32)

        Return bytKey 'Return the key.
    End Function

    '*************************
    '** Create An IV
    '*************************

    Public Function CreateRijndaelIVWithSHA512(ByVal strPassword As String) As Byte()
        'Convert strPassword to an array and store in chrData.
        Dim chrData() As Char = strPassword.ToCharArray
        'Use intLength to get strPassword size.
        Dim intLength As Integer = chrData.GetUpperBound(0)
        'Declare bytDataToHash and make it the same size as chrData.
        Dim bytDataToHash(intLength) As Byte

        'Use For Next to convert and store chrData into bytDataToHash.
        For i As Integer = 0 To chrData.GetUpperBound(0)
            bytDataToHash(i) = CByte(Asc(chrData(i)))
        Next

        'Declare what hash to use.
        Dim SHA512 As New System.Security.Cryptography.SHA512Managed
        'Declare bytResult, Hash bytDataToHash and store it in bytResult.
        Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
        'Declare bytIV(15).  It will hold 128 bits.
        Dim bytIV(15) As Byte

        'Use For Next to put a specific size (128 bits) of bytResult into bytIV.
        'The 0 To 30 for bytKey used the first 256 bits of the hashed password.
        'The 32 To 47 will put the next 128 bits into bytIV.
        For i As Integer = 32 To 47
            bytIV(i - 32) = bytResult(i)
        Next

        Return bytIV 'Return the IV.
    End Function

    'This gets an IV without SHA512 hashing
    Public Function CreateRijndaelIVWithoutSHA512(ByVal strPassword As String) As Byte()
        Dim bytIV As Byte()
        Dim bytSalt As Byte() = System.Text.Encoding.ASCII.GetBytes("saltsalt")
        Dim pdb As New PasswordDeriveBytes(strPassword, bytSalt)
        Dim pdb2 As New Rfc2898DeriveBytes(strPassword, bytSalt)
        bytIV = pdb2.GetBytes(16)

        Return bytIV 'Return the IV.
    End Function

    Function CreateDESKey() As Byte()
        Dim R() As Byte = {24, 244, 230, 15, 145, 57, 192, 86}
        Return R
    End Function

    Function CreateDESIV() As Byte()
        Dim R() As Byte = {158, 133, 174, 222, 231, 182, 216, 64}
        Return R
    End Function

    Function CreateTripleDESKey() As Byte()
        Dim R() As Byte = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, _
  15, 16, 17, 18, 19, 20, 21, 22, 23, 24}
        Return R
    End Function

    Function CreateTripleDESIV() As Byte()
        Dim R() As Byte = {8, 7, 6, 5, 4, 3, 2, 1}
        Return R
    End Function

    Function CreateAESKey() As Byte()
        Dim R() As Byte = {168, 34, 103, 160, 7, 33, 183, 125, 192, _
                           218, 32, 187, 63, 166, 174, 234, 156, 207, _
                           144, 59, 212, 234, 196, 244, 79, 140, 91, 7, _
                           160, 115, 95, 116}
        Return R
    End Function

    Function CreateAESIV() As Byte()
        Dim R() As Byte = {178, 138, 133, 117, 178, 81, 230, 107, 192, 243, 56, 167, 198, 8, 52, 32}
        Return R
    End Function

    Function CreateRC2Key() As Byte()
        Dim R() As Byte = {66, 96, 143, 243, 22, 64, 28, 97, 127, 15, 1, 185, 165, 197, 5, 55}
        Return R
    End Function

    Function CreateRC2IV() As Byte()
        Dim R() As Byte = {177, 89, 140, 245, 239, 136, 238, 147}
        Return R
    End Function
End Class
