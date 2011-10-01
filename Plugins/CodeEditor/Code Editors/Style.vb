Imports System.ComponentModel
Imports System.Drawing
Imports Alsing.SourceCode

Public Class Style
    Inherits Component

    Private _components As Container
    Public Name As String = ""
    Public TextStyle As TextStyle

    Public Sub New(ByVal container As IContainer)
        container.Add(Me)
        InitializeComponent()
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    <Category("Color")>
    Public Property ForeColor As Color
        Get
            Return TextStyle.ForeColor
        End Get
        Set(ByVal value As Color)
            TextStyle.ForeColor = value
        End Set
    End Property

    <Category("Color")>
    Public Property BackColor As Color
        Get
            Return TextStyle.BackColor
        End Get
        Set(ByVal value As Color)
            TextStyle.BackColor = value
        End Set
    End Property

    <Category("Font")>
    Public Property FontBold As Boolean
        Get
            Return TextStyle.Bold
        End Get

        Set(ByVal value As Boolean)
            TextStyle.Bold = value
        End Set
    End Property

    <Category("Font")>
    Public Property FontItalic As Boolean
        Get
            Return TextStyle.Italic
        End Get
        Set(ByVal value As Boolean)
            TextStyle.Italic = value
        End Set
    End Property

    <Category("Font")>
    Public Property FontUnderline As Boolean
        Get
            Return TextStyle.Underline
        End Get
        Set(ByVal value As Boolean)
            TextStyle.Underline = value
        End Set
    End Property

    Public ReadOnly Property Components As Container
        Get
            Return _components
        End Get
    End Property


    Public Overrides Function ToString() As String
        Return Name
    End Function

#Region " Component Designer generated code "

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        _components = New System.ComponentModel.Container()
    End Sub

#End Region
End Class