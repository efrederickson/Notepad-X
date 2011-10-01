'
' Created by SharpDevelop.
' User: elijah
' Date: 9/21/2011
' Time: 1:40 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Partial Class LSharpForm
Inherits DockContent	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		Dim list As Alsing.SourceCode.SyntaxDefinitionList= new Alsing.SourceCode.SyntaxDefinitionList()
        list.GetLanguageFromFile("LSharp.syn")
        Dim l2 As System.Collections.Generic.List(Of Alsing.SourceCode.SyntaxDefinition)= list.GetSyntaxDefinitions()
        Dim def As Alsing.SourceCode.SyntaxDefinition = l2(0)
        syntaxDocument1.Parser.Init(def)
	End Sub
	
	Sub Button2_Click(sender As Object, e As EventArgs)
		Me.Close()
	End Sub
	
	Sub Button1_Click(sender As Object, e As EventArgs)
		Try
			outputTextBox.Text = Printer.WriteToString(Runtime.Eval(Reader.Read(New System.IO.StringReader("(do " & syntaxdocument1.Text & ")"), ReadTable.DefaultReadTable()), LSharpEnvironment))
		Catch ex As Exception
			outputTextBox.Text = ex.ToString()
		End Try
	End Sub
	
	Sub LSharpForm_FormClosing(sender As Object, e As FormClosingEventArgs)
	End Sub
	
	Sub LSharpForm_VisibleChanged(sender As Object, e As EventArgs)
		Me.Size = New Size(595, 352)
	End Sub
	
	Sub LSharpForm_Load(sender As Object, e As EventArgs)
		Me.Size = New Size(595, 352)
	End Sub
	
	Public Sub SetSize()
	    Me.Size = New Size(595, 352)
	End Sub
End Class
