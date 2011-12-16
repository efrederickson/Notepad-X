/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/15/2011
 * Time: 1:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IExtendFramework.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NotepadX.DefaultExtensions
{
    /// <summary>
    /// Description of TXTEditor.
    /// </summary>
    public partial class TXTEditor : DockContent, ITextEditor
    {
        public TXTEditor()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        public WeifenLuo.WinFormsUI.Docking.DockContent DockingPanel {
            get {
                return this;
            }
        }
        
        public IFileExtension Extension {
            get {
                return new TXTExtension();
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
            TXTEditor e = new TXTEditor();
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
                SaveFileDialog s = new SaveFileDialog() { Filter = "Text File (*.txt)|*.txt" };
                if (s.ShowDialog() == DialogResult.OK)
                    _filename = s.FileName;
            }
            System.IO.File.WriteAllText(_filename, textBox1.Text);
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
                textBox1.Text = System.IO.File.ReadAllText(filename);
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
    }
}
