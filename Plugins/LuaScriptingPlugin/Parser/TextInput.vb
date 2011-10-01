Imports System.Collections.Generic
Imports System.Text

Public Class TextInput
    Implements ParserInput(Of Char)
	Private InputText As String

	Private LineBreaks As List(Of Integer)

	Public Sub New(text As String)
		InputText = text

		LineBreaks = New List(Of Integer)()
		LineBreaks.Add(0)
		For index As Integer = 0 To InputText.Length - 1
			If InputText(index) = ControlChars.Lf Then
				LineBreaks.Add(index + 1)
			End If
		Next
		LineBreaks.Add(InputText.Length)
	End Sub

	#Region "ParserInput<char> Members"

	Public ReadOnly Property Length() As Integer Implements ParserInput(Of Char).Length
		Get
			Return InputText.Length
		End Get
	End Property

	Public Function HasInput(pos As Integer) As Boolean Implements ParserInput(Of Char).HasInput
		Return pos < InputText.Length
	End Function

	Public Function GetInputSymbol(pos As Integer) As Char Implements ParserInput(Of Char).GetInputSymbol
		Return InputText(pos)
	End Function

	Public Function GetSubSection(position As Integer, length As Integer) As Char() Implements ParserInput(Of Char).GetSubSection
		Return InputText.Substring(position, length).ToCharArray()
	End Function

	Public Function FormErrorMessage(position As Integer, message As String) As String Implements ParserInput(Of Char).FormErrorMessage
		Dim line As Integer
		Dim col As Integer
		GetLineColumnNumber(position, line, col)
		Dim ch As String = If(HasInput(position), "'" & GetInputSymbol(position) & "'", Nothing)
		Return [String].Format("Line {0}, Col {1} {2}: {3}", line, col, ch, message)
	End Function

	#End Region

	Public Sub GetLineColumnNumber(pos As Integer, ByRef line As Integer, ByRef col As Integer)
		col = 1
		For line = 1 To LineBreaks.Count - 1
			If LineBreaks(line) > pos Then
				For p As Integer = LineBreaks(line - 1) To pos - 1
					If InputText(p) = ControlChars.Tab Then
						col += 4
					Else
						col += 1
					End If
				Next
				Exit For
			End If
		Next
	End Sub

	Public Function GetSubString(start As Integer, length As Integer) As String
		Return InputText.Substring(start, length)
	End Function
End Class
