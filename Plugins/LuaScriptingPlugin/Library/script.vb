﻿Imports System.Windows.Forms
Imports System.Reflection
Imports System.IO
Imports System.Linq
Imports System.Collections.Generic

Namespace Library
    ' The class for this Lua script
    Public NotInheritable Class script

        Private Sub New()
        End Sub
        Shared currentModule As LuaTable

        Public Shared Sub RegisterModule(ByVal enviroment As LuaTable)
            Dim [module] As New LuaTable()
            RegisterFunctions([module])
            enviroment.SetNameValue("script", [module])
            [module].SetNameValue("_G", enviroment)
            currentModule = [module]
        End Sub

        Public Shared Sub RegisterFunctions(ByVal [module] As LuaTable)
            [module].Register("Run", AddressOf Run)
            [module].Register("MessageBox", AddressOf _MessageBox)
            [module].Register("Create", AddressOf CreateObject)
            [module].Register("GetAssembly", AddressOf AddReference)
            Dim metaTable As New LuaTable()
            metaTable.Register("__index", AddressOf CreateObject)
            [module].MetaTable = metaTable
        End Sub

        Public Shared Function Run(ByVal values As LuaValue()) As LuaValue
            Dim data As LuaUserdata = TryCast(values(0), LuaUserdata)
            Dim form As Form = DirectCast(data.Value, Form)
            form.PerformLayout()
            Application.Run(form)
            Return Nothing
        End Function

        Public Shared Function AddReference(ByVal values As LuaValue()) As LuaValue
            Dim path As LuaString = TryCast(values(0), LuaString)
            If IO.Path.IsPathRooted(path.Text) Then
                Return LuaAssembly.From(Assembly.LoadFile(path.Text))
            Else
                ' Deprecated, but VERY useful :)
                Return LuaAssembly.From(Assembly.LoadWithPartialName(path.Text))
            End If
        End Function

        Public Shared Function _MessageBox(ByVal values As LuaValue()) As LuaValue
            Dim messageString As LuaString = TryCast(values(0), LuaString)
            Dim captionString As LuaString = If(values.Length > 1, TryCast(values(1), LuaString), Nothing)
            If captionString Is Nothing Then
                MessageBox.Show(messageString.Text, "Notepad X Lua")
            Else
                MessageBox.Show(messageString.Text, captionString.Text)
            End If
            Return Nothing
        End Function

        Public Shared Function CreateObject(ByVal values As LuaValue()) As LuaValue
            Dim typeString As LuaString = TryCast(values(0), LuaString)
            Dim assembly As LuaAssembly = TryCast(values(1), LuaAssembly)
            Dim typeName As String = typeString.Text
            Dim type As Type = CType(assembly.Value, System.Reflection.Assembly).[GetType](typeName)

            Dim func As New LuaFunction(Function(args As LuaValue())
                                            Dim control As Object = Activator.CreateInstance(type)
                                            Dim table As LuaTable = TryCast(args(0), LuaTable)
                                            Dim name As String = Nothing
                                            If table Is Nothing Then
                                                GoTo PAST_TABLE
                                            End If
                                            If table.Length > 0 Then
                                                'AddChildControls(control, table)
                                            End If

                                            If table.Count > 0 Then
                                                For Each pair As KeyValuePair(Of LuaValue, LuaValue) In table.KeyValuePairs
                                                    Dim member As String = TryCast(pair.Key, LuaString).Text

                                                    If member = "Name" Then
                                                        name = DirectCast(pair.Value.Value, String)
                                                        Continue For
                                                    End If

                                                    SetMemberValue(control, type, member, pair.Value.Value)
                                                Next
                                            End If
PAST_TABLE:
                                            Dim data As New LuaUserdata(control)
                                            data.MetaTable = GetControlMetaTable()

                                            If name IsNot Nothing Then
                                                Dim enviroment As LuaTable = TryCast(currentModule.GetValue("_G"), LuaTable)
                                                enviroment.SetNameValue(name, data)
                                            End If

                                            Return data

                                        End Function)

            currentModule.SetNameValue(typeString.Text, func)
            Return func.Invoke(values)
        End Function

        Shared controlMetaTable As LuaTable
        Private Shared Function GetControlMetaTable() As LuaTable
            If controlMetaTable Is Nothing Then
                controlMetaTable = New LuaTable()

                controlMetaTable.SetNameValue("__index", New LuaFunction(Function(values)
                                                                             Dim control As LuaUserdata = TryCast(values(0), LuaUserdata)
                                                                             Dim type As Type = control.Value.[GetType]()

                                                                             Dim member As LuaString = TryCast(values(1), LuaString)
                                                                             If member IsNot Nothing Then
                                                                                 Return GetMemberValue(control.Value, type, member.Text)
                                                                             End If

                                                                             Dim index As LuaNumber = TryCast(values(1), LuaNumber)
                                                                             If index IsNot Nothing Then
                                                                                 Return GetIndexerValue(control.Value, type, index.Number)
                                                                             End If

                                                                             Return LuaNil.Nil

                                                                         End Function))

                controlMetaTable.SetNameValue("__newindex", New LuaFunction(Function(values)
                                                                                Dim control As LuaUserdata = TryCast(values(0), LuaUserdata)
                                                                                Dim member As LuaString = TryCast(values(1), LuaString)
                                                                                Dim value As LuaValue = values(2)

                                                                                Dim type As Type = control.Value.[GetType]()
                                                                                SetMemberValue(control.Value, type, member.Text, value.Value)
                                                                                Return Nothing

                                                                            End Function))
            End If

            Return controlMetaTable
        End Function

        Private Shared Function GetMemberValue(ByVal control As Object, ByVal type As Type, ByVal member As String) As LuaValue
            Dim propertyInfo As PropertyInfo = type.GetProperty(member)
            If propertyInfo IsNot Nothing Then
                Dim value As Object = propertyInfo.GetValue(control, Nothing)
                Return ToLuaValue(value)
            Else
                Return New LuaFunction(Function(args)
                                           Dim members As MemberInfo() = type.GetMember(member)

                                           If members.Length = 0 Then
                                               Throw New InvalidOperationException(String.Format("{0} is not defined in {1}", member, type.FullName))
                                           End If

                                           For Each memberInfo As MemberInfo In members
                                               Dim methodInfo As MethodInfo = TryCast(memberInfo, MethodInfo)
                                               If methodInfo IsNot Nothing Then
                                                   Try
                                                       Dim result As Object = methodInfo.Invoke(control, args.[Select](Function(a) a.Value).ToArray())
                                                       Return ToLuaValue(result)
                                                   Catch generatedExceptionName As TargetParameterCountException
                                                   Catch generatedExceptionName As ArgumentException
                                                   Catch generatedExceptionName As MethodAccessException
                                                   Catch generatedExceptionName As InvalidOperationException
                                                   End Try
                                               End If
                                           Next
                                           Return LuaNil.Nil

                                       End Function)

                Throw New Exception(String.Format("Cannot get {0} from {1}", member, control))
            End If
        End Function

        Private Shared Function GetIndexerValue(ByVal control As Object, ByVal type As Type, ByVal index As Double) As LuaValue
            Dim members As MemberInfo() = type.GetMember("Item")

            If members.Length = 0 Then
                Throw New InvalidOperationException(String.Format("Indexer is not defined in {0}", type.FullName))
            End If

            For Each memberInfo As MemberInfo In members
                Dim propertyInfo As PropertyInfo = TryCast(memberInfo, PropertyInfo)
                If propertyInfo IsNot Nothing Then
                    Try
                        Dim result As Object = propertyInfo.GetValue(control, New Object() {CInt(Math.Truncate(index)) - 1})
                        Return ToLuaValue(result)
                    Catch generatedExceptionName As TargetParameterCountException
                    Catch generatedExceptionName As ArgumentException
                    Catch generatedExceptionName As MethodAccessException
                    Catch generatedExceptionName As TargetInvocationException
                    End Try
                End If
            Next

            Return LuaNil.Nil
        End Function

        Private Shared Function ToLuaValue(ByVal value As Object) As LuaValue
            If TypeOf value Is Integer OrElse TypeOf value Is Double Then
                Return New LuaNumber(Convert.ToDouble(value))
            ElseIf TypeOf value Is String Then
                Return New LuaString(DirectCast(value, String))
            ElseIf TypeOf value Is Boolean Then
                Return LuaBoolean.From(CBool(value))
            ElseIf value Is Nothing Then
                Return LuaNil.Nil
            ElseIf value.[GetType]().IsEnum Then
                Return New LuaString(value.ToString())
            ElseIf value.[GetType]().IsArray Then
                Dim table As New LuaTable()
                For Each item As Var In TryCast(value, Array)
                    table.AddValue(ToLuaValue(item))
                Next
                Return table
            ElseIf TypeOf value Is LuaValue Then
                Return DirectCast(value, LuaValue)
            Else
                Dim data As New LuaUserdata(value)
                data.MetaTable = GetControlMetaTable()
                Return data
            End If
        End Function

        Private Shared Sub SetMemberValue(ByVal control As Object, ByVal type As Type, ByVal member As String, ByVal value As Object)
            Dim propertyInfo As PropertyInfo = type.GetProperty(member)
            If propertyInfo IsNot Nothing Then
                SetPropertyValue(control, value, propertyInfo)
            Else
                Throw New NotImplementedException()
            End If
        End Sub

        Private Shared Sub SetPropertyValue(ByVal obj As Object, ByVal value As Object, ByVal propertyInfo As PropertyInfo)
            If propertyInfo.PropertyType.FullName = "System.Int32" Then
                propertyInfo.SetValue(obj, CInt(Math.Truncate(CDbl(value))), Nothing)
            ElseIf propertyInfo.PropertyType.IsEnum Then
                Dim enumValue As Object = [Enum].Parse(propertyInfo.PropertyType, DirectCast(value, String))
                propertyInfo.SetValue(obj, enumValue, Nothing)
            ElseIf propertyInfo.PropertyType.FullName = "System.Drawing.Image" Then
                Dim enviroment As LuaTable = TryCast(currentModule.GetValue("_G"), LuaTable)
                Dim workDir As LuaString = TryCast(enviroment.GetValue("_WORKDIR"), LuaString)
                Dim image = System.Drawing.Image.FromFile(Path.Combine(workDir.Text, DirectCast(value, String)))
                propertyInfo.SetValue(obj, image, Nothing)
            Else
                propertyInfo.SetValue(obj, value, Nothing)
            End If
        End Sub
    End Class
End Namespace
