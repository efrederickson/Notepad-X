''' <summary>
''' Data Class for Available Plugin.  Holds and instance of the loaded Plugin, as well as the Plugins Assembly Path
''' </summary>
Public Class AvailablePlugin
    ''' <summary>
    ''' The running Instance of this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Instance() As IPlugin = Nothing

    ''' <summary>
    ''' The filename of this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssemblyPath() As String = ""

End Class
