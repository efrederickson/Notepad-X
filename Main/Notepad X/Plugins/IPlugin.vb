Public Interface IPlugin
    ''' <summary>
    ''' The Plugin Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Version As String
    ''' <summary>
    ''' The Plugin Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Name As String
    ''' <summary>
    ''' The Plugin Description
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Description As String
    ''' <summary>
    ''' The Plugin Creator/Author
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Author As String
    ''' <summary>
    ''' The http(s):// file location of the DLL for updates.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DownloadURL As String
    ''' <summary>
    ''' The OriginalFileName of the Plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property OriginalFileName As String
    ''' <summary>
    ''' Determines whether there is a 'help' page for this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property HasHelpPage As Boolean
    ''' <summary>
    ''' The Help TabPage for this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property HelpPage As TabPage

    ''' <summary>
    ''' Determines whether there is an 'options' page for this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property HasOptionsPage As Boolean
    ''' <summary>
    ''' The Options TabPage for this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property OptionsPage As TabPage

    ''' <summary>
    ''' Determines whether there is an 'about' page for this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property HasAboutPage As Boolean
    ''' <summary>
    ''' The About TabPage for this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property AboutPage As TabPage

    ''' <summary>
    ''' The control for this plugin.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ToolStripItem As ToolStripMenuItem

    ''' <summary>
    ''' The Path for the ToolStripMenuItem
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MenuItemPath As String

    ''' <summary>
    ''' Creates the plugin.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Initialize()
    ''' <summary>
    ''' Removes the plugin from existence.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Dispose()
End Interface
