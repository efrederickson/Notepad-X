using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace GoogleTranslateSidebarPlugin
{
	public class Translate : UserControl
	{
		private IContainer components;
		private SplitContainer splitContainer1;
		private ComboBox to_Combo;
		private Label label3;
		private ComboBox from_Combo;
		private Label label2;
		private Label label1;
		private ToolStrip toolStrip1;
		private TextBox translatedText;
		private Button button1;
		//private Main main = Static.MainInstance;
		public Hashtable LanguageList = new Hashtable();
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
			this.splitContainer1 = new SplitContainer();
			this.button1 = new Button();
			this.to_Combo = new ComboBox();
			this.label3 = new Label();
			this.from_Combo = new ComboBox();
			this.label2 = new Label();
			this.label1 = new Label();
			this.translatedText = new TextBox();
			this.toolStrip1 = new ToolStrip();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.FixedPanel = FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = Orientation.Horizontal;
			this.splitContainer1.Panel1.Controls.Add(this.button1);
			this.splitContainer1.Panel1.Controls.Add(this.to_Combo);
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.from_Combo);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1MinSize = 93;
			this.splitContainer1.Panel2.Controls.Add(this.translatedText);
			this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
			this.splitContainer1.Size = new Size(180, 361);
			this.splitContainer1.SplitterDistance = 93;
			this.splitContainer1.TabIndex = 0;
			this.button1.Location = new Point(74, 70);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 22);
			this.button1.TabIndex = 10;
			this.button1.Text = "Translate";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.to_Combo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.to_Combo.FormattingEnabled = true;
			this.to_Combo.Location = new Point(46, 43);
			this.to_Combo.Name = "to_Combo";
			this.to_Combo.Size = new Size(103, 21);
			this.to_Combo.TabIndex = 9;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(8, 46);
			this.label3.Name = "label3";
			this.label3.Size = new Size(26, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "To :";
			this.from_Combo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.from_Combo.FormattingEnabled = true;
			this.from_Combo.Location = new Point(46, 20);
			this.from_Combo.Name = "from_Combo";
			this.from_Combo.Size = new Size(103, 21);
			this.from_Combo.TabIndex = 7;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(8, 23);
			this.label2.Name = "label2";
			this.label2.Size = new Size(36, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "From :";
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 161);
			this.label1.Location = new Point(9, 5);
			this.label1.Name = "label1";
			this.label1.Size = new Size(120, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Translate Document Text";
			this.translatedText.BackColor = SystemColors.Window;
			this.translatedText.BorderStyle = BorderStyle.None;
			this.translatedText.Dock = DockStyle.Fill;
			this.translatedText.Location = new Point(0, 0);
			this.translatedText.Multiline = true;
			this.translatedText.Name = "translatedText";
			this.translatedText.ReadOnly = true;
			this.translatedText.Size = new Size(180, 264);
			this.translatedText.TabIndex = 0;
			this.toolStrip1.Dock = DockStyle.Bottom;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Location = new Point(0, 237);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = ToolStripRenderMode.System;
			this.toolStrip1.Size = new Size(180, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStrip1.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.splitContainer1);
			base.Name = "Translate";
			base.Size = new Size(180, 361);
			base.Load += new EventHandler(this.Translate_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public Translate()
		{
			this.LanguageList.Add("English", "en");
			this.LanguageList.Add("Greek", "el");
			this.LanguageList.Add("Italian", "it");
			this.LanguageList.Add("Russian", "ru");
			this.LanguageList.Add("German", "de");
			this.LanguageList.Add("Albanian", "sq");
			this.LanguageList.Add("Afrikaans", "af");
			this.LanguageList.Add("Vietnamese", "vi");
			this.LanguageList.Add("Bulgarian", "bg");
			this.LanguageList.Add("Belarusian", "be");
			this.LanguageList.Add("Chinese Simplified", "zh-CN");
			this.LanguageList.Add("Chinese  Traditional", "zh-TW");
			this.LanguageList.Add("Dutch", "nl");
			this.LanguageList.Add("Danish", "da");
			this.LanguageList.Add("Estonian", "et");
			this.LanguageList.Add("French", "fr");
			this.LanguageList.Add("Finnish", "fi");
			this.LanguageList.Add("Hebrew", "iw");
			this.LanguageList.Add("Japanese", "ja");
			this.LanguageList.Add("Indonesian", "id");
			this.LanguageList.Add("Irish", "ga");
			this.LanguageList.Add("Icelandic", "is");
			this.LanguageList.Add("Korean", "ko");
			this.LanguageList.Add("Croatian", "hr");
			this.LanguageList.Add("Latvian", "lv");
			this.LanguageList.Add("Lithuanian", "lt");
			this.LanguageList.Add("Maltese", "mt");
			this.LanguageList.Add("Norwegian", "no");
			this.LanguageList.Add("Hungarian", "hu");
			this.LanguageList.Add("Persian", "fa");
			this.LanguageList.Add("Portuguese", "pt");
			this.LanguageList.Add("Polish", "pl");
			this.LanguageList.Add("Romanian", "ro");
			this.LanguageList.Add("Spanish", "es");
			this.LanguageList.Add("Serbian", "sr");
			this.LanguageList.Add("Slovak", "sk");
			this.LanguageList.Add("Slovenian", "sl");
			this.LanguageList.Add("Swedish", "sv");
			this.LanguageList.Add("Czech", "cs");
			this.LanguageList.Add("Thai", "th");
			this.LanguageList.Add("Turkish", "tr");
			this.LanguageList.Add("Yiddish", "yi");
			this.InitializeComponent();
			string[] array = new string[this.LanguageList.Keys.Count];
			this.LanguageList.Keys.CopyTo(array, 0);
			Array.Sort<string>(array);
			this.from_Combo.Items.AddRange(array);
			this.to_Combo.Items.AddRange(array);
			this.from_Combo.SelectedIndex = 10;
			this.to_Combo.SelectedIndex = 10;
		}

		private void Translate_Load(object sender, EventArgs e)
		{
		}
		private void button1_Click(object sender, EventArgs e)
		{
			string text = null;
			string languagePair = this.LanguageList[this.from_Combo.SelectedItem].ToString() + "|" + this.LanguageList[this.to_Combo.SelectedItem].ToString();
			try
                {
                    NotepadX.TextEditor _frm =(NotepadX.TextEditor) NotepadX.Main.MDIParent1.DockPanel1.ActiveDocument;
                    text = _frm.TextBox1.Text;
                    _frm.TextBox1.Text = Translator.TranslateText(text, languagePair);
                }
                catch 
                {
                    MessageBox.Show("Please Select a Notepad X document with text!");
                }
		}
	}
}
