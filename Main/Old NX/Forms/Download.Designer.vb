<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Download
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Download))
        Me.DownloadWorker = New System.ComponentModel.BackgroundWorker()
        Me.cancelButton = New System.Windows.Forms.Button()
        Me.info = New System.Windows.Forms.Label()
        Me.status = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New Library.Controls.ProgressBar()
        Me.SuspendLayout()
        '
        'DownloadWorker
        '
        Me.DownloadWorker.WorkerReportsProgress = True
        Me.DownloadWorker.WorkerSupportsCancellation = True
        '
        'cancelButton
        '
        Me.cancelButton.Location = New System.Drawing.Point(251, 54)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 0
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'info
        '
        Me.info.AutoSize = True
        Me.info.Location = New System.Drawing.Point(12, 9)
        Me.info.Name = "info"
        Me.info.Size = New System.Drawing.Size(78, 13)
        Me.info.TabIndex = 1
        Me.info.Text = "Downloading..."
        '
        'status
        '
        Me.status.AutoSize = True
        Me.status.Location = New System.Drawing.Point(12, 59)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(0, 13)
        Me.status.TabIndex = 5
        '
        'ProgressBar1
        '
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.Navy, System.Drawing.Color.Blue}
        CBlendItems1.iPoint = New Single() {0.0!, 1.0!}
        Me.ProgressBar1.BarColorBlend = CBlendItems1
        Me.ProgressBar1.BarColorSolid = System.Drawing.Color.Lime
        Me.ProgressBar1.BarColorSolidB = System.Drawing.Color.White
        Me.ProgressBar1.BarLength = Library.Controls.ProgressBar.eBarLength.Full
        Me.ProgressBar1.BarLengthValue = CType(25, Short)
        Me.ProgressBar1.BarPadding = New System.Windows.Forms.Padding(0)
        Me.ProgressBar1.BarStyleFill = Library.Controls.ProgressBar.eBarStyle.Solid
        Me.ProgressBar1.BarStyleHatch = System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard
        Me.ProgressBar1.BarStyleLinear = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.ProgressBar1.BarStyleTexture = Nothing
        Me.ProgressBar1.BarStyleWrapMode = System.Drawing.Drawing2D.WrapMode.Clamp
        Me.ProgressBar1.BarType = Library.Controls.ProgressBar.eBarType.Bar
        Me.ProgressBar1.BorderWidth = CType(1, Short)
        Me.ProgressBar1.Corners.All = CType(0, Short)
        Me.ProgressBar1.Corners.LowerLeft = CType(0, Short)
        Me.ProgressBar1.Corners.LowerRight = CType(0, Short)
        Me.ProgressBar1.Corners.UpperLeft = CType(0, Short)
        Me.ProgressBar1.Corners.UpperRight = CType(0, Short)
        Me.ProgressBar1.CornersApply = Library.Controls.ProgressBar.eCornersApply.Both
        Me.ProgressBar1.CylonInterval = CType(1, Short)
        Me.ProgressBar1.CylonMove = 5.0!
        Me.ProgressBar1.FillDirection = Library.Controls.ProgressBar.eFillDirection.Up_Right
        CFocalPoints1.CenterPoint = CType(resources.GetObject("CFocalPoints1.CenterPoint"), System.Drawing.PointF)
        CFocalPoints1.FocusScales = CType(resources.GetObject("CFocalPoints1.FocusScales"), System.Drawing.PointF)
        Me.ProgressBar1.FocalPoints = CFocalPoints1
        Me.ProgressBar1.Location = New System.Drawing.Point(15, 25)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Orientation = Library.Controls.ProgressBar.eOrientation.Horizontal
        Me.ProgressBar1.Shape = Library.Controls.ProgressBar.eShape.Rectangle
        Me.ProgressBar1.ShapeTextFont = New System.Drawing.Font("Arial Black", 30.0!)
        Me.ProgressBar1.ShapeTextRotate = Library.Controls.ProgressBar.eRotateText.None
        Me.ProgressBar1.Size = New System.Drawing.Size(311, 20)
        Me.ProgressBar1.TabIndex = 4
        Me.ProgressBar1.TextAlignment = System.Drawing.StringAlignment.Center
        Me.ProgressBar1.TextAlignmentVert = System.Drawing.StringAlignment.Center
        Me.ProgressBar1.TextPlacement = Library.Controls.ProgressBar.eTextPlacement.OverBar
        Me.ProgressBar1.TextRotate = Library.Controls.ProgressBar.eRotateText.None
        Me.ProgressBar1.TextShow = Library.Controls.ProgressBar.eTextShow.TextOnly
        Me.ProgressBar1.Value = 1
        '
        'Download
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 83)
        Me.ControlBox = False
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.info)
        Me.Controls.Add(Me.cancelButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Download"
        Me.ShowIcon = False
        Me.Text = "Download"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DownloadWorker As System.ComponentModel.BackgroundWorker
    Friend Shadows WithEvents cancelButton As System.Windows.Forms.Button
    Friend WithEvents info As System.Windows.Forms.Label
    Friend WithEvents status As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As Library.Controls.ProgressBar
End Class
