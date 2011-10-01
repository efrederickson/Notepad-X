Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.ComponentModel

Namespace Controls
    ''' <summary>
    ''' TaskbarNotifier allows to display MSN style/Skinned instant messaging popups
    ''' </summary>1
    <ToolboxItem(True)> _
    Public Class TaskbarNotifier
        Inherits System.Windows.Forms.Form
#Region "TaskbarNotifier Protected Members"
        Protected BackgroundBitmap As Bitmap = Nothing
        Protected CloseBitmap As Bitmap = Nothing
        Protected CloseBitmapLocation As Point
        Protected CloseBitmapSize As Size
        Protected RealTitleRectangle As Rectangle
        Protected RealContentRectangle As Rectangle
        Protected WorkAreaRectangle As Rectangle
        Protected timer As New Timer()
        Protected m_taskbarState As TaskbarStates = TaskbarStates.hidden
        Protected m_titleText As String
        Protected m_contentText As String
        Protected m_normalTitleColor As Color = Color.FromArgb(255, 0, 0)
        Protected m_hoverTitleColor As Color = Color.FromArgb(255, 0, 0)
        Protected m_normalContentColor As Color = Color.FromArgb(0, 0, 0)
        Protected m_hoverContentColor As Color = Color.FromArgb(0, 0, &H66)
        Protected m_normalTitleFont As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)
        Protected m_hoverTitleFont As New Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel)
        Protected m_normalContentFont As New Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel)
        Protected m_hoverContentFont As New Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel)
        Protected nShowEvents As Integer
        Protected nHideEvents As Integer
        Protected nVisibleEvents As Integer
        Protected nIncrementShow As Integer
        Protected nIncrementHide As Integer
        Protected bIsMouseOverPopup As Boolean = False
        Protected bIsMouseOverClose As Boolean = False
        Protected bIsMouseOverContent As Boolean = False
        Protected bIsMouseOverTitle As Boolean = False
        Protected bIsMouseDown As Boolean = False
        Protected bKeepVisibleOnMouseOver As Boolean = True
        ' Added Rev 002
        Protected bReShowOnMouseOver As Boolean = False
        ' Added Rev 002
#End Region
#Region "TaskbarNotifier Public Members"
        Public TitleRectangle As Rectangle
        Public ContentRectangle As Rectangle
        Public TitleClickable As Boolean = False
        Public ContentClickable As Boolean = True
        Public CloseClickable As Boolean = True
        Public EnableSelectionRectangle As Boolean = True
        Public Event CloseClick As EventHandler '= Nothing
        Public Event TitleClick As EventHandler '= Nothing
        Public Event ContentClick As EventHandler '= Nothing
#End Region

#Region "TaskbarNotifier Enums"
        ''' <summary>
        ''' List of the different popup animation status
        ''' </summary>
        Public Enum TaskbarStates
            hidden = 0
            appearing = 1
            visible = 2
            disappearing = 3
        End Enum
#End Region

#Region "TaskbarNotifier Constructor"
        ''' <summary>
        ''' The Constructor for TaskbarNotifier
        ''' </summary>
        Public Sub New()
            ' Window Style
            FormBorderStyle = FormBorderStyle.None
            WindowState = FormWindowState.Minimized
            MyBase.Show()
            MyBase.Hide()
            WindowState = FormWindowState.Normal
            ShowInTaskbar = False
            TopMost = True
            MaximizeBox = False
            MinimizeBox = False
            ControlBox = False

            timer.Enabled = True
            AddHandler timer.Tick, New EventHandler(AddressOf OnTimer)
        End Sub
#End Region

#Region "TaskbarNotifier Properties"
        ''' <summary>
        ''' Get the current TaskbarState (hidden, showing, visible, hiding)
        ''' </summary>
        Public ReadOnly Property TaskbarState() As TaskbarStates
            Get
                Return m_taskbarState
            End Get
        End Property

        ''' <summary>
        ''' Get/Set the popup Title Text
        ''' </summary>
        Public Property TitleText() As String
            Get
                Return m_titleText
            End Get
            Set(ByVal value As String)
                m_titleText = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the popup Content Text
        ''' </summary>
        Public Property ContentText() As String
            Get
                Return m_contentText
            End Get
            Set(ByVal value As String)
                m_contentText = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the Normal Title Color
        ''' </summary>
        Public Property NormalTitleColor() As Color
            Get
                Return m_normalTitleColor
            End Get
            Set(ByVal value As Color)
                m_normalTitleColor = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the Hover Title Color
        ''' </summary>
        Public Property HoverTitleColor() As Color
            Get
                Return m_hoverTitleColor
            End Get
            Set(ByVal value As Color)
                m_hoverTitleColor = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the Normal Content Color
        ''' </summary>
        Public Property NormalContentColor() As Color
            Get
                Return m_normalContentColor
            End Get
            Set(ByVal value As Color)
                m_normalContentColor = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the Hover Content Color
        ''' </summary>
        Public Property HoverContentColor() As Color
            Get
                Return m_hoverContentColor
            End Get
            Set(ByVal value As Color)
                m_hoverContentColor = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the Normal Title Font
        ''' </summary>
        Public Property NormalTitleFont() As Font
            Get
                Return m_normalTitleFont
            End Get
            Set(ByVal value As Font)
                m_normalTitleFont = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the Hover Title Font
        ''' </summary>
        Public Property HoverTitleFont() As Font
            Get
                Return m_hoverTitleFont
            End Get
            Set(ByVal value As Font)
                m_hoverTitleFont = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the Normal Content Font
        ''' </summary>
        Public Property NormalContentFont() As Font
            Get
                Return m_normalContentFont
            End Get
            Set(ByVal value As Font)
                m_normalContentFont = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get/Set the Hover Content Font
        ''' </summary>
        Public Property HoverContentFont() As Font
            Get
                Return m_hoverContentFont
            End Get
            Set(ByVal value As Font)
                m_hoverContentFont = value
                Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Indicates if the popup should remain visible when the mouse pointer is over it.
        ''' Added Rev 002
        ''' </summary>
        Public Property KeepVisibleOnMousOver() As Boolean
            Get
                Return bKeepVisibleOnMouseOver
            End Get
            Set(ByVal value As Boolean)
                bKeepVisibleOnMouseOver = value
            End Set
        End Property

        ''' <summary>
        ''' Indicates if the popup should appear again when mouse moves over it while it's disappearing.
        ''' Added Rev 002
        ''' </summary>
        Public Property ReShowOnMouseOver() As Boolean
            Get
                Return bReShowOnMouseOver
            End Get
            Set(ByVal value As Boolean)
                bReShowOnMouseOver = value
            End Set
        End Property

#End Region

#Region "TaskbarNotifier Public Methods"
        <DllImport("user32.dll")> _
        Private Shared Function ShowWindow(ByVal hWnd As IntPtr, ByVal nCmdShow As Int32) As [Boolean]
        End Function
        ''' <summary>
        ''' Displays the popup for a certain amount of time
        ''' </summary>
        ''' <param name="strTitle">The string which will be shown as the title of the popup</param>
        ''' <param name="strContent">The string which will be shown as the content of the popup</param>
        ''' <param name="nTimeToShow">Duration of the showing animation (in milliseconds)</param>
        ''' <param name="nTimeToStay">Duration of the visible state before collapsing (in milliseconds)</param>
        ''' <param name="nTimeToHide">Duration of the hiding animation (in milliseconds)</param>
        ''' <returns>Nothing</returns>
        Public Overloads Sub Show(ByVal strTitle As String, ByVal strContent As String, ByVal nTimeToShow As Integer, ByVal nTimeToStay As Integer, ByVal nTimeToHide As Integer)
            WorkAreaRectangle = Screen.GetWorkingArea(WorkAreaRectangle)
            m_titleText = strTitle
            m_contentText = strContent
            nVisibleEvents = nTimeToStay
            CalculateMouseRectangles()

            ' We calculate the pixel increment and the timer value for the showing animation
            Dim nEvents As Integer
            If nTimeToShow > 10 Then
                nEvents = Math.Min((nTimeToShow \ 10), BackgroundBitmap.Height)
                nShowEvents = nTimeToShow \ nEvents
                nIncrementShow = BackgroundBitmap.Height \ nEvents
            Else
                nShowEvents = 10
                nIncrementShow = BackgroundBitmap.Height
            End If

            ' We calculate the pixel increment and the timer value for the hiding animation
            If nTimeToHide > 10 Then
                nEvents = Math.Min((nTimeToHide \ 10), BackgroundBitmap.Height)
                nHideEvents = nTimeToHide \ nEvents
                nIncrementHide = BackgroundBitmap.Height \ nEvents
            Else
                nHideEvents = 10
                nIncrementHide = BackgroundBitmap.Height
            End If

            Select Case m_taskbarState
                Case TaskbarStates.hidden
                    m_taskbarState = TaskbarStates.appearing
                    SetBounds(WorkAreaRectangle.Right - BackgroundBitmap.Width - 17, WorkAreaRectangle.Bottom - 1, BackgroundBitmap.Width, 0)
                    timer.Interval = nShowEvents
                    timer.Start()
                    ' We Show the popup without stealing focus
                    ShowWindow(Me.Handle, 4)
                    Exit Select

                Case TaskbarStates.appearing
                    Refresh()
                    Exit Select

                Case TaskbarStates.visible
                    timer.[Stop]()
                    timer.Interval = nVisibleEvents
                    timer.Start()
                    Refresh()
                    Exit Select

                Case TaskbarStates.disappearing
                    timer.[Stop]()
                    m_taskbarState = TaskbarStates.visible
                    SetBounds(WorkAreaRectangle.Right - BackgroundBitmap.Width - 17, WorkAreaRectangle.Bottom - BackgroundBitmap.Height - 1, BackgroundBitmap.Width, BackgroundBitmap.Height)
                    timer.Interval = nVisibleEvents
                    timer.Start()
                    Refresh()
                    Exit Select
            End Select
        End Sub

        ''' <summary>
        ''' Hides the popup
        ''' </summary>
        ''' <returns>Nothing</returns>
        Public Shadows Sub Hide()
            If m_taskbarState <> TaskbarStates.hidden Then
                timer.[Stop]()
                m_taskbarState = TaskbarStates.hidden
                MyBase.Hide()
            End If
        End Sub

        ''' <summary>
        ''' Sets the background bitmap and its transparency color
        ''' </summary>
        ''' <param name="strFilename">Path of the Background Bitmap on the disk</param>
        ''' <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
        ''' <returns>Nothing</returns>
        Public Sub SetBackgroundBitmap(ByVal strFilename As String, ByVal transparencyColor As Color)
            BackgroundBitmap = New Bitmap(strFilename)
            Width = BackgroundBitmap.Width
            Height = BackgroundBitmap.Height
            Region = BitmapToRegion(BackgroundBitmap, transparencyColor)
        End Sub

        ''' <summary>
        ''' Sets the background bitmap and its transparency color
        ''' </summary>
        ''' <param name="image">Image/Bitmap object which represents the Background Bitmap</param>
        ''' <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
        ''' <returns>Nothing</returns>
        Public Sub SetBackgroundBitmap(ByVal image As Image, ByVal transparencyColor As Color)
            BackgroundBitmap = New Bitmap(image)
            Width = BackgroundBitmap.Width
            Height = BackgroundBitmap.Height
            Region = BitmapToRegion(BackgroundBitmap, transparencyColor)
        End Sub

        ''' <summary>
        ''' Sets the 3-State Close Button bitmap, its transparency color and its coordinates
        ''' </summary>
        ''' <param name="strFilename">Path of the 3-state Close button Bitmap on the disk (width must a multiple of 3)</param>
        ''' <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
        ''' <param name="position">Location of the close button on the popup</param>
        ''' <returns>Nothing</returns>
        Public Sub SetCloseBitmap(ByVal strFilename As String, ByVal transparencyColor As Color, ByVal position As Point)
            CloseBitmap = New Bitmap(strFilename)
            CloseBitmap.MakeTransparent(transparencyColor)
            CloseBitmapSize = New Size(CloseBitmap.Width \ 3, CloseBitmap.Height)
            CloseBitmapLocation = position
        End Sub

        ''' <summary>
        ''' Sets the 3-State Close Button bitmap, its transparency color and its coordinates
        ''' </summary>
        ''' <param name="image">Image/Bitmap object which represents the 3-state Close button Bitmap (width must be a multiple of 3)</param>
        ''' <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
        ''' /// <param name="position">Location of the close button on the popup</param>
        Public Sub SetCloseBitmap(ByVal image As Image, ByVal transparencyColor As Color, ByVal position As Point)
            CloseBitmap = New Bitmap(image)
            CloseBitmap.MakeTransparent(transparencyColor)
            CloseBitmapSize = New Size(CloseBitmap.Width \ 3, CloseBitmap.Height)
            CloseBitmapLocation = position
        End Sub
#End Region

#Region "TaskbarNotifier Protected Methods"
        Protected Sub DrawCloseButton(ByVal grfx As Graphics)
            If CloseBitmap IsNot Nothing Then
                Dim rectDest As New Rectangle(CloseBitmapLocation, CloseBitmapSize)
                Dim rectSrc As Rectangle

                If bIsMouseOverClose Then
                    If bIsMouseDown Then
                        rectSrc = New Rectangle(New Point(CloseBitmapSize.Width * 2, 0), CloseBitmapSize)
                    Else
                        rectSrc = New Rectangle(New Point(CloseBitmapSize.Width, 0), CloseBitmapSize)
                    End If
                Else
                    rectSrc = New Rectangle(New Point(0, 0), CloseBitmapSize)
                End If


                grfx.DrawImage(CloseBitmap, rectDest, rectSrc, GraphicsUnit.Pixel)
            End If
        End Sub

        Protected Sub DrawText(ByVal grfx As Graphics)
            If m_titleText IsNot Nothing AndAlso m_titleText.Length <> 0 Then
                Dim sf As New StringFormat()
                sf.Alignment = StringAlignment.Near
                sf.LineAlignment = StringAlignment.Center
                sf.FormatFlags = StringFormatFlags.NoWrap
                sf.Trimming = StringTrimming.EllipsisCharacter
                ' Added Rev 002
                If bIsMouseOverTitle Then
                    grfx.DrawString(m_titleText, m_hoverTitleFont, New SolidBrush(m_hoverTitleColor), TitleRectangle, sf)
                Else
                    grfx.DrawString(m_titleText, m_normalTitleFont, New SolidBrush(m_normalTitleColor), TitleRectangle, sf)
                End If
            End If

            If m_contentText IsNot Nothing AndAlso m_contentText.Length <> 0 Then
                Dim sf As New StringFormat()
                sf.Alignment = StringAlignment.Center
                sf.LineAlignment = StringAlignment.Center
                sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces
                sf.Trimming = StringTrimming.Word
                ' Added Rev 002
                If bIsMouseOverContent Then
                    grfx.DrawString(m_contentText, m_hoverContentFont, New SolidBrush(m_hoverContentColor), ContentRectangle, sf)
                    If EnableSelectionRectangle Then
                        ControlPaint.DrawBorder3D(grfx, RealContentRectangle, Border3DStyle.Etched, Border3DSide.Top Or Border3DSide.Bottom Or Border3DSide.Left Or Border3DSide.Right)

                    End If
                Else
                    grfx.DrawString(m_contentText, m_normalContentFont, New SolidBrush(m_normalContentColor), ContentRectangle, sf)
                End If
            End If
        End Sub

        Protected Sub CalculateMouseRectangles()
            Dim grfx As Graphics = CreateGraphics()
            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces
            Dim sizefTitle As SizeF = grfx.MeasureString(m_titleText, m_hoverTitleFont, TitleRectangle.Width, sf)
            Dim sizefContent As SizeF = grfx.MeasureString(m_contentText, m_hoverContentFont, ContentRectangle.Width, sf)
            grfx.Dispose()

            ' Added Rev 002
            'We should check if the title size really fits inside the pre-defined title rectangle
            If sizefTitle.Height > TitleRectangle.Height Then
                RealTitleRectangle = New Rectangle(TitleRectangle.Left, TitleRectangle.Top, TitleRectangle.Width, TitleRectangle.Height)
            Else
                RealTitleRectangle = New Rectangle(TitleRectangle.Left, TitleRectangle.Top, CInt(Math.Truncate(sizefTitle.Width)), CInt(Math.Truncate(sizefTitle.Height)))
            End If
            RealTitleRectangle.Inflate(0, 2)

            ' Added Rev 002
            'We should check if the Content size really fits inside the pre-defined Content rectangle
            If sizefContent.Height > ContentRectangle.Height Then
                RealContentRectangle = New Rectangle((ContentRectangle.Width - CInt(Math.Truncate(sizefContent.Width))) \ 2 + ContentRectangle.Left, ContentRectangle.Top, CInt(Math.Truncate(sizefContent.Width)), ContentRectangle.Height)
            Else
                RealContentRectangle = New Rectangle((ContentRectangle.Width - CInt(Math.Truncate(sizefContent.Width))) \ 2 + ContentRectangle.Left, (ContentRectangle.Height - CInt(Math.Truncate(sizefContent.Height))) \ 2 + ContentRectangle.Top, CInt(Math.Truncate(sizefContent.Width)), CInt(Math.Truncate(sizefContent.Height)))
            End If
            RealContentRectangle.Inflate(0, 2)
        End Sub

        Protected Function BitmapToRegion(ByVal bitmap As Bitmap, ByVal transparencyColor As Color) As Region
            If bitmap Is Nothing Then
                Throw New ArgumentNullException("Bitmap", "Bitmap cannot be null!")
            End If

            Dim height As Integer = bitmap.Height
            Dim width As Integer = bitmap.Width

            Dim path As New GraphicsPath()

            For j As Integer = 0 To height - 1
                For i As Integer = 0 To width - 1
                    If bitmap.GetPixel(i, j) = transparencyColor Then
                        Continue For
                    End If

                    Dim x0 As Integer = i

                    While (i < width) AndAlso (bitmap.GetPixel(i, j) <> transparencyColor)
                        i += 1
                    End While

                    path.AddRectangle(New Rectangle(x0, j, i - x0, 1))
                Next
            Next

            Dim region As New Region(path)
            path.Dispose()
            Return region
        End Function
#End Region

#Region "TaskbarNotifier Events Overrides"
        Protected Sub OnTimer(ByVal obj As [Object], ByVal ea As EventArgs)
            Select Case m_taskbarState
                Case TaskbarStates.appearing
                    If Height < BackgroundBitmap.Height Then
                        SetBounds(Left, Top - nIncrementShow, Width, Height + nIncrementShow)
                    Else
                        timer.[Stop]()
                        Height = BackgroundBitmap.Height
                        timer.Interval = nVisibleEvents
                        m_taskbarState = TaskbarStates.visible
                        timer.Start()
                    End If
                    Exit Select

                Case TaskbarStates.visible
                    timer.[Stop]()
                    timer.Interval = nHideEvents
                    ' Added Rev 002
                    If (bKeepVisibleOnMouseOver AndAlso Not bIsMouseOverPopup) OrElse (Not bKeepVisibleOnMouseOver) Then
                        m_taskbarState = TaskbarStates.disappearing
                    End If
                    'taskbarState = TaskbarStates.disappearing;		// Rev 002
                    timer.Start()
                    Exit Select

                Case TaskbarStates.disappearing
                    ' Added Rev 002
                    If bReShowOnMouseOver AndAlso bIsMouseOverPopup Then
                        m_taskbarState = TaskbarStates.appearing
                    Else
                        If Top < WorkAreaRectangle.Bottom Then
                            SetBounds(Left, Top + nIncrementHide, Width, Height - nIncrementHide)
                        Else
                            Hide()
                        End If
                    End If
                    Exit Select
            End Select

        End Sub

        Protected Overrides Sub OnMouseEnter(ByVal ea As EventArgs)
            MyBase.OnMouseEnter(ea)
            bIsMouseOverPopup = True
            Refresh()
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal ea As EventArgs)
            MyBase.OnMouseLeave(ea)
            bIsMouseOverPopup = False
            bIsMouseOverClose = False
            bIsMouseOverTitle = False
            bIsMouseOverContent = False
            Refresh()
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
            MyBase.OnMouseMove(mea)

            Dim bContentModified As Boolean = False

            If (mea.X > CloseBitmapLocation.X) AndAlso (mea.X < CloseBitmapLocation.X + CloseBitmapSize.Width) AndAlso (mea.Y > CloseBitmapLocation.Y) AndAlso (mea.Y < CloseBitmapLocation.Y + CloseBitmapSize.Height) AndAlso CloseClickable Then
                If Not bIsMouseOverClose Then
                    bIsMouseOverClose = True
                    bIsMouseOverTitle = False
                    bIsMouseOverContent = False
                    Cursor = Cursors.Hand
                    bContentModified = True
                End If
            ElseIf RealContentRectangle.Contains(New Point(mea.X, mea.Y)) AndAlso ContentClickable Then
                If Not bIsMouseOverContent Then
                    bIsMouseOverClose = False
                    bIsMouseOverTitle = False
                    bIsMouseOverContent = True
                    Cursor = Cursors.Hand
                    bContentModified = True
                End If
            ElseIf RealTitleRectangle.Contains(New Point(mea.X, mea.Y)) AndAlso TitleClickable Then
                If Not bIsMouseOverTitle Then
                    bIsMouseOverClose = False
                    bIsMouseOverTitle = True
                    bIsMouseOverContent = False
                    Cursor = Cursors.Hand
                    bContentModified = True
                End If
            Else
                If bIsMouseOverClose OrElse bIsMouseOverTitle OrElse bIsMouseOverContent Then
                    bContentModified = True
                End If

                bIsMouseOverClose = False
                bIsMouseOverTitle = False
                bIsMouseOverContent = False
                Cursor = Cursors.[Default]
            End If

            If bContentModified Then
                Refresh()
            End If
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
            MyBase.OnMouseDown(mea)
            bIsMouseDown = True

            If bIsMouseOverClose Then
                Refresh()
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal mea As MouseEventArgs)
            MyBase.OnMouseUp(mea)
            bIsMouseDown = False

            If bIsMouseOverClose Then
                Hide()

                RaiseEvent CloseClick(Me, New EventArgs())
            ElseIf bIsMouseOverTitle Then
                RaiseEvent TitleClick(Me, New EventArgs())
            ElseIf bIsMouseOverContent Then
                RaiseEvent ContentClick(Me, New EventArgs())
            End If
        End Sub

        Protected Overrides Sub OnPaintBackground(ByVal pea As PaintEventArgs)
            Dim grfx As Graphics = pea.Graphics
            grfx.PageUnit = GraphicsUnit.Pixel

            Dim offScreenGraphics As Graphics
            Dim offscreenBitmap As Bitmap

            offscreenBitmap = New Bitmap(BackgroundBitmap.Width, BackgroundBitmap.Height)
            offScreenGraphics = Graphics.FromImage(offscreenBitmap)

            If BackgroundBitmap IsNot Nothing Then
                offScreenGraphics.DrawImage(BackgroundBitmap, 0, 0, BackgroundBitmap.Width, BackgroundBitmap.Height)
            End If

            DrawCloseButton(offScreenGraphics)
            DrawText(offScreenGraphics)

            grfx.DrawImage(offscreenBitmap, 0, 0)
        End Sub
#End Region
    End Class
End Namespace