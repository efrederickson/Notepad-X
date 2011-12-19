<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartInfoScreen
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
        Dim CBlendItems1 As Library.Controls.cBlendItems = New Library.Controls.cBlendItems()
        Dim CFocalPoints1 As Library.Controls.cFocalPoints = New Library.Controls.cFocalPoints()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartInfoScreen))
        Me.Version = New System.Windows.Forms.Label()
        Me.Copyright = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.progressLabel = New System.Windows.Forms.Label()
        Me.totalProgressBar = New Library.Controls.ProgressBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Version
        '
        Me.Version.BackColor = System.Drawing.Color.Transparent
        Me.Version.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.Location = New System.Drawing.Point(1, 233)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(496, 20)
        Me.Version.TabIndex = 4
        Me.Version.Text = "Version Major.Minor.Build Build Revision"
        Me.Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Copyright
        '
        Me.Copyright.BackColor = System.Drawing.Color.Transparent
        Me.Copyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Copyright.Location = New System.Drawing.Point(4, 172)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New System.Drawing.Size(493, 47)
        Me.Copyright.TabIndex = 5
        Me.Copyright.Text = "Copyright"
        Me.Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel1.Location = New System.Drawing.Point(69, 117)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(341, 13)
        Me.LinkLabel1.TabIndex = 6
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Notepad X on Sourceforge: http://sourceforge.com/projects/notepadx" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.Location = New System.Drawing.Point(26, 145)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(441, 15)
        Me.LinkLabel2.TabIndex = 7
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Notepad X on the MP website: http://elijah.awesome99.org/index.php/notepad-x"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 60.0!)
        Me.Label1.Location = New System.Drawing.Point(35, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(413, 91)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Notepad X"
        '
        'progressLabel
        '
        Me.progressLabel.AutoSize = True
        Me.progressLabel.BackColor = System.Drawing.Color.Transparent
        Me.progressLabel.Location = New System.Drawing.Point(11, 281)
        Me.progressLabel.Name = "progressLabel"
        Me.progressLabel.Size = New System.Drawing.Size(54, 13)
        Me.progressLabel.TabIndex = 9
        Me.progressLabel.Text = "Loading..."
        '
        'totalProgressBar
        '
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.Navy, System.Drawing.Color.Blue}
        CBlendItems1.iPoint = New Single() {0.0!, 1.0!}
        Me.totalProgressBar.BarColorBlend = CBlendItems1
        Me.totalProgressBar.BarColorSolid = System.Drawing.Color.Lime
        Me.totalProgressBar.BarColorSolidB = System.Drawing.Color.White
        Me.totalProgressBar.BarLength = Library.Controls.ProgressBar.eBarLength.Full
        Me.totalProgressBar.BarLengthValue = CType(25, Short)
        Me.totalProgressBar.BarPadding = New System.Windows.Forms.Padding(0)
        Me.totalProgressBar.BarStyleFill = Library.Controls.ProgressBar.eBarStyle.Solid
        Me.totalProgressBar.BarStyleHatch = System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard
        Me.totalProgressBar.BarStyleLinear = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.totalProgressBar.BarStyleTexture = Nothing
        Me.totalProgressBar.BarStyleWrapMode = System.Drawing.Drawing2D.WrapMode.Clamp
        Me.totalProgressBar.BarType = Library.Controls.ProgressBar.eBarType.Bar
        Me.totalProgressBar.BorderWidth = CType(1, Short)
        Me.totalProgressBar.Corners.All = CType(2, Short)
        Me.totalProgressBar.Corners.LowerLeft = CType(2, Short)
        Me.totalProgressBar.Corners.LowerRight = CType(2, Short)
        Me.totalProgressBar.Corners.UpperLeft = CType(2, Short)
        Me.totalProgressBar.Corners.UpperRight = CType(2, Short)
        Me.totalProgressBar.CornersApply = Library.Controls.ProgressBar.eCornersApply.Both
        Me.totalProgressBar.CylonInterval = CType(1, Short)
        Me.totalProgressBar.CylonMove = 5.0!
        Me.totalProgressBar.FillDirection = Library.Controls.ProgressBar.eFillDirection.Up_Right
        CFocalPoints1.CenterPoint = CType(resources.GetObject("CFocalPoints1.CenterPoint"), System.Drawing.PointF)
        CFocalPoints1.FocusScales = CType(resources.GetObject("CFocalPoints1.FocusScales"), System.Drawing.PointF)
        Me.totalProgressBar.FocalPoints = CFocalPoints1
        Me.totalProgressBar.Location = New System.Drawing.Point(12, 297)
        Me.totalProgressBar.Maximum = 6
        Me.totalProgressBar.Name = "totalProgressBar"
        Me.totalProgressBar.Orientation = Library.Controls.ProgressBar.eOrientation.Horizontal
        Me.totalProgressBar.Shape = Library.Controls.ProgressBar.eShape.Rectangle
        Me.totalProgressBar.ShapeTextFont = New System.Drawing.Font("Arial Black", 30.0!)
        Me.totalProgressBar.ShapeTextRotate = Library.Controls.ProgressBar.eRotateText.None
        Me.totalProgressBar.Size = New System.Drawing.Size(472, 20)
        Me.totalProgressBar.TabIndex = 11
        Me.totalProgressBar.TextAlignment = System.Drawing.StringAlignment.Center
        Me.totalProgressBar.TextAlignmentVert = System.Drawing.StringAlignment.Center
        Me.totalProgressBar.TextPlacement = Library.Controls.ProgressBar.eTextPlacement.OverBar
        Me.totalProgressBar.TextRotate = Library.Controls.ProgressBar.eRotateText.None
        Me.totalProgressBar.TextShow = Library.Controls.ProgressBar.eTextShow.None
        Me.totalProgressBar.Value = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 327)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Loading Core..."
        '
        'StartInfoScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(496, 323)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.totalProgressBar)
        Me.Controls.Add(Me.progressLabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.Copyright)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StartInfoScreen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Copyright As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents progressLabel As System.Windows.Forms.Label
    Friend WithEvents totalProgressBar As Library.Controls.ProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
