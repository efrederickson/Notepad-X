Imports System.Collections.Generic
Imports System.Text

Public Enum Associativity
    NonAssociative
    LeftAssociative
    RightAssociative
End Enum

Public Class OperTable
    Shared precedence As New Dictionary(Of String, Integer)()
    Shared associativity As Associativity()

    Shared Sub New()
        Dim operators As New List(Of String())()
        operators.Add(New String() {"or"})
        operators.Add(New String() {"and"})
        operators.Add(New String() {"==", "~="})
        operators.Add(New String() {">", ">=", "<", "<="})
        operators.Add(New String() {".."})
        operators.Add(New String() {"+", "-"})
        operators.Add(New String() {"*", "/", "%"})
        operators.Add(New String() {"#", "not"})
        operators.Add(New String() {"^"})

        For index As Integer = 0 To operators.Count - 1
            For Each oper As String In operators(index)
                precedence.Add(oper, index)
            Next
        Next

        associativity = New Associativity(operators.Count - 1) {}
        associativity(0) = LuaScriptingPlugin.Associativity.LeftAssociative
        associativity(1) = LuaScriptingPlugin.Associativity.LeftAssociative
        associativity(2) = LuaScriptingPlugin.Associativity.NonAssociative
        associativity(3) = LuaScriptingPlugin.Associativity.LeftAssociative
        associativity(4) = LuaScriptingPlugin.Associativity.LeftAssociative
        associativity(5) = LuaScriptingPlugin.Associativity.LeftAssociative
        associativity(6) = LuaScriptingPlugin.Associativity.LeftAssociative
        associativity(7) = LuaScriptingPlugin.Associativity.NonAssociative
        associativity(8) = LuaScriptingPlugin.Associativity.RightAssociative
    End Sub

    ''' <summary>
    ''' Whether the input text is an operator or not
    ''' </summary>
    ''' <param name="oper"></param>
    ''' <returns></returns>
    Public Shared Function Contains(ByVal oper As String) As Boolean
        Return precedence.ContainsKey(oper)
    End Function

    ''' <summary>
    ''' whether operLeft has higher precedence than operRight
    ''' </summary>
    ''' <param name="operLeft"></param>
    ''' <param name="operRight"></param>
    ''' <returns></returns>
    Public Shared Function IsPrior(ByVal operLeft As String, ByVal operRight As String) As Boolean
        If operLeft Is Nothing Then
            Return False
        End If
        If operRight Is Nothing Then
            Return True
        End If

        Dim priLeft As Integer = precedence(operLeft)
        Dim priRight As Integer = precedence(operRight)
        If priLeft > priRight Then
            Return True
        ElseIf priLeft < priRight Then
            Return False
        Else
            Select Case associativity(priLeft)
                Case LuaScriptingPlugin.Associativity.LeftAssociative
                    Return True
                Case LuaScriptingPlugin.Associativity.RightAssociative
                    Return False
                Case Else
                    Return True
            End Select
        End If
    End Function
End Class
