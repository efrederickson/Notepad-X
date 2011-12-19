/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/15/2011
 * Time: 12:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace NotepadX
{
    /// <summary>
    /// Program Entry
    /// </summary>
    internal sealed class Program : WindowsFormsApplicationBase
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mf = new MainForm();
            mf.ProcessParameters(null, args);
            Application.Run(mf);
        }
        
        public Program()
        {
            this.IsSingleInstance = true;
            this.EnableVisualStyles = true;
            this.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
            this.StartupNextInstance += new StartupNextInstanceEventHandler(Program_StartupNextInstance);
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
