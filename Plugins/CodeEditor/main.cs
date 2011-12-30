using Alsing.SourceCode;
using IExtendFramework.Text;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace CodeEditor
{

    public class Main : NotepadX.Plugins.IPlugin
    {
        public string Author {
            get { return "Elijah Frederickson"; }
        }

        public string Description {
            get { return "The Code Editing Utility for Notepad X"; }
        }

        public bool Dispose()
        {
            return true;
        }

        public string UpdateUrl {
            get { return ""; }
        }

        public bool Initialize()
        {
            int c = 0;
            Code_Editors.LoadingForm loadingForm = new CodeEditor.Code_Editors.LoadingForm();
            loadingForm.Set(0, c);
            loadingForm.Show();
            int i = 0;
            CodeEditor.SyntaxDefinitionList list = new CodeEditor.SyntaxDefinitionList();
            if (!System.IO.File.Exists(Application.LocalUserAppDataPath + "\\hasExtractedCodeSyntax")) {
                List<Stream> strms = new LanguageList().LanguagesList;
                Directory.CreateDirectory(Application.LocalUserAppDataPath + "\\SyntaxDefinitions\\");
                for (i = 0; i <= strms.Count - 1; i++) {
                    c = strms.Count * 2 + 1;
                    Stream langStream = strms[i];
                    string path = string.Format("{0}\\SyntaxDefinitions\\{1}.syn", Application.LocalUserAppDataPath, System.IO.Path.GetFileName(System.IO.Path.GetTempFileName()));
                    FileStream file = new FileStream(path, FileMode.Create);
                    if (langStream == null) {
                        Interaction.MsgBox(string.Format("Stream {0} is empty. Please restart Notepad X!", i));
                    } else {
                        langStream.CopyTo(file);
                        langStream.Close();
                    }
                    file.Close();
                    if (langStream != null) {
                        list.GetLanguageFromFile(path);
                    }
                    loadingForm.Set(i, c);
                }
                System.IO.File.Create(Application.LocalUserAppDataPath + "\\hasExtractedCodeSyntax");
            } else {
                string[] files234 = System.IO.Directory.GetFiles(Application.LocalUserAppDataPath + "\\SyntaxDefinitions\\");
                if (files234.Count() > 0) {
                    for (int i2 = 0; i2 <= files234.Count() - 1; i2++) {
                        list.GetLanguageFromFile(files234[i2]);
                    }
                } else {
                    System.IO.File.Delete(Application.LocalUserAppDataPath + "\\hasExtractedCodeSyntax");
                    MessageBox.Show("Error: no syntax files! Please restart Notepad X");
                }
            }
            string[] files = System.IO.Directory.GetFiles(SpecialDirectories.MyDocuments + "\\Notepad X\\SyntaxFiles\\");
            if (files.Count() > 0) {
                for (int i2 = 0; i2 <= files.Count() - 1; i2++) {
                    try {
                        list.GetLanguageFromFile(files[i2]);
                    } catch (Exception ex) {
                        MessageBox.Show("Cannot load '" + files[i2] + "':\n" + ex.ToString());
                    }
                }
            }
            
            foreach (SyntaxDefinition syntax in list.GetSyntaxDefinitions()) {
                foreach (FileType e in syntax.FileTypes)
                {
                    EditForm f = new EditForm("", "", "", syntax, e);
                    IExtendFramework.Text.FileExtensionManager.AddEditor(f as ITextEditor);
                    while (i > c)
                        c++;
                           loadingForm.Set(i++, c);
                }
            }
            loadingForm.Close();
            return true;
        }

        public string Name {
            get { return "Allows many new code file types to be opened"; }
        }

        public string Version {
            get { return "1.0"; }
        }

        public System.Windows.Forms.TabPage AboutPage {
            get { return null; }
        }

        public TabPage HelpPage {
            get { return null; }
        }

        public System.Windows.Forms.TabPage OptionsPage {
            get { return null; }
        }

        public Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Resolver);
        }

        public static System.Reflection.Assembly Resolver(object sender, ResolveEventArgs args)
        {
            Assembly a1 = Assembly.GetExecutingAssembly();
            Stream s = a1.GetManifestResourceStream("CodeEditor.SyntaxBox.dll");
            if (s == null) {
                throw new Exception("Cannot Load the SyntaxBox!");
            }
            byte[] block = new byte[s.Length + 1];
            s.Read(block, 0, block.Length);
            Assembly a2 = Assembly.Load(block);
            return a2;
        }

    }
}
