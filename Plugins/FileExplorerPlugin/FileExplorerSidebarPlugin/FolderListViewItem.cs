using System;
using System.Windows.Forms;
namespace FileExplorerPlugin
{
	internal class FolderListViewItem : ListViewItem
	{
		public FolderListViewItem(string text)
		{
			base.Text = text;
		}
	}
}
