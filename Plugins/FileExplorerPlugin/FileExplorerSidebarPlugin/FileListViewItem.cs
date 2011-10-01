using System;
using System.Windows.Forms;
namespace FileExplorerPlugin
{
	internal class FileListViewItem : ListViewItem
	{
		public FileListViewItem(string text)
		{
			base.Text = text;
		}
	}
}
