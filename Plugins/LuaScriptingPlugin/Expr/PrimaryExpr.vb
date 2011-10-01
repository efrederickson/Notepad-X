Imports System.Collections.Generic
Imports System.Text

Public Partial Class PrimaryExpr
	Inherits Term
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Dim baseValue As LuaValue = Me.Base.Evaluate(enviroment)

		For Each access As Access In Me.Accesses
			baseValue = access.Evaluate(baseValue, enviroment)
		Next

		Return baseValue
	End Function

	Public Overrides Function Simplify() As Term
		If Me.Accesses.Count = 0 Then
			Return Me.Base.Simplify()
		Else
			Return Me
		End If
	End Function
End Class
