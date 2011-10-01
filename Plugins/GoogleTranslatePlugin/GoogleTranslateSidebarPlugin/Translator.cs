using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
namespace GoogleTranslateSidebarPlugin
{
    public static class Translator
    {
        public static string TranslateText(string input, string languagePair)
        {
            return Translator.TranslateText(input, languagePair, Encoding.UTF8);
        }
        public static string TranslateText(string input, string languagePair, Encoding encoding)
        {
            string address = string.Format("http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}&key={2}", HttpUtility.UrlEncode(input), languagePair, "ABQIAAAAIuckriZuRA3FYdnyrDw-AhSjNWOZoB0auXpsmkMg8ggGncMXqhS0My6GzcyAUgk6vV4pWciUdN4aPA");
            string text = string.Empty;
            using (WebClient webClient = new WebClient() { Encoding = encoding })
            {
                text = webClient.DownloadString(address);
            }
            Match match = Regex.Match(text, "(?<=<div id=result_box dir=\"ltr\">)(.*?)(?=</div>)");
            text = text.Substring(36, text.Length - 36 - 51);
            if (match.Success)
            {
                text = match.Value;
            }
            return text;
        }
    }
}
