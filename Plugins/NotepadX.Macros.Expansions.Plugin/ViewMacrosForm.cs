/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/19/2011
 * Time: 11:45 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NotepadX.Macros.Expansions.Plugin
{
    /// <summary>
    /// Description of ViewMacrosForm.
    /// </summary>
    public partial class ViewMacrosForm : Form
    {
        public ViewMacrosForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            AddMacroForm amf = new AddMacroForm();
            if (amf.ShowDialog() == DialogResult.OK)
            {
                ThePluginClass.Macros.Add(amf.Result);
            }
            
            // refresh UI
            ViewMacrosForm_Load(sender, e);
        }
        
        void ViewMacrosForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Macro m in ThePluginClass.Macros)
            {
                if (m.Name == null)
                    continue;
                listBox1.Items.Add(m.Name);
            }
        }
        
        void Button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            ThePluginClass.Macros.RemoveAt(listBox1.SelectedIndex);
            
            // refresh UI
            ViewMacrosForm_Load(sender, e);
        }
        
        void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listBox1.SelectedIndex == -1)
            //    return;
            Macro m = ThePluginClass.Macros[listBox1.SelectedIndex];
            macroNameLabel.Text = "Macro Name: " + m.Name;
            descriptionTextBox.Text = "Description: " + m.Description;
            filenameTextBox.Text = "File name: " + m.Filename;
        }
    }
}
