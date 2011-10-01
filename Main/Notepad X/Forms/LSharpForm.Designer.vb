'
' Created by SharpDevelop.
' User: elijah
' Date: 9/21/2011
' Time: 1:40 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class LSharpForm
	Inherits DockContent
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
	    Me.components = New System.ComponentModel.Container()
	    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LSharpForm))
	    Me.label1 = New System.Windows.Forms.Label()
	    Me.evalButton = New System.Windows.Forms.Button()
	    Me.outputTextBox = New System.Windows.Forms.TextBox()
	    Me.label2 = New System.Windows.Forms.Label()
	    Me.inputTextBox = New Alsing.Windows.Forms.SyntaxBoxControl()
	    Me.syntaxDocument1 = New Alsing.SourceCode.SyntaxDocument(Me.components)
	    Me.SuspendLayout
	    '
	    'label1
	    '
	    Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
	    Me.label1.Location = New System.Drawing.Point(12, 9)
	    Me.label1.Name = "label1"
	    Me.label1.Size = New System.Drawing.Size(68, 20)
	    Me.label1.TabIndex = 0
	    Me.label1.Text = "Command:"
	    '
	    'evalButton
	    '
	    Me.evalButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
	    Me.evalButton.Location = New System.Drawing.Point(502, 12)
	    Me.evalButton.Name = "evalButton"
	    Me.evalButton.Size = New System.Drawing.Size(75, 70)
	    Me.evalButton.TabIndex = 2
	    Me.evalButton.Text = "Run"
	    Me.evalButton.UseVisualStyleBackColor = true
	    AddHandler Me.evalButton.Click, AddressOf Me.Button1_Click
	    '
	    'outputTextBox
	    '
	    Me.outputTextBox.AcceptsReturn = true
	    Me.outputTextBox.AcceptsTab = true
	    Me.outputTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
	    Me.outputTextBox.Location = New System.Drawing.Point(0, 137)
	    Me.outputTextBox.Multiline = true
	    Me.outputTextBox.Name = "outputTextBox"
	    Me.outputTextBox.ReadOnly = true
	    Me.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
	    Me.outputTextBox.Size = New System.Drawing.Size(589, 191)
	    Me.outputTextBox.TabIndex = 3
	    Me.outputTextBox.WordWrap = false
	    '
	    'label2
	    '
	    Me.label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
	    Me.label2.BackColor = System.Drawing.Color.Transparent
	    Me.label2.Location = New System.Drawing.Point(12, 115)
	    Me.label2.Name = "label2"
	    Me.label2.Size = New System.Drawing.Size(68, 16)
	    Me.label2.TabIndex = 4
	    Me.label2.Text = "Output:"
	    '
	    'inputTextBox
	    '
	    Me.inputTextBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight
	    Me.inputTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
	    Me.inputTextBox.AutoListPosition = Nothing
	    Me.inputTextBox.AutoListSelectedText = "a123"
	    Me.inputTextBox.AutoListVisible = false
	    Me.inputTextBox.BackColor = System.Drawing.Color.White
	    Me.inputTextBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None
	    Me.inputTextBox.CopyAsRTF = false
	    Me.inputTextBox.Document = Me.syntaxDocument1
	    Me.inputTextBox.FontName = "Courier new"
	    Me.inputTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl
	    Me.inputTextBox.InfoTipCount = 1
	    Me.inputTextBox.InfoTipPosition = Nothing
	    Me.inputTextBox.InfoTipSelectedIndex = 1
	    Me.inputTextBox.InfoTipVisible = false
	    Me.inputTextBox.Location = New System.Drawing.Point(86, 12)
	    Me.inputTextBox.LockCursorUpdate = false
	    Me.inputTextBox.Name = "inputTextBox"
	    Me.inputTextBox.ShowScopeIndicator = false
	    Me.inputTextBox.ShowTabGuides = true
	    Me.inputTextBox.ShowWhitespace = true
	    Me.inputTextBox.Size = New System.Drawing.Size(410, 119)
	    Me.inputTextBox.SmoothScroll = false
	    Me.inputTextBox.SplitView = false
	    Me.inputTextBox.SplitviewH = -4
	    Me.inputTextBox.SplitviewV = -4
	    Me.inputTextBox.TabGuideColor = System.Drawing.Color.FromArgb(CType(CType(233,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(233,Byte),Integer))
	    Me.inputTextBox.TabIndex = 5
	    Me.inputTextBox.Text = "syntaxBoxControl1"
	    Me.inputTextBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark
	    '
	    'syntaxDocument1
	    '
	    Me.syntaxDocument1.Lines = New String() {""}
	    Me.syntaxDocument1.MaxUndoBufferSize = 1000
	    Me.syntaxDocument1.Modified = false
	    Me.syntaxDocument1.UndoStep = 0
	    '
	    'LSharpForm
	    '
	    Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
	    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
	    Me.ClientSize = New System.Drawing.Size(589, 328)
	    Me.Controls.Add(Me.inputTextBox)
	    Me.Controls.Add(Me.label2)
	    Me.Controls.Add(Me.outputTextBox)
	    Me.Controls.Add(Me.evalButton)
	    Me.Controls.Add(Me.label1)
	    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
	    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
	    Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
	    Me.Name = "LSharpForm"
	    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
	    Me.Text = "LSharp"
	    AddHandler FormClosing, AddressOf Me.LSharpForm_FormClosing
	    AddHandler Load, AddressOf Me.LSharpForm_Load
	    AddHandler VisibleChanged, AddressOf Me.LSharpForm_VisibleChanged
	    Me.ResumeLayout(false)
	    Me.PerformLayout
	End Sub
	Private syntaxDocument1 As Alsing.SourceCode.SyntaxDocument
	Private inputTextBox As Alsing.Windows.Forms.SyntaxBoxControl
	Private evalButton As System.Windows.Forms.Button
	Private outputTextBox As System.Windows.Forms.TextBox
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
End Class
