/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/16/2011
 * Time: 2:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using IExtendFramework.Text;

namespace NotepadX.Macros
{
    /// <summary>
    /// some static functions for NXM
    /// </summary>
    public class Functions
    {
        public static object Open(Environment e, object[] args)
        {
            NotepadX.MainForm.Instance.ProcessParameters(null, args as string[]);
            return true;
        }
        
        public static object Save(Environment e, object[] args)
        {
            try {
                (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).Save();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object SaveAs(Environment e, object[] args)
        {
            try {
                (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).SaveAs();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object Undo(Environment e, object[] args)
        {
            try {
                (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).Undo();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object Redo(Environment e, object[] args)
        {
            try {
                (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).Redo();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object Print(Environment e, object[] args)
        {
            try {
                (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).Print();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object PrintPreview(Environment e, object[] args)
        {
            try {
                (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).PrintPreview();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object PrintSetup(Environment e, object[] args)
        {
            try {
                (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).PrintSetup();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object Name(Environment e, object[] args)
        {
            try {
                return (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).Filename;
            } catch (Exception) {
                return null;
            }
        }
        
        public static object CloseCurrentWindow(Environment e, object[] args)
        {
            try {
                (NotepadX.MainForm.Instance.CurrentDocument as ITextEditor).Dispose();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object Exit(Environment e, object[] args)
        {
            try {
                Application.Exit();
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object MessageBox(Environment e, object[] args)
        {
            try {
                System.Windows.Forms.MessageBox.Show(args[0] as string, "Notepad X");
            } catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static object Calculate(Environment e, object[] args)
        {
            try {
                return AdvancedMathProcesser.Calculate(args[0].ToString());
            } catch (Exception) {
                return null;
            }
        }
    }
}
