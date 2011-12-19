<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Macros
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.openFileButton = New System.Windows.Forms.Button()
        Me.macroPathTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.outputListBox = New System.Windows.Forms.ListBox()
        Me.runMacroButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.exitButton = New System.Windows.Forms.Button()
        Me.commandTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.runCommandButton = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'openFileButton
        '
        Me.openFileButton.Location = New System.Drawing.Point(226, 29)
        Me.openFileButton.Name = "openFileButton"
        Me.openFileButton.Size = New System.Drawing.Size(87, 23)
        Me.openFileButton.TabIndex = 1
        Me.openFileButton.Text = "Open File"
        Me.openFileButton.UseVisualStyleBackColor = True
        '
        'macroPathTextBox
        '
        Me.macroPathTextBox.Location = New System.Drawing.Point(44, 32)
        Me.macroPathTextBox.Name = "macroPathTextBox"
        Me.macroPathTextBox.Size = New System.Drawing.Size(176, 20)
        Me.macroPathTextBox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "File:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'outputListBox
        '
        Me.outputListBox.FormattingEnabled = True
        Me.outputListBox.Location = New System.Drawing.Point(12, 129)
        Me.outputListBox.Name = "outputListBox"
        Me.outputListBox.Size = New System.Drawing.Size(294, 160)
        Me.outputListBox.TabIndex = 9999999
        Me.outputListBox.TabStop = False
        '
        'runMacroButton
        '
        Me.runMacroButton.Location = New System.Drawing.Point(226, 84)
        Me.runMacroButton.Name = "runMacroButton"
        Me.runMacroButton.Size = New System.Drawing.Size(87, 24)
        Me.runMacroButton.TabIndex = 4
        Me.runMacroButton.Text = "Run File"
        Me.runMacroButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Output:"
        '
        'exitButton
        '
        Me.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.exitButton.Location = New System.Drawing.Point(512, 263)
        Me.exitButton.Name = "exitButton"
        Me.exitButton.Size = New System.Drawing.Size(75, 23)
        Me.exitButton.TabIndex = 5
        Me.exitButton.Text = "Exit"
        Me.exitButton.UseVisualStyleBackColor = True
        '
        'commandTextBox
        '
        Me.commandTextBox.Location = New System.Drawing.Point(75, 59)
        Me.commandTextBox.Name = "commandTextBox"
        Me.commandTextBox.Size = New System.Drawing.Size(145, 20)
        Me.commandTextBox.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Command:"
        '
        'runCommandButton
        '
        Me.runCommandButton.Location = New System.Drawing.Point(227, 55)
        Me.runCommandButton.Name = "runCommandButton"
        Me.runCommandButton.Size = New System.Drawing.Size(86, 23)
        Me.runCommandButton.TabIndex = 3
        Me.runCommandButton.Text = "Run Command"
        Me.runCommandButton.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.HorizontalScrollbar = True
        Me.ListBox1.Location = New System.Drawing.Point(319, 29)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(268, 160)
        Me.ListBox1.TabIndex = 10000000
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(316, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 13)
        Me.Label4.TabIndex = 10000001
        Me.Label4.Text = "Macros Already Installed"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(512, 195)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 42)
        Me.Button1.TabIndex = 10000002
        Me.Button1.Text = "Run this Macro"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Macros
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.exitButton
        Me.ClientSize = New System.Drawing.Size(599, 298)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.runCommandButton)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.commandTextBox)
        Me.Controls.Add(Me.exitButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.runMacroButton)
        Me.Controls.Add(Me.outputListBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.macroPathTextBox)
        Me.Controls.Add(Me.openFileButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Macros"
        Me.ShowIcon = False
        Me.Text = "Macros"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents openFileButton As System.Windows.Forms.Button
    Friend WithEvents macroPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents outputListBox As System.Windows.Forms.ListBox
    Friend WithEvents runMacroButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents exitButton As System.Windows.Forms.Button
    Friend WithEvents commandTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents runCommandButton As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
