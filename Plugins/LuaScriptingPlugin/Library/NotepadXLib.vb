Imports System.Windows.Forms

Namespace Library
    ' The class for Notepad X
    Public NotInheritable Class NotepadXLib
        Private Sub New()
        End Sub
        Shared currentModule As LuaTable

        Public Shared Sub RegisterModule(ByVal enviroment As LuaTable)
            Dim [module] As New LuaTable()
            RegisterFunctions([module])
            enviroment.SetNameValue("NotepadX", [module])
            [module].SetNameValue("_G", enviroment)
            currentModule = [module]
        End Sub

        Public Shared Sub RegisterFunctions(ByVal [module] As LuaTable)
            [module].SetNameValue("MainForm", New LuaUserdata(NotepadX.Main.MDIParent1))
            [module].Register("RegisterMenuItem", Function(values() As LuaValue) As LuaValue
        Dim t As ToolStripMenuItem = DirectCast(values(0).Value, ToolStripMenuItem)
        'NotepadX.Main.MDIParent1.MenuStrip.Items.Add(t)
                                                      NotepadX.Main.PluginManager.GetMenuItemFromString(values(1).Value, t)
                                                      Return LuaNil.Nil
                                                  End Function)
            Dim metaTable As New LuaTable()
            metaTable.Register("__index", Function(values() As LuaValue) As LuaValue
        Dim t As ToolStripMenuItem = DirectCast(values(0).Value, ToolStripMenuItem)
                                              NotepadX.Main.MDIParent1.MenuStrip.Items.Insert(900, t)
                                              Return LuaNil.Nil
                                          End Function)
            [module].MetaTable = metaTable
        End Sub

    End Class
End Namespace