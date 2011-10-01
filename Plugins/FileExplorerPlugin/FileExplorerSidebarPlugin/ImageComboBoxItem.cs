namespace FileExplorerPlugin
{
	internal class ImageComboBoxItem
	{
		private string _text;
		private int _imageIndex;
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}
		public int ImageIndex
		{
			get
			{
				return this._imageIndex;
			}
			set
			{
				this._imageIndex = value;
			}
		}
		public ImageComboBoxItem() : this("")
		{
		}
		public ImageComboBoxItem(string text) : this(text, -1)
		{
		}
		public ImageComboBoxItem(string text, int imageIndex)
		{
			this._text = text;
			this._imageIndex = imageIndex;
		}
		public override string ToString()
		{
			return this._text;
		}
	}
}
