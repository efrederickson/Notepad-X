Imports System.Collections.Generic
Imports System.Text

Public Partial Class OperatorExpr
	Inherits Expr
	Public Terms As New LinkedList(Of Object)()

	Public Sub Add(oper As String)
		Terms.AddLast(oper)
	End Sub

	Public Sub Add(term As Term)
		Terms.AddLast(term)
	End Sub

	Public Function BuildExpressionTree() As Term
		Dim node = Me.Terms.First
		Dim term As Term = TryCast(node.Value, Term)

		If Me.Terms.Count = 1 Then
			Return term
		Else
			If term IsNot Nothing Then
				Return BuildExpressionTree(TryCast(node.Value, Term), node.[Next])
			End If

			Dim oper As String = TryCast(node.Value, String)

			If oper IsNot Nothing Then
				Return BuildExpressionTree(Nothing, node)
			End If

			Return Nothing
		End If
	End Function

	' Operator-precedence parsing algorithm
	Private Shared Function BuildExpressionTree(leftTerm As Term, node As LinkedListNode(Of Object)) As Term
		Dim oper As String = TryCast(node.Value, String)
		Dim rightNode = node.[Next]
		Dim rightTerm As Term = TryCast(rightNode.Value, Term)

		If rightNode.[Next] Is Nothing Then
			' last node
			Return New Operation(oper, leftTerm, rightTerm)
		Else
			Dim nextOper As String = TryCast(rightNode.[Next].Value, String)

			If OperTable.IsPrior(oper, nextOper) Then
				Return BuildExpressionTree(New Operation(oper, leftTerm, rightTerm), rightNode.[Next])
			Else
				Return New Operation(oper, leftTerm, BuildExpressionTree(rightTerm, rightNode.[Next]))
			End If
		End If
	End Function

	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Dim term As Term = Me.BuildExpressionTree()
		Return term.Evaluate(enviroment)
	End Function

	Public Overrides Function Simplify() As Term
		Return Me.BuildExpressionTree().Simplify()
	End Function
End Class
