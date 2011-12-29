/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/28/2011
 * Time: 3:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IExtendFramework.Text;

namespace CodeEditor
{
    /// <summary>
    /// Holder of an Extension
    /// </summary>
    public class FileExtension : IFileExtension
    {
        string Ext, Desc, Cat;
        public FileExtension(string ext, string desc, string cat)
        {
            this.Ext = ext;
            this.Desc = desc;
            this.Cat = cat;
        }
        
        public string Extension {
            get {
                return Ext;
            }
        }
        
        public string Description {
            get {
                return Desc;
            }
        }
        
        public string Category {
            get {
                return this.Cat;
            }
        }
    }
}
