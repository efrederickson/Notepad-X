/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/28/2011
 * Time: 3:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeEditor.Code_Editors
{
    /// <summary>
    /// Description of LoadingForm.
    /// </summary>
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        public void Set(int c, int max)
        {
            this.progressBar1.Maximum = max;
            progressBar1.Value = c;
        }
    }
}
