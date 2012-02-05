using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Alsing.SourceCode;
using Alsing.Windows.Forms;
namespace CodeEditor
{

    /// <summary>
    /// Summary description for EditForm.
    /// </summary>
    public class EditForm : WeifenLuo.WinFormsUI.Docking.DockContent, IExtendFramework.Text.ITextEditor
    {
        public SyntaxDefinition Syntax;
        public FileType Ext;
        
        #region F0RMZ D3Z1GN3R
        IContainer components;
        public Document Doc;
        public SyntaxBoxControl sBox;
        private SyntaxDocument withEventsField_sDoc;
        public SyntaxDocument sDoc {
            get { return withEventsField_sDoc; }
            set {
                if (withEventsField_sDoc != null) {
                    withEventsField_sDoc.ModifiedChanged -= sDoc_ModifiedChanged;
                    withEventsField_sDoc.Change -= sDoc_Change;
                }
                withEventsField_sDoc = value;
                if (withEventsField_sDoc != null) {
                    withEventsField_sDoc.ModifiedChanged += sDoc_ModifiedChanged;
                    withEventsField_sDoc.Change += sDoc_Change;
                }
            }
        }
        StatusBar statusBar1;
        StatusBarPanel statusBarPanel1;
        StatusBarPanel statusBarPanel2;
        StatusBarPanel statusBarPanel3;
        SyntaxDocument syntaxDocument1;
        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_NewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem {
            get { return withEventsField_NewToolStripMenuItem; }
            set {
                if (withEventsField_NewToolStripMenuItem != null) {
                    withEventsField_NewToolStripMenuItem.Click -= NewToolStripMenuItem_Click;
                }
                withEventsField_NewToolStripMenuItem = value;
                if (withEventsField_NewToolStripMenuItem != null) {
                    withEventsField_NewToolStripMenuItem.Click += NewToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_OpenToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem {
            get { return withEventsField_OpenToolStripMenuItem; }
            set {
                if (withEventsField_OpenToolStripMenuItem != null) {
                    withEventsField_OpenToolStripMenuItem.Click -= OpenToolStripMenuItem_Click;
                }
                withEventsField_OpenToolStripMenuItem = value;
                if (withEventsField_OpenToolStripMenuItem != null) {
                    withEventsField_OpenToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_SaveToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem {
            get { return withEventsField_SaveToolStripMenuItem; }
            set {
                if (withEventsField_SaveToolStripMenuItem != null) {
                    withEventsField_SaveToolStripMenuItem.Click -= SaveToolStripMenuItem_Click;
                }
                withEventsField_SaveToolStripMenuItem = value;
                if (withEventsField_SaveToolStripMenuItem != null) {
                    withEventsField_SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_SaveAsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem {
            get { return withEventsField_SaveAsToolStripMenuItem; }
            set {
                if (withEventsField_SaveAsToolStripMenuItem != null) {
                    withEventsField_SaveAsToolStripMenuItem.Click -= SaveAsToolStripMenuItem_Click;
                }
                withEventsField_SaveAsToolStripMenuItem = value;
                if (withEventsField_SaveAsToolStripMenuItem != null) {
                    withEventsField_SaveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
                }
            }
        }
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_PrintSetupToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PrintSetupToolStripMenuItem {
            get { return withEventsField_PrintSetupToolStripMenuItem; }
            set {
                if (withEventsField_PrintSetupToolStripMenuItem != null) {
                    withEventsField_PrintSetupToolStripMenuItem.Click -= PrintSetupToolStripMenuItem_Click;
                }
                withEventsField_PrintSetupToolStripMenuItem = value;
                if (withEventsField_PrintSetupToolStripMenuItem != null) {
                    withEventsField_PrintSetupToolStripMenuItem.Click += PrintSetupToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_PrintToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PrintToolStripMenuItem {
            get { return withEventsField_PrintToolStripMenuItem; }
            set {
                if (withEventsField_PrintToolStripMenuItem != null) {
                    withEventsField_PrintToolStripMenuItem.Click -= PrintToolStripMenuItem_Click;
                }
                withEventsField_PrintToolStripMenuItem = value;
                if (withEventsField_PrintToolStripMenuItem != null) {
                    withEventsField_PrintToolStripMenuItem.Click += PrintToolStripMenuItem_Click;
                }
            }
        }
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_UndoToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem {
            get { return withEventsField_UndoToolStripMenuItem; }
            set {
                if (withEventsField_UndoToolStripMenuItem != null) {
                    withEventsField_UndoToolStripMenuItem.Click -= UndoToolStripMenuItem_Click;
                }
                withEventsField_UndoToolStripMenuItem = value;
                if (withEventsField_UndoToolStripMenuItem != null) {
                    withEventsField_UndoToolStripMenuItem.Click += UndoToolStripMenuItem_Click;
                }
            }
        }
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_CutToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CutToolStripMenuItem {
            get { return withEventsField_CutToolStripMenuItem; }
            set {
                if (withEventsField_CutToolStripMenuItem != null) {
                    withEventsField_CutToolStripMenuItem.Click -= CutToolStripMenuItem_Click;
                }
                withEventsField_CutToolStripMenuItem = value;
                if (withEventsField_CutToolStripMenuItem != null) {
                    withEventsField_CutToolStripMenuItem.Click += CutToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_CopyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem {
            get { return withEventsField_CopyToolStripMenuItem; }
            set {
                if (withEventsField_CopyToolStripMenuItem != null) {
                    withEventsField_CopyToolStripMenuItem.Click -= CopyToolStripMenuItem_Click;
                }
                withEventsField_CopyToolStripMenuItem = value;
                if (withEventsField_CopyToolStripMenuItem != null) {
                    withEventsField_CopyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_PasteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem {
            get { return withEventsField_PasteToolStripMenuItem; }
            set {
                if (withEventsField_PasteToolStripMenuItem != null) {
                    withEventsField_PasteToolStripMenuItem.Click -= PasteToolStripMenuItem_Click;
                }
                withEventsField_PasteToolStripMenuItem = value;
                if (withEventsField_PasteToolStripMenuItem != null) {
                    withEventsField_PasteToolStripMenuItem.Click += PasteToolStripMenuItem_Click;
                }
            }
        }
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_SelectAllToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem {
            get { return withEventsField_SelectAllToolStripMenuItem; }
            set {
                if (withEventsField_SelectAllToolStripMenuItem != null) {
                    withEventsField_SelectAllToolStripMenuItem.Click -= SelectAllToolStripMenuItem_Click;
                }
                withEventsField_SelectAllToolStripMenuItem = value;
                if (withEventsField_SelectAllToolStripMenuItem != null) {
                    withEventsField_SelectAllToolStripMenuItem.Click += SelectAllToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_TimeDateToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem TimeDateToolStripMenuItem {
            get { return withEventsField_TimeDateToolStripMenuItem; }
            set {
                if (withEventsField_TimeDateToolStripMenuItem != null) {
                    withEventsField_TimeDateToolStripMenuItem.Click -= TimeDateToolStripMenuItem_Click;
                }
                withEventsField_TimeDateToolStripMenuItem = value;
                if (withEventsField_TimeDateToolStripMenuItem != null) {
                    withEventsField_TimeDateToolStripMenuItem.Click += TimeDateToolStripMenuItem_Click;
                }
            }
        }
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_FindToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem FindToolStripMenuItem {
            get { return withEventsField_FindToolStripMenuItem; }
            set {
                if (withEventsField_FindToolStripMenuItem != null) {
                    withEventsField_FindToolStripMenuItem.Click -= FindToolStripMenuItem_Click;
                }
                withEventsField_FindToolStripMenuItem = value;
                if (withEventsField_FindToolStripMenuItem != null) {
                    withEventsField_FindToolStripMenuItem.Click += FindToolStripMenuItem_Click;
                }
            }
        }
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_ShowTABCharactersToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ShowTABCharactersToolStripMenuItem {
            get { return withEventsField_ShowTABCharactersToolStripMenuItem; }
            set {
                if (withEventsField_ShowTABCharactersToolStripMenuItem != null) {
                    withEventsField_ShowTABCharactersToolStripMenuItem.Click -= ShowTABCharactersToolStripMenuItem_Click;
                }
                withEventsField_ShowTABCharactersToolStripMenuItem = value;
                if (withEventsField_ShowTABCharactersToolStripMenuItem != null) {
                    withEventsField_ShowTABCharactersToolStripMenuItem.Click += ShowTABCharactersToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_ShowTabGuidesToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ShowTabGuidesToolStripMenuItem {
            get { return withEventsField_ShowTabGuidesToolStripMenuItem; }
            set {
                if (withEventsField_ShowTabGuidesToolStripMenuItem != null) {
                    withEventsField_ShowTabGuidesToolStripMenuItem.Click -= ShowTabGuidesToolStripMenuItem_Click;
                }
                withEventsField_ShowTabGuidesToolStripMenuItem = value;
                if (withEventsField_ShowTabGuidesToolStripMenuItem != null) {
                    withEventsField_ShowTabGuidesToolStripMenuItem.Click += ShowTabGuidesToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_ShowEOLMarkersToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ShowEOLMarkersToolStripMenuItem {
            get { return withEventsField_ShowEOLMarkersToolStripMenuItem; }
            set {
                if (withEventsField_ShowEOLMarkersToolStripMenuItem != null) {
                    withEventsField_ShowEOLMarkersToolStripMenuItem.Click -= ShowEOLMarkersToolStripMenuItem_Click;
                }
                withEventsField_ShowEOLMarkersToolStripMenuItem = value;
                if (withEventsField_ShowEOLMarkersToolStripMenuItem != null) {
                    withEventsField_ShowEOLMarkersToolStripMenuItem.Click += ShowEOLMarkersToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_GotoLineToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem GotoLineToolStripMenuItem {
            get { return withEventsField_GotoLineToolStripMenuItem; }
            set {
                if (withEventsField_GotoLineToolStripMenuItem != null) {
                    withEventsField_GotoLineToolStripMenuItem.Click -= GotoLineToolStripMenuItem_Click;
                }
                withEventsField_GotoLineToolStripMenuItem = value;
                if (withEventsField_GotoLineToolStripMenuItem != null) {
                    withEventsField_GotoLineToolStripMenuItem.Click += GotoLineToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_ReplaceToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ReplaceToolStripMenuItem {
            get { return withEventsField_ReplaceToolStripMenuItem; }
            set {
                if (withEventsField_ReplaceToolStripMenuItem != null) {
                    withEventsField_ReplaceToolStripMenuItem.Click -= ReplaceToolStripMenuItem_Click;
                }
                withEventsField_ReplaceToolStripMenuItem = value;
                if (withEventsField_ReplaceToolStripMenuItem != null) {
                    withEventsField_ReplaceToolStripMenuItem.Click += ReplaceToolStripMenuItem_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_ShowScopeIndicatorToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ShowScopeIndicatorToolStripMenuItem {
            get { return withEventsField_ShowScopeIndicatorToolStripMenuItem; }
            set {
                if (withEventsField_ShowScopeIndicatorToolStripMenuItem != null) {
                    withEventsField_ShowScopeIndicatorToolStripMenuItem.Click -= ShowScopeIndicatorToolStripMenuItem_Click;
                }
                withEventsField_ShowScopeIndicatorToolStripMenuItem = value;
                if (withEventsField_ShowScopeIndicatorToolStripMenuItem != null) {
                    withEventsField_ShowScopeIndicatorToolStripMenuItem.Click += ShowScopeIndicatorToolStripMenuItem_Click;
                }
            }
        }
        internal System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_DocumentSettingsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentSettingsToolStripMenuItem {
            get { return withEventsField_DocumentSettingsToolStripMenuItem; }
            set {
                if (withEventsField_DocumentSettingsToolStripMenuItem != null) {
                    withEventsField_DocumentSettingsToolStripMenuItem.Click -= DocumentSettingsToolStripMenuItem_Click;
                }
                withEventsField_DocumentSettingsToolStripMenuItem = value;
                if (withEventsField_DocumentSettingsToolStripMenuItem != null) {
                    withEventsField_DocumentSettingsToolStripMenuItem.Click += DocumentSettingsToolStripMenuItem_Click;
                }
            }
        }
        internal Alsing.Windows.Forms.CoreLib.IntelliMouseControl IntelliMouseControl1;
        private System.Windows.Forms.ToolStripMenuItem withEventsField_ExitToolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem1 {
            get { return withEventsField_ExitToolStripMenuItem1; }
            set {
                if (withEventsField_ExitToolStripMenuItem1 != null) {
                    withEventsField_ExitToolStripMenuItem1.Click -= ExitToolStripMenuItem1_Click;
                }
                withEventsField_ExitToolStripMenuItem1 = value;
                if (withEventsField_ExitToolStripMenuItem1 != null) {
                    withEventsField_ExitToolStripMenuItem1.Click += ExitToolStripMenuItem1_Click;
                }
            }
        }
        private System.Windows.Forms.ToolStripMenuItem withEventsField_ShowFaoldingToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ShowFaoldingToolStripMenuItem {
            get { return withEventsField_ShowFaoldingToolStripMenuItem; }
            set {
                if (withEventsField_ShowFaoldingToolStripMenuItem != null) {
                    withEventsField_ShowFaoldingToolStripMenuItem.Click -= ShowFaoldingToolStripMenuItem_Click;
                }
                withEventsField_ShowFaoldingToolStripMenuItem = value;
                if (withEventsField_ShowFaoldingToolStripMenuItem != null) {
                    withEventsField_ShowFaoldingToolStripMenuItem.Click += ShowFaoldingToolStripMenuItem_Click;
                }
            }

        }
        
        #endregion
        
        //create an EditForm and attach our opened document and tell the parser to use the given syntax.
        public EditForm(string title, string path, string documentText, SyntaxDefinition SyntaxDefinition, FileType ext)
        {
            Load += EditForm_Load;
            Closing += EditForm_Closing;
            InitializeComponent();

            this.Doc = new Document(title, path, documentText);
            sBox.Document = sDoc;
            sBox.Document.Parser.Init(SyntaxDefinition);
            this.Syntax = SyntaxDefinition;
            sBox.Document.Text = Doc.DocumentText;
            this.Text = Doc.Title;
            this.TabText = Doc.Title;
            this.Ext = ext;
            this.statusBar1.Visible = false;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if ((disposing)) {
                if ((components != null)) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        //event fired when the caret moves

        //event fired when the modified flag of the document changes (eg if you undo every change , the modified flag will be reset
        public void sDoc_ModifiedChanged(object sender, EventArgs e)
        {
            try {
                string s = "";
                if ((sDoc.Modified)) {
                    s = " *";
                }
                this.Text = Doc.Title + s;
                this.TabText = Doc.Title + s;
                statusBarPanel1.Text = "Undo buffer :" + Constants.vbTab + sDoc.UndoStep;
                //show number of steps in the undostack in one of the panels in the statusbar
            } catch (Exception) {
                //hmmm....
            }
        }

        //event fired when the content is modified
        public void sDoc_Change(object sender, EventArgs e)
        {
            statusBarPanel1.Text = "Undo buffer :" + Constants.vbTab + sDoc.UndoStep;
        }

        //save the content of the editor
        public void SaveAs(string FileName)
        {
            try {
                StreamWriter fs = new StreamWriter(FileName, false, Encoding.Default);
                fs.Write(sDoc.Text);
                fs.Flush();
                fs.Close();
            } catch (Exception x) {
                MessageBox.Show(x.Message);
            }
            sDoc.Modified = false;
            Doc.Title = System.IO.Path.GetFileName(Doc.Path);
            this.Text = Doc.Title;
        }

        //occurs when a form is about to be closed
        public void EditForm_Closing(object sender, CancelEventArgs e)
        {
            if ((sDoc.Modified)) {
                DialogResult res = MessageBox.Show(string.Format("Save changes to {0}?", Doc.Title), "Notepad X", MessageBoxButtons.YesNoCancel);

                if ((res == DialogResult.Cancel)) {
                    e.Cancel = true;
                    return;
                }
                if ((res == DialogResult.No)) {
                    e.Cancel = false;
                    return;
                }

                if ((res == DialogResult.Yes)) {
                    if ((!string.IsNullOrEmpty(Doc.Path))) {
                        SaveAs(Doc.Path);
                    } else {
                        SaveFileDialog savedialog = new SaveFileDialog();
                        if (savedialog.ShowDialog() == DialogResult.OK) {
                            SaveAs(savedialog.FileName);
                        }
                    }
                    e.Cancel = false;
                    return;
                }
            }
        }

        #region " Windows Form Designer generated code "

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
            this.sDoc = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.sBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.syntaxDocument1 = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GotoLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.FindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowTABCharactersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowTabGuidesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowFaoldingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowEOLMarkersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowScopeIndicatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.IntelliMouseControl1 = new Alsing.Windows.Forms.CoreLib.IntelliMouseControl();
            ((System.ComponentModel.ISupportInitialize)this.statusBarPanel1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.statusBarPanel2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.statusBarPanel3).BeginInit();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            //statusBar1
            //
            this.statusBar1.Location = new System.Drawing.Point(0, 471);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                this.statusBarPanel1,
                                                this.statusBarPanel2,
                                                this.statusBarPanel3
                                            });
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(504, 22);
            this.statusBar1.TabIndex = 1;
            //
            //statusBarPanel1
            //
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 200;
            //
            //statusBarPanel2
            //
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Width = 200;
            //
            //statusBarPanel3
            //
            this.statusBarPanel3.Name = "statusBarPanel3";
            //
            //sDoc
            //
            this.sDoc.Lines = new string[] { "abc" };
            this.sDoc.MaxUndoBufferSize = 1000;
            this.sDoc.Modified = false;
            this.sDoc.UndoStep = 0;
            //
            //sBox
            //
            this.sBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.sBox.AutoListPosition = null;
            this.sBox.AutoListSelectedText = "a123";
            this.sBox.AutoListVisible = false;
            this.sBox.BackColor = System.Drawing.Color.White;
            this.sBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.sBox.ChildBorderColor = System.Drawing.Color.White;
            this.sBox.ChildBorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.sBox.CopyAsRTF = true;
            this.sBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBox.Document = this.sDoc;
            this.sBox.FontName = "Courier new";
            this.sBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sBox.InfoTipCount = 1;
            this.sBox.InfoTipPosition = null;
            this.sBox.InfoTipSelectedIndex = 1;
            this.sBox.InfoTipVisible = false;
            this.sBox.Location = new System.Drawing.Point(0, 24);
            this.sBox.LockCursorUpdate = false;
            this.sBox.Name = "sBox";
            this.sBox.ScopeIndicatorColor = System.Drawing.Color.Black;
            this.sBox.Size = new System.Drawing.Size(504, 447);
            this.sBox.SmoothScroll = false;
            this.sBox.SplitviewH = -4;
            this.sBox.SplitviewV = -4;
            this.sBox.TabGuideColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(222)), Convert.ToInt32(Convert.ToByte(219)), Convert.ToInt32(Convert.ToByte(214)));
            this.sBox.TabIndex = 3;
            this.sBox.Text = "syntaxBoxControl1";
            this.sBox.TooltipDelay = 100;
            this.sBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            //
            //syntaxDocument1
            //
            this.syntaxDocument1.Lines = new string[] { "" };
            this.syntaxDocument1.MaxUndoBufferSize = 1000;
            this.syntaxDocument1.Modified = false;
            this.syntaxDocument1.UndoStep = 0;
            //
            //MenuStrip1
            //
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                               this.FileToolStripMenuItem,
                                               this.EditToolStripMenuItem,
                                               this.ToolsToolStripMenuItem,
                                               this.ExitToolStripMenuItem1
                                           });
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(504, 24);
            this.MenuStrip1.TabIndex = 4;
            this.MenuStrip1.Text = "MenuStrip1";
            //
            //FileToolStripMenuItem
            //
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                  this.NewToolStripMenuItem,
                                                                  this.OpenToolStripMenuItem,
                                                                  this.SaveToolStripMenuItem,
                                                                  this.SaveAsToolStripMenuItem,
                                                                  this.ToolStripSeparator1,
                                                                  this.PrintSetupToolStripMenuItem,
                                                                  this.PrintToolStripMenuItem,
                                                                  this.ToolStripSeparator2,
                                                                  this.ExitToolStripMenuItem
                                                              });
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "&File";
            //
            //NewToolStripMenuItem
            //
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.NewToolStripMenuItem.Text = "&New";
            //
            //OpenToolStripMenuItem
            //
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.OpenToolStripMenuItem.Text = "&Open";
            //
            //SaveToolStripMenuItem
            //
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.SaveToolStripMenuItem.Text = "&Save";
            //
            //SaveAsToolStripMenuItem
            //
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) | System.Windows.Forms.Keys.S);
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.SaveAsToolStripMenuItem.Text = "Save As";
            //
            //ToolStripSeparator1
            //
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            //
            //PrintSetupToolStripMenuItem
            //
            this.PrintSetupToolStripMenuItem.Name = "PrintSetupToolStripMenuItem";
            this.PrintSetupToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.PrintSetupToolStripMenuItem.Text = "Print Preview";
            //
            //PrintToolStripMenuItem
            //
            this.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            this.PrintToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.PrintToolStripMenuItem.Text = "&Print";
            //
            //ToolStripSeparator2
            //
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            //
            //ExitToolStripMenuItem
            //
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4);
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.ExitToolStripMenuItem.Text = "E&xit";
            //
            //EditToolStripMenuItem
            //
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                  this.UndoToolStripMenuItem,
                                                                  this.ToolStripSeparator3,
                                                                  this.CutToolStripMenuItem,
                                                                  this.CopyToolStripMenuItem,
                                                                  this.PasteToolStripMenuItem,
                                                                  this.ToolStripSeparator5,
                                                                  this.SelectAllToolStripMenuItem,
                                                                  this.TimeDateToolStripMenuItem,
                                                                  this.GotoLineToolStripMenuItem,
                                                                  this.ToolStripSeparator6,
                                                                  this.FindToolStripMenuItem,
                                                                  this.ReplaceToolStripMenuItem,
                                                                  this.ToolStripSeparator4,
                                                                  this.ShowTABCharactersToolStripMenuItem,
                                                                  this.ShowTabGuidesToolStripMenuItem,
                                                                  this.ShowFaoldingToolStripMenuItem,
                                                                  this.ShowEOLMarkersToolStripMenuItem,
                                                                  this.ShowScopeIndicatorToolStripMenuItem
                                                              });
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "&Edit";
            //
            //UndoToolStripMenuItem
            //
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z);
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.UndoToolStripMenuItem.Text = "Undo";
            //
            //ToolStripSeparator3
            //
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(185, 6);
            //
            //CutToolStripMenuItem
            //
            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
            this.CutToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X);
            this.CutToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.CutToolStripMenuItem.Text = "Cut";
            //
            //CopyToolStripMenuItem
            //
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C);
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.CopyToolStripMenuItem.Text = "Copy";
            //
            //PasteToolStripMenuItem
            //
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V);
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.PasteToolStripMenuItem.Text = "Paste";
            //
            //ToolStripSeparator5
            //
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(185, 6);
            //
            //SelectAllToolStripMenuItem
            //
            this.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem";
            this.SelectAllToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A);
            this.SelectAllToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.SelectAllToolStripMenuItem.Text = "Select All";
            //
            //TimeDateToolStripMenuItem
            //
            this.TimeDateToolStripMenuItem.Name = "TimeDateToolStripMenuItem";
            this.TimeDateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.TimeDateToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.TimeDateToolStripMenuItem.Text = "Time/Date";
            //
            //GotoLineToolStripMenuItem
            //
            this.GotoLineToolStripMenuItem.Name = "GotoLineToolStripMenuItem";
            this.GotoLineToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.GotoLineToolStripMenuItem.Text = "Goto Line";
            //
            //ToolStripSeparator6
            //
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(185, 6);
            //
            //FindToolStripMenuItem
            //
            this.FindToolStripMenuItem.Name = "FindToolStripMenuItem";
            this.FindToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F);
            this.FindToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.FindToolStripMenuItem.Text = "Find";
            //
            //ReplaceToolStripMenuItem
            //
            this.ReplaceToolStripMenuItem.Name = "ReplaceToolStripMenuItem";
            this.ReplaceToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R);
            this.ReplaceToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ReplaceToolStripMenuItem.Text = "Replace";
            //
            //ToolStripSeparator4
            //
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(185, 6);
            //
            //ShowTABCharactersToolStripMenuItem
            //
            this.ShowTABCharactersToolStripMenuItem.Name = "ShowTABCharactersToolStripMenuItem";
            this.ShowTABCharactersToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ShowTABCharactersToolStripMenuItem.Text = "Show TAB Characters";
            //
            //ShowTabGuidesToolStripMenuItem
            //
            this.ShowTabGuidesToolStripMenuItem.Name = "ShowTabGuidesToolStripMenuItem";
            this.ShowTabGuidesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ShowTabGuidesToolStripMenuItem.Text = "Show Tab Guides";
            //
            //ShowFaoldingToolStripMenuItem
            //
            this.ShowFaoldingToolStripMenuItem.Checked = true;
            this.ShowFaoldingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowFaoldingToolStripMenuItem.Name = "ShowFaoldingToolStripMenuItem";
            this.ShowFaoldingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ShowFaoldingToolStripMenuItem.Text = "Show Folding";
            //
            //ShowEOLMarkersToolStripMenuItem
            //
            this.ShowEOLMarkersToolStripMenuItem.Name = "ShowEOLMarkersToolStripMenuItem";
            this.ShowEOLMarkersToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ShowEOLMarkersToolStripMenuItem.Text = "Show EOL Markers";
            //
            //ShowScopeIndicatorToolStripMenuItem
            //
            this.ShowScopeIndicatorToolStripMenuItem.Name = "ShowScopeIndicatorToolStripMenuItem";
            this.ShowScopeIndicatorToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ShowScopeIndicatorToolStripMenuItem.Text = "Show Scope Indicator";
            //
            //ToolsToolStripMenuItem
            //
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.DocumentSettingsToolStripMenuItem });
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ToolsToolStripMenuItem.Text = "Tools";
            this.ToolsToolStripMenuItem.Visible = false;
            //
            //DocumentSettingsToolStripMenuItem
            //
            this.DocumentSettingsToolStripMenuItem.Name = "DocumentSettingsToolStripMenuItem";
            this.DocumentSettingsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.DocumentSettingsToolStripMenuItem.Text = "Document Settings";
            //
            //ExitToolStripMenuItem1
            //
            this.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1";
            this.ExitToolStripMenuItem1.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4);
            this.ExitToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.ExitToolStripMenuItem1.Text = "Exit";
            //
            //IntelliMouseControl1
            //
            this.IntelliMouseControl1.Image = null;
            this.IntelliMouseControl1.Location = new System.Drawing.Point(152, 117);
            this.IntelliMouseControl1.Name = "IntelliMouseControl1";
            this.IntelliMouseControl1.Size = new System.Drawing.Size(32, 32);
            this.IntelliMouseControl1.TabIndex = 5;
            this.IntelliMouseControl1.Text = "IntelliMouseControl1";
            this.IntelliMouseControl1.TransparencyKey = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(0)), Convert.ToInt32(Convert.ToByte(255)));
            //
            //EditForm
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(504, 493);
            this.Controls.Add(this.IntelliMouseControl1);
            this.Controls.Add(this.sBox);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.MenuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            this.Name = "EditForm";
            this.Text = "Edit form";
            ((System.ComponentModel.ISupportInitialize)this.statusBarPanel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.statusBarPanel2).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.statusBarPanel3).EndInit();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void NewToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.Text = "";
            Doc.DocumentText = "";
            Doc.Title = "";
            Doc.Path= "";
        }

        private void UndoToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.Undo();
        }

        private void CutToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.Cut();
        }

        private void CopyToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.Copy();
        }

        private void PasteToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.Paste();
        }

        private void SelectAllToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.SelectAll();
        }

        private void TimeDateToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            //sBox.Caret.Position
            TextPoint pos = sBox.Caret.Position;
            sDoc.InsertText(DateTime.Now.ToString(), pos.X, pos.Y);
        }

        private void FindToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.ShowFind();
        }

        private void OpenToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK) {
                sBox.Text = System.IO.File.ReadAllText(o.FileName);
            }
            Doc.Path = o.FileName;
        }

        private void SaveToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Doc.Path)) {
                SaveFileDialog s = new SaveFileDialog();
                if (s.ShowDialog() == DialogResult.OK) {
                    Doc.Path = s.FileName;
                }
            }
            SaveAs(Doc.Path);
        }

        private void SaveAsToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            Doc.Path = "";
            SaveToolStripMenuItem_Click(sender, e);
        }

        private void PrintSetupToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            var pd = new SourceCodePrintDocument(sDoc);
            PrintPreviewDialog dlgPrintPreview = new PrintPreviewDialog();
            dlgPrintPreview.Document = pd;
            dlgPrintPreview.ShowDialog(this);
        }

        private void PrintToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            var pd = new SourceCodePrintDocument(sDoc);
            PrintDialog dlgPrint = new PrintDialog();
            dlgPrint.Document = pd;
            if ((dlgPrint.ShowDialog(this) == DialogResult.OK)) {
                pd.Print();
            }
        }

        private void ShowTABCharactersToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.ShowWhitespace = !sBox.ShowWhitespace;
            ShowTABCharactersToolStripMenuItem.Checked = sBox.ShowWhitespace;
        }

        private void ShowTabGuidesToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.ShowTabGuides = !sBox.ShowTabGuides;
            ShowTabGuidesToolStripMenuItem.Checked = sBox.ShowTabGuides;
        }

        private void ShowFaoldingToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sDoc.Folding = !sDoc.Folding;
            ShowFaoldingToolStripMenuItem.Checked = sDoc.Folding;
        }

        private void ShowEOLMarkersToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.ShowEOLMarker = !sBox.ShowEOLMarker;
            ShowEOLMarkersToolStripMenuItem.Checked = sBox.ShowEOLMarker;
        }

        private void GotoLineToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.ShowGotoLine();
        }

        private void ReplaceToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.ShowReplace();
        }

        private void ShowScopeIndicatorToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            sBox.ShowScopeIndicator = !sBox.ShowScopeIndicator;
            ShowScopeIndicatorToolStripMenuItem.Checked = sBox.ShowScopeIndicator;
        }

        private void EditForm_Load(System.Object sender, System.EventArgs e)
        {
            this.MenuStrip1.SendToBack();
        }

        private void DocumentSettingsToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            //Dim settings As New SettingsForm(sBox)
            //settings.ShowDialog()
        }

        private void ExitToolStripMenuItem1_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        public WeifenLuo.WinFormsUI.Docking.DockContent DockingPanel {
            get { return this; }
        }

        public IExtendFramework.Text.IFileExtension Extension {
            get {
                // TODO: description, category
                return new FileExtension(Ext.Extension, "A(n) " + Ext.Name, "Programming");
            }
        }

        public string Filename {
            get {
                return Doc.Path;
            }
            set {
                this.Doc.Path = value;
                sBox.Text = System.IO.File.ReadAllText(value);
            }
        }

        public IExtendFramework.Text.IDocument CurrentDocument {
            get {
                throw new NotImplementedException();
            }
        }

        public int UndoBuffer {
            get {
                return sDoc.UndoStep;
            }
        }

        public IExtendFramework.Text.ITextEditor Create(string filename)
        {
            return new EditForm(filename, filename,File.Exists(filename) ? File.ReadAllText(filename) : "", Syntax, Ext);
        }

        public void Undo()
        {
            syntaxDocument1.Undo();
        }

        public void Redo()
        {
            syntaxDocument1.Redo();
        }

        public void SaveAs()
        {
            SaveAsToolStripMenuItem_Click(null, EventArgs.Empty);
        }

        public void PrintPreview()
        {
            throw new NotImplementedException();
        }

        public void PrintSetup()
        {
            throw new NotImplementedException();
        }

        public void Cut()
        {
            sBox.Cut();
        }

        public void Copy()
        {
            sBox.Copy();
        }

        public void Paste()
        {
            sBox.Paste();
        }

        public void Insert(int index, string text)
        {
            TextPoint pos = sBox.Caret.Position;
            sDoc.InsertText(text, index, index);
        }

        public void ChangeFont(System.Drawing.Font newFont)
        {
            sBox.Font = newFont;
        }

        public void ChangeColor(System.Drawing.Color newColor)
        {
            sBox.ForeColor = newColor;
        }

        public void Open(string filename)
        {
            Doc.Path = filename;
            sBox.Text = File.ReadAllText(filename);
        }

        public void SelectAll()
        {
            sBox.SelectAll();
        }
        
        public string DocumentText {
            get {
                return sDoc.Text;
            }
            set {
                sDoc.Text = value;
            }
        }
        
        public void Print()
        {
            PrintToolStripMenuItem_Click(null, EventArgs.Empty);
        }
        
        public void Save()
        {
            SaveToolStripMenuItem_Click(null, EventArgs.Empty);
        }
    }
}
