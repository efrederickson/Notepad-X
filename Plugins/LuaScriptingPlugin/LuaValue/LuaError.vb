Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class LuaError
	Inherits Exception
	Public Sub New(message As String)
		MyBase.New(message)
	End Sub

	Public Sub New(message As String, innerException As Exception)
		MyBase.New(message, innerException)
	End Sub

	Public Sub New(messageformat As String, ParamArray args As Object())
		MyBase.New(String.Format(messageformat, args))
	End Sub
End Class
