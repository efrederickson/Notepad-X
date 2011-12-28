using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Alsing.SourceCode;
using System.IO;
namespace CodeEditor
{

	public class SyntaxDefinitionList
	{

		private readonly List<SyntaxDefinition> languages = new List<SyntaxDefinition>();
		public SyntaxDefinition GetLanguageFromFile(string path)
		{
			SyntaxDefinition s = default(SyntaxDefinition);
			s = SyntaxDefinition.FromSyntaxFile(path);
			languages.Add(s);
			return s;
		}

		public List<SyntaxDefinition> GetSyntaxDefinitions()
		{
			return languages;
		}
	}
}
