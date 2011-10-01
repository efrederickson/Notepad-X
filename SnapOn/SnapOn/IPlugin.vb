Imports System.Windows.Forms

Public Interface IPlugin
    ''' <summary>
    ''' The Snap-on Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Version As String
    ''' <summary>
    ''' The Snap-on Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Name As String
    ''' <summary>
    ''' The http(s):// file location of the Package ([SnapOn].pack file) for this Snap-On
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DownloadURL As String

    ''' <summary>
    ''' The Form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Form As Form
    ''' <summary>
    ''' Creates the snap-on form
    ''' </summary>
    ''' <remarks></remarks>
    Sub Initialize()
    ''' <summary>
    ''' closes the snap-on form
    ''' </summary>
    ''' <remarks></remarks>
    Sub Dispose()
End Interface
