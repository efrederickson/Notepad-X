Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text

Namespace Library
	Public NotInheritable Class MathLib
		Private Sub New()
		End Sub
		Public Shared Sub RegisterModule(enviroment As LuaTable)
			Dim [module] As New LuaTable()
			RegisterFunctions([module])
			enviroment.SetNameValue("math", [module])
		End Sub

		Public Shared Sub RegisterFunctions([module] As LuaTable)
			[module].SetNameValue("huge", New LuaNumber(Double.MaxValue))
			[module].SetNameValue("pi", New LuaNumber(Math.PI))
			[module].Register("abs", AddressOf abs)
			[module].Register("acos", AddressOf acos)
			[module].Register("asin", AddressOf asin)
			[module].Register("atan", AddressOf atan)
			[module].Register("atan2", AddressOf atan2)
			[module].Register("ceil", AddressOf ceil)
			[module].Register("cos", AddressOf cos)
			[module].Register("cosh", AddressOf cosh)
			[module].Register("deg", AddressOf deg)
			[module].Register("exp", AddressOf exp)
			[module].Register("floor", AddressOf floor)
			[module].Register("fmod", AddressOf fmod)
			[module].Register("log", AddressOf log)
			[module].Register("log10", AddressOf log10)
			[module].Register("max", AddressOf max)
			[module].Register("min", AddressOf min)
			[module].Register("modf", AddressOf modf)
			[module].Register("pow", AddressOf pow)
			[module].Register("rad", AddressOf rad)
			[module].Register("random", AddressOf random)
			[module].Register("randomseed", AddressOf randomseed)
			[module].Register("sin", AddressOf sin)
			[module].Register("sinh", AddressOf sinh)
			[module].Register("sqrt", AddressOf sqrt)
			[module].Register("tan", AddressOf tan)
			[module].Register("tanh", AddressOf tanh)
		End Sub

		Public Shared Function abs(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Abs(number.Number))
		End Function

		Public Shared Function acos(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Acos(number.Number))
		End Function

		Public Shared Function asin(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Asin(number.Number))
		End Function

		Public Shared Function atan(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Atan(number.Number))
		End Function

		Public Shared Function atan2(values As LuaValue()) As LuaValue
			Dim numbers = CheckArgs2(values)
			Return New LuaNumber(Math.Atan2(numbers.Item1, numbers.Item2))
		End Function

		Public Shared Function ceil(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Ceiling(number.Number))
		End Function

		Public Shared Function cos(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Cos(number.Number))
		End Function

		Public Shared Function cosh(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Cosh(number.Number))
		End Function

		Public Shared Function deg(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(number.Number * 180 / Math.PI)
		End Function

		Public Shared Function exp(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Exp(number.Number))
		End Function

		Public Shared Function floor(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Floor(number.Number))
		End Function

		Public Shared Function fmod(values As LuaValue()) As LuaValue
			Dim numbers = CheckArgs2(values)
			Return New LuaNumber(Math.IEEERemainder(numbers.Item1, numbers.Item2))
		End Function

		Public Shared Function log(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Log(number.Number))
		End Function

		Public Shared Function log10(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Log10(number.Number))
		End Function

		Public Shared Function max(values As LuaValue()) As LuaValue
			Return New LuaNumber(values.Max(Function(v) TryCast(v, LuaNumber).Number))
		End Function

		Public Shared Function min(values As LuaValue()) As LuaValue
			Return New LuaNumber(values.Min(Function(v) TryCast(v, LuaNumber).Number))
		End Function

		Public Shared Function modf(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Dim [integer] As Double = Math.Floor(number.Number)
			Return New LuaMultiValue(New LuaNumber() {New LuaNumber([integer]), New LuaNumber(number.Number - [integer])})
		End Function

		Public Shared Function pow(values As LuaValue()) As LuaValue
			Dim numbers = CheckArgs2(values)
			Return New LuaNumber(Math.Pow(numbers.Item1, numbers.Item2))
		End Function

		Public Shared Function rad(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(number.Number * Math.PI / 180)
		End Function

		Shared randomGenerator As New Random()
		Public Shared Function random(values As LuaValue()) As LuaValue
			If values.Length = 0 Then
				Return New LuaNumber(randomGenerator.NextDouble())
			ElseIf values.Length = 1 Then
				Dim number1 As LuaNumber = TryCast(values(0), LuaNumber)
				Return New LuaNumber(randomGenerator.[Next](CInt(Math.Truncate(number1.Number))))
			Else
				Dim numbers = CheckArgs2(values)
				Return New LuaNumber(randomGenerator.[Next](CInt(Math.Truncate(numbers.Item1)), CInt(Math.Truncate(numbers.Item2))))
			End If
		End Function

		Public Shared Function randomseed(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			randomGenerator = New Random(CInt(Math.Truncate(number.Number)))
			Return number
		End Function

		Public Shared Function sin(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Sin(number.Number))
		End Function

		Public Shared Function sinh(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Sinh(number.Number))
		End Function

		Public Shared Function sqrt(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Sqrt(number.Number))
		End Function

		Public Shared Function tan(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Tan(number.Number))
		End Function

		Public Shared Function tanh(values As LuaValue()) As LuaValue
			Dim number As LuaNumber = CheckArgs(values)
			Return New LuaNumber(Math.Tanh(number.Number))
		End Function

		Private Shared Function CheckArgs(values As LuaValue()) As LuaNumber
			If values.Length >= 1 Then
				Dim number As LuaNumber = TryCast(values(0), LuaNumber)
				If number IsNot Nothing Then
					Return number
				Else
					Throw New LuaError("bad argument #1 to 'abs' (number expected, got {0})", values(0).GetTypeCode())
				End If
			Else
				Throw New LuaError("bad argument #1 to 'abs' (number expected, got no value)")
			End If
		End Function

		Private Shared Function CheckArgs2(values As LuaValue()) As Tuple(Of Double, Double)
			If values.Length >= 2 Then
				Dim number1 As LuaNumber = TryCast(values(0), LuaNumber)
				If number1 Is Nothing Then
					Throw New LuaError("bad argument #1 to 'abs' (number expected, got {0})", values(0).GetTypeCode())
				End If

				Dim number2 As LuaNumber = TryCast(values(1), LuaNumber)
				If number2 Is Nothing Then
					Throw New LuaError("bad argument #2 to 'abs' (number expected, got {0})", values(1).GetTypeCode())
				End If

				Return Tuple.Create(number1.Number, number2.Number)
			Else
				Throw New LuaError("bad argument #1 to 'abs' (number expected, got no value)")
			End If
		End Function
	End Class
End Namespace
