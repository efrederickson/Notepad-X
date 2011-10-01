Imports System.Collections.Generic
Imports System.Text

Public Partial Class FunctionBody
	Public Function Evaluate(enviroment As LuaTable) As LuaValue
        Return New LuaFunction(New LuaFunc(Function(args As LuaValue())
                                               Dim table = New LuaTable(enviroment)

                                               Dim names As List(Of String) = Me.ParamList.NameList

                                               If names.Count > 0 Then
                                                   Dim argCount As Integer = Math.Min(names.Count, args.Length)

                                                   For i As Integer = 0 To argCount - 1
                                                       table.SetNameValue(names(i), args(i))
                                                   Next

                                                   If Me.ParamList.HasVarArg Then
                                                       If argCount < args.Length Then
                                                           Dim remainedArgs As LuaValue() = New LuaValue(args.Length - argCount - 1) {}
                                                           For i As Integer = 0 To remainedArgs.Length - 1
                                                               remainedArgs(i) = args(argCount + i)
                                                           Next
                                                           table.SetNameValue("...", New LuaMultiValue(remainedArgs))
                                                       End If
                                                   End If
                                               ElseIf Me.ParamList.IsVarArg IsNot Nothing Then
                                                   table.SetNameValue("...", New LuaMultiValue(args))
                                               End If

                                               Me.Chunk.Enviroment = table

                                               Return Me.Chunk.Execute()
                                           End Function))
	End Function
End Class
