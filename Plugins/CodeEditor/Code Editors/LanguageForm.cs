using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using Alsing.SourceCode;
namespace CodeEditor
{

    /// <summary>
    /// Summary description for LanguageForm.
    /// </summary>
    public class LanguageForm : Form
    {
        
        private Button withEventsField_btnCancel;
        public Button btnCancel {
            get { return withEventsField_btnCancel; }
            set {
                if (withEventsField_btnCancel != null) {
                    withEventsField_btnCancel.Click -= btnCancel_Click;
                }
                withEventsField_btnCancel = value;
                if (withEventsField_btnCancel != null) {
                    withEventsField_btnCancel.Click += btnCancel_Click;
                }
            }
        }
        private Button withEventsField_btnOK;
        public Button btnOK {
            get { return withEventsField_btnOK; }
            set {
                if (withEventsField_btnOK != null) {
                    withEventsField_btnOK.Click -= btnOK_Click;
                }
                withEventsField_btnOK = value;
                if (withEventsField_btnOK != null) {
                    withEventsField_btnOK.Click += btnOK_Click;
                }
            }
        }
        IContainer components;
        public EditForm EditForm;
        ImageList imlIcons;
        private ListView withEventsField_lvFileTypes;
        public ListView lvFileTypes {
            get { return withEventsField_lvFileTypes; }
            set {
                if (withEventsField_lvFileTypes != null) {
                    withEventsField_lvFileTypes.DoubleClick -= lvFileTypes_DoubleClick;
                }
                withEventsField_lvFileTypes = value;
                if (withEventsField_lvFileTypes != null) {
                    withEventsField_lvFileTypes.DoubleClick += lvFileTypes_DoubleClick;
                }
            }
        }
        private TreeView withEventsField_trvFileTypes;
        public TreeView trvFileTypes {
            get { return withEventsField_trvFileTypes; }
            set {
                if (withEventsField_trvFileTypes != null) {
                    withEventsField_trvFileTypes.AfterSelect -= trvFileTypes_AfterSelect;
                }
                withEventsField_trvFileTypes = value;
                if (withEventsField_trvFileTypes != null) {
                    withEventsField_trvFileTypes.AfterSelect += trvFileTypes_AfterSelect;
                }
            }

        }
        public LanguageForm()
        {
            Load += LanguageForm_Load;
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }


        public LanguageForm(CodeEditor.SyntaxDefinitionList LangList)
        {
            Load += LanguageForm_Load;
            InitializeComponent();
            trvFileTypes.Nodes.Clear();
            foreach (SyntaxDefinition syntax in LangList.GetSyntaxDefinitions()) {
                TreeNode tn = trvFileTypes.Nodes.Add(syntax.Name);
                tn.Tag = syntax;
            }
            trvFileTypes.SelectedNode = trvFileTypes.Nodes[0];
        }

        protected override void Dispose(bool disposing)
        {
            if ((disposing)) {
                if ((components != null)) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void LanguageForm_Load(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OK();
        }


        private void trvFileTypes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var syntax = (SyntaxDefinition)e.Node.Tag;
            lvFileTypes.Items.Clear();
            foreach (var ft_loopVariable in syntax.FileTypes) {
                var ft = ft_loopVariable;
                ListViewItem lvi = lvFileTypes.Items.Add(string.Format("{0}   ({1})", ft.Name, ft.Extension));
                lvi.Tag = ft;
                lvi.ImageIndex = 0;
            }
        }

        private void OK()
        {
            if ((lvFileTypes.SelectedItems.Count == 0)) {
                lvFileTypes.Items[0].Selected = true;
            }

            SyntaxDefinition syntax = (SyntaxDefinition)trvFileTypes.SelectedNode.Tag;
            var ft = (FileType)lvFileTypes.SelectedItems[0].Tag;
            var doc = new Document("", "", "");
            doc.Title = ("Untitled" + ft.Extension);
            doc.Path = Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments + "\\";
            string DocName = "Document";
            doc.DocumentText = "";
            doc.Path = doc.Path + DocName + ft.Extension;
            doc.Title = doc.Path;
            EditForm ef = new EditForm(doc.Title, doc.Path, doc.DocumentText, syntax, ft);
            EditForm = ef;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvFileTypes_DoubleClick(object sender, EventArgs e)
        {
            OK();
        }

        #region " Windows Form Designer generated code "

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of me method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageForm));
            this.lvFileTypes = new System.Windows.Forms.ListView();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.trvFileTypes = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            //
            //lvFileTypes
            //
            this.lvFileTypes.HideSelection = false;
            this.lvFileTypes.LargeImageList = this.imlIcons;
            this.lvFileTypes.Location = new System.Drawing.Point(176, 8);
            this.lvFileTypes.Name = "lvFileTypes";
            this.lvFileTypes.Size = new System.Drawing.Size(272, 345);
            this.lvFileTypes.SmallImageList = this.imlIcons;
            this.lvFileTypes.TabIndex = 0;
            this.lvFileTypes.UseCompatibleStateImageBehavior = false;
            //
            //imlIcons
            //
            this.imlIcons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imlIcons.ImageStream");
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcons.Images.SetKeyName(0, "");
            //
            //btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(285, 359);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            //
            //btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(373, 359);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            //
            //trvFileTypes
            //
            this.trvFileTypes.HideSelection = false;
            this.trvFileTypes.Location = new System.Drawing.Point(8, 8);
            this.trvFileTypes.Name = "trvFileTypes";
            this.trvFileTypes.Size = new System.Drawing.Size(168, 345);
            this.trvFileTypes.TabIndex = 3;
            //
            //LanguageForm
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(458, 394);
            this.ControlBox = false;
            this.Controls.Add(this.trvFileTypes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvFileTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LanguageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a Language";
            this.ResumeLayout(false);

        }

        #endregion

    }
}
