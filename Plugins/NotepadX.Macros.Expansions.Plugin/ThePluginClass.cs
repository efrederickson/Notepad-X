/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/19/2011
 * Time: 11:33 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IExtendFramework.Text;
using NotepadX.Plugins;

namespace NotepadX.Macros.Expansions.Plugin
{
    /// <summary>
    /// the plugin
    /// </summary>
    public class ThePluginClass : IPlugin
    {
        public static List<Macro> Macros = new List<Macro>();
        
        ToolStripMenuItem viewMacrosItem;
        public string Name {
            get {
                return "Macro Expansion Pack for Notepad X";
            }
        }
        
        public string Author {
            get {
                return "Elijah Frederickson";
            }
        }
        
        public string Version {
            get {
                return "1.0";
            }
        }
        
        public string Description {
            get {
                return "Macro Utilities for Notepad X";
            }
        }
        
        public string UpdateUrl {
            get {
                return "";
            }
        }
        
        public System.Windows.Forms.TabPage AboutPage {
            get {
                return null;
            }
        }
        
        public System.Windows.Forms.TabPage OptionsPage {
            get {
                return null;
            }
        }
        
        public System.Windows.Forms.TabPage HelpPage {
            get {
                return null;
            }
        }
        
        public bool Initialize()
        {
            //NotepadX.MainForm.Instance.AddMenuItem(item, path, index);
            try {
                viewMacrosItem = new ToolStripMenuItem("View Macros");
                viewMacrosItem.Click += delegate(object sender, EventArgs e) {
                    new ViewMacrosForm().ShowDialog();
                };
                NotepadX.MainForm.Instance.AddMenuItem(viewMacrosItem, "macros", 99);
                NotepadX.MainForm.Instance.AddMenuItem(new ToolStripMenuItem() { Text = "---------", Enabled = false, Visible = true }, "macros", 1);
                
                // add all in Macros folder as menu items
                if (!Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Macros"))
                    Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Macros");
                
                // load or create INI file
                if (!File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Macros\\Macros.ini"))
                {
                    using (StreamWriter sw = new StreamWriter(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Macros\\Macros.ini"))
                    {
                        sw.WriteLine("[" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Macros\\SaveAndExitMacro.nxm]");
                        sw.WriteLine("Name=Save and Exit");
                        sw.WriteLine("Description=Saves and exits Notepad X");
                        sw.Close();
                    }
                }
                INIDocument doc = new INIDocument(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Macros\\Macros.ini");
                foreach (string sname in doc.SectionNames)
                {
                    // get Name (and Description?)
                    string name = doc[sname]["Name"];
                    ToolStripMenuItem i = new ToolStripMenuItem(name);
                    i.Tag = sname; // filename
                    i.Click += delegate { NotepadX.MainForm.Instance.RunMacro(File.ReadAllText(sname)); };
                    Macros.Add(new Macro(sname, doc[sname]["Description"], doc[sname]["Name"]));
                }
                // weird glitch, but it must go.
                Macros.RemoveAt(0);
            } catch (Exception ex) {
                MessageBox.Show("Error starting macro expansions: " + ex.ToString());
            }
            
            return true;
        }
        
        public bool Dispose()
        {
            INIDocument d = new INIDocument();
            foreach (Macro m in Macros)
            {
                d.SetValue(m.Filename, "Name", m.Name);
                d.SetValue(m.Filename, "Description", m.Description);
            }
            d.Save(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Macros\\Macros.ini");
            
            return true;
        }
    }
}