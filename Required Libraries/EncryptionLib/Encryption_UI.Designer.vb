<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Encryption_UI
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

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
        Me.asciiRadioButton = New System.Windows.Forms.RadioButton()
        Me.rijndaelRadioButton = New System.Windows.Forms.RadioButton()
        Me.tripleDesRadioButton = New System.Windows.Forms.RadioButton()
        Me.xorRadioButton = New System.Windows.Forms.RadioButton()
        Me.desRadioButton = New System.Windows.Forms.RadioButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rsaRadioButton = New System.Windows.Forms.RadioButton()
        Me.rcTwoRadioButton = New System.Windows.Forms.RadioButton()
        Me.aesRadioButton = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.outputLabel = New System.Windows.Forms.Label()
        Me.encryptButton = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.codeTextBox = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'asciiRadioButton
        '
        Me.asciiRadioButton.AutoSize = True
        Me.asciiRadioButton.Checked = True
        Me.asciiRadioButton.Location = New System.Drawing.Point(16, 21)
        Me.asciiRadioButton.Name = "asciiRadioButton"
        Me.asciiRadioButton.Size = New System.Drawing.Size(52, 17)
        Me.asciiRadioButton.TabIndex = 0
        Me.asciiRadioButton.TabStop = True
        Me.asciiRadioButton.Text = "ASCII"
        Me.asciiRadioButton.UseVisualStyleBackColor = True
        '
        'rijndaelRadioButton
        '
        Me.rijndaelRadioButton.AutoSize = True
        Me.rijndaelRadioButton.Location = New System.Drawing.Point(16, 44)
        Me.rijndaelRadioButton.Name = "rijndaelRadioButton"
        Me.rijndaelRadioButton.Size = New System.Drawing.Size(63, 17)
        Me.rijndaelRadioButton.TabIndex = 1
        Me.rijndaelRadioButton.Text = "Rijndael"
        Me.rijndaelRadioButton.UseVisualStyleBackColor = True
        '
        'tripleDesRadioButton
        '
        Me.tripleDesRadioButton.AutoSize = True
        Me.tripleDesRadioButton.Location = New System.Drawing.Point(16, 90)
        Me.tripleDesRadioButton.Name = "tripleDesRadioButton"
        Me.tripleDesRadioButton.Size = New System.Drawing.Size(76, 17)
        Me.tripleDesRadioButton.TabIndex = 2
        Me.tripleDesRadioButton.Text = "Triple DES"
        Me.tripleDesRadioButton.UseVisualStyleBackColor = True
        '
        'xorRadioButton
        '
        Me.xorRadioButton.AutoSize = True
        Me.xorRadioButton.Location = New System.Drawing.Point(16, 113)
        Me.xorRadioButton.Name = "xorRadioButton"
        Me.xorRadioButton.Size = New System.Drawing.Size(41, 17)
        Me.xorRadioButton.TabIndex = 3
        Me.xorRadioButton.Text = "Xor"
        Me.xorRadioButton.UseVisualStyleBackColor = True
        '
        'desRadioButton
        '
        Me.desRadioButton.AutoSize = True
        Me.desRadioButton.Location = New System.Drawing.Point(16, 67)
        Me.desRadioButton.Name = "desRadioButton"
        Me.desRadioButton.Size = New System.Drawing.Size(47, 17)
        Me.desRadioButton.TabIndex = 4
        Me.desRadioButton.Text = "DES"
        Me.desRadioButton.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(150, 85)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(228, 253)
        Me.TextBox1.TabIndex = 5
        Me.TextBox1.Text = "Hello"
        Me.TextBox1.WordWrap = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rsaRadioButton)
        Me.GroupBox1.Controls.Add(Me.rcTwoRadioButton)
        Me.GroupBox1.Controls.Add(Me.aesRadioButton)
        Me.GroupBox1.Controls.Add(Me.desRadioButton)
        Me.GroupBox1.Controls.Add(Me.asciiRadioButton)
        Me.GroupBox1.Controls.Add(Me.rijndaelRadioButton)
        Me.GroupBox1.Controls.Add(Me.xorRadioButton)
        Me.GroupBox1.Controls.Add(Me.tripleDesRadioButton)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(129, 281)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Encryption Type"
        '
        'rsaRadioButton
        '
        Me.rsaRadioButton.AutoSize = True
        Me.rsaRadioButton.Location = New System.Drawing.Point(16, 185)
        Me.rsaRadioButton.Name = "rsaRadioButton"
        Me.rsaRadioButton.Size = New System.Drawing.Size(47, 17)
        Me.rsaRadioButton.TabIndex = 7
        Me.rsaRadioButton.TabStop = True
        Me.rsaRadioButton.Text = "RSA"
        Me.rsaRadioButton.UseVisualStyleBackColor = True
        '
        'rcTwoRadioButton
        '
        Me.rcTwoRadioButton.AutoSize = True
        Me.rcTwoRadioButton.Location = New System.Drawing.Point(16, 161)
        Me.rcTwoRadioButton.Name = "rcTwoRadioButton"
        Me.rcTwoRadioButton.Size = New System.Drawing.Size(46, 17)
        Me.rcTwoRadioButton.TabIndex = 6
        Me.rcTwoRadioButton.TabStop = True
        Me.rcTwoRadioButton.Text = "RC2"
        Me.rcTwoRadioButton.UseVisualStyleBackColor = True
        '
        'aesRadioButton
        '
        Me.aesRadioButton.AutoSize = True
        Me.aesRadioButton.Location = New System.Drawing.Point(16, 137)
        Me.aesRadioButton.Name = "aesRadioButton"
        Me.aesRadioButton.Size = New System.Drawing.Size(46, 17)
        Me.aesRadioButton.TabIndex = 5
        Me.aesRadioButton.TabStop = True
        Me.aesRadioButton.Text = "AES"
        Me.aesRadioButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(147, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Text To Encrypt:"
        '
        'outputLabel
        '
        Me.outputLabel.Location = New System.Drawing.Point(12, 387)
        Me.outputLabel.Name = "outputLabel"
        Me.outputLabel.Size = New System.Drawing.Size(366, 195)
        Me.outputLabel.TabIndex = 8
        Me.outputLabel.Text = "Output:"
        '
        'encryptButton
        '
        Me.encryptButton.Location = New System.Drawing.Point(159, 344)
        Me.encryptButton.Name = "encryptButton"
        Me.encryptButton.Size = New System.Drawing.Size(75, 23)
        Me.encryptButton.TabIndex = 9
        Me.encryptButton.Text = "Encrypt"
        Me.encryptButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 353)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Code:"
        '
        'codeTextBox
        '
        Me.codeTextBox.Location = New System.Drawing.Point(53, 346)
        Me.codeTextBox.Name = "codeTextBox"
        Me.codeTextBox.Size = New System.Drawing.Size(100, 20)
        Me.codeTextBox.TabIndex = 11
        '
        'Encryption_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 591)
        Me.Controls.Add(Me.codeTextBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.encryptButton)
        Me.Controls.Add(Me.outputLabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Encryption_UI"
        Me.Text = "Encryption_UI"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents asciiRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents rijndaelRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents tripleDesRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents xorRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents desRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents outputLabel As System.Windows.Forms.Label
    Friend WithEvents encryptButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents codeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents rsaRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents rcTwoRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents aesRadioButton As System.Windows.Forms.RadioButton
End Class
