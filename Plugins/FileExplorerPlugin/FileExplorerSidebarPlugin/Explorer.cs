using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace FileExplorerPlugin
{
    public class Explorer : UserControl
    {
        private ImageComboBox systemDrives;
        private string CurrentDirectory;
        private IContainer components;
        private SplitContainer splitContainer1;
        private ToolStrip toolStrip1;
        private ToolStripButton _Open;
        private ListView fileList;
        private ImageList iconsList;
        private ImageList imageDriverList;
        private ContextMenuStrip _context;
        private ToolStripMenuItem _MenuOpen;
        private ToolStripMenuItem _MenuDelete;
        private ToolStripButton _Delete;
        private ToolStripSeparator toolStripSeparator1;
        public Explorer()
        {
            this.InitializeComponent();
            this.systemDrives = new ImageComboBox();
            this.splitContainer1.Panel1.Controls.Add(this.systemDrives);
            this.systemDrives.Dock = DockStyle.Fill;
            this.systemDrives.ImageList = this.imageDriverList;
            this.systemDrives.FormattingEnabled = true;
            this.systemDrives.Location = new Point(0, 0);
            this.systemDrives.DropDownStyle = ComboBoxStyle.DropDownList;
            this.systemDrives.Name = "systemDrives";
            this.systemDrives.TabIndex = 0;
            this.systemDrives.Size = new Size(180, 21);
            this.systemDrives.SelectedValueChanged += new EventHandler(this.systemDrives_SelectedValueChanged);
        }
        private void systemDrives_SelectedValueChanged(object sender, EventArgs e)
        {
            this.CurrentDirectory = this.systemDrives.SelectedItem.ToString();
            this.UpdateDirectory();
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            for (int i = 0; i < drives.Length; i++)
            {
                DriveInfo driveInfo = drives[i];
                if (driveInfo.IsReady)
                {
                    int imageIndex;
                    switch (driveInfo.DriveType)
                    {
                        case DriveType.Removable:
                        {
                            imageIndex = 0;
                            break;
                        }
                        case DriveType.Fixed:
                        {
                            imageIndex = 0;
                            break;
                        }
                        case DriveType.Network:
                        {
                            goto IL_45;
                        }
                        case DriveType.CDRom:
                        {
                            imageIndex = 1;
                            break;
                        }
                        default:
                        {
                            goto IL_45;
                        }
                    }
                    IL_47:
                    this.systemDrives.Items.Add(new ImageComboBoxItem(driveInfo.RootDirectory.ToString(), imageIndex));
                    goto IL_69;
                    IL_45:
                    imageIndex = 0;
                    goto IL_47;
                }
                IL_69:;
            }
            this._Open.Text = (this._MenuOpen.Text = (this._MenuOpen.ToolTipText ="Open"));
            this._Delete.Text = (this._MenuDelete.Text = (this._MenuDelete.ToolTipText = "Delete"));
            this._Open.Image = (this._MenuOpen.Image = null);
            this._Delete.Image = (this._MenuDelete.Image = null);
        }
        private void UpdateDirectory()
        {
            this.fileList.Items.Clear();
            if (this.CurrentDirectory != Directory.GetDirectoryRoot(this.CurrentDirectory))
            {
                FolderListViewItem folderListViewItem = new FolderListViewItem("..");
                this.fileList.Items.Add(folderListViewItem);
                folderListViewItem.ImageKey = "folder";
            }
            string[] directories = Directory.GetDirectories(this.CurrentDirectory);
            for (int i = 0; i < directories.Length; i++)
            {
                string path = directories[i];
                FolderListViewItem folderListViewItem2 = new FolderListViewItem(new DirectoryInfo(path).Name);
                this.fileList.Items.Add(folderListViewItem2);
                folderListViewItem2.ImageKey = "folder";
            }
            string[] files = Directory.GetFiles(this.CurrentDirectory);
            for (int j = 0; j < files.Length; j++)
            {
                string path2 = files[j];
                FileListViewItem fileListViewItem = new FileListViewItem(Path.GetFileName(path2));
                this.fileList.Items.Add(fileListViewItem);
                fileListViewItem.ImageKey = "file";
            }
        }
        private void OpenListItem()
        {
            try
            {
                if (this.fileList.SelectedItems[0].GetType() == typeof(FolderListViewItem))
                {
                    if (this.fileList.SelectedItems[0].Text == "..")
                    {
                        this.CurrentDirectory = Directory.GetParent(this.CurrentDirectory).FullName;
                    }
                    else
                    {
                        this.CurrentDirectory = this.CurrentDirectory + "\\" + this.fileList.SelectedItems[0].Text;
                    }
                    this.UpdateDirectory();
                    return;
                }
                if (this.fileList.SelectedItems[0].GetType() == typeof(FileListViewItem))
                {
                    //Static.MainInstance.system_open(this.CurrentDirectory + "\\" + this.fileList.SelectedItems[0].Text);
                    NotepadX.Main.MDIParent1.OPEN(this.CurrentDirectory + "\\" + this.fileList.SelectedItems[0].Text);
                }
            }
            catch
            {
                MessageBox.Show("Error Loading Folder");
            }
        }
        private void fileList_ItemActivate(object sender, EventArgs e)
        {
            this.OpenListItem();
        }
        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabled = true;
            if (this.fileList.SelectedItems.Count == 0)
            {
                enabled = false;
            }
            this._MenuOpen.Enabled = enabled;
            this._Open.Enabled = enabled;
            this._MenuDelete.Enabled = enabled;
            this._Delete.Enabled = enabled;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fileList.SelectedItems.Count > 0)
            {
                this.OpenListItem();
            }
        }
        private void _MenuDelete_Click(object sender, EventArgs e)
        {
            if (this.fileList.SelectedItems[0].GetType() == typeof(FolderListViewItem))
            {
                MessageBox.Show(this.fileList.SelectedItems[0].Text);
                try
                {
                    Directory.Delete(Path.Combine(this.CurrentDirectory, this.fileList.SelectedItems[0].Text), true);
                    this.fileList.Items.Remove(this.fileList.SelectedItems[0]);
                    this.fileList.Update();
                    return;
                }
                catch (Exception)
                {
                    return;
                }
            }
            if (this.fileList.SelectedItems[0].GetType() == typeof(FileListViewItem))
            {
                try
                {
                    File.Delete(Path.Combine(this.CurrentDirectory, this.fileList.SelectedItems[0].Text));
                    int index = this.fileList.SelectedItems[0].Index;
                    this.fileList.Items.Remove(this.fileList.SelectedItems[0]);
                    this.fileList.Update();
                    this.fileList.RedrawItems(index - 1, this.fileList.Items.Count - 1, false);
                }
                catch (Exception)
                {
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new Container();
            //ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Explorer));
            this.splitContainer1 = new SplitContainer();
            this.fileList = new ListView();
            this._context = new ContextMenuStrip(this.components);
            this._MenuOpen = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this._MenuDelete = new ToolStripMenuItem();
            this.iconsList = new ImageList(this.components);
            this.toolStrip1 = new ToolStrip();
            this._Open = new ToolStripButton();
            this._Delete = new ToolStripButton();
            this.imageDriverList = new ImageList(this.components);
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._context.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1MinSize = 20;
            this.splitContainer1.Panel2.Controls.Add(this.fileList);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new Size(180, 361);
            this.splitContainer1.SplitterDistance = 24;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            this.fileList.AutoArrange = false;
            this.fileList.BorderStyle = BorderStyle.None;
            this.fileList.ContextMenuStrip = this._context;
            this.fileList.Dock = DockStyle.Fill;
            this.fileList.HideSelection = false;
            this.fileList.LargeImageList = this.iconsList;
            this.fileList.Location = new Point(0, 0);
            this.fileList.Name = "fileList";
            this.fileList.ShowGroups = false;
            this.fileList.Size = new Size(180, 311);
            this.fileList.SmallImageList = this.iconsList;
            this.fileList.TabIndex = 1;
            this.fileList.TileSize = new Size(168, 16);
            this.fileList.UseCompatibleStateImageBehavior = false;
            this.fileList.View = View.Tile;
            this.fileList.ItemActivate += new EventHandler(this.fileList_ItemActivate);
            this.fileList.SelectedIndexChanged += new EventHandler(this.fileList_SelectedIndexChanged);
            this._context.Items.AddRange(new ToolStripItem[]
            {
                this._MenuOpen, 
                this.toolStripSeparator1, 
                this._MenuDelete
            });
            this._context.Name = "_context";
            this._context.Size = new Size(108, 54);
            this._MenuOpen.Font = new Font("Segoe UI", 9f, FontStyle.Bold, GraphicsUnit.Point, 161);
            this._MenuOpen.Name = "_MenuOpen";
            this._MenuOpen.Size = new Size(107, 22);
            this._MenuOpen.Text = "Open";
            this._MenuOpen.Click += new EventHandler(this.openToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(104, 6);
            this._MenuDelete.Name = "_MenuDelete";
            this._MenuDelete.Size = new Size(107, 22);
            this._MenuDelete.Text = "Delete";
            this._MenuDelete.Click += new EventHandler(this._MenuDelete_Click);
            this.iconsList.ColorDepth = ColorDepth.Depth32Bit;
            this.iconsList.ImageSize = new Size(16, 16);
            this.iconsList.TransparentColor = Color.Transparent;
            this.toolStrip1.Dock = DockStyle.Bottom;
            this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new ToolStripItem[]
            {
                this._Open, 
                this._Delete
            });
            this.toolStrip1.Location = new Point(0, 311);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = ToolStripRenderMode.System;
            this.toolStrip1.Size = new Size(180, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this._Open.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._Open.Image = Image.FromStream(GetEmbeddedStream("_Open.Image.png"));
            this._Open.ImageTransparentColor = Color.Magenta;
            this._Open.Name = "_Open";
            this._Open.Size = new Size(23, 22);
            this._Open.Text = "Open";
            this._Open.Click += new EventHandler(this.openToolStripMenuItem_Click);
            this._Delete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._Delete.Image = Image.FromStream(GetEmbeddedStream("_Delete.Image.png"));
            this._Delete.ImageTransparentColor = Color.Magenta;
            this._Delete.Name = "_Delete";
            this._Delete.Size = new Size(23, 22);
            this._Delete.Text = "toolStripButton1";
            this._Delete.Click += new EventHandler(this._MenuDelete_Click);
            //this.imageDriverList.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageDriverList.ImageStream");
            this.imageDriverList.TransparentColor = Color.Transparent;
            //this.imageDriverList.Images.SetKeyName(0, "drive");
            //this.imageDriverList.Images.SetKeyName(1, "dvd.png");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.splitContainer1);
            base.Name = "Explorer";
            base.Size = new Size(180, 361);
            base.Load += new EventHandler(this.UserControl1_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this._context.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
        }
        private System.IO.Stream GetEmbeddedStream(string name)
        {
            // The name of the embedded resource often uses the project name as a prefix.
            // This is set in the project properties page in VS2008. 
            string embeddedName = String.Format("FileExplorerPlugin.{0}", name);
            var myself = System.Reflection.Assembly.GetExecutingAssembly();
            return myself.GetManifestResourceStream(embeddedName);
        }
    }
}
