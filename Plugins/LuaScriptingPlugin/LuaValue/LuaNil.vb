Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class LuaNil
	Inherits LuaValue
	Public Shared ReadOnly Nil As New LuaNil()

	Private Sub New()
	End Sub

	Public Overrides ReadOnly Property Value() As Object
		Get
			Return Nothing
		End Get
	End Property

	Public Overrides Function GetTypeCode() As String
		Return "nil"
	End Function

	Public Overrides Function GetBooleanValue() As Boolean
		Return False
	End Function

	Public Overrides Function ToString() As String
		Return "nil"
	End Function
End Class
