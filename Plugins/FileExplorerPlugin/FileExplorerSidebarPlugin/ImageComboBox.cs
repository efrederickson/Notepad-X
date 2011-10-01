using System.Drawing;
using System.Windows.Forms;
namespace FileExplorerPlugin
{
	internal class ImageComboBox : ComboBox
	{
		private ImageList imageList;
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				this.imageList = value;
			}
		}
		public ImageComboBox()
		{
			base.DrawMode = DrawMode.OwnerDrawFixed;
		}
		protected override void OnDrawItem(DrawItemEventArgs ea)
		{
			ea.DrawBackground();
			ea.DrawFocusRectangle();
			Size imageSize = this.imageList.ImageSize;
			Rectangle bounds = ea.Bounds;
			try
			{
				ImageComboBoxItem imageComboBoxItem = (ImageComboBoxItem)base.Items[ea.Index];
				if (imageComboBoxItem.ImageIndex != -1)
				{
					this.imageList.Draw(ea.Graphics, bounds.Left, bounds.Top, imageComboBoxItem.ImageIndex);
					ea.Graphics.DrawString(imageComboBoxItem.Text, ea.Font, new SolidBrush(ea.ForeColor), (float)(bounds.Left + imageSize.Width), (float)bounds.Top);
				}
				else
				{
					ea.Graphics.DrawString(imageComboBoxItem.Text, ea.Font, new SolidBrush(ea.ForeColor), (float)bounds.Left, (float)bounds.Top);
				}
			}
			catch
			{
				if (ea.Index != -1)
				{
					ea.Graphics.DrawString(base.Items[ea.Index].ToString(), ea.Font, new SolidBrush(ea.ForeColor), (float)bounds.Left, (float)bounds.Top);
				}
				else
				{
					ea.Graphics.DrawString(this.Text, ea.Font, new SolidBrush(ea.ForeColor), (float)bounds.Left, (float)bounds.Top);
				}
			}
			base.OnDrawItem(ea);
		}
	}
}
