Imports System.Collections.Generic
Imports System.Text

Public Partial Class IfStmt
	Inherits Statement
	Public Condition As Expr

	Public ThenBlock As Chunk

	Public ElseifBlocks As New List(Of ElseifBlock)()

	Public ElseBlock As Chunk

End Class
