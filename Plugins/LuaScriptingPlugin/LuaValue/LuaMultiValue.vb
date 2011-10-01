Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class LuaMultiValue
	Inherits LuaValue
	Public Sub New(values As LuaValue())
		Me.Values = values
	End Sub

	Public Property Values() As LuaValue()
		Get
			Return m_Values
		End Get
		Set
			m_Values = Value
		End Set
	End Property
	Private m_Values As LuaValue()

	Public Overrides ReadOnly Property Value() As Object
		Get
			Return Me.Values
		End Get
	End Property

	Public Overrides Function GetTypeCode() As String
		Throw New InvalidOperationException()
	End Function

	Public Shared Function WrapLuaValues(values As LuaValue()) As LuaValue
		If values Is Nothing OrElse values.Length = 0 Then
			Return LuaNil.Nil
		ElseIf values.Length = 1 Then
			Return values(0)
		Else
			Return New LuaMultiValue(UnWrapLuaValues(values))
		End If
	End Function

	Public Shared Function UnWrapLuaValues(values As LuaValue()) As LuaValue()
		If values Is Nothing OrElse values.Length = 0 OrElse ContainsMultiValue(values) = False Then
			Return values
		End If

		If values.Length = 1 AndAlso TypeOf values(0) Is LuaMultiValue Then
			Return TryCast(values(0), LuaMultiValue).Values
		End If

		Dim neatValues As New List(Of LuaValue)(values.Length)

		For i As Integer = 0 To values.Length - 2
			Dim value As LuaValue = values(i)
			Dim multiValue As LuaMultiValue = TryCast(value, LuaMultiValue)

			If multiValue IsNot Nothing Then
				neatValues.Add(multiValue.Values(0))
			Else
				neatValues.Add(value)
			End If
		Next

		Dim lastValue As LuaValue = values(values.Length - 1)
		Dim lastMultiValue As LuaMultiValue = TryCast(lastValue, LuaMultiValue)

		If lastMultiValue IsNot Nothing Then
			neatValues.AddRange(lastMultiValue.Values)
		Else
			neatValues.Add(lastValue)
		End If

		Return neatValues.ToArray()
	End Function

	Private Shared Function ContainsMultiValue(values As LuaValue()) As Boolean
		For Each value As LuaValue In values
			If TypeOf value Is LuaMultiValue Then
				Return True
			End If
		Next
		Return False
	End Function
End Class
