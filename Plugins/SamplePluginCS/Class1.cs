using System.Windows.Forms;
namespace SamplePluginCS 
{
    public class Class1 : NotepadX.IPlugin
    {
        private System.Windows.Forms.ToolStripMenuItem _toolstripitem ;
        public string Author
        {
            //your name.
            get { return "your name"; }
        }

        public string Description
        {
            //the Plugins description
            get { return "Description"; }
        }

        public void Dispose()
        {
            //clean up the plugin and end
            _toolstripitem.Dispose(); 
        }

        public string DownloadURL
        {
            //Returns the URL of the actual dll
            get { return ""; }
        }

        public void Initialize()
        {
            //create the plugin Instance
            _toolstripitem = new System.Windows.Forms.ToolStripMenuItem("SamplePluginCS");
        }

        public string Name
        {
            // The plugins name
            get { return "SamplePluginCS"; }
        }

        public string OriginalFileName
        {
            // the file of the DLL.
            get { return DownloadURL; }
        }

        public System.Windows.Forms.ToolStripMenuItem ToolStripItem
        {
            // the ToolStripMenuItem used in Notepad X
            get { return _toolstripitem; }
        }

        public string Version
        {
            // the version of this plugin
            get { return "v1.0"; }
        }

        public string MenuItemPath
        { get { return "new/Sample Plugins"; } }

        public int Index
        {
            get
            {
                return 2;
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
