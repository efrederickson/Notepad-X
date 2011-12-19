Imports System
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Public Class Translator

    Public Shared Function TranslateText(ByVal input As String, ByVal languagePair As String) As String
        Return Translator.TranslateText(input, languagePair, Encoding.UTF8)
    End Function

    Public Shared Function TranslateText(ByVal input As String, ByVal languagePair As String, ByVal Encoding As Encoding) As String
        Dim address As String = String.Format("http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}&key={2}", HttpUtility.UrlEncode(Input), languagePair, "ABQIAAAAIuckriZuRA3FYdnyrDw-AhSjNWOZoB0auXpsmkMg8ggGncMXqhS0My6GzcyAUgk6vV4pWciUdN4aPA")
        Dim text As String = String.Empty
        Using webClient As New WebClient()
            webClient.Encoding = Encoding
            text = webClient.DownloadString(address)
        End Using
        Dim match As Match = Regex.Match(text, "(?<=<div id=result_box dir=" & ControlChars.Quote & "ltr" & ControlChars.Quote & ">)(.*?)(?=</div>)")
        text = text.Substring(36, text.Length - 36 - 51)
        If Match.Success Then
            text = match.Value
            Log.WriteLine("Translated " & input & " to " & match.Value)
        Else
            Log.WriteLine("Could not find a match for " & input)
        End If
        Return text
    End Function

    Public Shared Function FormatLanguages(ByVal inlang As String, ByVal outlang As String) As String
        Dim languageList As New Hashtable
        languageList.Add("English", "en")
        languageList.Add("Greek", "el")
        languageList.Add("Italian", "it")
        languageList.Add("Russian", "ru")
        languageList.Add("German", "de")
        languageList.Add("Albanian", "sq")
        languageList.Add("Afrikaans", "af")
        languageList.Add("Vietnamese", "vi")
        languageList.Add("Bulgarian", "bg")
        languageList.Add("Belarusian", "be")
        languageList.Add("Chinese Simplified", "zh-CN")
        languageList.Add("Chinese  Traditional", "zh-TW")
        languageList.Add("Dutch", "nl")
        languageList.Add("Danish", "da")
        languageList.Add("Estonian", "et")
        languageList.Add("French", "fr")
        languageList.Add("Finnish", "fi")
        languageList.Add("Hebrew", "iw")
        languageList.Add("Japanese", "ja")
        languageList.Add("Indonesian", "id")
        languageList.Add("Irish", "ga")
        languageList.Add("Icelandic", "is")
        languageList.Add("Korean", "ko")
        languageList.Add("Croatian", "hr")
        languageList.Add("Latvian", "lv")
        languageList.Add("Lithuanian", "lt")
        languageList.Add("Maltese", "mt")
        languageList.Add("Norwegian", "no")
        languageList.Add("Hungarian", "hu")
        languageList.Add("Persian", "fa")
        languageList.Add("Portuguese", "pt")
        languageList.Add("Polish", "pl")
        languageList.Add("Romanian", "ro")
        languageList.Add("Spanish", "es")
        languageList.Add("Serbian", "sr")
        languageList.Add("Slovak", "sk")
        languageList.Add("Slovenian", "sl")
        languageList.Add("Swedish", "sv")
        languageList.Add("Czech", "cs")
        languageList.Add("Thai", "th")
        languageList.Add("Turkish", "tr")
        languageList.Add("Yiddish", "yi")
        Return CStr(languageList(inlang)) & "|" & CStr(languageList(outlang))
    End Function
End Class
