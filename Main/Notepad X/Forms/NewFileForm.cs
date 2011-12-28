/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/15/2011
 * Time: 13:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using IExtendFramework.Text;

namespace NotepadX.Forms
{
    /// <summary>
    /// Description of NewFileForm.
    /// </summary>
    public partial class NewFileForm : Form
    {
        public ITextEditor Result;
        public string Filename;
        
        public NewFileForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            this.fileNameTextBox.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\filename.ext";
        }
        
        void NewFileForm_Load(object sender, EventArgs e)
        {
            foreach (ITextEditor iT in IExtendFramework.Text.FileExtensionManager.Editors)
            {
                IFileExtension ext = iT.Extension;
                if (!IsAlreadyInCategories(ext.Category))
                {
                    categoryListView.Items.Add(ext.Category);
                }
            }
        }
        
        bool IsAlreadyInCategories(string cat)
        {
            foreach (ListViewItem i in categoryListView.Items)
            {
                if (i.Text == cat)
                    return true;
            }
            return false;
        }
        
        void Button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        void Button2_Click(object sender, EventArgs e)
        {
            if (fileExtensionListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Error: no file selected!");
                return;
            }
            
            foreach (ITextEditor t in IExtendFramework.Text.FileExtensionManager.Editors)
            {
                if (t.Extension.Extension == (fileExtensionListView.SelectedItems[0].Tag as IFileExtension).Extension)
                {
                    this.Result =(ITextEditor) System.Activator.CreateInstance(t.GetType());
                    break;
                }
            }
            this.Filename = fileNameTextBox.Text;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        void CategoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryListView.SelectedItems.Count == 0)
                return;
            fileExtensionListView.Items.Clear();
            foreach (ITextEditor t in IExtendFramework.Text.FileExtensionManager.Editors)
            {
                if (t.Extension.Category == categoryListView.SelectedItems[0].Text)
                {
                    ListViewItem i = new ListViewItem(t.Extension.Extension);
                    i.Tag = t.Extension;
                    fileExtensionListView.Items.Add(i);
                }
            }
        }
        
        void FileExtensionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update description and filename
            if (fileExtensionListView.SelectedItems.Count == 0)
                return;
            
            IFileExtension ife = fileExtensionListView.SelectedItems[0].Tag as IFileExtension;
            descriptionLabel.Text = ife.Description;
            try
            {
                fileNameTextBox.Text = System.IO.Path.ChangeExtension(fileNameTextBox.Text, ife.Extension);
            }
            catch(Exception)
            { // probably not a valid filename.
            }
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            // browse for a filename
            
            SaveFileDialog s = new SaveFileDialog();
            if (fileExtensionListView.SelectedItems.Count == 0)
                s.Filter = "All Files (*.*)|*.*";
            else
                s.Filter =(fileExtensionListView.SelectedItems[0].Tag as IFileExtension).Extension +
                    " file|*" +
                    (fileExtensionListView.SelectedItems[0].Tag as IFileExtension).Extension;
            
            if (s.ShowDialog() == DialogResult.OK)
                fileNameTextBox.Text = s.FileName;
        }
    }
}
