using NotepadX;
using System;
using System.Windows.Forms;
namespace FileExplorerPlugin
{
    public class plugin : NotepadX.IPlugin
    {
        public bool FRM_Closed;
        private Explorer PluginContainer;
        public ToolStripMenuItem _toolbar;

        public string OriginalFileName
        {
            get
            {
                return DownloadURL;
            }
        }

        public string Name
        {
            get
            {
                return "File Explorer Plugin";
            }
        }
        public string Author
        {
            get
            {
                return "Elijah Frederickson";
            }
        }
        public string Description
        {
            get
            {
                return "A simple File Explorer";
            }
        }
        public string Version
        {
            get
            {
                return "1.5";
            }
        }
        public string DownloadURL
        {
            get
            {
                return "http://elijah.awesome99.org/FileExplorerPlugin.dll";
            }
        }
        public ToolStripMenuItem ToolStripItem
        {
            get
            {
                return _toolbar;
            }
        }
        public string MenuItemPath
        {
            get
            {
                return "tools/";
            }
        }

        public void Initialize()
        {
            try
            {
                FRM_Closed = true;
                PluginContainer = new Explorer();
                _toolbar = new ToolStripMenuItem("File Explorer");
                _toolbar.Click += item__Click;
            }
            catch(Exception e)
            {
                MessageBox.Show("File Explorer Plugin Error: " + e.ToString(),"Notepad X File Explorer Plugin");
            }
        }

        public void Dispose()
        {
            try
            {
                _toolbar.Dispose();
            }
            catch
            {
            }
        }

        public void item__Click(object sender, EventArgs e)
        {
            if (FRM_Closed)
            {
                WeifenLuo.WinFormsUI.Docking.DockContent frm =
                new WeifenLuo.WinFormsUI.Docking.DockContent();
                frm.Text = "File Explorer";
                //frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                frm.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
                //frm.TopMost = true;
                PluginContainer = new Explorer()
                { Parent = frm, Dock = DockStyle.Fill };
                frm.Show(NotepadX.Main.MDIParent1.DockPanel1);
                frm.FormClosing += frm_FormClosing;
                FRM_Closed = false;
            }
            else
            {
                MessageBox.Show("FileExplorer is already running!",
                                "Notepad X - File Explorer Plugin", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        void frm_FormClosing(object sender, EventArgs e)
        {
            FRM_Closed = true;
        }

        public int Index
        {
            get
            {
                return 3;
            }
        }

        public TabPage AboutPage
        {
            get
            {
                return null;
            }
        }
        public bool HasAboutPage
        {
            get
            {
                return false;
            }
        }

        public TabPage OptionsPage
        {
            get
            {
                return null;
            }
        }
        public TabPage HelpPage
        {
            get
            {
                return null;
            }
        }
        public bool HasOptionsPage
        {
            get
            {
                return false;
            }
        }
        public bool HasHelpPage
        {
            get
            {
                return false;
            }
        }
    }
}
