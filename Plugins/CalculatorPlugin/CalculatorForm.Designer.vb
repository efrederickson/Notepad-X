<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CalculatorForm
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
        Me.OperationTextBox = New System.Windows.Forms.TextBox()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.exitButton = New System.Windows.Forms.Button()
        Me.resultTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'OperationTextBox
        '
        Me.OperationTextBox.Location = New System.Drawing.Point(13, 13)
        Me.OperationTextBox.Name = "OperationTextBox"
        Me.OperationTextBox.Size = New System.Drawing.Size(199, 20)
        Me.OperationTextBox.TabIndex = 0
        Me.OperationTextBox.Text = "0"
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(13, 65)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(200, 23)
        Me.Button11.TabIndex = 14
        Me.Button11.Text = "Calculate"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(13, 95)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(199, 23)
        Me.Button14.TabIndex = 18
        Me.Button14.Text = "Copy Result"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'exitButton
        '
        Me.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.exitButton.Location = New System.Drawing.Point(13, 124)
        Me.exitButton.Name = "exitButton"
        Me.exitButton.Size = New System.Drawing.Size(199, 23)
        Me.exitButton.TabIndex = 19
        Me.exitButton.Text = "Exit"
        Me.exitButton.UseVisualStyleBackColor = True
        '
        'resultTextBox
        '
        Me.resultTextBox.Location = New System.Drawing.Point(13, 39)
        Me.resultTextBox.Name = "resultTextBox"
        Me.resultTextBox.ReadOnly = True
        Me.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.resultTextBox.Size = New System.Drawing.Size(199, 20)
        Me.resultTextBox.TabIndex = 15
        Me.resultTextBox.Text = "Result"
        '
        'CalculatorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.exitButton
        Me.ClientSize = New System.Drawing.Size(235, 163)
        Me.Controls.Add(Me.exitButton)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.resultTextBox)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.OperationTextBox)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "CalculatorForm"
        Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
        Me.ShowIcon = False
        Me.Text = "Calculator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OperationTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents exitButton As System.Windows.Forms.Button
    Friend WithEvents resultTextBox As System.Windows.Forms.TextBox
End Class
