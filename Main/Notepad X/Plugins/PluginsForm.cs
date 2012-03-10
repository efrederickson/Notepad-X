/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/29/2011
 * Time: 3:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NotepadX.Plugins
{
    /// <summary>
    /// Description of PluginsForm.
    /// </summary>
    public partial class PluginsForm : Form
    {
        public PluginsForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
        }
        
        void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Plugins|*.dll;*.exe|Packages|*.pack";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.ToLower().EndsWith(".pack"))
                {
                    CheckPack(ofd.FileName);
                }
                else
                {
                    File.Copy(ofd.FileName, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Plugins");
                    MainForm.PluginManager.AddPlugin(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Plugins\\" + Path.GetFileName(ofd.FileName));
                }
            }
        }
        
        void CheckPack(string filename)
        {
            string[] filenames = IExtendFramework.IO.Compression.Packages.Packages.ReadEntries(filename);
            foreach (string f in filenames)
            {
                if (f.ToLower().EndsWith(".dll") || f.ToLower().EndsWith(".exe"))
                {
                    byte[] b = IExtendFramework.IO.Compression.Packages.Packages.GetEntry(filename, f);
                    string newFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Notepad X\\Plugins\\" + Path.GetFileName(f);
                    FileStream newFile = new FileStream(newFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    newFile.Write(b, 0, b.Length);
                    newFile.Close();
                    MainForm.PluginManager.AddPlugin(newFileName);
                }
            }
        }
        
        void PluginsForm_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }
        
        void UpdateUI()
        {
            listView1.Items.Clear();
            foreach (AvailablePlugin ap in MainForm.PluginManager.AvailablePlugins)
            {
                if (ap.Instance != null)
                {
                    listView1.Items.Add(new ListViewItem(Path.GetFileNameWithoutExtension(ap.AssemblyPath)) { Tag = ap });
                }
            }
        }
        
        void Button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            bool b = (listView1.SelectedItems[0].Tag as AvailablePlugin).Instance.Initialize();
            if (!b)
                MessageBox.Show("Error starting plugin!");
        }
        
        void Button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            bool b = (listView1.SelectedItems[0].Tag as AvailablePlugin).Instance.Dispose();
            if (!b)
                MessageBox.Show("Error stopping plugin!");
        }
        
        void Button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            Program.FilesToDelete.Add((listView1.SelectedItems[0].Tag as AvailablePlugin).AssemblyPath);
        }
        
        void Button3_Click(object sender, EventArgs e)
        {
            //TODO
            MessageBox.Show("Not yet implemented!");
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            AvailablePlugin p = (AvailablePlugin) listView1.SelectedItems[0].Tag;
            authorLabel.Text = "Author: " + p.Instance.Author;
            descriptionLabel.Text = "Description: " + p.Instance.Description;
            versionLabel.Text = "Version: " + p.Instance.Version;
            nameLabel.Text = "Name: " + p.Instance.Name;
            updateUrlLabel.Text = "Update URL: " + p.Instance.UpdateUrl;
        }
    }
}
