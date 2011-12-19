Imports System
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms

Public Class PluginService
    'Inherits IPluginHost
    Public Parent As MDIParent
    'Keeps previously "PluginInfo"d plugins for faster access
    Private Hashes As New Hashtable() 'String()
    Public PluginINI As INIFile

    Public Sub New(ByVal parent As MDIParent, ByVal INIFileLocation As String)
        Me.Parent = parent
        LoadPluginINI(INIFileLocation)
    End Sub

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
    ''' Searches the Path for Plugins in .DLL format
    ''' </summary>
    ''' <param name="Path">Directory to search for Plugins in</param>
    Public Sub FindPlugins(ByVal Path As String)
        'First empty the collection, we're reloading them all
        ' AvailablePlugins.Clear()

        'Go through all the files in the plugin directory
        For Each fileOn As String In Directory.GetFiles(Path)
            Dim File As New FileInfo(fileOn)

            'Preliminary check, must be .dll
            If (File.Extension.ToLower.Equals(".dll")) Then
                'Add the 'plugin'
                AddPlugin(fileOn)
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
                            ret(1) = newPlugin.Instance.Author
                            ret(2) = newPlugin.Instance.Version
                            ret(3) = newPlugin.Instance.Description
                            ret(4) = newPlugin.Instance.DownloadURL
                            ret(5) = newPlugin.Instance.OriginalFileName
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

    ''' <summary>
    ''' get the plugin info from the Plugins.ini file and return as an array: Name, Author, Version, Description, DownloadURL, OriginalFilename
    ''' </summary>
    ''' <param name="filename">the filename</param>
    ''' <returns>String array (Name,Author,Version,Description, DownloadURL)</returns>
    ''' <remarks></remarks>
    Public Function GetINIPluginInfo(ByVal filename As String) As String()
        If PluginINI.GetSection(filename) IsNot Nothing Then
            Dim r(0 To 5) As String
            r(0) = PluginINI.GetSection(filename).GetKey("Name").Value
            r(1) = PluginINI.GetSection(filename).GetKey("Author").Value
            r(2) = PluginINI.GetSection(filename).GetKey("Version").Value
            r(3) = PluginINI.GetSection(filename).GetKey("Description").Value
            r(4) = PluginINI.GetSection(filename).GetKey("DownloadURL").Value
            r(5) = PluginINI.GetSection(filename).GetKey("OriginalFilename").Value
            Return r
        Else
            Return Nothing
        End If
    End Function

    Public Sub AddPlugin(ByVal FileName As String)
        'Create a new assembly from the plugin file we're adding..
        Log.WriteLine("PluginManager: Loading " & FileName)
        Dim pluginAssembly As Assembly = Assembly.LoadFrom(FileName)
        'Next we'll loop through all the Types found in the assembly
        If pluginAssembly IsNot Nothing Then
            Dim itemcount As Integer = 0
            Dim pluginsfound As Integer = 0
            For Each pluginType As Type In pluginAssembly.GetTypes()
                If pluginType.IsPublic Then 'Only look at public types
                    If Not pluginType.IsAbstract Then  'Only look at non-abstract types
                        'Log.WriteLine(String.Format("PluginManager: Checking Type '{0}' from {1}", pluginType.Name, FileName))
                        itemcount += 1
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
                            Try
                                newPlugin.Instance = CType(Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString())), IPlugin)
                                Log.WriteLine(String.Format("PluginManager: Type Match: '{0}' (from {2}) Implements '{1}'. Creating Plugin...", pluginType.Name, PluginInterface, FileName))
                                pluginsfound += 1
                                'Add the new plugin to our collection here
                                Me.AvailablePlugins.Add(newPlugin)
                                'Call the initialization sub of the plugin
                                newPlugin.Instance.Initialize()
                                GetMenuItemFromString(newPlugin.Instance.MenuItemPath, newPlugin.Instance.ToolStripItem)
                                'cleanup a bit
                                newPlugin = Nothing
                            Catch ex As Exception
                                newPlugin = Nothing
                                Log.WriteLine("Error: " & ex.ToString)
                                MsgBox(String.Format("Error Loading Plugin ""{0}"": {1}", FileName, ex))
                                Try
                                    IO.File.Delete(FileName)
                                Catch ex2 As Exception
                                End Try
                            End Try
                        Else
                        End If
                        typeInterface = Nothing ' Clean up
                    End If
                End If
            Next
            Log.WriteLine(String.Format("Scanned {0} Items from {1}, {2} Plugins found.", itemcount, FileName, pluginsfound))
        End If
        If pluginAssembly Is Nothing Then
            Throw New Exception("Empty Assembly!")
        End If
        pluginAssembly = Nothing 'more cleanup
    End Sub

    ''' <summary>
    ''' Loads or Creates and Loads an INI file containing Plugin Data
    ''' </summary>
    ''' <param name="IniFileLocation">The location of the Plugin INI</param>
    ''' <remarks></remarks>
    Public Sub LoadPluginINI(ByVal INIFileLocation As String)
        If IO.File.Exists(INIFileLocation) Then
            PluginINI = New INIFile
            PluginINI.Load(INIFileLocation)
        Else
            Dim inifileW As New StreamWriter(INIFileLocation)
            With inifileW
                .WriteLine("[Plugins]")
                .Close()
            End With
            PluginINI = New INIFile()
            PluginINI.Load(INIFileLocation)
        End If
    End Sub

    Public Sub SavePluginINI(ByVal INIFileLocation As String)
        PluginINI.Save(INIFileLocation)
    End Sub

    Sub GetMenuItemFromString(ByVal path As String, ByRef item As ToolStripMenuItem)
        If path.ToLower.StartsWith("new/") Then
            Dim toolstrip As ToolStripMenuItem = Nothing
            Dim path2 As String = path.Substring("new/".Length)
            For Each item2 As ToolStripMenuItem In Parent.MenuStrip.Items
                If item2.Text = path2 Then
                    toolstrip = item2
                End If
            Next
            Dim tmpitem As New ToolStripMenuItem("[plugin helper]") With {.Name = "pluginhelper"}
            If toolstrip Is Nothing Then
                toolstrip = New ToolStripMenuItem(path2, Nothing, New ToolStripMenuItem() {tmpitem})
            Else
                toolstrip.DropDownItems("pluginhelper").GetCurrentParent.Items.Add(item)
                Return
            End If
            tmpitem.Visible = False
            Parent.MenuStrip.Items.Insert(Parent.MenuStrip.Items.IndexOfKey("INSERT"), toolstrip)
            'toolstrip.Owner = Parent.MenuStrip
            tmpitem.GetCurrentParent.Items.Add(item)
            Return
        ElseIf path.ToLower.StartsWith("file") Then
            Parent.NewToolStripMenuItem.GetCurrentParent.Items.Add(item)
            Return
        ElseIf path.ToLower.StartsWith("tools") Then
            Parent.OptionsToolStripMenuItem.GetCurrentParent.Items.Add(item)
            Return
        ElseIf path.ToLower.StartsWith("help") Then
            Parent.HelpToolStripMenuItem1.GetCurrentParent().Items.Add(item)
            Return
        End If
        Parent.PluginMenuToolStripMenuItem.GetCurrentParent().Items.Add(item)
        Return
    End Sub
End Class
