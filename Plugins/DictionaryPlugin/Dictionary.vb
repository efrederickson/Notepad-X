Imports System.Windows.Forms

Public Class Dictionary
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    Dim adv As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Dim vrb As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Dim noun As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Dim adj As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Friend masterList As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
    Dim autoComp As AutoCompleteStringCollection
    Dim l As Integer
    Friend txt As String

    Private Sub AlwaysOnTopDictionary_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub LoadEventHandler(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim loadAdv() As String = My.Resources.data_ADV.Split(Chr(10))
        For l As Integer= 0 To loadAdv.Length - 1
            Dim tmp() As String = loadAdv(l).Split(CChar("|"))
            If Not adv.ContainsKey(tmp(0)) Then
                adv.Add(tmp(0), tmp(1))
                If Not masterList.ContainsKey(tmp(0)) Then
                    masterList.Add(tmp(0), tmp(0))
                End If
            End If
        Next

        Dim loadAdJ() As String = My.Resources.data_ADJ.Split(Chr(10))
        For Me.l = 0 To loadAdJ.Length - 1
            Dim tmp() As String = loadAdJ(l).Split(CChar("|"))
            If Not adj.ContainsKey(tmp(0)) Then
                adj.Add(tmp(0), tmp(1))
                If Not masterList.ContainsKey(tmp(0)) Then
                    masterList.Add(tmp(0), tmp(0))
                End If
            End If

        Next

        Dim loadVRB() As String = My.Resources.data_VRB.Split(Chr(10))
        For Me.l = 0 To loadVRB.Length - 1
            Dim tmp() As String = loadVRB(l).Split(CChar("|"))
            If Not vrb.ContainsKey(tmp(0)) Then
                vrb.Add(tmp(0), tmp(1))
                If Not masterList.ContainsKey(tmp(0)) Then
                    masterList.Add(tmp(0), tmp(0))
                End If
            End If
        Next

        Dim loadNOUN() As String = My.Resources.data_NOUN.Split(Chr(10))
        For Me.l = 0 To loadNOUN.Length - 1
            Dim tmp() As String = loadNOUN(l).Split(CChar("|"))
            If Not noun.ContainsKey(tmp(0)) Then
                noun.Add(tmp(0), tmp(1))
                If Not masterList.ContainsKey(tmp(0)) Then
                    masterList.Add(tmp(0), tmp(0))
                End If
            End If
        Next


        autoComp = txt_Srch.AutoCompleteCustomSource
        autoComp.AddRange(masterList.Keys.ToArray)

    End Sub


    'display info on selected term or search for similar terms
    Friend Sub GetDefinition()
        txt = txt_Srch.Text.Trim 'no leading or trailing white space

        If masterList.ContainsKey(txt) Then
            txt_Srch.Text = masterList(txt)
            CreateDefinitionHTML(masterList(txt)) 'display definition
        Else
            CreateDefinitionHTML("Word Not Found", True)
        End If

    End Sub

    Private Sub CreateDefinitionHTML(ByVal s As String, Optional ByVal override As Boolean = False)
        If override Then
            wb.DocumentText = "<html><head><title></title></head><body><h3>" & s & "</h3></body></html>"
            Return
        End If
        Try
            Dim dispStr As String = My.Resources.DefinitionHTML

            dispStr &= "<h3>" & masterList(txt) & "</h3>"


            If noun.ContainsKey(s) Then
                Dim tmp() As String = noun(s).Split(CChar(";"))
                dispStr &= "<span class=" & Chr(34) & "word" & Chr(34) & ">"
                dispStr &= "(NOUN)</span>"
                dispStr &= "<ul>" & Chr(10)
                For Me.l = 0 To tmp.Length - 1
                    dispStr &= "<li>" & tmp(l) & "</li>"
                Next
                dispStr &= "</ul>" & Chr(10)
                dispStr &= "<hr align=" & Chr(34) & "center" & Chr(34) & "width=" & Chr(34) & "300" & Chr(34) & ">"
            End If

            If vrb.ContainsKey(s) Then
                Dim tmp() As String = vrb(s).Split(CChar(";"))
                dispStr &= "<span class=" & Chr(34) & "word" & Chr(34) & ">"
                dispStr &= "(VERB)</span>"
                dispStr &= "<ul>" & Chr(10)
                For Me.l = 0 To tmp.Length - 1
                    dispStr &= "<li>" & tmp(l) & "</li>"
                Next
                dispStr &= "</ul>" & Chr(10)
                dispStr &= "<hr align=" & Chr(34) & "center" & Chr(34) & "width=" & Chr(34) & "300" & Chr(34) & ">"
            End If

            If adj.ContainsKey(s) Then
                Dim tmp() As String = adj(s).Split(CChar(";"))
                dispStr &= "<span class=" & Chr(34) & "word" & Chr(34) & ">"
                dispStr &= "(ADJECTIVE)</span>"
                dispStr &= "<ul>" & Chr(10)
                For Me.l = 0 To tmp.Length - 1
                    dispStr &= "<li>" & tmp(l) & "</li>"
                Next
                dispStr &= "</ul>" & Chr(10)
                dispStr &= "<hr align=" & Chr(34) & "center" & Chr(34) & "width=" & Chr(34) & "300" & Chr(34) & ">"
            End If


            If adv.ContainsKey(s) Then
                Dim tmp() As String = adv(s).Split(CChar(";"))
                dispStr &= "<span class=" & Chr(34) & "word" & Chr(34) & ">"
                dispStr &= "(ADVERB)</span>"
                dispStr &= "<ul>" & Chr(10)
                For Me.l = 0 To tmp.Length - 1
                    dispStr &= "<li>" & tmp(l) & "</li>"
                Next
                dispStr &= "</ul>" & Chr(10)
                dispStr &= "<hr align=" & Chr(34) & "center" & Chr(34) & "width=" & Chr(34) & "300" & Chr(34) & ">"
            End If
            dispStr &= "</body>" & Chr(10) & "</html>"
            wb.DocumentText = dispStr

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation, "Dictionary")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GetDefinition()
    End Sub

    Private Sub txt_Srch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_Srch.KeyUp
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            GetDefinition()
        End If
    End Sub

    Private Sub txt_Srch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Srch.TextChanged
        'GetDefinition()
    End Sub
End Class