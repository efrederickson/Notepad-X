Module Module1
    Public PluginInterface As String = "SnapOn.IPlugin"
    Public PluginManager As New PluginService
    Public Files As New List(Of String)
    Public startpath As String = Application.StartupPath
End Module
