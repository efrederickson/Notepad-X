using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
namespace CodeEditor
{
    public class LanguageList
    {

        List<Stream> _LangList = new List<Stream>();
        /// <summary>
        /// DO NOT INCLUDE THE .syn Extension!!
        /// </summary>
        /// <param name="lang">The Language e.g. C# or VB.NET or Cobol</param>
        /// <returns></returns>
        private string GetSyntaxFileName(SyntaxLanguage lang)
        {
            var file = Enum.GetName(typeof(SyntaxLanguage), lang);
            file += ".syn";
            return file;
        }

        private System.IO.Stream GetEmbeddedStream(string name)
        {

            // The name of the embedded resource often uses the project name as a prefix.
            // This is set in the project properties page in VS2008.
            string embeddedName = string.Format("CodeEditor.Syntax.{0}", name);
            var myself = Assembly.GetExecutingAssembly();
            return myself.GetManifestResourceStream(embeddedName);
        }

        /// <summary>
        /// Gets the List of supported Language Syntax files
        /// </summary>
        public List<Stream> LanguagesList {
            get {
                _LangList.Clear();
                SyntaxLanguage[] languages = (SyntaxLanguage[])Enum.GetValues(typeof(CodeEditor.LanguageList.SyntaxLanguage));
                foreach (LanguageList.SyntaxLanguage current in languages) {
                    Stream strm = GetEmbeddedStream(GetSyntaxFileName((SyntaxLanguage)current));
                    _LangList.Add(strm);
                }
                return _LangList;
            }
        }

        public enum SyntaxLanguage
        {
            ASP,
            AutoIt,
            Cobol,
            CPP,
            //CS
            CSharp,
            CSS,
            DataFlex,
            Delphi,
            DOSBatch,
            Fortran90,
            FoxPro,
            Gemix,
            HTML,
            Java,
            JavaScript,
            JSP,
            Lang6502,
            LotusScript,
            Lua,
            MSIL,
            MySQL_SQL,
            Nemerle,
            NotepadXMacro,
            npath,
            Oracle_SQL,
            Perl,
            PHP,
            Povray,
            Python,
            rtf,
            SmallTalk,
            SqlServer2K,
            SqlServer2K5,
            SQLServer2K_SQL,
            SqlServer7,
            SQLServer7_SQL,
            SystemPolicies,
            Template,
            Text,
            TurboPascal,
            VB,
            VBNET,
            VBScript,
            VRML97,
            XML

        }
    }
}
