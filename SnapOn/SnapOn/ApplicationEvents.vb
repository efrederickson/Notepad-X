Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Sub anerror(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles MyBase.UnhandledException
            MsgBox(e.exception.ToString())
            IO.File.WriteAllText(startpath & "\Error.txt", e.exception.ToString())
        End Sub
        Sub NewInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles MyBase.StartupNextInstance
            ' Copy the arguments to a string array
            Dim args As String() = New String(e.CommandLine.Count - 1) {}
            e.CommandLine.CopyTo(args, 0)

            ' Create an argument array for the Invoke method
            Dim parameters As Object() = New Object(1) {}
            parameters(0) = Me.MainForm
            parameters(1) = args

            ' Need to use invoke to because this is being called from another thread.
            Form1.Invoke(New Form1.ProcessParametersDelegate( _
                               AddressOf DirectCast(Application.MainForm, Form1).ProcessParameters), _
                           parameters)
        End Sub
    End Class
End Namespace

