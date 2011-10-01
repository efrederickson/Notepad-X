Imports System.Collections.Generic
Imports System.Text

Public Partial Class FunctionCall
	Inherits Access
	Public Overrides Function Evaluate(baseValue As LuaValue, enviroment As LuaTable) As LuaValue
		Dim [function] As LuaFunction = TryCast(baseValue, LuaFunction)

		If [function] IsNot Nothing Then
			If [function].[Function].Method.DeclaringType.FullName = "Language.Lua.Library.BaseLib" AndAlso ([function].[Function].Method.Name = "loadstring" OrElse [function].[Function].Method.Name = "dofile") Then
				If Me.Args.[String] IsNot Nothing Then
					Return [function].[Function].Invoke(New LuaValue() {Me.Args.[String].Evaluate(enviroment), enviroment})
				Else
					Return [function].[Function].Invoke(New LuaValue() {Me.Args.ArgList(0).Evaluate(enviroment), enviroment})
				End If
			End If

			If Me.Args.Table IsNot Nothing Then
				Return [function].[Function].Invoke(New LuaValue() {Me.Args.Table.Evaluate(enviroment)})
			ElseIf Me.Args.[String] IsNot Nothing Then
				Return [function].[Function].Invoke(New LuaValue() {Me.Args.[String].Evaluate(enviroment)})
			Else
				Dim args As List(Of LuaValue) = Me.Args.ArgList.ConvertAll(Function(arg) arg.Evaluate(enviroment))

				Return [function].[Function].Invoke(LuaMultiValue.UnWrapLuaValues(args.ToArray()))
			End If
		Else
			Throw New Exception("Invoke function call on non function value.")
		End If
	End Function
End Class
