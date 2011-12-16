/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/15/2011
 * Time: 13:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace NotepadX.Plugins
{
    /// <summary>
    /// A Notepad X Plugin
    /// </summary>
    public interface IPlugin
    {
        string Name
        {get; }
        
        string Author
        {get; }
        
        string Version
        {get; }
        
        string Description
        {get; }
        
        string UpdateUrl
        {get; }
        
        TabPage AboutPage
        {get; }
        
        TabPage OptionsPage
        {get; }
        
        TabPage HelpPage
        {get; }
        
        bool Initialize();
        bool Dispose();
    }
}
