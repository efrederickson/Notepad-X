Public Class ExtensionMods
    Shared Sub _Set(ByVal extension As String, ByVal extInfo As String, ByVal myName As String)
        With My.Computer.Registry
            .SetValue(String.Format("HKEY_CURRENT_USER\Software\Classes\NotepadX{0}.File", extension), "", "")
            .SetValue(String.Format("HKEY_CURRENT_USER\software\Classes\NotepadX{0}.File\", extension), "", extInfo)
            .SetValue(String.Format("HKEY_CURRENT_USER\software\Classes\NotepadX{0}.File\shell", extension), "", "")
            .SetValue(String.Format("HKEY_CURRENT_USER\software\Classes\NotepadX{0}.File\shell\open", extension), "", "")
            .SetValue(String.Format("HKEY_CURRENT_USER\software\Classes\NotepadX{0}.File\shell\open\command", extension), "", String.Format("{0}{1}{0} {0}%1{0}", ControlChars.Quote, myName))
            .SetValue("HKEY_CURRENT_USER\Software\Classes\" & extension, "", String.Format("NotepadX{0}.File", extension))
        End With
    End Sub

    Shared Sub SetBasicCommands(ByVal myName As String)
        With My.Computer.Registry
            .SetValue("HKEY_CURRENT_USER\Software\Classes\*", "", "")
            .SetValue("HKEY_CURRENT_USER\Software\Classes\*\shell", "", "")
            .SetValue("HKEY_CURRENT_USER\Software\Classes\*\shell\Encrypt", "", "")
            .SetValue("HKEY_CURRENT_USER\Software\Classes\*\shell\Encrypt\command", "", String.Format("{0}{1}{0} {0}%1{0} {0}encrypt{0}", ControlChars.Quote, myName))
            .SetValue("HKEY_CURRENT_USER\Software\Classes\*", "", "")
            .SetValue("HKEY_CURRENT_USER\Software\Classes\*\shell", "", "")
            .SetValue("HKEY_CURRENT_USER\Software\Classes\*\shell\Decrypt", "", "")
            .SetValue("HKEY_CURRENT_USER\Software\Classes\*\shell\Decrypt\command", "", String.Format("{0}{1}{0} {0}%1{0} {0}decrypt{0}", ControlChars.Quote, myName))
        End With
    End Sub
End Class
