Imports System.Collections.Generic
Imports System.Text

Public Partial Class Parser
	Private m_position As Integer

	Public Property Position() As Integer
		Get
			Return m_position
		End Get
		Set
			m_position = value
		End Set
	End Property

	Private Input As ParserInput(Of Char)

	Public Errors As New List(Of Tuple(Of Integer, String))()
	Private ErrorStatck As New Stack(Of Integer)()

	''' <summary>
	''' Memories parsing results, key is (PositionStart, Noterminal), value is (SyntacticElement, success, PostionAfter).
	''' </summary>
	Private ParsingResults As New Dictionary(Of Tuple(Of Integer, String), Tuple(Of Object, Boolean, Integer))()

	Public Sub New()
	End Sub

	Private Sub SetInput(input__1 As ParserInput(Of Char))
		Input = input__1
		m_position = 0
		ParsingResults.Clear()
	End Sub

	Private Function TerminalMatch(terminal As Char) As Boolean
		If Input.HasInput(m_position) Then
			Dim symbol As Char = Input.GetInputSymbol(m_position)
			Return terminal = symbol
		End If
		Return False
	End Function

	Private Function TerminalMatch(terminal As Char, pos As Integer) As Boolean
		If Input.HasInput(pos) Then
			Dim symbol As Char = Input.GetInputSymbol(pos)
			Return terminal = symbol
		End If
		Return False
	End Function

	Private Function MatchTerminal(terminal As Char, ByRef success As Boolean) As Char
		success = False
		If Input.HasInput(m_position) Then
			Dim symbol As Char = Input.GetInputSymbol(m_position)
			If terminal = symbol Then
				m_position += 1
				success = True
			End If
			Return symbol
		End If
		Return ControlChars.NullChar
	End Function

	Private Function MatchTerminalRange(start As Char, [end] As Char, ByRef success As Boolean) As Char
		success = False
		If Input.HasInput(m_position) Then
			Dim symbol As Char = Input.GetInputSymbol(m_position)
			If start <= symbol AndAlso symbol <= [end] Then
				m_position += 1
				success = True
			End If
			Return symbol
		End If
		Return ControlChars.NullChar
	End Function

	Private Function MatchTerminalSet(terminalSet As String, isComplement As Boolean, ByRef success As Boolean) As Char
		success = False
		If Input.HasInput(m_position) Then
			Dim symbol As Char = Input.GetInputSymbol(m_position)
			Dim match As Boolean = If(isComplement, terminalSet.IndexOf(symbol) = -1, terminalSet.IndexOf(symbol) > -1)
			If match Then
				m_position += 1
				success = True
			End If
			Return symbol
		End If
		Return ControlChars.NullChar
	End Function

	Private Function MatchTerminalString(terminalString As String, ByRef success As Boolean) As String
		Dim currrent_position As Integer = m_position
		For Each terminal As Char In terminalString
			MatchTerminal(terminal, success)
			If Not success Then
				m_position = currrent_position
				Return Nothing
			End If
		Next
		success = True
		Return terminalString
	End Function

	Private Function [Error](message As String) As Integer
		Errors.Add(New Tuple(Of Integer, String)(m_position, message))
		Return Errors.Count
	End Function

	Private Sub ClearError(count As Integer)
		Errors.RemoveRange(count, Errors.Count - count)
	End Sub

	Public Function GetEorrorMessages() As String
		Dim text As New StringBuilder()
		For Each msg As Tuple(Of Integer, String) In Errors
			text.Append(Input.FormErrorMessage(msg.Item1, msg.Item2))
			text.AppendLine()
		Next
		Return text.ToString()
	End Function
End Class
