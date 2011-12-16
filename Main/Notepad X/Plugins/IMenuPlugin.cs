/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/15/2011
 * Time: 13:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace NotepadX.Plugins
{
    /// <summary>
    /// A Menu Item
    /// </summary>
    public interface IMenuPlugin
    {
        ToolStripMenuItem Item
        {get; }
        
        string Path
        {get; }
        
        int Index
        {get; }
        
        bool Initialize();
        bool Dispose();
    }
}
