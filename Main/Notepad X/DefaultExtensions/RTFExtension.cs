/*
 * User: elijah
 * Date: 2/4/2012
 * Time: 2:49 PM
 */
using System;
using IExtendFramework.Text;

namespace NotepadX.DefaultExtensions
{
    /// <summary>
    /// Description of RTFExtension.
    /// </summary>
    public class RTFExtension : IFileExtension
    {
        public RTFExtension()
        {
        }
        
        public string Extension {
            get {
                return ".rtf";
            }
        }
        
        public string Description {
            get {
                return "Rich Text File";
            }
        }
        
        public string Category {
            get {
                return "Text";
            }
        }
    }
}
