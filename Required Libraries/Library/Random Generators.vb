Namespace Library.Random

    ''' <summary>
    ''' A Random String Generator. Has a certain chance of being a CAPITAL letter.
    ''' </summary>
    ''' <remarks></remarks>
    Class RandomStringGenerator
        Dim RNG As New RandomNumberGenerator(DateTime.Now.Millisecond)
        Dim Letters() As String = {"q", "w", "e", "r", "t", "y", "u", "i", "o", "p", _
            "a", "s", "d", "f", "g", "h", "j", "k", "l", _
            "z", "x", "c", "v", "b", "n", "m"}

        ''' <summary>
        ''' The actual function that generates the Random String
        ''' </summary>
        ''' <param name="StrLength">The length of the Random String</param>
        ''' <returns>This is the random String</returns>
        ''' <remarks></remarks>
        Function GenerateString(ByVal StrLength As Integer) As String
            Dim Str As String = ""
            If StrLength < 1 Then
                Throw New Exception("Invalid Minimum String Length. Minimum: 1")
            End If
            For i = 1 To StrLength
                Dim Cha As Char
                Cha = Letters(RNG.Generate(0, Letters.Count))
                Dim A As Integer = RNG.Generate(0, 2)
                If A = 1 Then Cha = Char.ToUpper(Cha)
                Str &= Cha
            Next
            Return Str
        End Function
    End Class

    ''' <summary>
    ''' A Random Number Generator
    ''' </summary>
    ''' <remarks></remarks>
    Class RandomNumberGenerator
        Dim RNG As New System.Random(DateTime.Now.Millisecond)
        Private WithEvents Worker As New System.ComponentModel.BackgroundWorker
        ''' <summary>
        ''' This Generates the Number
        ''' </summary>
        ''' <param name="Minimum">The Minimum number</param>
        ''' <param name="Maximum">The Maximum number</param>
        ''' <returns>The randomized number</returns>
        ''' <remarks></remarks>
        Function Generate(ByVal Minimum As Integer, ByVal Maximum As Integer) As Integer
            Return RNG.Next(Minimum, Maximum)
        End Function

        Sub New()
            Worker.RunWorkerAsync()
        End Sub

        Sub New(ByVal _Seed As Integer)
            Worker.RunWorkerAsync()
            RNG = New System.Random(_Seed)
        End Sub

        Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
            Worker.WorkerReportsProgress = True
            While True
                Worker.ReportProgress(0)
                Threading.Thread.Sleep(100)
            End While
        End Sub

        Private Sub Worker_ReportProgress(ByVal sender As Object, ByVal e As ComponentModel.ProgressChangedEventArgs) Handles Worker.ProgressChanged
            RNG = New System.Random(RNG.Next())
        End Sub
    End Class

    ''' <summary>
    ''' A Random Date Generator
    ''' </summary>
    ''' <remarks></remarks>
    Class RandomDateGenerator
        Dim RNG As New System.Random(DateTime.Now.Millisecond)

        ''' <summary>
        ''' The Function that generates the Date
        ''' </summary>
        ''' <param name="mindate">The Minimum Date</param>
        ''' <param name="maxDate">The Maximum Date</param>
        ''' <returns>The Randomized Date</returns>
        ''' <remarks></remarks>
        Function Generate(ByVal mindate As Date, ByVal maxDate As Date) As Date
            Dim RandomDate As New Date()
            Dim Month, Year, Day, Minute, Hour, Second, Millisecond As Integer
            Try
                Month = RNG.Next(mindate.Month, maxDate.Month)
            Catch ex As Exception
                Month = RNG.Next()
            End Try
            Try
                Year = RNG.Next(mindate.Year, maxDate.Year)
            Catch ex As Exception
                Year = RNG.Next(Date.Now.Year)
            End Try
            Try
                Day = RNG.Next(mindate.Day, maxDate.Day)
            Catch ex As Exception
                Day = RNG.Next(30)
            End Try
            Try
                Minute = RNG.Next(mindate.Minute, maxDate.Minute)
            Catch ex As Exception
                Minute = RNG.Next(59)
            End Try
            Try
                Hour = RNG.Next(mindate.Hour, maxDate.Hour)
            Catch ex As Exception
                Hour = RNG.Next(23)
            End Try
            Try
                Second = RNG.Next(mindate.Second, maxDate.Second)
            Catch ex As Exception
                Second = RNG.Next(59)
            End Try
            Try
                Millisecond = RNG.Next(mindate.Millisecond, maxDate.Millisecond)
            Catch ex As Exception
                Millisecond = RNG.Next(999)
            End Try
            RandomDate = New Date(Year, Month, Day, Hour, Minute, Second, Millisecond)

            Return RandomDate
        End Function
    End Class

End Namespace
