/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/19/2011
 * Time: 10:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IExtendFramework.Text;

namespace NotepadX.DefaultExtensions
{
    /// <summary>
    /// Description of NXMExtension.
    /// </summary>
    public class NXMExtension : IFileExtension
    {
        public string Extension {
            get {
                return ".nxm";
            }
        }
        
        public string Description {
            get {
                return "Notepad X Macro";
            }
        }
        
        public string Category {
            get {
                return "Scripting";
            }
        }
    }
}
