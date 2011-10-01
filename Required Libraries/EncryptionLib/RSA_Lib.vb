Imports System.Security.Cryptography
Imports System.Text

Public Class RSA_Lib

    ' define the triple des provider
    Private m_rsa As New RSACryptoServiceProvider

    ' define the string handler
    Private m_utf8 As New UTF8Encoding

    ' define the local property arrays
    Private m_key() As Byte
    Private m_iv() As Byte

    Public Sub New()
        RSA.Create()
    End Sub

    Public Function Encrypt(ByVal text As String) As String
        Dim input() As Byte = m_utf8.GetBytes(text)
        Dim output() As Byte = m_rsa.Encrypt(input, True)
        Return m_utf8.GetString(output)
    End Function

    Public Function Encrypt(ByVal input() As Byte) As Byte()
        Dim output() As Byte = m_rsa.Encrypt(input, True)
        Return output
    End Function

    Public Function Decrypt(ByVal text As String) As String
        Dim input() As Byte = m_utf8.GetBytes(text)
        Dim output() As Byte
        If input.Length > 127 Then Throw New Exception("Input To Large!")
        output = m_rsa.Decrypt(input, True)
        Return m_utf8.GetString(output)
    End Function

    Function Decrypt(ByVal input() As Byte) As Byte()
        Dim output() As Byte = m_rsa.Decrypt(input, True)
        Return output
    End Function
End Class
