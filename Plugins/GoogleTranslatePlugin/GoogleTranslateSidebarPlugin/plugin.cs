using NotepadX;
//using SideBarPlugin;
using System;
using System.Windows.Forms;
namespace GoogleTranslateSidebarPlugin
{
	public class plugin : IPlugin
    {
        private Translate PluginContainer;
        private WeifenLuo.WinFormsUI.Docking.DockContent Form;

        public string Name
        {
            get
            {
                return "Google Translate Plugin";
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
                return "Google Translate Plugin";
            }
        }
        public string Version
        {
            get
            {
                return "1.0";
            }
        }
        public ToolStripMenuItem ToolStripItem
        {
            get
            {
                return _toolitem;
            }
        }
        private ToolStripMenuItem _toolitem;

        public string DownloadURL
        {
            get
            {
                return "http://elijah.awesome99.org/GoogleTranslatePlugin.dll";
            }
        }
        public string OriginalFileName
        {
            get
            {
                return DownloadURL;
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
                {
                    _toolitem = new ToolStripMenuItem("Google Translate");
                    this.Form = new WeifenLuo.WinFormsUI.Docking.DockContent();
                    Form.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
                    Form.Text = "Translator";
                    this.PluginContainer = new Translate();
                    this.PluginContainer.Text = "Google Translate";
                    this.PluginContainer.Name = "Google Translate";
                    this.PluginContainer.Dock = DockStyle.Fill;
                    Form.Controls.Add(this.PluginContainer);
                    _toolitem.Click += new EventHandler(_toolitem_click);
                }
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            try
            {
                _toolitem.Dispose();
                Form.Dispose();
            }
            catch
            {
            }
        }

        private void _toolitem_click(object sender, EventArgs e)
        {
            try
            {
                Form.Show(NotepadX.Main.MDIParent1.DockPanel1);
            }
            catch
            {
                this.Form = new WeifenLuo.WinFormsUI.Docking.DockContent();
                Form.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
                Form.Text = "Translator";
                this.PluginContainer = new Translate();
                this.PluginContainer.Text = "Google Translate";
                this.PluginContainer.Name = "Google Translate";
                this.PluginContainer.Dock = DockStyle.Fill;
                Form.Controls.Add(this.PluginContainer);

                Form.Show(NotepadX.Main.MDIParent1.DockPanel1);
            }
        }

        public int Index
        {
            get
            {
                return 4;
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
        public bool HasOptionsPage
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
        public bool HasHelpPage
        {
            get
            {
                return false;
            }
        }
        public TabPage HelpPage
        {
            get { return null; }
        }
    }
	
}
