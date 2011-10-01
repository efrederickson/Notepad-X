Imports System.Collections.Generic
Imports System.Text

Public Partial Class ForInStmt
	Inherits Statement
	Public NameList As New List(Of String)()

	Public ExprList As New List(Of Expr)()

	Public Body As Chunk

End Class
