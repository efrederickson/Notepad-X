Public Class ExtensionMods
    Shared Sub _Set(ByVal extension As String, ByVal extInfo As String, ByVal myName As String)
        With My.Computer.Registry
            .SetValue("HKEY_CURRENT_USER\Software\Classes\WebBrowser" & extension & ".File", "", "")
            .SetValue("HKEY_CURRENT_USER\software\Classes\WebBrowser" & extension & ".File\", "", extInfo)
            .SetValue("HKEY_CURRENT_USER\software\Classes\WebBrowser" & extension & ".File\shell", "", "")
            .SetValue("HKEY_CURRENT_USER\software\Classes\WebBrowser" & extension & ".File\shell\open", "", "")
            .SetValue("HKEY_CURRENT_USER\software\Classes\WebBrowser" & extension & ".File\shell\open\command", "", ControlChars.Quote & myName & ControlChars.Quote & " " & ControlChars.Quote & "%1" & ControlChars.Quote)
            .SetValue("HKEY_CURRENT_USER\Software\Classes\" & extension, "", "WebBrowser" & extension & ".File")
        End With
    End Sub
End Class
