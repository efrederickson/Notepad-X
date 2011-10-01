Namespace Library.Collections
    ''' <summary>
    ''' A Simple "Reverse" Queue.
    ''' It acts as a First-in-first-out List(Of Object)
    ''' </summary>
    ''' <remarks><see cref="System.Collections.Queue">Based off of Queue</see></remarks>
    Public Class FIFOList
        Private _internalCollection As New List(Of Object)

        Public ReadOnly Property Count As Integer
            Get
                Return _internalCollection.Count
            End Get
        End Property

        Public Sub Clear()
            _internalCollection.Clear()
        End Sub

        Public Function Contains(ByVal value As Object) As Boolean
            For Each obj In _internalCollection
                If obj = value Then
                    Return True
                End If
            Next
            Return False
        End Function

        Sub AddItem(ByVal item As Object)
            If item Is Nothing Then
                Throw New Exception("Cannot Add null/Nothing to FIFOLists")
            End If
            _internalCollection.Add(item)
        End Sub

        Function GetItem() As Object
            Dim item As Object = _internalCollection(0)
            _internalCollection.RemoveAt(0)
            Return item
        End Function
    End Class
End Namespace
