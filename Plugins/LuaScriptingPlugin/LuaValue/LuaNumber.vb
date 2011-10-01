Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class LuaNumber
	Inherits LuaValue
	Public Sub New(number As Double)
		Me.Number = number
	End Sub

	Public Property Number() As Double
		Get
			Return m_Number
		End Get
		Set
			m_Number = Value
		End Set
	End Property
	Private m_Number As Double

	Public Overrides ReadOnly Property Value() As Object
		Get
			Return Me.Number
		End Get
	End Property

	Public Overrides Function GetTypeCode() As String
		Return "number"
	End Function

	Public Overrides Function ToString() As String
		Return Me.Number.ToString()
	End Function
End Class
