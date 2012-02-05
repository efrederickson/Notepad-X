/*
 * User: elijah
 * Date: 2/4/2012
 * Time: 2:34 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NotepadX.Forms
{
    /// <summary>
    /// Description of HelpForm.
    /// </summary>
    public partial class HelpForm : Form
    {
        public HelpForm()
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
    }
}
