Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Delegate Function LuaFunc(args As LuaValue()) As LuaValue

Public Class LuaFunction
	Inherits LuaValue
	Public Sub New([function] As LuaFunc)
		Me.[Function] = [function]
	End Sub

	Public Property [Function]() As LuaFunc
		Get
			Return m_Function
		End Get
		Set
			m_Function = Value
		End Set
	End Property
	Private m_Function As LuaFunc

	Public Overrides ReadOnly Property Value() As Object
		Get
			Return Me.[Function]
		End Get
	End Property

	Public Overrides Function GetTypeCode() As String
		Return "function"
	End Function

	Public Function Invoke(args As LuaValue()) As LuaValue
		Return Me.[Function].Invoke(args)
	End Function
End Class
