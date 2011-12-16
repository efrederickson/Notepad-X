/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/15/2011
 * Time: 1:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IExtendFramework.Text;

namespace NotepadX.DefaultExtensions
{
    /// <summary>
    /// Description of TXTExtension.
    /// </summary>
    public class TXTExtension : IFileExtension
    {
        
        public string Extension {
            get {
                return ".txt";
            }
        }
        
        public string Description {
            get {
                return "Text File";
            }
        }
        
        public string Category {
            get {
                return "Text";
            }
        }
    }
}
