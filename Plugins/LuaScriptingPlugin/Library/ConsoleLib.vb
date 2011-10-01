Namespace Library
    ' The class for this Lua script
    Public NotInheritable Class ConsoleLib
        Private Sub New()
        End Sub
        Private _sh As UInteger
        Shared currentModule As LuaTable

        Public Shared Sub RegisterModule(ByVal enviroment As LuaTable)
            Dim [module] As New LuaTable()
            RegisterFunctions([module])
            enviroment.SetNameValue("Console", [module])
            [module].SetNameValue("_G", enviroment)
            currentModule = [module]
        End Sub

        Public Shared Sub RegisterFunctions(ByVal [module] As LuaTable)
            [module].Register("Write", AddressOf Write)
            [module].Register("WriteLine", AddressOf WriteLine)
            '[module].Register("Read", AddressOf Read)
            '[module].Register("ReadLine", AddressOf ReadLine)
            [module].Register("Clear", Function(vals() As LuaValue) As LuaValue
                                           Console.Clear()
                                           Return Nothing
                                       End Function)
            Dim metaTable As New LuaTable()
            metaTable.Register("__index", AddressOf Write)
            [module].MetaTable = metaTable
        End Sub

        Public Shared Function Write(ByVal values As LuaValue()) As LuaValue
            Dim data As LuaString = TryCast(values(0), LuaString)
            Console.Write(data)
            Return Nothing
        End Function

        Public Shared Function WriteLine(ByVal values() As LuaValue) As LuaValue
            Dim data As LuaString = TryCast(values(0), LuaString)
            Console.WriteLine(data)
            Return Nothing
        End Function

        Public Shared Function Read(ByVal values As LuaValue()) As LuaValue
            Console.Write("Input> ")
            Return New LuaString(ChrW(Console.Read()))
        End Function

        Public Shared Function ReadLine(ByVal values() As LuaValue) As LuaValue
            Console.Write("Input> ")
            Return New LuaString(Console.ReadLine())
        End Function

    End Class
End Namespace


