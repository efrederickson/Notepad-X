Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Library
	Public NotInheritable Class OSLib
		Private Sub New()
		End Sub
		Public Shared Sub RegisterModule(enviroment As LuaTable)
			Dim [module] As New LuaTable()
			RegisterFunctions([module])
			enviroment.SetNameValue("os", [module])
		End Sub

		Public Shared Sub RegisterFunctions([module] As LuaTable)
			[module].Register("clock", AddressOf clock)
			[module].Register("date", AddressOf [date])
			[module].Register("time", AddressOf time)
			[module].Register("execute", AddressOf execute)
			[module].Register("exit", AddressOf [exit])
			[module].Register("getenv", AddressOf getenv)
			[module].Register("remove", AddressOf remove)
			[module].Register("rename", AddressOf rename)
			[module].Register("tmpname", AddressOf tmpname)
		End Sub

		Public Shared Function clock(values As LuaValue()) As LuaValue
			Dim seconds As Integer = Environment.TickCount \ 1000
			Return New LuaNumber(seconds)
		End Function

		Public Shared Function [date](values As LuaValue()) As LuaValue
			Dim format As LuaString = TryCast(values(0), LuaString)
			If format IsNot Nothing Then
				If format.Text = "*t" Then
					Dim table As New LuaTable()
					Dim now As DateTime = DateTime.Now
					table.SetNameValue("year", New LuaNumber(now.Year))
					table.SetNameValue("month", New LuaNumber(now.Month))
					table.SetNameValue("day", New LuaNumber(now.Day))
					table.SetNameValue("hour", New LuaNumber(now.Hour))
					table.SetNameValue("min", New LuaNumber(now.Minute))
					table.SetNameValue("sec", New LuaNumber(now.Second))
					table.SetNameValue("wday", New LuaNumber(CInt(now.DayOfWeek)))
					table.SetNameValue("yday", New LuaNumber(now.DayOfYear))
					table.SetNameValue("isdst", LuaBoolean.From(now.IsDaylightSavingTime()))
				Else
					Return New LuaString(DateTime.Now.ToString(format.Text))
				End If
			End If

			Return New LuaString(DateTime.Now.ToShortDateString())
		End Function

		Public Shared Function time(values As LuaValue()) As LuaValue
			Return New LuaNumber(New TimeSpan(DateTime.Now.Ticks).TotalSeconds)
		End Function

		Public Shared Function execute(values As LuaValue()) As LuaValue
			If values.Length > 0 Then
				Dim command As LuaString = TryCast(values(0), LuaString)
				System.Diagnostics.Process.Start(command.Text)
			End If
			Return New LuaNumber(1)
		End Function

		Public Shared Function [exit](values As LuaValue()) As LuaValue
			System.Threading.Thread.CurrentThread.Abort()
			Return New LuaNumber(0)
		End Function

		Public Shared Function getenv(values As LuaValue()) As LuaValue
			Dim name As LuaString = TryCast(values(0), LuaString)
			Dim variable As String = Environment.GetEnvironmentVariable(name.Text)
			If variable Is Nothing Then
				Return LuaNil.Nil
			Else
				Return New LuaString(variable)
			End If
		End Function

		Public Shared Function remove(values As LuaValue()) As LuaValue
			Dim file__1 As LuaString = TryCast(values(0), LuaString)
			If File.Exists(file__1.Text) Then
				File.Delete(file__1.Text)
				Return New LuaString("File is deleted.")
			ElseIf Directory.Exists(file__1.Text) Then
				If Directory.GetFileSystemEntries(file__1.Text).Length = 0 Then
					Directory.Delete(file__1.Text)
					Return New LuaString("Directory is deleted.")
				Else
					Return New LuaMultiValue(New LuaValue() {LuaNil.Nil, New LuaString("Directory is not empty.")})
				End If
			Else
				Return New LuaMultiValue(New LuaValue() {LuaNil.Nil, New LuaString("File or directory does not exist.")})
			End If
		End Function

		Public Shared Function rename(values As LuaValue()) As LuaValue
			Dim oldName As LuaString = TryCast(values(0), LuaString)
			Dim newName As LuaString = TryCast(values(1), LuaString)

			If File.Exists(oldName.Text) Then
				File.Move(oldName.Text, newName.Text)
				Return New LuaString("File is renamed.")
			ElseIf Directory.Exists(oldName.Text) Then
				Directory.Move(oldName.Text, newName.Text)
				Return New LuaString("Directory is renamed.")
			Else
				Return New LuaMultiValue(New LuaValue() {LuaNil.Nil, New LuaString("File or directory does not exist.")})
			End If
		End Function

		Public Shared Function tmpname(values As LuaValue()) As LuaValue
			Return New LuaString(Path.GetTempFileName())
		End Function
	End Class
End Namespace
