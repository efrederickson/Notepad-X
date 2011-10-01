Imports System.Collections.Generic
Imports System.Text

Public Partial Class ForStmt
	Inherits Statement
	Public VarName As String

	Public Start As Expr

	Public [End] As Expr

	Public [Step] As Expr

	Public Body As Chunk

End Class
