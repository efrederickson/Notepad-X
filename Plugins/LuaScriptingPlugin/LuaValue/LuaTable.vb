Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class LuaTable
	Inherits LuaValue
	Private list As List(Of LuaValue)

	Private dict As Dictionary(Of LuaValue, LuaValue)

	Public Sub New()
	End Sub

	Public Sub New(parent As LuaTable)
		Me.MetaTable = New LuaTable()
		Me.MetaTable.SetNameValue("__index", parent)
		Me.MetaTable.SetNameValue("__newindex", parent)
	End Sub

	Public Property MetaTable() As LuaTable
		Get
			Return m_MetaTable
		End Get
		Set
			m_MetaTable = Value
		End Set
	End Property
	Private m_MetaTable As LuaTable

	Public Overrides ReadOnly Property Value() As Object
		Get
			Return Me
		End Get
	End Property

	Public Overrides Function GetTypeCode() As String
		Return "table"
	End Function

	Public ReadOnly Property Length() As Integer
		Get
			If Me.list Is Nothing Then
				Return 0
			Else
				Return Me.list.Count
			End If
		End Get
	End Property

	Public ReadOnly Property Count() As Integer
		Get
			If Me.dict Is Nothing Then
				Return 0
			Else
				Return Me.dict.Count
			End If
		End Get
	End Property

	Public Overrides Function ToString() As String
		If Me.MetaTable IsNot Nothing Then
			Dim [function] As LuaFunction = TryCast(Me.MetaTable.GetValue("__tostring"), LuaFunction)
			If [function] IsNot Nothing Then
				Return [function].Invoke(New LuaValue() {Me}).ToString()
			End If
		End If

		Return "Table " & Me.GetHashCode()
	End Function

	Public ReadOnly Property ListValues() As IEnumerable(Of LuaValue)
		Get
			Return Me.list
		End Get
	End Property

	Public ReadOnly Property Keys() As IEnumerable(Of LuaValue)
        Get
            Dim l As New List(Of LuaValue)
            If Me.Length > 0 Then
                For index As Integer = 1 To Me.list.Count
                    'yield Return 
                    l.Add(New LuaNumber(index))
                Next
            End If
            If Me.Count > 0 Then
                For Each key As LuaValue In Me.dict.Keys
                    'yield Return 
                    l.Add(key)
                Next
            End If
            Return l
        End Get
	End Property

	Public ReadOnly Property KeyValuePairs() As IEnumerable(Of KeyValuePair(Of LuaValue, LuaValue))
		Get
			Return Me.dict
		End Get
	End Property

	Public Function ContainsKey(key As LuaValue) As Boolean
		If Me.dict IsNot Nothing Then
			If Me.dict.ContainsKey(key) Then
				Return True
			End If
		End If

		If Me.list IsNot Nothing Then
			Dim index As LuaNumber = TryCast(key, LuaNumber)
			If index IsNot Nothing AndAlso index.Number = CInt(Math.Truncate(index.Number)) Then
				Return index.Number >= 1 AndAlso index.Number <= Me.list.Count
			End If
		End If

		Return False
	End Function

	Public Sub AddValue(value As LuaValue)
		If Me.list Is Nothing Then
			Me.list = New List(Of LuaValue)()
		End If

		Me.list.Add(value)
	End Sub

	Public Sub InsertValue(index As Integer, value As LuaValue)
		If index > 0 AndAlso index <= Me.Length + 1 Then
			Me.list.Insert(index - 1, value)
		Else
			Throw New ArgumentOutOfRangeException("index")
		End If
	End Sub

	Public Function Remove(item As LuaValue) As Boolean
		Return Me.list.Remove(item)
	End Function

	Public Sub RemoveAt(index As Integer)
		Me.list.RemoveAt(index - 1)
	End Sub

	Public Sub Sort()
		Me.list.Sort(Function(a, b) 
		Dim n As LuaNumber = TryCast(a, LuaNumber)
		Dim m As LuaNumber = TryCast(b, LuaNumber)
		If n IsNot Nothing AndAlso m IsNot Nothing Then
			Return n.Number.CompareTo(m.Number)
		End If

		Dim s As LuaString = TryCast(a, LuaString)
		Dim t As LuaString = TryCast(b, LuaString)
		If s IsNot Nothing AndAlso t IsNot Nothing Then
			Return s.Text.CompareTo(t.Text)
		End If

		Return 0

End Function)
	End Sub

	Public Sub Sort(compare As LuaFunction)
		Me.list.Sort(Function(a, b) 
		Dim result As LuaValue = compare.Invoke(New LuaValue() {a, b})
		Dim boolValue As LuaBoolean = TryCast(result, LuaBoolean)
		If boolValue IsNot Nothing AndAlso boolValue.BoolValue = True Then
			Return 1
		Else
			Return -1
		End If

End Function)
	End Sub

	Public Function GetValue(index As Integer) As LuaValue
		If index > 0 AndAlso index <= Me.Length Then
			Return Me.list(index - 1)
		End If

		Return LuaNil.Nil
	End Function

	Public Function GetValue(name As String) As LuaValue
		Dim key As LuaValue = Me.GetKey(name)

		If key Is LuaNil.Nil Then
			If Me.MetaTable IsNot Nothing Then
				Return Me.GetValueFromMetaTable(name)
			End If

			Return LuaNil.Nil
		Else
			Return Me.dict(key)
		End If
	End Function

	Public Function GetKey(key As String) As LuaValue
		If Me.dict Is Nothing Then
			Return LuaNil.Nil
		End If

		For Each value As LuaValue In Me.dict.Keys
			Dim str As LuaString = TryCast(value, LuaString)

			If str IsNot Nothing AndAlso String.Equals(str.Text, key, StringComparison.Ordinal) Then
				Return value
			End If
		Next

		Return LuaNil.Nil
	End Function

	Public Sub SetNameValue(name As String, value As LuaValue)
		If value Is LuaNil.Nil Then
			Me.RemoveKey(name)
		Else
			Me.RawSetValue(name, value)
		End If
	End Sub

	Private Sub RemoveKey(name As String)
		Dim key As LuaValue = Me.GetKey(name)

		If key IsNot LuaNil.Nil Then
			Me.dict.Remove(key)
		End If
	End Sub

	Public Sub SetKeyValue(key As LuaValue, value As LuaValue)
		Dim number As LuaNumber = TryCast(key, LuaNumber)

		If number IsNot Nothing AndAlso number.Number = CInt(Math.Truncate(number.Number)) Then
			Dim index As Integer = CInt(Math.Truncate(number.Number))

			If index = Me.Length + 1 Then
				Me.AddValue(value)
				Return
			End If

			If index > 0 AndAlso index <= Me.Length Then
				Me.list(index - 1) = value
				Return
			End If
		End If

		If value Is LuaNil.Nil Then
			Me.RemoveKey(key)
			Return
		End If

		If Me.dict Is Nothing Then
			Me.dict = New Dictionary(Of LuaValue, LuaValue)()
		End If

		Me.dict(key) = value
	End Sub

	Private Sub RemoveKey(key As LuaValue)
		If key IsNot LuaNil.Nil AndAlso Me.dict IsNot Nothing AndAlso Me.dict.ContainsKey(key) Then
			Me.dict.Remove(key)
		End If
	End Sub

	Public Function GetValue(key As LuaValue) As LuaValue
		If key Is LuaNil.Nil Then
			Return LuaNil.Nil
		Else
			Dim number As LuaNumber = TryCast(key, LuaNumber)

			If number IsNot Nothing AndAlso number.Number = CInt(Math.Truncate(number.Number)) Then
				Dim index As Integer = CInt(Math.Truncate(number.Number))

				If index > 0 AndAlso index <= Me.Length Then
					Return Me.list(index - 1)
				End If
			End If

			If Me.dict IsNot Nothing AndAlso Me.dict.ContainsKey(key) Then
				Return Me.dict(key)
			ElseIf Me.MetaTable IsNot Nothing Then
				Return Me.GetValueFromMetaTable(key)
			End If

			Return LuaNil.Nil
		End If
	End Function

	Private Function GetValueFromMetaTable(name As String) As LuaValue
		Dim indexer As LuaValue = Me.MetaTable.GetValue("__index")

		Dim table As LuaTable = TryCast(indexer, LuaTable)

		If table IsNot Nothing Then
			Return table.GetValue(name)
		End If

		Dim [function] As LuaFunction = TryCast(indexer, LuaFunction)

		If [function] IsNot Nothing Then
			Return [function].[Function].Invoke(New LuaValue() {New LuaString(name)})
		End If

		Return LuaNil.Nil
	End Function

	Private Function GetValueFromMetaTable(key As LuaValue) As LuaValue
		Dim indexer As LuaValue = Me.MetaTable.GetValue("__index")

		Dim table As LuaTable = TryCast(indexer, LuaTable)

		If table IsNot Nothing Then
			Return table.GetValue(key)
		End If

		Dim [function] As LuaFunction = TryCast(indexer, LuaFunction)

		If [function] IsNot Nothing Then
			Return [function].[Function].Invoke(New LuaValue() {key})
		End If

		Return LuaNil.Nil
	End Function

	Public Function Register(name As String, [function] As LuaFunc) As LuaFunction
		Dim luaFunc As New LuaFunction([function])
		Me.SetNameValue(name, luaFunc)
		Return luaFunc
	End Function

	Public Function RawGetValue(key As LuaValue) As LuaValue
		If Me.dict IsNot Nothing AndAlso Me.dict.ContainsKey(key) Then
			Return Me.dict(key)
		End If

		Return LuaNil.Nil
	End Function

	Public Sub RawSetValue(name As String, value As LuaValue)
		Dim key As LuaValue = Me.GetKey(name)

		If key Is LuaNil.Nil Then
			key = New LuaString(name)
		End If

		If Me.dict Is Nothing Then
			Me.dict = New Dictionary(Of LuaValue, LuaValue)()
		End If

		Me.dict(key) = value
	End Sub
End Class
