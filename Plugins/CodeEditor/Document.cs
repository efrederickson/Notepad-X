using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
namespace CodeEditor
{
	public class Document
	{
		public string DocumentText;
		public string Path;

		public string Title;
		public void Save(bool overrideFileNAme, string newFileNAme = "")
		{
			if (!overrideFileNAme) {
				System.IO.File.WriteAllText(Path, DocumentText);
			} else {
				System.IO.File.WriteAllText(newFileNAme, DocumentText);
			}
		}

		public Document(string title, string path, string document)
		{
			this.DocumentText = document;
			this.Path = path;
			this.Title = title;
		}
	}
}
