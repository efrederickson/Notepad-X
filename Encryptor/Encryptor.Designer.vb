<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Encryptor
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
        Me.codeTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.encryptButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.desRadioButton = New System.Windows.Forms.RadioButton()
        Me.asciiRadioButton = New System.Windows.Forms.RadioButton()
        Me.rijndaelRadioButton = New System.Windows.Forms.RadioButton()
        Me.xorRadioButton = New System.Windows.Forms.RadioButton()
        Me.tripleDesRadioButton = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'codeTextBox
        '
        Me.codeTextBox.Location = New System.Drawing.Point(53, 201)
        Me.codeTextBox.Name = "codeTextBox"
        Me.codeTextBox.Size = New System.Drawing.Size(100, 20)
        Me.codeTextBox.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 208)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Code:"
        '
        'encryptButton
        '
        Me.encryptButton.Location = New System.Drawing.Point(159, 199)
        Me.encryptButton.Name = "encryptButton"
        Me.encryptButton.Size = New System.Drawing.Size(75, 23)
        Me.encryptButton.TabIndex = 16
        Me.encryptButton.Text = "Encrypt"
        Me.encryptButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.desRadioButton)
        Me.GroupBox1.Controls.Add(Me.asciiRadioButton)
        Me.GroupBox1.Controls.Add(Me.rijndaelRadioButton)
        Me.GroupBox1.Controls.Add(Me.xorRadioButton)
        Me.GroupBox1.Controls.Add(Me.tripleDesRadioButton)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(129, 154)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Encryption Type"
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 173)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "File:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(53, 170)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 20
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(553, 239)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.codeTextBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.encryptButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Encryptor"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents codeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents encryptButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents desRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents asciiRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents rijndaelRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents xorRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents tripleDesRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
