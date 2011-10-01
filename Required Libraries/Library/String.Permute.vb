Imports System.IO

Namespace Library.String
    ''' <summary>
    ''' A String Permutation Utility
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Permutation
        Dim Strings As New List(Of String)

        ''' <summary>
        ''' A String Permutation Function
        ''' </summary>
        ''' <param name="stringToPermute">This is the String to Permute</param>
        ''' <returns></returns>
        ''' <remarks>This may take a while, depending the strings Length, so I
        ''' recommend using a BackgroundWorker or a something of that sort
        ''' </remarks>
        Public Function Permute(ByVal stringToPermute As String) As List(Of String)
            Strings.Clear()
            PermuteB("", stringToPermute)
            Return Strings
        End Function

        Private Sub PermuteB(ByVal beginningS As String, ByVal endingS As String)
            Dim newS As String

            If endingS.Length <= 1 Then
                Dim wholeS As String = beginningS & endingS
                Strings.Add(wholeS)
            Else
                For i As Integer = 0 To endingS.Length - 1
                    newS = endingS.Substring(0, i)
                    If i + 1 <= endingS.Length Then
                        newS &= endingS.Substring(i + 1)
                    End If
                    PermuteB(beginningS & endingS.Chars(i), newS)
                Next
            End If

        End Sub
    End Class

    ''' <summary>
    ''' A Class to find a Strings maximum Permutations
    ''' </summary>
    ''' <remarks></remarks>
    Class StringFactorials
        ''' <summary>
        ''' Get the Maximum Permutation Count of a string (the Factorial)
        ''' </summary>
        ''' <param name="inputStringLength">the Length of a String</param>
        ''' <returns>The Permutation Count</returns>
        ''' <remarks></remarks>
        Shared Function FindFactorialOfString(ByVal inputStringLength As Integer) As Double
            Dim RV As Double = 1
            Dim i As Double
            For i = 1 To inputStringLength
                RV *= i
            Next
            Return RV
        End Function
    End Class
End Namespace

