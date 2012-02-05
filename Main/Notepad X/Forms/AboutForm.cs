/*
 * User: elijah
 * Date: 2/4/2012
 * Time: 2:24 PM
 */
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using IExtendFramework;

namespace NotepadX.Forms
{
    /// <summary>
    /// Description of AboutForm.
    /// </summary>
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        void AboutForm_Load(object sender, EventArgs e)
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                AssemblyName n = asm.GetName();
                AdvancedString a = "";
                a += "Name: ";
                a += n.Name;
                a += ", Version: ";
                a += n.Version.ToString();
                listBox1.Items.Add(a);
            }
        }
    }
}
