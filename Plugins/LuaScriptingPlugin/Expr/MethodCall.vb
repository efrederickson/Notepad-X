Imports System.Collections.Generic
Imports System.Text

Public Partial Class MethodCall
	Inherits Access
	Public Overrides Function Evaluate(baseValue As LuaValue, enviroment As LuaTable) As LuaValue
		Dim value As LuaValue = LuaValue.GetKeyValue(baseValue, New LuaString(Me.Method))
		Dim [function] As LuaFunction = TryCast(value, LuaFunction)

		If [function] IsNot Nothing Then
			If Me.Args.Table IsNot Nothing Then
				Return [function].[Function].Invoke(New LuaValue() {baseValue, Me.Args.Table.Evaluate(enviroment)})
			ElseIf Me.Args.[String] IsNot Nothing Then
				Return [function].[Function].Invoke(New LuaValue() {baseValue, Me.Args.[String].Evaluate(enviroment)})
			Else
				Dim args As List(Of LuaValue) = Me.Args.ArgList.ConvertAll(Function(arg) arg.Evaluate(enviroment))
				args.Insert(0, baseValue)
				Return [function].[Function].Invoke(args.ToArray())
			End If
		Else
			Throw New Exception("Invoke method call on non function value.")
		End If
	End Function
End Class
