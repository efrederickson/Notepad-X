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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Encryptor))
        Me.codeTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.encryptButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rsaRadioButton = New System.Windows.Forms.RadioButton()
        Me.desRadioButton = New System.Windows.Forms.RadioButton()
        Me.rcTwoRadioButton = New System.Windows.Forms.RadioButton()
        Me.asciiRadioButton = New System.Windows.Forms.RadioButton()
        Me.aesRadioButton = New System.Windows.Forms.RadioButton()
        Me.rijndaelRadioButton = New System.Windows.Forms.RadioButton()
        Me.xorRadioButton = New System.Windows.Forms.RadioButton()
        Me.tripleDesRadioButton = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'codeTextBox
        '
        Me.codeTextBox.Location = New System.Drawing.Point(54, 285)
        Me.codeTextBox.Name = "codeTextBox"
        Me.codeTextBox.Size = New System.Drawing.Size(100, 20)
        Me.codeTextBox.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 292)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Code:"
        '
        'encryptButton
        '
        Me.encryptButton.Location = New System.Drawing.Point(160, 283)
        Me.encryptButton.Name = "encryptButton"
        Me.encryptButton.Size = New System.Drawing.Size(75, 23)
        Me.encryptButton.TabIndex = 16
        Me.encryptButton.Text = "Encrypt"
        Me.encryptButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rsaRadioButton)
        Me.GroupBox1.Controls.Add(Me.desRadioButton)
        Me.GroupBox1.Controls.Add(Me.rcTwoRadioButton)
        Me.GroupBox1.Controls.Add(Me.asciiRadioButton)
        Me.GroupBox1.Controls.Add(Me.aesRadioButton)
        Me.GroupBox1.Controls.Add(Me.rijndaelRadioButton)
        Me.GroupBox1.Controls.Add(Me.xorRadioButton)
        Me.GroupBox1.Controls.Add(Me.tripleDesRadioButton)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(129, 236)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Encryption Type"
        '
        'rsaRadioButton
        '
        Me.rsaRadioButton.AutoSize = True
        Me.rsaRadioButton.Location = New System.Drawing.Point(16, 182)
        Me.rsaRadioButton.Name = "rsaRadioButton"
        Me.rsaRadioButton.Size = New System.Drawing.Size(47, 17)
        Me.rsaRadioButton.TabIndex = 23
        Me.rsaRadioButton.TabStop = True
        Me.rsaRadioButton.Text = "RSA"
        Me.rsaRadioButton.UseVisualStyleBackColor = True
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
        'rcTwoRadioButton
        '
        Me.rcTwoRadioButton.AutoSize = True
        Me.rcTwoRadioButton.Location = New System.Drawing.Point(16, 159)
        Me.rcTwoRadioButton.Name = "rcTwoRadioButton"
        Me.rcTwoRadioButton.Size = New System.Drawing.Size(46, 17)
        Me.rcTwoRadioButton.TabIndex = 22
        Me.rcTwoRadioButton.TabStop = True
        Me.rcTwoRadioButton.Text = "RC2"
        Me.rcTwoRadioButton.UseVisualStyleBackColor = True
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
        'aesRadioButton
        '
        Me.aesRadioButton.AutoSize = True
        Me.aesRadioButton.Location = New System.Drawing.Point(16, 136)
        Me.aesRadioButton.Name = "aesRadioButton"
        Me.aesRadioButton.Size = New System.Drawing.Size(46, 17)
        Me.aesRadioButton.TabIndex = 21
        Me.aesRadioButton.TabStop = True
        Me.aesRadioButton.Text = "AES"
        Me.aesRadioButton.UseVisualStyleBackColor = True
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
        Me.Label1.Location = New System.Drawing.Point(16, 257)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "File:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(54, 254)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(296, 20)
        Me.TextBox1.TabIndex = 20
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(148, 230)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(184, 17)
        Me.CheckBox1.TabIndex = 21
        Me.CheckBox1.Text = "Create Backup File (unencrypted)"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Encryptor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 318)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.codeTextBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.encryptButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Encryptor"
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
    Friend WithEvents rsaRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents rcTwoRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents aesRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox

End Class
