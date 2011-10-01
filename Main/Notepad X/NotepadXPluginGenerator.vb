Imports Microsoft.CSharp
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports System.Text
' TODO:
Namespace NotepadXLSharp
	Public Class NotepadXPluginGenerator
		Public Shared Function CreateClass(className As String, LSharpCode As DefinedMethod()) As Type
			Dim source As New StringBuilder("using System;" & vbLf)
			source.AppendLine("using LSharp; ")
				source.AppendLine(String.Format("class {0} : {1} {{", className, "object, NotepadX.IPlugin"))
			For Each method As DefinedMethod In LSharpCode
				Try
					source.Append("public object " + method.Name & "(")
					If method.args.Length > 0 Then
						source.Append("object " + method.args(0))
						For i As Integer = 1 To method.args.Length - 1
							source.Append(", object " + method.args(i))
						Next
					End If
					source.Append(") {" & vbNewline)
					source.Append(vbTab & "LSharp.Environment env = new LSharp.Environment();" & vbLf)
					If method.args.Length > 0 Then
						source.AppendLine((vbTab & "Runtime.EvalString(""(= " + method.args(0) & " "" + ") + method.args(0) & " + "")"", env);")
						For i As Integer = 1 To method.args.Length - 1
							source.AppendLine((vbTab & "Runtime.EvalString(""(= " + method.args(i) & " "" + ") + method.args(i) & " + "")"", env);")
						Next
					End If
					source.Append(vbTab & "Runtime.Eval(Reader.Read(new System.IO.StringReader(""" + method.Commands & """), ReadTable.DefaultReadTable()), env);" & vbLf)
					source.Append("return null; // TODO: return results of above commands" & vbLf)
					' TODO: return results of above commands
					source.Append("}" & vbLf)
				Catch e As Exception
					Console.WriteLine(e.Message)
					source.AppendLine("// ERROR: " & e.Message)
				End Try
			Next
			source.AppendLine("}")
			Console.WriteLine(source.ToString())

			Dim cscompiler As New CSharpCodeProvider()
			Dim compiler As ICodeCompiler = cscompiler.CreateCompiler()
			Dim compparams As New CompilerParameters()
			compparams.GenerateInMemory = True

			compparams.ReferencedAssemblies.Add("LSharp.dll")
			'			foreach (Assembly a in AssemblyCache.Instance().Assemblies()) {
			'				compparams.ReferencedAssemblies.Add(a.FullName);
			'			}

			Dim compresult As CompilerResults = compiler.CompileAssemblyFromSource(compparams, source.ToString())
			If compresult Is Nothing Or compresult.Errors.Count > 0 Then
				For Each e As CompilerError In compresult.Errors
					Console.WriteLine(e)
				Next
				Throw New Exception("class creation error")
			End If

			Dim o As [Object] = TypeCache.Instance().FindType(className)
			If o IsNot Nothing Then
				Dim a As Assembly = Assembly.GetAssembly(TryCast(o, Type))
				AssemblyCache.Instance().Remove(a)
			End If

			AssemblyCache.Instance().Add(compresult.CompiledAssembly)
			Return compresult.CompiledAssembly.[GetType](className)
		End Function
	End Class
End Namespace
