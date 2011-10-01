Imports System.Collections.Generic
Imports System.Text
Imports System.Globalization

Public Partial Class NumberLiteral
	Inherits Term
	Public Overrides Function Evaluate(enviroment As LuaTable) As LuaValue
		Dim number As Double

		If String.IsNullOrEmpty(Me.HexicalText) Then
			number = Double.Parse(Me.Text, NumberStyles.AllowDecimalPoint Or NumberStyles.AllowExponent)
		Else
			number = Integer.Parse(Me.HexicalText, NumberStyles.HexNumber)
		End If

		Return New LuaNumber(number)
	End Function
End Class
