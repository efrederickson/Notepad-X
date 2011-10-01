Public Class Searcher
    Dim adv As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Dim vrb As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Dim noun As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Dim adj As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Friend masterList As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)

    Friend txt As String
    
    Sub New()
        Dim l As Integer
        Dim loadAdv() As String = My.Resources.data_ADV.Split(Chr(10))
        For l = 0 To loadAdv.Length - 1
            Dim tmp() As String = loadAdv(l).Split("|")
            If Not adv.ContainsKey(tmp(0)) Then
                adv.Add(tmp(0), tmp(1))
                If Not masterList.ContainsKey(tmp(0)) Then
                    masterList.Add(tmp(0).ToLower, tmp(0))
                End If
            End If
        Next

        Dim loadAdJ() As String = My.Resources.data_ADJ.Split(Chr(10))
        For l = 0 To loadAdJ.Length - 1
            Dim tmp() As String = loadAdJ(l).Split("|")
            If Not adj.ContainsKey(tmp(0)) Then
                adj.Add(tmp(0), tmp(1))
                If Not masterList.ContainsKey(tmp(0)) Then
                    masterList.Add(tmp(0).ToLower, tmp(0))
                End If
            End If

        Next

        Dim loadVRB() As String = My.Resources.data_VRB.Split(Chr(10))
        For l = 0 To loadVRB.Length - 1
            Dim tmp() As String = loadVRB(l).Split("|")
            If Not vrb.ContainsKey(tmp(0)) Then
                vrb.Add(tmp(0), tmp(1))
                If Not masterList.ContainsKey(tmp(0)) Then
                    masterList.Add(tmp(0).ToLower, tmp(0))
                End If
            End If
        Next

        Dim loadNOUN() As String = My.Resources.data_NOUN.Split(Chr(10))
        For l = 0 To loadNOUN.Length - 1
            Dim tmp() As String = loadNOUN(l).Split("|")
            If Not noun.ContainsKey(tmp(0)) Then
                noun.Add(tmp(0), tmp(1))
                If Not masterList.ContainsKey(tmp(0)) Then
                    masterList.Add(tmp(0).ToLower, tmp(0))
                End If
            End If
        Next
    End Sub


    'display info on selected term or search for similar terms
    Friend Function IsWord(ByVal txt As String) As Boolean
        txt = txt.Trim(New Char() {" ", ".", ",", "?", "!", _
                                   ControlChars.Quote, "'", ":", ";", vbNewLine}) 'no leading or trailing white space
        If masterList.ContainsKey(txt.ToLower) Then
            Return True
        Else
            Return False
        End If
    End Function


End Class
