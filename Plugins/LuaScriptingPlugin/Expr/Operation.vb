Imports System.Collections.Generic
Imports System.Text

''' <summary>
''' Represent Unary or Binary Operation, for Unary Operation the LeftOperand is not used.
''' </summary>
Public Partial Class Operation
    Inherits Term
    Public [Operator] As String

    Public LeftOperand As Term

    Public RightOperand As Term

    Public Sub New(ByVal oper As String)
        Me.[Operator] = oper
    End Sub

    Public Sub New(ByVal oper As String, ByVal left As Term, ByVal right As Term)
        Me.[Operator] = oper
        Me.LeftOperand = If(left Is Nothing, Nothing, left.Simplify())
        Me.RightOperand = If(right Is Nothing, Nothing, right.Simplify())
    End Sub

    Public Overrides Function Evaluate(ByVal enviroment As LuaTable) As LuaValue
        If Me.LeftOperand Is Nothing Then
            Return PrefixUnaryOperation([Operator], RightOperand, enviroment)
        ElseIf Me.RightOperand Is Nothing Then
            Return LeftOperand.Evaluate(enviroment)
        Else
            Return InfixBinaryOperation(LeftOperand, [Operator], RightOperand, enviroment)
        End If
    End Function

    Private Function PrefixUnaryOperation(ByVal [Operator] As String, ByVal RightOperand As Term, ByVal enviroment As LuaTable) As LuaValue
        Dim rightValue As LuaValue = RightOperand.Evaluate(enviroment)

        Select Case [Operator]
            Case "-"
                Dim number = TryCast(rightValue, LuaNumber)
                If number IsNot Nothing Then
                    Return New LuaNumber(-number.Number)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__unm", rightValue, Nothing)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {rightValue})
                    End If
                End If
                Exit Select
            Case "#"
                Dim table = TryCast(rightValue, LuaTable)
                If table IsNot Nothing Then
                    Return New LuaNumber(table.Length)
                End If
                Dim str = TryCast(rightValue, LuaString)
                If str IsNot Nothing Then
                    Return New LuaNumber(str.Text.Length)
                End If
                Exit Select
            Case "not"
                Dim rightBool = TryCast(rightValue, LuaBoolean)
                If rightBool IsNot Nothing Then
                    Return LuaBoolean.From(Not rightBool.BoolValue)
                End If
                Exit Select
        End Select

        Return LuaNil.Nil
    End Function

    Private Function InfixBinaryOperation(ByVal LeftOperand As Term, ByVal [Operator] As String, ByVal RightOperand As Term, ByVal enviroment As LuaTable) As LuaValue
        Dim leftValue As LuaValue = LeftOperand.Evaluate(enviroment)
        Dim rightValue As LuaValue = RightOperand.Evaluate(enviroment)

        Select Case [Operator]
            Case "+"
                Dim left = TryCast(leftValue, LuaNumber)
                Dim right = TryCast(rightValue, LuaNumber)
                If left IsNot Nothing AndAlso right IsNot Nothing Then
                    Return New LuaNumber(left.Number + right.Number)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__add", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case "-"
                Dim left = TryCast(leftValue, LuaNumber)
                Dim right = TryCast(rightValue, LuaNumber)
                If left IsNot Nothing AndAlso right IsNot Nothing Then
                    Return New LuaNumber(left.Number - right.Number)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__sub", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case "*"
                Dim left = TryCast(leftValue, LuaNumber)
                Dim right = TryCast(rightValue, LuaNumber)
                If left IsNot Nothing AndAlso right IsNot Nothing Then
                    Return New LuaNumber(left.Number * right.Number)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__mul", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case "/"
                Dim left = TryCast(leftValue, LuaNumber)
                Dim right = TryCast(rightValue, LuaNumber)
                If left IsNot Nothing AndAlso right IsNot Nothing Then
                    Return New LuaNumber(left.Number / right.Number)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__div", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case "%"
                Dim left = TryCast(leftValue, LuaNumber)
                Dim right = TryCast(rightValue, LuaNumber)
                If left IsNot Nothing AndAlso right IsNot Nothing Then
                    Return New LuaNumber(left.Number Mod right.Number)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__mod", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case "^"
                Dim left = TryCast(leftValue, LuaNumber)
                Dim right = TryCast(rightValue, LuaNumber)
                If left IsNot Nothing AndAlso right IsNot Nothing Then
                    Return New LuaNumber(Math.Pow(left.Number, right.Number))
                Else
                    Dim func As LuaFunction = GetMetaFunction("__pow", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case "=="
                Return LuaBoolean.From(leftValue.Equals(rightValue))
            Case "~="
                Return LuaBoolean.From(leftValue.Equals(rightValue) = False)
            Case "<"
                Dim compare__1 As System.Nullable(Of Integer) = Compare(leftValue, rightValue)
                If compare__1 IsNot Nothing Then
                    Return LuaBoolean.From(compare__1 < 0)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__lt", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case ">"
                Dim compare__1 As System.Nullable(Of Integer)
                compare__1 = Compare(leftValue, rightValue)
                If compare__1 IsNot Nothing Then
                    Return LuaBoolean.From(compare__1 > 0)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__gt", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case "<="
                Dim compare__1 As System.Nullable(Of Integer)
                compare__1 = Compare(leftValue, rightValue)
                If compare__1 IsNot Nothing Then
                    Return LuaBoolean.From(compare__1 <= 0)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__le", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case ">="
                Dim compare__1 As System.Nullable(Of Integer)
                compare__1 = Compare(leftValue, rightValue)
                If compare__1 IsNot Nothing Then
                    Return LuaBoolean.From(compare__1 >= 0)
                Else
                    Dim func As LuaFunction = GetMetaFunction("__ge", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case ".."
                If (TypeOf leftValue Is LuaString OrElse TypeOf leftValue Is LuaNumber) AndAlso (TypeOf rightValue Is LuaString OrElse TypeOf rightValue Is LuaNumber) Then
                    Return New LuaString(String.Concat(leftValue, rightValue))
                Else
                    Dim func As LuaFunction = GetMetaFunction("__concat", leftValue, rightValue)
                    If func IsNot Nothing Then
                        Return func.Invoke(New LuaValue() {leftValue, rightValue})
                    End If
                End If
                Exit Select
            Case "and"
                Dim leftBool As Boolean = leftValue.GetBooleanValue()
                Dim rightBool As Boolean = rightValue.GetBooleanValue()
                If leftBool = False Then
                    Return leftValue
                Else
                    Return rightValue
                End If
            Case "or"
                Dim leftBool As Boolean = leftValue.GetBooleanValue()
                Dim rightBool As Boolean = rightValue.GetBooleanValue()
                If leftBool = True Then
                    Return leftValue
                Else
                    Return rightValue
                End If
        End Select

        Return Nothing
    End Function

    Private Shared Function Compare(ByVal leftValue As LuaValue, ByVal rightValue As LuaValue) As System.Nullable(Of Integer)
        Dim left As LuaNumber = TryCast(leftValue, LuaNumber)
        Dim right As LuaNumber = TryCast(rightValue, LuaNumber)
        If left IsNot Nothing AndAlso right IsNot Nothing Then
            Return left.Number.CompareTo(right.Number)
        End If

        Dim leftString As LuaString = TryCast(leftValue, LuaString)
        Dim rightString As LuaString = TryCast(rightValue, LuaString)
        If leftString IsNot Nothing AndAlso rightString IsNot Nothing Then
            Return StringComparer.Ordinal.Compare(leftString.Text, rightString.Text)
        End If

        Return Nothing
    End Function

    Private Shared Function GetMetaFunction(ByVal name As String, ByVal leftValue As LuaValue, ByVal rightValue As LuaValue) As LuaFunction
        Dim left As LuaTable = TryCast(leftValue, LuaTable)

        If left IsNot Nothing Then
            Dim func As LuaFunction = TryCast(left.GetValue(name), LuaFunction)

            If func IsNot Nothing Then
                Return func
            End If
        End If

        Dim right As LuaTable = TryCast(rightValue, LuaTable)

        If right IsNot Nothing Then
            Return TryCast(right.GetValue(name), LuaFunction)
        End If

        Return Nothing
    End Function
End Class
