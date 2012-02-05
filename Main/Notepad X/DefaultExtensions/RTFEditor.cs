/*
 * User: elijah
 * Date: 2/4/2012
 * Time: 2:49 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using IExtendFramework.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NotepadX.DefaultExtensions
{
    /// <summary>
    /// Description of RTFEditor.
    /// </summary>
    public partial class RTFEditor : DockContent, ITextEditor
    {
        public RTFEditor()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        public IFileExtension Extension {
            get {
                return new RTFExtension();
            }
        }
        
        private string _filename;
        public string Filename {
            get {
                return _filename;
            }
            set {
                Open(value);
            }
        }
        
        public IDocument CurrentDocument {
            get {
                return null;
            }
        }
        
        public int UndoBuffer {
            get {
                return (textBox1.CanUndo == true ? 1 : 0);
            }
        }
        
        public ITextEditor Create(string FileName)
        {
            RTFEditor e = new RTFEditor();
            if (File.Exists(FileName))
                e.Open(FileName);
            return e;
        }
        
        public void Undo()
        {
            textBox1.Undo();
        }
        
        public void Redo()
        {
            textBox1.Undo();
        }
        
        public void Save()
        {
            if (_filename == "")
            {
                SaveFileDialog s = new SaveFileDialog() { Filter = "Rich Text File (*.rtf)|*.rtf" };
                if (s.ShowDialog() == DialogResult.OK)
                    _filename = s.FileName;
            }
            textBox1.SaveFile(_filename, RichTextBoxStreamType.RichText);
        }
        
        public void SaveAs()
        {
            _filename = "";
            Save();
        }
        
        public void Print()
        {
            throw new NotImplementedException();
        }
        
        public void PrintPreview()
        {
            throw new NotImplementedException();
        }
        
        public void PrintSetup()
        {
            throw new NotImplementedException();
        }
        
        public void Cut()
        {
            textBox1.Cut();
        }
        
        public void Copy()
        {
            textBox1.Copy();
        }
        
        public void Paste()
        {
            textBox1.Paste();
        }
        
        public void Insert(int index, string text)
        {
            textBox1.Text.Insert(index, text);
        }
        
        public void ChangeFont(Font newFont)
        {
            textBox1.Font = newFont;
        }
        
        public void ChangeColor(Color newColor)
        {
            textBox1.ForeColor = newColor;
        }
        
        public void Open(string filename)
        {
            try {
                textBox1.LoadFile(filename, RichTextBoxStreamType.RichText);
                this._filename = filename;
            } catch (Exception e) {
                MessageBox.Show("Cannot open file '" + filename + "'!\n" + e.Message);
            }
        }
        
        public void SelectAll()
        {
            textBox1.SelectAll();
        }
        
        void Timer1_Tick(object sender, EventArgs e)
        {
            this.Text = Path.GetFileName(Filename);
        }
        
        public string DocumentText
        {
            get
            {
                try {
                    return textBox1.Text;
                } catch (Exception) {
                    return "";
                }
            }
            set
            {
                textBox1.Text = value;
            }
        }
        
        void SetColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                textBox1.SelectionColor = cd.Color;
        }
        
        void SetFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog cd = new FontDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                textBox1.SelectionFont = cd.Font;
        }
    }
}
