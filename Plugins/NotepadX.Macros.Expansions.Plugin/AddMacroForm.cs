/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/19/2011
 * Time: 12:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NotepadX.Macros.Expansions.Plugin
{
    /// <summary>
    /// Description of AddMacroForm.
    /// </summary>
    public partial class AddMacroForm : Form
    {
        public Macro Result
        {get; set; }
        
        public AddMacroForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd= new OpenFileDialog();
            ofd.Filter = "Notepad X Macros|*.nxm|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox1.Text = ofd.FileName;
        }
        
        void Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        void Button2_Click(object sender, EventArgs e)
        {
            Result = new Macro(textBox1.Text, textBox2.Text, System.IO.Path.GetFileNameWithoutExtension(textBox1.Text));
            DialogResult = DialogResult.OK;
            Close();
        }
        
        void AddMacroForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
