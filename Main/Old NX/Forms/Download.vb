Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Net
Imports System.IO

''' <summary>
''' To start an download use :
''' DLL As New Download(URL,path,type)
''' DLL.ShowDialog();
''' </summary>
Public Class Download
    Inherits Form

    Public Enum DownloadTypes
        General
        Update
        Autoclose
        List
        Toolbar
        Translation
        Plugin
        IconPack
        Highlighter
        UpdateCheck
    End Enum

    ''' <summary>
    ''' Use General as default, or Autoclose to close the window the the download is completed
    ''' </summary>
    Public file_type As DownloadTypes

    Public Delegate Sub DownloadCompletedHandler(ByVal sender As Object, ByVal e As DownloadCompleted)
    Public Event DownloadCompletedEvent As DownloadCompletedHandler

    'public string file_type 
    'general ,list, toolbar , translation , plugin , iconpack , highlighter
    Public file_url As String
    Public file_path As String

    ''' <summary>
    ''' Creates a Download window to download a file over the internet
    ''' </summary>
    ''' <param name="url">The url of the file</param>
    ''' <param name="path__1">The path of the file to be saved</param>
    ''' <param name="type">The DownloadTypes type variable, Use General (AND) Autoclose as default</param>
    Public Sub New(ByVal url As String, ByVal path__1 As String, ByVal type As DownloadTypes)
        file_type = type
        file_url = url
        file_path = path__1
        InitializeComponent()
        info.Text = "Downloading " & Path.GetFileName(path__1)
        Me.Text = "Downloading " & Path.GetFileName(path__1)
    End Sub

    Private Sub Download_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Me.Text = "Download"

        ' Check if internet connection is available
        Dim ok As Boolean = True
        If Not My.Computer.Network.IsAvailable Then
            If MessageBox.Show("No internet connection found " & vbLf & " do you want to cancel the download ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                ok = False
                Me.DialogResult = DialogResult.No
                Me.Close()
            End If
        End If
        If ok Then
            DownloadWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cancelButton.Click
        DownloadWorker.CancelAsync()
        ProgressBar1.Value = 0
        status.Text = "Download Stopped"
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub backgroundWorker1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles DownloadWorker.DoWork
        'Try
        ' first, we need to get the exact size (in bytes) of the file we are downloading
        Dim url As New Uri(file_url)

        Dim request As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(url), System.Net.HttpWebRequest)
        'request.Referer = "Notepad X"
        Dim response As System.Net.HttpWebResponse = DirectCast(request.GetResponse(), System.Net.HttpWebResponse)
        response.Close()

        ' gets the size of the file in bytes
        Dim iSize As Int64 = response.ContentLength

        ' keeps track of the total bytes downloaded so we can update the progress bar
        Dim iRunningByteTotal As Int64 = 0

        ' use the webclient object to download the file
        Using client As New System.Net.WebClient()
            ' open the file at the remote URL for reading
            Using streamRemote As System.IO.Stream = client.OpenRead(New Uri(file_url))
                ' using the FileStream object, we can write the downloaded bytes to the file system
                Using streamLocal As Stream = New FileStream(file_path, FileMode.Create, FileAccess.Write, FileShare.None)

                    ' loop the stream and get the file into the byte buffer
                    Dim iByteSize As Integer = 0

                    ' this will prevent any error if we cant get the size
                    If iSize = -1 Then
                        iSize = 8
                    End If

                    Dim byteBuffer As Byte() = New Byte(CInt(iSize) - 1) {}
                    While (InlineAssignHelper(iByteSize, streamRemote.Read(byteBuffer, 0, byteBuffer.Length))) > 0

                        ' write the bytes to the file system at the file path specified
                        streamLocal.Write(byteBuffer, 0, iByteSize)

                        iRunningByteTotal += iByteSize

                        ' calculate the progress out of a base "100"
                        Dim dIndex As Double = CDbl(iRunningByteTotal)
                        Dim dTotal As Double = CDbl(byteBuffer.Length)
                        Dim dProgressPercentage As Double = (dIndex / dTotal)
                        Dim iProgressPercentage As Integer = CInt(Math.Truncate(dProgressPercentage * 100))

                        ' this will prevent any errors
                        If iProgressPercentage > 100 Then
                            iProgressPercentage = 100
                        End If

                        ' update the progress bar

                        DownloadWorker.ReportProgress(iProgressPercentage)
                    End While
                    ' clean up the file stream
                    streamLocal.Close()
                End Using
                ' close the connection to the remote server
                streamRemote.Close()
            End Using

        End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "Notepad X", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Me.DialogResult = DialogResult.No
        'End Try
    End Sub

    Private Sub backgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles DownloadWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        ProgressBar1.TextValue = "Bytes Processed: " & CStr(e.UserState) & ", " & ProgressBar1.ValuePercent & "%"
    End Sub

    Private Sub DownloaderCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles DownloadWorker.RunWorkerCompleted
        Dim dllCompleted As New DownloadCompleted(file_url, file_path, file_type)

        Try
            RaiseEvent DownloadCompletedEvent(Me, dllCompleted)
        Catch generatedExceptionName As Exception
        End Try
        If Not Me.DialogResult = DialogResult.No Then Me.DialogResult = DialogResult.Yes
        If file_type = DownloadTypes.Autoclose Then
            Me.Close()
        ElseIf file_type = DownloadTypes.General Then
            info.Text = "Download Complete"
            cancelButton.Text = "Close"
        Else
            Me.Close()
        End If
    End Sub

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function
End Class

''' <summary>
''' DownloadCompletedEvent
''' </summary>
''' <remarks></remarks>
Public Class DownloadCompleted
    Inherits EventArgs
    Public Sub New(ByVal Url As [String], ByVal Path As [String], ByVal Type As Download.DownloadTypes)
        FileUrl = Url
        FilePath = Path
        DownloadType = Type
    End Sub

    Public DownloadType As Download.DownloadTypes
    Public FilePath As [String]
    Public FileUrl As [String]
End Class
