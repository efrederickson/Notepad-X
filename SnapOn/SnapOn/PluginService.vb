Imports System.IO
Imports System.Reflection

Public Class PluginService
    'Inherits IPluginHost
    'Keeps previously "GetPluginInfo"d plugins for faster access
    Private Hashes As New Hashtable() 'String()

    ''' <summary>
    ''' A Collection of all Plugins Found and Loaded
    ''' </summary>
    Public Property AvailablePlugins() As AvailablePlugins = New AvailablePlugins()

    ''' <summary>
    ''' Searches the Application's Startup Directory for Plugins
    ''' </summary>
    Public Sub FindPlugins()
        FindPlugins(AppDomain.CurrentDomain.BaseDirectory)
    End Sub

    ''' <summary>
    ''' Searches the Path for Plugins in .DLL/.EXE format
    ''' </summary>
    ''' <param name="Path">Directory to search for Plugins in</param>
    Public Sub FindPlugins(ByVal Path As String)
        'First empty the collection, we're reloading them all
        AvailablePlugins.Clear()

        'Go through all the files in the plugin directory
        For Each fileOn As String In Directory.GetFiles(Path)
            Dim File As New FileInfo(fileOn)

            'Preliminary check, must be .dll
            If (File.Extension.Equals(".dll") Or File.Extension.Equals(".exe")) Then
                'Add the 'plugin'
                Me.AddPlugin(fileOn)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Unloads and Closes pluginNameOrPath
    ''' </summary>
    Public Sub ClosePlugin(ByVal pluginNameOrPath As String)
        Dim tmp As AvailablePlugin = Nothing
        For Each pluginOn As AvailablePlugin In AvailablePlugins
            If pluginOn.Instance.Name.ToLower.Equals(pluginNameOrPath.ToLower) Or pluginOn.AssemblyPath.ToLower.Equals(pluginNameOrPath.ToLower) Then
                pluginOn.Instance.Dispose()
                pluginOn.Instance = Nothing
                tmp = pluginOn
                Exit For
            End If
        Next
        If tmp IsNot Nothing Then
            remove(tmp)
        End If
    End Sub

    Private Sub remove(ByVal pl As AvailablePlugin)
        AvailablePlugins.Remove(pl)
        pl = Nothing
    End Sub

    ''' <summary>
    ''' Close all the plugins
    ''' </summary>
    Public Sub ClosePlugins()
        For Each pluginOn As AvailablePlugin In AvailablePlugins
            Try
                'Close all plugin instances
                'We call the plugins Dispose sub first incase it has to do 
                'Its own cleanup stuff
                pluginOn.Instance.Dispose()

                'After we give the plugin a chance to tidy up, get rid of it
                pluginOn.Instance = Nothing
            Catch ex As Exception
            End Try
        Next

        'Finally, clear our collection of available plugins
        AvailablePlugins.Clear()
    End Sub

    ''' <summary>
    ''' Reads a toolbar file and returns the properties as array (Name,Author,Version,Description,DownloadURL)
    ''' </summary>
    ''' <returns>String array (Name,Author,Version,Description, DownloadURL)</returns>
    ''' <param name="FileName">Filename of the plugin</param>
    Public Function GetPluginInfo(ByVal FileName As String, ByVal InterfaceToFind As String) As String()
        If Hashes.Contains(FileName) Then
            Return CType(Hashes(FileName), String())
        End If
        Dim ret As String() = Nothing
        Try
            Dim pluginAssembly As Assembly = Assembly.LoadFrom(FileName)
            For Each pluginType As Type In pluginAssembly.GetTypes()
                If pluginType.IsPublic Then
                    If Not pluginType.IsAbstract Then
                        Dim typeInterface As Type = pluginType.GetInterface(InterfaceToFind, True)
                        If (typeInterface <> Nothing) Then
                            Dim newPlugin As AvailablePlugin = New AvailablePlugin()
                            newPlugin.AssemblyPath = FileName
                            newPlugin.Instance = CType(Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString())), IPlugin)
                            ReDim ret(0 To 5)
                            ret(0) = newPlugin.Instance.Name
                            ret(1) = newPlugin.Instance.Version
                            ret(2) = newPlugin.Instance.DownloadURL
                            Hashes(FileName) = ret
                            newPlugin.Instance = Nothing
                            newPlugin = Nothing
                        End If
                        typeInterface = Nothing
                    End If
                End If
            Next
            pluginAssembly = Nothing
        Catch ex As Exception
            ret = Nothing
        End Try
        Return ret
    End Function

    Public Sub AddPlugin(ByVal FileName As String)
        'Create a new assembly from the plugin file we're adding..
        Dim pluginAssembly As Assembly = Assembly.LoadFrom(FileName)
        'Next we'll loop through all the Types found in the assembly
        If pluginAssembly IsNot Nothing Then
            For Each pluginType As Type In pluginAssembly.GetTypes()
                If pluginType.IsPublic Then 'Only look at public types
                    If Not pluginType.IsAbstract Then  'Only look at non-abstract types
                        'Gets a type object of the interface we need the plugins to match
                        Dim typeInterface As Type = pluginType.GetInterface(PluginInterface, True)

                        'Make sure the interface we want to use actually exists
                        If (typeInterface IsNot Nothing) Then
                            'Create a new available plugin since the type implements the IPlugin interface
                            Dim newPlugin As New AvailablePlugin()

                            'Set the filename where we found it
                            newPlugin.AssemblyPath = FileName

                            'Create a new instance and store the instance in the collection for later use
                            'We could change this later on to not load an instance.. we have 2 options
                            '1- Make one instance, and use it whenever we need it.. it's always there
                            '2- Don't make an instance, and instead make an instance whenever we use it, then close it
                            'For now we'll just make an instance of all the plugins
                            newPlugin.Instance = CType(Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString())), IPlugin)

                            'Add the new plugin to our collection here
                            Me.AvailablePlugins.Add(newPlugin)
                            'Call the initialization sub of the plugin
                            newPlugin.Instance.Initialize()
                            'cleanup a bit
                            newPlugin = Nothing
                        Else
                        End If
                        typeInterface = Nothing ' Clean			
                    End If
                End If
            Next
        End If
        If pluginAssembly Is Nothing Then
            Throw New Exception("Empty Assembly!")
        End If
        pluginAssembly = Nothing 'more cleanup
    End Sub
End Class
