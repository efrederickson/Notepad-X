Imports System.Text

''' <summary>
''' Collection for creating Packages
''' </summary>
Public Class PackFileInfoCollection
    Public FileList As System.Collections.ArrayList
    Public FileNames As System.Collections.ArrayList

    Public Sub New()
        FileList = New System.Collections.ArrayList()
        FileNames = New System.Collections.ArrayList()
    End Sub
    '''<summary>
    ''' Add a file to the collection
    ''' </summary>
    ''' <param name="Path">File path</param>
    ''' <param name="ExtraPath">Extra Path for example \folder1, Can also be empty</param>
    Public Sub AddFile(ByVal Path As [String], ByVal ExtraPath As [String])
        If Not FileList.Contains(Path) Then
            FileList.Add(Path)
            FileNames.Add(ExtraPath & New System.IO.FileInfo(Path).Name)
        End If
    End Sub
    '''<summary>
    ''' Add a file to the collection
    ''' </summary>
    ''' <param name="Path">File path</param>
    Public Sub AddFile(ByVal Path As [String])
        AddFile(Path, "")
    End Sub
    ''' <summary>
    ''' Add all the files from a directory to the collection
    ''' </summary>
    ''' <param name="Directory">Directory path</param>
    ''' <param name="ExtraPath">Extra Path for example \folder1, Can also be empty</param>
    Public Sub AddDirectory(ByVal Directory As [String], ByVal ExtraPath As [String])
        For Each File As [String] In System.IO.Directory.GetFiles(Directory)
            AddFile(File, ExtraPath)
        Next
    End Sub
    ''' <summary>
    ''' Add all the files from a directory to the collection
    ''' </summary>
    ''' <param name="Directory">Directory path</param>
    Public Sub AddDirectory(ByVal Directory As [String])
        AddDirectory(Directory, "")
    End Sub
    ''' <summary>
    ''' Returns the file info string which will be used for the package
    ''' </summary>
    Public Function CreateFileInfo() As [String]
        Dim Info As [String] = ""
        For Each FilePath As [String] In FileList
            Info += Convert.ToString(FileNames(FileList.IndexOf(FilePath))) & ";" & (New System.IO.FileInfo(FilePath).Length) & "|"
        Next
        If Info.EndsWith("|") Then
            Info = Info.Substring(0, Info.Length - 1)
        End If
        Return Info
    End Function
End Class
Public NotInheritable Class Packages

    Private Sub New()
    End Sub
    ''' <summary>
    ''' Unpack the package file at the selected directory
    ''' </summary>
    Public Shared Function Unpack(ByVal PackFilePath As [String], ByVal DestinationFolder As [String]) As Boolean
        If Not System.IO.Directory.Exists(DestinationFolder) Then
            System.IO.Directory.CreateDirectory(DestinationFolder)
        End If

        Dim PackFile As New System.IO.FileStream(PackFilePath, System.IO.FileMode.Open)

        Dim Buffer As [Byte]()

        'Read File Info Size
        Buffer = New [Byte](3) {}
        PackFile.Read(Buffer, 0, 4)

        Dim ByteInfoBytesSize As [Byte]() = Buffer

        'Fix original array size from size 4
        While ByteInfoBytesSize(0) = 0
            Dim temp As [Byte]() = ByteInfoBytesSize
            ByteInfoBytesSize = New [Byte](ByteInfoBytesSize.Length - 2) {}
            For i As Integer = 0 To ByteInfoBytesSize.Length - 1
                ByteInfoBytesSize(i) = temp(i + 1)
            Next
        End While
        Dim InfoSize As Integer = Integer.Parse(System.Text.Encoding.[Default].GetString(ByteInfoBytesSize))

        'Read File Info
        Buffer = New [Byte](InfoSize - 1) {}
        PackFile.Read(Buffer, 0, InfoSize)
        Dim FileInfo As [String] = System.Text.Encoding.[Default].GetString(Buffer)

        Console.WriteLine("Unpacking package file {0}", PackFilePath)

        Dim Files As [String]() = FileInfo.Split("|"c)
        For Each File As [String] In Files
            Dim FileName As [String] = File.Split(";"c)(0)
            Dim FileByteSize As Integer = Integer.Parse(File.Split(";"c)(1))

            Dim FilePath As [String] = DestinationFolder & "\" & FileName
            If System.IO.File.Exists(FilePath) Then
                System.IO.File.Delete(FilePath)
            End If
            If FileName.Contains("\") Then
                If Not System.IO.Directory.Exists(DestinationFolder & "\" & FileName.Split("\"c)(0)) Then
                    System.IO.Directory.CreateDirectory(DestinationFolder & "\" & FileName.Split("\"c)(0))
                End If
            End If
            Dim newFile As New System.IO.FileStream(FilePath, System.IO.FileMode.Create)
            Console.WriteLine("-Unpacking file :{0}", FileName)

            Buffer = New [Byte](FileByteSize - 1) {}
            PackFile.Read(Buffer, 0, FileByteSize)

            newFile.Write(Buffer, 0, FileByteSize)
            newFile.Close()
        Next
        PackFile.Close()
        Return True
    End Function
    ''' <summary>
    ''' Create an file pack , join all the files from PackFileInfoCollection into a single file
    ''' </summary>
    Public Shared Function Pack(ByVal Collection As PackFileInfoCollection, ByVal PackDestination As [String]) As Boolean

        Dim FileInfo As [String] = Collection.CreateFileInfo()

        Dim InfoBytes As [Byte]() = System.Text.Encoding.[Default].GetBytes(FileInfo)
        Dim InfoBytesSize As Integer = InfoBytes.Length
        Dim ByteInfoBytesSize As [Byte]() = System.Text.Encoding.[Default].GetBytes(InfoBytesSize.ToString())

        'Fix Array Size to 4
        If ByteInfoBytesSize.Length < 4 Then
            Dim d As Integer = 4 - ByteInfoBytesSize.Length
            Dim temp As [Byte]() = ByteInfoBytesSize
            ByteInfoBytesSize = New [Byte](3) {}
            For i As Integer = 3 To 0 Step -1
                If i - d >= 0 Then
                    ByteInfoBytesSize(i) = temp(i - d)
                Else
                    ByteInfoBytesSize(i) = 0
                End If
            Next
        End If
        If System.IO.File.Exists(PackDestination) Then
            System.IO.File.Delete(PackDestination)
        End If
        Dim packFile As New System.IO.FileStream(PackDestination, System.IO.FileMode.CreateNew)

        'Write InfoByteSize as Bytes[]
        packFile.Write(ByteInfoBytesSize, 0, 4)

        'Write Info as Bytes[]
        packFile.Write(InfoBytes, 0, InfoBytes.Length)

        'Write Files as Bytes[]

        For Each File As [String] In Collection.FileList
            Dim FileBytes As [Byte]() = System.IO.File.ReadAllBytes(File)
            packFile.Write(FileBytes, 0, FileBytes.Length)
        Next
        packFile.Close()

        Return True
    End Function
End Class