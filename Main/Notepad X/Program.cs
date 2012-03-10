/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/15/2011
 * Time: 12:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IExtendFramework.Plugins;
using Microsoft.VisualBasic.ApplicationServices;

namespace NotepadX
{
    /// <summary>
    /// Program Entry
    /// </summary>
    internal sealed class Program : WindowsFormsApplicationBase
    {
        public static List<string> FilesToDelete = new List<string>();
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // check folders
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Plugins"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Plugins");
            FileFilterSettings.Load();
            new Program();
            
            MainForm mf = new MainForm();
            Application.Run(mf);
            
            // CLOSING
            FileFilterSettings.Save();
            foreach (NotepadX.Plugins.AvailablePlugin p in NotepadX.MainForm.PluginManager.AvailablePlugins)
            {
                bool a;
                if (p.Instance != null)
                    a = p.Instance.Dispose();
                else
                    a = p.MenuItem.Dispose();
                if (!a)
                    MessageBox.Show("Error disposing plugin '" + p.AssemblyPath + "'!");
            }
            foreach (string filename in FilesToDelete)
            {
                try {
                    File.Delete(filename);
                } catch (Exception) {
                    
                }
            }
        }
        
        public Program()
        {
            this.IsSingleInstance = true;
            this.EnableVisualStyles = true;
            this.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
            this.StartupNextInstance += new StartupNextInstanceEventHandler(Program_StartupNextInstance);
            this.Shutdown += delegate {
                FileFilterSettings.Save();
                foreach (NotepadX.Plugins.AvailablePlugin p in NotepadX.MainForm.PluginManager.AvailablePlugins)
                {
                    bool a;
                    if (p.Instance != null)
                        a = p.Instance.Dispose();
                    else
                        a = p.MenuItem.Dispose();
                    if (!a)
                        MessageBox.Show("Error disposing plugin '" + p.AssemblyPath + "'!");
                }
                foreach (string filename in FilesToDelete)
                {
                    try {
                        File.Delete(filename);
                    } catch (Exception) {
                        
                    }
                }
            };
        }

        void Program_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            // Copy the arguments to a string array
            string[] args = new string[e.CommandLine.Count];
            e.CommandLine.CopyTo(args, 0);

            // Create an argument array for the Invoke method
            object[] parameters = new object[2];
            parameters[0] = MainForm;
            parameters[1] = args;

            // Need to use invoke to because this is being called from another thread.
            NotepadX.MainForm.Instance.Invoke(new MainForm.ProcessParametersDelegate(NotepadX.MainForm.Instance.ProcessParameters), parameters);
            NotepadX.MainForm.Instance.BringToFront();
        }
    }
}
