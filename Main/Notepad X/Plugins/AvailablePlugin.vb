﻿
Public Class AvailablePlugins
    Inherits System.Collections.CollectionBase
    'A Simple class to hold some info about our Available Plugins

    ''' <summary>
    ''' Add a Plugin to the collection of Available plugins
    ''' </summary>
    ''' <param name="pluginToAdd">The Plugin to Add</param>
    Public Sub Add(ByVal pluginToAdd As AvailablePlugin)
        Me.List.Add(pluginToAdd)
    End Sub

    ''' <summary>
    ''' Gets the AssemblyPath from the plugin from the collection at id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function _Get(ByVal id As Integer) As String
        Return (CType(Me.List(id), AvailablePlugin)).AssemblyPath
    End Function

    ''' <summary>
    ''' Remove a Plugin to the collection of available plugins
    ''' </summary>
    ''' <param name="pluginToRemove">The Plugin to Remove</param>
    Public Sub Remove(ByVal pluginToRemove As AvailablePlugin)
        Me.List.Remove(pluginToRemove)
        pluginToRemove = Nothing
    End Sub

    ''' <summary>
    ''' Checks if pluginNameOrPath exists in the collection of available plugins
    ''' </summary>
    ''' <param name="pluginNameOrPath">The Name/Path of the plugin to find</param>
    ''' <returns>True if found, false if not found</returns>
    ''' <remarks></remarks>
    Public Function Exist(ByVal pluginNameOrPath As String) As Boolean
        For Each pluginOn As AvailablePlugin In Me.List
            Try
                If ((pluginOn.Instance.Name.ToLower.Equals(pluginNameOrPath.ToLower)) Or pluginOn.AssemblyPath.ToLower.Equals(pluginNameOrPath.ToLower)) Then
                    Return True
                End If
            Catch
            End Try
        Next
        Return False
    End Function

    ''' <summary>
    ''' Finds a plugin in the available Plugins
    ''' </summary>
    ''' <param name="pluginNameOrPath">The name or File path of the plugin to find</param>
    ''' <returns>Available Plugin, or nothing if the plugin is not found</returns>
    Public Function Find(ByVal pluginNameOrPath As String) As AvailablePlugin
        'Loop through all the plugins
        For Each pluginOn As AvailablePlugin In Me.List
            Try
                'Find the one with the matching name or filename
                If ((pluginOn.Instance.Name.Equals(pluginNameOrPath)) Or pluginOn.AssemblyPath.Equals(pluginNameOrPath)) Then
                    Return pluginOn
                End If
            Catch
            End Try
        Next
        Return Nothing
    End Function
End Class

''' <summary>
''' Data Class for Available Plugin.  Holds and instance of the loaded Plugin, as well as the Plugins Assembly Path
''' </summary>
Public Class AvailablePlugin
    'This is the actual AvailablePlugin object.. 
    'Holds an instance of the plugin to access
    'Also holds assembly path
    Private myInstance As IPlugin = Nothing
    Private myAssemblyPath As String = ""

    ''' <summary>
    ''' The running Instance of this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Instance As IPlugin
        Get
            Return myInstance
        End Get
        Set(ByVal value As IPlugin)
            myInstance = value
        End Set
    End Property

    ''' <summary>
    ''' The filename of this plugin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssemblyPath As String
        Get
            Return myAssemblyPath
        End Get
        Set(ByVal value As String)
            myAssemblyPath = value
        End Set
    End Property

End Class